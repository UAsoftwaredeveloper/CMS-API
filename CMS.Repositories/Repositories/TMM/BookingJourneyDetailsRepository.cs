using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class BookingJourneyDetailsRepository : Repository<BookingJourneyDetails>, IBookingJourneyDetailsRepository
    {
        public BookingJourneyDetailsRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
