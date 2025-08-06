using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class BookingTransactionDetailsRepository : Repository<BookingTransactionDetails>, IBookingTransactionDetailsRepository
    {
        public BookingTransactionDetailsRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
