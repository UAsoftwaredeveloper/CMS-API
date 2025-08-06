using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        public UsersRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
