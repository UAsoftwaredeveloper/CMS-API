using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class UserSearchLogsRepository:Repository<UserSearchLogs>, IUserSearchLogsRepository
    {
        public UserSearchLogsRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
