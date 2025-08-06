using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.UserRoleMenuPermissions;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class UserRoleMenuPermissionService : IUserRoleMenuPermissionService
    {
        public IUserRoleMenuPermissionRepository _userRoleMenuPermissionRepository;
        public IMapper _mapper;
        public UserRoleMenuPermissionService(IUserRoleMenuPermissionRepository usersRepository, IMapper mapper)
        {
            _userRoleMenuPermissionRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<UserRoleMenuPermissionModal> CreateUserRoleMenuPermission(UserRoleMenuPermissionModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {


                var result = await _userRoleMenuPermissionRepository.Insert(_mapper.Map<UserRoleMenuPermissionModal, UserRoleMenuPermission>(modal));
                return _mapper.Map<UserRoleMenuPermission, UserRoleMenuPermissionModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UserRoleMenuPermissionModal> UpdateUserRoleMenuPermission(UserRoleMenuPermissionModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _userRoleMenuPermissionRepository.Update(_mapper.Map<UserRoleMenuPermissionModal, UserRoleMenuPermission>(modal));
                return _mapper.Map<UserRoleMenuPermission, UserRoleMenuPermissionModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<UserRoleMenuPermissionModal>> GetAllUserRoleMenuPermission(UserRoleMenuPermissionFilter filter)
        {
            if (filter != null)
            {
                var result =
                _userRoleMenuPermissionRepository.GetAll(deleted: false).Include(x=>x.MenuMaster).Include(x=>x.UserRole)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.Active !=null, x => x.Active == filter.Active)
                .WhereIf(!string.IsNullOrEmpty(filter.RoleName), x => EF.Functions.Like(x.UserRole.Name, $"%{filter.RoleName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.MenuName), x => EF.Functions.Like(x.MenuMaster.Name, $"%{filter.MenuName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.MenuMaster.Name, $"%{filter.Query}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.UserRole.Name, $"%{filter.Query}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Users.UserName, $"%{filter.Query}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Users.Email, $"%{filter.Query}%"))
                ;

                return await Task.FromResult(_mapper.Map<PaginatedList<UserRoleMenuPermission>, PaginatedList<UserRoleMenuPermissionModal>>(new PaginatedList<UserRoleMenuPermission>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _userRoleMenuPermissionRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<UserRoleMenuPermission>, PaginatedList<UserRoleMenuPermissionModal>>(new PaginatedList<UserRoleMenuPermission>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<UserRoleMenuPermissionModal> GetById(int Id)
        {
            var result = await _userRoleMenuPermissionRepository.GetAll(true).Include(x => x.Users).Include(x => x.UserRole).Include(x => x.MenuMaster).FirstOrDefaultAsync(x => x.Id == Id);
            return _mapper.Map<UserRoleMenuPermission, UserRoleMenuPermissionModal>(result);

        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _userRoleMenuPermissionRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<List<UserRoleMenuPermissionModal>> CreateUserRoleMenuPermissionList(List<UserRoleMenuPermissionModal> modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {

                var result = await _userRoleMenuPermissionRepository.InsertRange(_mapper.Map<List<UserRoleMenuPermissionModal>, List<UserRoleMenuPermission>>(modal));
                return _mapper.Map<List<UserRoleMenuPermission>, List<UserRoleMenuPermissionModal>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<UserRoleMenuPermissionModal>> UpdateUserRoleMenuPermissionList(List<UserRoleMenuPermissionModal> modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {

                var result = await _userRoleMenuPermissionRepository.UpdateRange(_mapper.Map<List<UserRoleMenuPermissionModal>, List<UserRoleMenuPermission>>(modal));
                return _mapper.Map<List<UserRoleMenuPermission>, List<UserRoleMenuPermissionModal>>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
