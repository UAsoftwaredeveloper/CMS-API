using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IUsersService
    {
        Task<PaginatedList<UsersModal>> GetAllUsers(UsersFilter filter);
        Task<UsersModal> GetById(int Id);
    }
}