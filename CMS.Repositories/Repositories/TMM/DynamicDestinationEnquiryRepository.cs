using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class DynamicDestinationEnquiryRepository : Repository<DynamicDestinationEnquiry>, IDynamicDestinationEnquiryRepository
    {
        public DynamicDestinationEnquiryRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
