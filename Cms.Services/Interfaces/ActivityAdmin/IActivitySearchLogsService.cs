using Cms.Services.Filters.ActivityAdmin;
using Cms.Services.Models.ActivityAdmin;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.ActivityAdmin
{
    public interface IActivitySearchLogsService
    {
        Task<PaginatedList<ActivitySearchLogsModal>> GetAllActivitySearchLogs(ActivitySearchLogsFilter filter);
        Task<ActivitySearchLogsModal> GetById(int Id);
    }
}