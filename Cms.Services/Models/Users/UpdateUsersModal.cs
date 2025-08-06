using Cms.Services.Models.Common;

namespace Cms.Services.Models.Users
{
    public class UpdateUsersModal : UpdateModal
    {
        public int RoleId {  get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PhoneNumber { get; set; }


    }
}
