using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class HotelSearchDetailsRepository : Repository<HotelSearchDetails>, IHotelSearchDetailsRepository
    {
        public HotelSearchDetailsRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
