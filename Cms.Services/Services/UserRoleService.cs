using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.UserRole;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class UserRoleService : IUserRoleService
    {
        public IUserRoleRepository _userRoleRepository;
        public IMapper _mapper;
        public UserRoleService(IUserRoleRepository usersRepository, IMapper mapper)
        {
            _userRoleRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CreateUserRoleModal> CreateUserRole(CreateUserRoleModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _userRoleRepository.Insert(_mapper.Map<CreateUserRoleModal, UserRole>(modal));
                return _mapper.Map<CreateUserRoleModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UpdateUserRoleModal> UpdateUserRole(UpdateUserRoleModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _userRoleRepository.Update(_mapper.Map<UpdateUserRoleModal, UserRole>(modal));
                return _mapper.Map<UserRole, UpdateUserRoleModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async  Task<PaginatedList<UserRoleModal>> GetAllUserRole(UserRoleFilter filter)
        {
            if (filter != null)
            {
                var query =
                _userRoleRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(!string.IsNullOrEmpty(filter.RoleName), x => EF.Functions.Like(x.Name, $"%{filter.RoleName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Name, $"%{filter.Query}%"))
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id);
                var result = _mapper.Map<PaginatedList<UserRole>, PaginatedList<UserRoleModal>>(new PaginatedList<UserRole>(query.ToList(), filter.PageSize, filter.PageNumber));
                return await Task.FromResult(result);
            }
            else
            {
                var result =
                _userRoleRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<UserRole>, PaginatedList<UserRoleModal>>(new PaginatedList<UserRole>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _userRoleRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<UserRoleModal> GetById(int Id)
        {
            try
            {

            var result = await _userRoleRepository.Get(Id).Result.FirstOrDefaultAsync();
            return _mapper.Map<UserRole,UserRoleModal>(result);
            }catch(Exception ex)
            {
                throw ex;
            }
        
        }
    }
}
