using Microsoft.EntityFrameworkCore;
using Pharmacy_Management_System.Data.DbContexts.ModelBuilders;
using Pharmacy_Management_System.Domain.Contexts;
using Pharmacy_Management_System.Domain.Entities.Roles;
using Pharmacy_Management_System.Domain.Entities.Users;

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

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.ConfigureAllModelBuilders();

            base.OnModelCreating(modelBuilder);
        }
    }
}
