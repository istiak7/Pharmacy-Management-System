using Pharmacy_Management_System.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy_Management_System.Domain.Entities.Roles
{
    public class Role : BaseEntity
    {
        [Column("role_name")]
        public string RoleName { get; set; }

        [Column("role_description")]
        public string RoleDescription { get; set; }

        public virtual List<UserRole> UserRoles { get; set; }
        public virtual List<RolePermission> RolePermissions { get; set; }
    }
}
