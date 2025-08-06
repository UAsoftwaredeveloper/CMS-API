using Cms.Services.Models.MenuMaster;
using Cms.Services.Models.UserRoleMenuPermissions;
using Cms.Services.Models.Users;
using System;
using System.Collections.Generic;

namespace CMS.Models
{
    public class AuthUser:UsersModal
    {
        public string UserToken {  get; set; }
        public DateTime ExpiryTime { get; set; }
        public virtual List<MenuMasterModal> MenuMasters { get; set; }

    }
}
