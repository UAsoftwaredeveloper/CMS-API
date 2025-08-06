using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IGroupTravelFlightEnqueryDetailsService
    {
        Task<PaginatedList<GroupTravelFlightEnqueryDetailsModal>> GetAllGroupTravelFlightEnqueryDetails(GroupTravelFlightEnqueryDetailsFilter filter);
        Task<GroupTravelFlightEnqueryDetailsModal> GetById(int Id);
    }
}