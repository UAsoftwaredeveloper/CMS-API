using Cms.Services.Models.Common;
using Cms.Services.Models.UserRole;

namespace Cms.Services.Models.Users
{
    public class UsersModal : EntityModal
    {
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PhoneNumber { get; set; }
        public virtual UserRoleModal UserRole { get; set; }


    }
}
