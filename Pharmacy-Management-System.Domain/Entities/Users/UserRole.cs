using Pharmacy_Management_System.Domain.Entities.Roles;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy_Management_System.Domain.Entities.Users
{
    public class UserRole : BaseEntity
    {
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("assigned_at", TypeName = "timestamp with time zone")]
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
