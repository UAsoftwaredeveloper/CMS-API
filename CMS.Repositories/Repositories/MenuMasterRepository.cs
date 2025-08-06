using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class MenuMasterRepository : Repository<MenuMaster>, IMenuMasterRepository
    {
        public MenuMasterRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
