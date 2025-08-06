using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface ICruiseBookingTransactionDetailsService
    {
        Task<PaginatedList<CruiseBookingTransactionDetailsModal>> GetAllCruiseBookingTransactionDetails(CruiseBookingTransactionDetailsFilter filter);
        Task<PaginatedList<CruiseBookingTransactionDetailsModal>> GetUsersAllCruiseBookingTransactionDetails(CruiseBookingTransactionDetailsFilter filter);
        Task<CruiseBookingTransactionDetailsModal> GetById(int Id);
    }
}