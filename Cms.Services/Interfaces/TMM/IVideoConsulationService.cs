using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IVideoConsulationService
    {
        Task<PaginatedList<VideoConsulationModal>> GetAllVideoConsulation(VideoConsulationFilter filter);
        Task<VideoConsulationModal> GetById(int Id);
        Task<PaginatedList<VideoConsulationModal>> GetUsersAllVideoConsulation(VideoConsulationFilter filter);
    }
}