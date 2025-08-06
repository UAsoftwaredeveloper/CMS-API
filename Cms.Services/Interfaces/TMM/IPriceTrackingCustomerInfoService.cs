using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IPriceTrackingCustomerInfoService
    {
        Task<PaginatedList<PriceTrackingCustomerInfoModal>> GetAllPriceTrackingCustomerInfo(PriceTrackingCustomerInfoFilter filter);
        Task<PriceTrackingCustomerInfoModal> GetById(int Id);
    }
}