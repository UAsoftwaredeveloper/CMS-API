using Cms.Services.Filters.TransferAdmin;
using Cms.Services.Models.TransferAdmin;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TransferAdmin
{
    public interface ICarBookingTransactionDetailsService
    {
        Task<PaginatedList<CarBookingTransactionDetailsModal>> GetAllCarBookingTransactionDetails(CarBookingTransactionDetailsFilter filter);
        Task<CarBookingTransactionDetailsModal> GetById(int Id);
        Task<PaginatedList<CarBookingTransactionDetailsModal>> GetUsersAllCarBookingTransactionDetails(CarBookingTransactionDetailsFilter filter);
    }
}