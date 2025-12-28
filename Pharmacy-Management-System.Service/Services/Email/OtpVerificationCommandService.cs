using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
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
    public class OtpVerificationCommandService : IOtpVerificationCommandService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<EmailCommandService> _logger;

        public OtpVerificationCommandService(IDistributedCache distributedCache, ILogger<EmailCommandService> logger)
        {
            _distributedCache = distributedCache;
            _logger = logger;
        }
        public async Task<Result> OtpVerificationAsync(VerifyOtpRequestDto request)
        {
            var otpKey = $"otp:{request.Email}";

            //Get the OTP from Redis
            var storeOtp = _distributedCache.GetString(otpKey);
            _logger.LogInformation(storeOtp);
            if(string.IsNullOrEmpty(storeOtp))
            {
                return Utility.GetErrorMsg("OTP has expired or does not exist.");
            }
            else if(storeOtp != request.Otp)
            {
                return Utility.GetErrorMsg("Invalid OTP.");
            }
            var verifiedEmailKey = $"verified_email:{request.Email}";
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24) // Set expiration time for verified email
            };

            // Mark email as verified in Redis
            await _distributedCache.SetStringAsync(verifiedEmailKey, "true", options);

            // Remove OTP after successful verification
            await _distributedCache.RemoveAsync(otpKey); 

            return Utility.GetSuccessMsg("OTP verified successfully.");
        }

    }
}
