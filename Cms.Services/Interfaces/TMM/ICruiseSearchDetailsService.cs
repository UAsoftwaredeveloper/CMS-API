using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface ICruiseSearchDetailsService
    {
        Task<PaginatedList<CruiseSearchDetailsModal>> GetAllCruiseSearchDetails(CruiseSearchDetailsFilter filter);
        Task<CruiseSearchDetailsModal> GetById(int Id);
    }
}