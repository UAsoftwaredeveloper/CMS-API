namespace Cms.Services.Filters
{
    public class UserRoleMenuPermissionFilter:CommonFilter
    {
        public string UserName {  get; set; }
        public string RoleName {  get; set; }
        public string MenuName {  get; set; }
    }
}
