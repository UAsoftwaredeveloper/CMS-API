using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class MasterAirlinesRepository:Repository<MasterAirlines>,IMasterAirlinesRepository
    {
        public MasterAirlinesRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
