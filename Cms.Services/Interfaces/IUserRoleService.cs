using Cms.Services.Filters;
using Cms.Services.Models.UserRole;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<CreateUserRoleModal> CreateUserRole(CreateUserRoleModal modal);
        Task<PaginatedList<UserRoleModal>> GetAllUserRole(UserRoleFilter filter);
        Task<UserRoleModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<UpdateUserRoleModal> UpdateUserRole(UpdateUserRoleModal modal);
    }
}