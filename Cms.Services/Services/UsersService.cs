using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.Users;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class UsersService : IUsersService
    {
        public IUsersRepository _usersRepository;
        public IMapper _mapper;
        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CreateUsersModal> CreateUsers(CreateUsersModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _usersRepository.Insert(_mapper.Map<CreateUsersModal, Users>(modal));
                return _mapper.Map<Users, CreateUsersModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UpdateUsersModal> UpdateUsers(UpdateUsersModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _usersRepository.Update(_mapper.Map<UpdateUsersModal, Users>(modal));
                return _mapper.Map<Users, UpdateUsersModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<UsersModal>> GetAllUsers(UserFilter filter)
        {
            try
            {


                if (filter != null)
                {
                    var result =
                    _usersRepository.GetAll(deleted: false).Include(x => x.UserRole)
                    .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                    .WhereIf(!string.IsNullOrEmpty(filter.UserName), x => EF.Functions.Like(x.UserName, $"%{filter.UserName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.PhoneNumber), x => EF.Functions.Like(x.PhoneNumber, $"%{filter.PhoneNumber}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.Role), x => EF.Functions.Like(x.UserRole.Name, $"%{filter.Role}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.UserRole.Name, $"%{filter.Query}%")
                    || EF.Functions.Like(x.UserName, $"%{filter.Query}%") || EF.Functions.Like(x.PhoneNumber, $"%{filter.Query}%")
                    )

                    .WhereIf(filter.Id > 0, x => x.Id == filter.Id);
                    return await Task.FromResult(_mapper.Map<PaginatedList<UsersModal>>(new PaginatedList<Users>(result, filter.PageSize, filter.PageNumber)));
                }
                else
                {
                    var query =
                    _usersRepository.GetAll(deleted: false).Include(x => x.UserRole);
                    var result = new PaginatedList<Users>(query.ToList(), filter.PageSize, filter.PageNumber);
                    return await Task.FromResult(_mapper.Map<PaginatedList<UsersModal>>(result));
                }
            }catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _usersRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<UsersModal> GetUsersModalById(int Id)
        {
            return _mapper.Map<Users, UsersModal>(await _usersRepository.Get(Id).Result.Include(x=>x.UserRole).FirstOrDefaultAsync());
        }

        public Task<UsersModal> Login(string username, string password)
        {

            var result = _usersRepository.GetAll(true).Include(x=>x.UserRole).FirstOrDefault(x => x.UserName == username && x.Password == password);
            return Task.FromResult(_mapper.Map<Users, UsersModal>(result));
        }
    }
}
