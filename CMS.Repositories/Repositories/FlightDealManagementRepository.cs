using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class FlightDealManagementRepository:Repository<FlightDealManagement>,IFlightDealManagementRepository
    {
        public FlightDealManagementRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
