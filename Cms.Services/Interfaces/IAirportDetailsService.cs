using Cms.Services.Filters;
using Cms.Services.Models.AirportDetails;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface IAirportDetailsService
    {
        Task<PaginatedList<AirportDetailsModal>> GetAllAirportDetails(AirportDetailsFilter filter);
        Task<AirportDetailsModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
    }
}