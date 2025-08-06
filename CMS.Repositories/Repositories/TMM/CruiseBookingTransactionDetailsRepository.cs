using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class CruiseBookingTransactionDetailsRepository : Repository<CruiseBookingTransactionDetails>, ICruiseBookingTransactionDetailsRepository
    {
        public CruiseBookingTransactionDetailsRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
