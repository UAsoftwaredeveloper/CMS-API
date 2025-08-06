using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cms.Services.Filters;
using Cms.Services.Models.OpenAPIDataModel.CarHireDealsData;
using Cms.Services.Models.OpenAPIDataModel.HotelDealsData;

namespace Cms.Services.Interfaces
{
    public interface ICarHireDealsDataService
    {
        Task<List<CarHireDealsData>> GetAllDealManagement(CarHireDealFilter filter);
    }
}
