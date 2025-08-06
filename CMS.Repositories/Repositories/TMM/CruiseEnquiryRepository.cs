using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class CruiseEnquiryRepository : Repository<CruiseEnquiry>, ICruiseEnquiryRepository
    {
        public CruiseEnquiryRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
