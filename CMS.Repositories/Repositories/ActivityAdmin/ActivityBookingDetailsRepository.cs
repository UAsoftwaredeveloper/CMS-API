using CMS.Repositories.Interfaces.ActivityAdmin;
using DataManager;
using DataManager.ActivityAdmin;

namespace CMS.Repositories.Repositories.ActivityAdmin
{
    public class ActivityBookingDetailsRepository : Repository<ActivityBookingDetails>, IActivityBookingDetailsRepository
    {
        public ActivityBookingDetailsRepository(ActivityAdminDBContext activityAdminDB) : base(activityAdminDB) { }
    }
}
