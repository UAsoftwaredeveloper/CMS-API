using Cms.Services.Filters;
using Cms.Services.Models.UserRoleMenuPermissions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface IUserRoleMenuPermissionService
    {
        Task<UserRoleMenuPermissionModal> CreateUserRoleMenuPermission(UserRoleMenuPermissionModal modal);
        Task<List<UserRoleMenuPermissionModal>> CreateUserRoleMenuPermissionList(List<UserRoleMenuPermissionModal> modal);
        Task<PaginatedList<UserRoleMenuPermissionModal>> GetAllUserRoleMenuPermission(UserRoleMenuPermissionFilter filter);
        Task<UserRoleMenuPermissionModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<UserRoleMenuPermissionModal> UpdateUserRoleMenuPermission(UserRoleMenuPermissionModal modal);
        Task<List<UserRoleMenuPermissionModal>> UpdateUserRoleMenuPermissionList(List<UserRoleMenuPermissionModal> modal);
    }
}