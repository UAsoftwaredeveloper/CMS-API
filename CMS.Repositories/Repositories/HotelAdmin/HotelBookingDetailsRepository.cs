using CMS.Repositories.Interfaces.HotelAdmin;
using DataManager;
using DataManager.HotelAdmin;

namespace CMS.Repositories.Repositories.HotelAdmin
{
    public class HotelBookingDetailsRepository : Repository<HotelBookingDetails>, IHotelBookingDetailsRepository
    {
        public HotelBookingDetailsRepository(HotelAdminDBContext activityAdminDB) : base(activityAdminDB) { }
    }
}
