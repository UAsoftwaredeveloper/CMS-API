using CMS.Repositories.Interfaces;
using CMS.Repositories.Interfaces.ActivityAdmin;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class AirportDetailsRepository:Repository<AirportDetails>, IAirportDetailsRepository
    {
        public AirportDetailsRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
