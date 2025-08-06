using Cms.Services.Filters;
using Cms.Services.Models.FlightFareResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface IFlightFaresDetailsDataService
    {
        Task<List<Itinerary>> GetAllFlightFaresDetailsData(FlightSearchDetails airSearchQuery);
    }
}