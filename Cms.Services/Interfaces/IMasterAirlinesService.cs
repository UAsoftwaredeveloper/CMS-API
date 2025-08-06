using Cms.Services.Filters;
using Cms.Services.Models.MasterAirlines;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface IMasterAirlinesService
    {
        Task<PaginatedList<MasterAirlinesModal>> GetAllMasterAirlines(MasterAirlinesFilter filter);
        Task<MasterAirlinesModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
    }
}