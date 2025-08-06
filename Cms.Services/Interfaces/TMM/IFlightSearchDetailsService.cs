using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IFlightSearchDetailsService
    {
        Task<PaginatedList<FlightSearchDetailsModal>> GetAllFlightSearchDetails(FlightSearchDetailsFilter filter);
        Task<FlightSearchDetailsModal> GetById(int Id);
    }
}