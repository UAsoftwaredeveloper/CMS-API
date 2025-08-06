using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters.TMM;
using Cms.Services.Interfaces.TMM;
using Cms.Services.Models.TMMModals;
using CMS.Repositories.Interfaces.TMM;
using DataManager.TMMDbClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services.TMM
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<UsersModal>> GetAllUsers(UsersFilter filter)
        {
            if (filter != null)
            {
                var result =
                _usersRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.Facebook != null && filter.Facebook.Value==true, x => !string.IsNullOrEmpty(x.FacebookAuthToken))
                .WhereIf(filter.TMM != null && filter.TMM.Value==true, x => string.IsNullOrEmpty(x.FacebookAuthToken) && string.IsNullOrEmpty(x.GoogleAuthToken))
                .WhereIf(filter.TMM != null && filter.TMM.Value==false, x => !string.IsNullOrEmpty(x.FacebookAuthToken) || !string.IsNullOrEmpty(x.GoogleAuthToken))
                .WhereIf(filter.Facebook != null && filter.Facebook.Value==false, x => string.IsNullOrEmpty(x.FacebookAuthToken))
                .WhereIf(filter.Google != null && filter.Google.Value==true, x => !string.IsNullOrEmpty(x.GoogleAuthToken))
                .WhereIf(filter.Google != null && filter.Google.Value==false, x => string.IsNullOrEmpty(x.GoogleAuthToken))
                .WhereIf(filter.Company != null && filter.Company.Value==true, x => !string.IsNullOrEmpty(x.CompanyName))
                .WhereIf(filter.Company != null && filter.Company.Value==false, x => string.IsNullOrEmpty(x.CompanyName))
                .WhereIf(filter.EmailVerified != null, x => x.EmailVerified == filter.EmailVerified)
                .WhereIf(filter.PhoneVerified != null, x => x.PhoneVerified == filter.PhoneVerified)
                .WhereIf(filter.Active != null, x => x.Active == filter.Active)
                .WhereIf(!string.IsNullOrEmpty(filter.Email), x => EF.Functions.Like(x.Email, $"%{filter.Email}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CompanyName), x => EF.Functions.Like(x.CompanyName, $"%{filter.CompanyName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Name), x => EF.Functions.Like(x.Name, $"%{filter.Name}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Email, $"%{filter.Query}%")).OrderByDescending(x => x.Id);

                return await Task.FromResult(_mapper.Map<PaginatedList<Users>, PaginatedList<UsersModal>>(new PaginatedList<Users>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _usersRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<Users>, PaginatedList<UsersModal>>(new PaginatedList<Users>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<UsersModal> GetById(int Id)
        {
            return _mapper.Map<Users, UsersModal>(await _usersRepository.Entites().FirstOrDefaultAsync(x => x.Id == Id));

        }
    }
}
