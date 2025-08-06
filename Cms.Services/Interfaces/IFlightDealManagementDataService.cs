using Cms.Services.Filters;
using Cms.Services.Models.OpenAPIDataModel.FlightDealManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public interface IFlightDealManagementDataService
    {
        Task<List<FlightDealManagementData>> GetAllDealManagement(FlightDealManagementFilter filter);
    }
}