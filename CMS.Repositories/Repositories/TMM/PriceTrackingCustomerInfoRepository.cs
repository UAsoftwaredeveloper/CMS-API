using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class PriceTrackingCustomerInfoRepository : Repository<PriceTrackingCustomerInfo>, IPriceTrackingCustomerInfoRepository
    {
        public PriceTrackingCustomerInfoRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
