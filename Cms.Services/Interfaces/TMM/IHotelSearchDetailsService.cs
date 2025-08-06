using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IHotelSearchDetailsService
    {
        Task<PaginatedList<HotelSearchDetailsModal>> GetAllHotelSearchDetails(HotelSearchDetailsFilter filter);
        Task<HotelSearchDetailsModal> GetById(int Id);
    }
}