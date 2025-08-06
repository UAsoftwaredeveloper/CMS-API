using Cms.Services.Filters;
using Cms.Services.Models.UserSearchLogs;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface IUserSearchLogsService
    {
        Task<UserSearchLogsModal> CreateUserSearchLogs(UserSearchLogsModal modal);
        Task<PaginatedList<UserSearchLogsModal>> GetAllUserSearchLogs(UserSearchLogsFilter filter);
        Task<UserSearchLogsModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<UserSearchLogsModal> UpdateUserSearchLogs(UserSearchLogsModal modal);
    }
}