using Pharmacy_Management_System.Domains.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy_Management_System.Domains.Entities.Users
{
    public class User : BaseEntity
    {
        [Column("username")]
        public string Username { get; set; }

        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        public virtual List<UserRole> UserRoles { get; set; }
    }
}
