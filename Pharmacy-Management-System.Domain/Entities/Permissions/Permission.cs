using Pharmacy_Management_System.Domain.Entities.MenuItems;
using Pharmacy_Management_System.Domain.Entities.Roles;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy_Management_System.Domain.Entities.Permissions
{
    public class Permission : BaseEntity
    {
        [Column("permission_name")]
        public string PermissionName { get; set; }

        [Column("permission_description")]
        public string PermissionDescription { get; set; }

        public virtual List<RolePermission> RolePermissions { get; set; }
        public virtual List<MenuPermission> MenuPermissions { get; set; }
    }
}
