using Cms.Services.Filters;
using Cms.Services.Models.CarHireDeals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface ICarHireDealsService
    {
        Task<PaginatedList<CarHireDealsModal>> GetCarHireDealManagement(CarHireDealFilter filter);
        Task<CarHireDealsModal> GetById(int Id);
        Task<CreateCarHireDealsModel> CreateDealManagement(CreateCarHireDealsModel modal);
        Task<UpdateCarHireModel> UpdateDealManagement(UpdateCarHireModel modal);

        
    }
}
