using Pharmacy_Management_System.Application.Features.Email.Command.Dtos;
using Pharmacy_Management_System.Application.ServiceInterfaces.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Management_System.Application.Features.Email.Command
{
    public sealed record OtpVerificationCommand(VerifyOtpRequestDto model) : Common.MediatR.ICommand;
    public class OtpVerificationCommandHandler : Common.MediatR.ICommandHandler<OtpVerificationCommand>
    {
        private readonly IOtpVerificationCommandService _otpVerificationCommandService;
        public OtpVerificationCommandHandler(IOtpVerificationCommandService otpVerificationCommandService)
        {
            _otpVerificationCommandService = otpVerificationCommandService;
        }
        public async Task<Result> Handle(OtpVerificationCommand command, CancellationToken cancellationToken)
        {
            return await _otpVerificationCommandService.OtpVerificationAsync(command.model);

        }
    }
}
