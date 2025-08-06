using Cms.Services.Models.Common;
using Cms.Services.Models.MenuMaster;
using Cms.Services.Models.UserRole;
using Cms.Services.Models.Users;

namespace Cms.Services.Models.UserRoleMenuPermissions
{
    public class UserRoleMenuPermissionModal:EntityModal
    {
        public int? RoleId { get; set; }
        public int? MenuId { get; set; }
        public int? UserId { get; set; }
        public bool? Edit { get; set; }
        public bool? Delete { get; set; }
        public bool? Create { get; set; }
        public bool? View { get; set; }
        public virtual UserRoleModal UserRole { get; set; } = null;
        public virtual MenuMasterModal MenuMaster { get; set; } = null;
        public virtual UsersModal Users { get; set; } = null;


    }
}
