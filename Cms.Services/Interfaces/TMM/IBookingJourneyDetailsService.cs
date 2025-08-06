using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IBookingJourneyDetailsService
    {
        Task<PaginatedList<BookingJourneyDetailsModal>> GetAllBookingJourneyDetails(BookingJourneyDetailsFilter filter);
        Task<BookingJourneyDetailsModal> GetById(int Id);
    }
}