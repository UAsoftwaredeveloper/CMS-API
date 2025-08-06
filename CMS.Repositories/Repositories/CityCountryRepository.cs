using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class CityCountryRepository:Repository<CityCountry>, ICityCountryRepository
    {
        public CityCountryRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
