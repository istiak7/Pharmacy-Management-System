using Npgsql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pharmacy_Management_System.Data.DbContexts;
using Pharmacy_Management_System.Domain.Interfaces.Transactions;
using Pharmacy_Management_System.Domain.Contexts;

namespace Pharmacy_Management_System.Data.Setups
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            string? connectionString,
            string? readConnectionString)
        {
            #region Null Checks

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            if (string.IsNullOrWhiteSpace(readConnectionString))
                throw new ArgumentNullException(nameof(readConnectionString));

            #endregion

            #region Register Raw Connection

            services.AddScoped<NpgsqlConnection>(_ => new NpgsqlConnection(connectionString));

            services.AddKeyedScoped<NpgsqlConnection>(
                "ReadConnection",
                (_, _) => new NpgsqlConnection(readConnectionString)
            );

            #endregion

            #region Register Read DbContext

            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                options.UseNpgsql(readConnectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

#if DEBUG
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
#endif
            }, ServiceLifetime.Scoped);

            services.AddScoped<IReadDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            
            #endregion

            #region Register Write DbContext

            services.AddDbContext<ApplicationDbContextWrite>((provider, options) =>
            {
                options.UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContextWrite).Assembly.FullName));

#if DEBUG
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
#endif
            }, ServiceLifetime.Scoped);

            services.AddScoped<IApplicationDbContext>
                (provider => provider.GetRequiredService<ApplicationDbContextWrite>());

            #endregion

            services.AddScoped<ITransactionUtil, TransactionUtil>();
            
            return services;
        }
    }
}   
