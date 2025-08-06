using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class UserRoleRepository:Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
