using Pharmacy_Management_System.Application.Dtos.Requests.Users;
using Pharmacy_Management_System.Service.Validators.Users;
using FluentValidation;

namespace Pharmacy_Management_System.DependencyExtensions
{
    public static class RegisterServices
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssemblyContaining<UserRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<LoginRequest>();
        }
    }
}
