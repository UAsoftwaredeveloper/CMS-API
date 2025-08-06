using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class UserRoleMenuPermissionRepository : Repository<UserRoleMenuPermission>, IUserRoleMenuPermissionRepository
    {
        public UserRoleMenuPermissionRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
