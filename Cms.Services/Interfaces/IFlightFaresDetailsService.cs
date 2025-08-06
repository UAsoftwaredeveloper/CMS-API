using Cms.Services.Filters;
using Cms.Services.Models.FlightFaresDetails;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface IFlightFaresDetailsService
    {
        Task<FlightFaresDetailsModal> CreateFlightFaresDetails(FlightFaresDetailsModal modal);
        Task<List<FlightFaresDetailsModal>> CreateFlightFaresDetailsList(List<FlightFaresDetailsModal> modal);
        Task<PaginatedList<FlightFaresDetailsModal>> GetAllFlightFaresDetails(FlightFaresDetailsFilter filter);
        Task<FlightFaresDetailsModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<FlightFaresDetailsModal> UpdateFlightFaresDetails(FlightFaresDetailsModal modal);
        Task<List<FlightFaresDetailsModal>> UpdateFlightFaresDetailsList(List<FlightFaresDetailsModal> modal);
    }
}