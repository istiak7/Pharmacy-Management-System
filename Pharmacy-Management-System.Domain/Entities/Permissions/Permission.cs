using Pharmacy_Management_System.Domains.Entities.Roles;
using Pharmacy_Management_System.Domains.Entities;
using Pharmacy_Management_System.Domains.Entities.MenuItems;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy_Management_System.Domains.Entities.Permissions
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
