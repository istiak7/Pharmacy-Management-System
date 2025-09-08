using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy_Management_System.Domains.Entities.MenuItems
{
    public class MenuItem : BaseEntity
    {
        [Column("menu_title")]
        public string MenuTitle { get; set; }

        [Column("menu_description")]
        public string MenuDescription { get; set; }

        [Column("parent_menu_id")]
        public int? ParentMenuId { get; set; }

        [Column("menu_url")]
        public string MenuUrl { get; set; }

        [Column("menu_icon")]
        public string MenuIcon { get; set; }

        [Column("display_order")]
        public int DisplayOrder { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;
        public virtual MenuItem ParentMenu { get; set; }
        public virtual List<MenuItem> ChildMenus { get; set; }
        public virtual List<MenuPermission> MenuPermissions { get; set; }
    }
}
