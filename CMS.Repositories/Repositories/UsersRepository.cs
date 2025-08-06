using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class UsersRepository:Repository<Users>,IUsersRepository
    {
        public UsersRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
