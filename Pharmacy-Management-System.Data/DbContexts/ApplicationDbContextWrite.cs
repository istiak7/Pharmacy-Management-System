using Microsoft.EntityFrameworkCore;
using Pharmacy_Management_System.Domain.Contexts;

namespace Pharmacy_Management_System.Data.DbContexts
{
    public partial class ApplicationDbContextWrite : DbContext, IApplicationDbContext
    {
        #region Constructor
        public ApplicationDbContextWrite()
        {

        }

        public ApplicationDbContextWrite(DbContextOptions<ApplicationDbContextWrite> options) : base(options)
        {

        }
        #endregion Constructor

        #region DbSets


        #endregion
    }
}
