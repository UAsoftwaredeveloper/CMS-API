using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class PortalRepository:Repository<Portals>,IPortalRepository
    {
        public PortalRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
