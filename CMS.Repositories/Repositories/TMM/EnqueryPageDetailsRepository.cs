using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class EnqueryPageDetailsRepository : Repository<EnqueryPageDetails>, IEnqueryPageDetailsRepository
    {
        public EnqueryPageDetailsRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
