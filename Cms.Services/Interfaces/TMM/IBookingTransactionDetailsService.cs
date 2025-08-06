using Cms.Services.Filters.FrontEnd;
using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IBookingTransactionDetailsService
    {
        Task<PaginatedList<BookingTransactionDetailsModal>> GetAllBookingTransactionDetails(BookingTransactionDetailsFilter filter);
        Task<PaginatedList<BookingTransactionDetailsModal>> GetAllBookingTransactionDetailsPublic(FlightBookingTransactionFilter filter);
        Task<BookingTransactionDetailsModal> GetById(long Id);
    }
}