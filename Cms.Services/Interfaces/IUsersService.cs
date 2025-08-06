using Cms.Services.Filters;
using Cms.Services.Models.Users;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface IUsersService
    {
        Task<CreateUsersModal> CreateUsers(CreateUsersModal modal);
        Task<PaginatedList<UsersModal>> GetAllUsers(UserFilter filter);
        Task<UsersModal> GetUsersModalById(int Id);
        Task<UsersModal> Login(string username, string password);
        Task<bool> SoftDelete(int Id);
        Task<UpdateUsersModal> UpdateUsers(UpdateUsersModal modal);
    }
}