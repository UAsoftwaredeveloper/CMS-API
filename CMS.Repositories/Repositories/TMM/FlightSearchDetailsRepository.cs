using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class FlightSearchDetailsRepository : Repository<FlightSearchDetails>, IFlightSearchDetailsRepository
    {
        public FlightSearchDetailsRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
