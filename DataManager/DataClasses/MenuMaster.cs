using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.DataClasses
{
    [Table("MenuMaster")]
    public class MenuMaster:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url {  get; set; }
        public string ControllerName {  get; set; }
        public string ActionName {  get; set; }
        [ForeignKey(nameof(ParentMenu))]
        public int? ParentId {  get; set; }
        public int? DisplayOrder {  get; set; }
        public virtual MenuMaster ParentMenu { get; set; }
        public virtual ICollection<MenuMaster> ChildMenus { get; set; }
    }
}
