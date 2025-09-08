using Microsoft.EntityFrameworkCore;
using Pharmacy_Management_System.Data.DbContexts.ModelBuilders;
using Pharmacy_Management_System.Domain.Entities;
using Pharmacy_Management_System.Domain.Entities.MenuItems;
using Pharmacy_Management_System.Domain.Entities.Permissions;
using Pharmacy_Management_System.Domain.Entities.Roles;
using Pharmacy_Management_System.Domain.Entities.Users;
using Pharmacy_Management_System.Domains.Contexts;

namespace Pharmacy_Management_System.Data.DbContexts
{
    public class ApplicationDbContext : DbContext, IReadDbContext
    {
        #region Constructor
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #endregion Constructor

        #region DbSets


        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureAllModelBuilders();

            base.OnModelCreating(modelBuilder);
        }
    }
}
