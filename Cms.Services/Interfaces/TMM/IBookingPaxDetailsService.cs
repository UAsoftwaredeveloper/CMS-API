using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IBookingPaxDetailsService
    {
        Task<PaginatedList<BookingPaxDetailsModal>> GetAllBookingPaxDetails(BookingPaxDetailsFilter filter);
        Task<BookingPaxDetailsModal> GetById(int Id);
    }
}