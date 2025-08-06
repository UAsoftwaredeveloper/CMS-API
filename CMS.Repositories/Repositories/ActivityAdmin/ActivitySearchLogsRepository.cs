using CMS.Repositories.Interfaces.ActivityAdmin;
using DataManager;
using DataManager.ActivityAdmin;

namespace CMS.Repositories.Repositories.ActivityAdmin
{
    public class ActivitySearchLogsRepository : Repository<ActivitySearchLogs>, IActivitySearchLogsRepository
    {
        public ActivitySearchLogsRepository(ActivityAdminDBContext activityAdminDB) : base(activityAdminDB) { }
    }
}
