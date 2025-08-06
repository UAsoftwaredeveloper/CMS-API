using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class HotelDealsRepository:Repository<HotelDeals>, IHotelDealsRepository
    {
        public HotelDealsRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
