using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class BookingPaxDetailsRepository : Repository<BookingPaxDetails>, IBookingPaxDetailsRepository
    {
        public BookingPaxDetailsRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
