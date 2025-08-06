using Cms.Services.Filters;
using Cms.Services.Models.Portals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface IPortalService
    {
        Task<CreatePortalModal> CreatePortal(CreatePortalModal modal);
        Task<PaginatedList<PortalModal>> GetAllPortal(PortalFilter filter);
        Task<PortalModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<UpdatePortalModal> UpdatePortal(UpdatePortalModal modal);
    }
}