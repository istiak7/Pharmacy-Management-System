using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Pharmacy_Management_System.Domains.Contexts
{
    public interface IApplicationDbContext : IInfrastructure<IServiceProvider>
    {
        DatabaseFacade Database { get; }

        #region DbSets


        #endregion

        #region Methods
        void Dispose();

        #endregion
    }
}
