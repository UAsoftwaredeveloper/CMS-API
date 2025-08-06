using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class FlightFaresDetailsRepository:Repository<FlightFaresDetails>,IFlightFaresDetailsRepository
    {
        public FlightFaresDetailsRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
