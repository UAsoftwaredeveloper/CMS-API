using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class FlightsEnquiryRepository : Repository<FlightsEnquiry>, IFlightsEnquiryRepository
    {
        public FlightsEnquiryRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
