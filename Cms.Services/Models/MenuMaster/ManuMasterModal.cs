using Cms.Services.Models.Common;
using System.Collections.Generic;

namespace Cms.Services.Models.MenuMaster
{
    public class MenuMasterModal:EntityModal
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int? ParentId { get; set; }
        public int? DisplayOrder {  get; set; }
        public virtual MenuMasterModal ParentMenu { get; set; }
        public virtual List<MenuMasterModal> ChildMenus { get; set; }

    }
}
