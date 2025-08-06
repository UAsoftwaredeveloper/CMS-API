using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IEnqueryPageDetailsService
    {
        Task<PaginatedList<EnqueryPageDetailsModal>> GetAllEnqueryPageDetails(EnqueryPageDetailsFilter filter);
        Task<EnqueryPageDetailsModal> GetById(int Id);
    }
}