using Cms.Services.Filters;
using Cms.Services.Models.FlightDealManagement;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface IFlightDealManagementService
    {
        Task<CreateFlightDealManagementModal> CreateFlightDealManagement(CreateFlightDealManagementModal modal);
        Task<PaginatedList<FlightDealManagementModal>> GetAllFlightDealManagement(FlightDealManagementFilter filter);
        Task<FlightDealManagementModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<UpdateFlightDealManagementModal> UpdateFlightDealManagement(UpdateFlightDealManagementModal modal);
    }
}