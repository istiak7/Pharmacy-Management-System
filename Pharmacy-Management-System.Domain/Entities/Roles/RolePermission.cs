using Pharmacy_Management_System.Domain.Entities.Permissions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy_Management_System.Domain.Entities.Roles
{
    public class RolePermission : BaseEntity
    {
        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("permission_id")]
        public int PermissionId { get; set; }

        [Column("assigned_at", TypeName = "timestamp with time zone")]
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
