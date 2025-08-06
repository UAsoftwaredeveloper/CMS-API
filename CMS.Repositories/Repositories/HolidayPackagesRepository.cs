using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class HolidayPackagesRepository:Repository<HolidayPackages>, IHolidayPackagesRepository
    {
        public HolidayPackagesRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
