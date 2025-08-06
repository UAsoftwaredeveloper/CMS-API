using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.DataClasses
{
    [Table("UserRoleMenuPermission")]
    public class UserRoleMenuPermission:Entity
    {
        [ForeignKey(nameof(UserRole))]
        public int? RoleId {  get; set; }
       
        [ForeignKey(nameof(MenuMaster))]
        public int? MenuId {  get; set; }
        [ForeignKey(nameof(Users))]
        public int? UserId {  get; set; }
        public bool? Edit {  get; set; }
        public bool? Delete {  get; set; }
        public bool? Create {  get; set; }
        public bool? View {  get; set; }
        public virtual UserRole UserRole { get; set; }=null;
        public virtual MenuMaster MenuMaster { get; set; }=null ;
        public virtual Users Users { get; set; } = null;

    }
}
