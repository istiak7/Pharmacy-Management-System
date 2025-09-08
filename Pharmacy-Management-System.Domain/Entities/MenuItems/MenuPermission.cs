using Pharmacy_Management_System.Domains.Entities.Permissions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy_Management_System.Domains.Entities.MenuItems
{
    public class MenuPermission : BaseEntity
    {
        [Column("menu_id")]
        public int MenuId { get; set; }

        [Column("permission_id")]
        public int PermissionId { get; set; }

        [Column("assigned_at", TypeName = "timestamp with time zone")]
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        public virtual MenuItem MenuItem { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
