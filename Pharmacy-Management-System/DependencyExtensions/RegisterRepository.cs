using Pharmacy_Management_System.Application.Repositories.Users;
using Pharmacy_Management_System.Repo.Repositories.Users;

namespace Pharmacy_Management_System.DependencyExtensions
{
    public static class RegisterRepository
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
