using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Pharmacy_Management_System.Application.Common.Utilities;
using Pharmacy_Management_System.Application.Features.Email.Command.Dtos;
using Pharmacy_Management_System.Application.ServiceInterfaces.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Service.Services.Email
{
    public class EmailCommandService : IEmailCommandService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<EmailCommandService> _logger;
        private readonly SmtpSettings _settings;
        public EmailCommandService(IDistributedCache distributedCache, ILogger<EmailCommandService> logger,
               IOptions<SmtpSettings> settings)
        {
            _distributedCache = distributedCache;
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task<Result> SendOtpEmailAsync(string email, string otp)
        {
            // STEP 1: Store in Redis
            try
            {
                var cacheKey = $"otp:{email}";
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                };

                await _distributedCache.SetStringAsync(cacheKey, otp, options);
                _logger.LogInformation($"OTP stored in Redis for {email}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Redis Error");
                return Utility.GetErrorMsg("System Error: Could not generate OTP. Please try again.");
            }

            // STEP 2: Send Email
            try
            {
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(_settings.From));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = "Your Verification Code";
                message.Body = new TextPart("html")
                {
                    Text = $@"<h3>Your Code: {otp}</h3>"
                };

                using var client = new SmtpClient();

                // Gmail SMTP Settings
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                // Use your Email and the NEW APP PASSWORD here
                await client.AuthenticateAsync(_settings.UserName, _settings.Password);

                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return Utility.GetSuccessMsg("OTP sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SMTP Email Error");

                // Optional: If email fails, delete the OTP from Redis so the user doesn't have a ghost code
                await _distributedCache.RemoveAsync($"otp:{email}");

                return Utility.GetErrorMsg($"Failed to send email. Error: {ex.Message}");
            }
        }
    }
}
