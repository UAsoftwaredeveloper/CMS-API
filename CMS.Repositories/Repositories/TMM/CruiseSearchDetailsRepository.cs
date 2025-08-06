using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class CruiseSearchDetailsRepository : Repository<CruiseSearchDetails>, ICruiseSearchDetailsRepository
    {
        public CruiseSearchDetailsRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
