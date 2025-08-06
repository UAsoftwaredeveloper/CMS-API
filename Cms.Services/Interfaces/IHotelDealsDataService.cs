using Cms.Services.Filters;
using Cms.Services.Models.OpenAPIDataModel.HotelDealsData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public interface IHotelDealsDataService
    {
        Task<List<HotelDealsData>> GetAllDealManagement(HotelDealsFilter filter);
    }
}