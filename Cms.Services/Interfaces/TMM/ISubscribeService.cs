using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface ISubscribeService
    {
        Task<PaginatedList<SubscribeModal>> GetAllSubscribe(SubscribeFilter filter);
        Task<SubscribeModal> GetById(int Id);
    }
}