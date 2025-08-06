using Cms.Services.Filters;
using Cms.Services.Models.HotelDeals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface IHotelDealsService
    {
        Task<CreateHotelDealsModal> CreateDealManagement(CreateHotelDealsModal modal);
        Task<PaginatedList<HotelDealsModal>> GetAllDealManagement(HotelDealsFilter filter);
        Task<HotelDealsModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<UpdateHotelDealsModal> UpdateDealManagement(UpdateHotelDealsModal modal);
    }
}