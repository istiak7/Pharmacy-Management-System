using Pharmacy_Management_System.Application.Repositories.Users;
using Pharmacy_Management_System.Application.RepositoryInterfaces.Roles;
using Pharmacy_Management_System.Application.ServiceInterfaces.Roles;
using Pharmacy_Management_System.Repo.Repositories.Roles;
using Pharmacy_Management_System.Repo.Repositories.Users;
using Pharmacy_Management_System.Service.Services.Roles;

namespace Pharmacy_Management_System.DependencyExtensions
{
    public static class RegisterRepository
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleCommandRepository, RoleCommandRepository>();
        }
    }
}
