using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.UserSearchLogs;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class UserSearchLogsService : IUserSearchLogsService
    {
        public IUserSearchLogsRepository _userSearchLogsRepository;
        public IMapper _mapper;
        public UserSearchLogsService(IUserSearchLogsRepository UserSearchLogsRepository, IMapper mapper)
        {
            _userSearchLogsRepository = UserSearchLogsRepository ?? throw new ArgumentNullException(nameof(UserSearchLogsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<UserSearchLogsModal> CreateUserSearchLogs(UserSearchLogsModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _userSearchLogsRepository.Insert(_mapper.Map<UserSearchLogsModal, UserSearchLogs>(modal));
                return _mapper.Map<UserSearchLogs, UserSearchLogsModal>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<UserSearchLogsModal> UpdateUserSearchLogs(UserSearchLogsModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _userSearchLogsRepository.Update(_mapper.Map<UserSearchLogsModal, UserSearchLogs>(modal));
                return _mapper.Map<UserSearchLogs, UserSearchLogsModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<UserSearchLogsModal>> GetAllUserSearchLogs(UserSearchLogsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _userSearchLogsRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.Active !=null, x => x.Active == filter.Active)
                .WhereIf(!string.IsNullOrEmpty(filter.PortalIds)&& filter.PortalIds.Split(",").LongLength>0, x => filter.PortalIds.Split(',',' ').ToList().Any(xx=>xx==x.PortalId.ToString()))
                .WhereIf(!string.IsNullOrEmpty(filter.PageUrl), x => EF.Functions.Like(x.VisitedPageUrl, $"%{filter.PageUrl}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SearchedKeywords), x => EF.Functions.Like(x.SearchText.ToLower(), $"%{filter.SearchedKeywords.ToLower()}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PageType), x => EF.Functions.Like(x.VisitedPageType, $"%{filter.PageType}%"))
                .WhereIf(filter.Active != null && filter.Active.Value, x => x.Active == filter.Active)
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.SearchResults, $"%{filter.Query}%")
                || EF.Functions.Like(x.SearchText, $"%{filter.Query}%")
                || EF.Functions.Like(x.VisitedPageName, $"%{filter.Query}%"))
                ;

                return await Task.FromResult(_mapper.Map<PaginatedList<UserSearchLogs>, PaginatedList<UserSearchLogsModal>>(new PaginatedList<UserSearchLogs>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _userSearchLogsRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<UserSearchLogs>, PaginatedList<UserSearchLogsModal>>(new PaginatedList<UserSearchLogs>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _userSearchLogsRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<UserSearchLogsModal> GetById(int Id)
        {
            return _mapper.Map<UserSearchLogs, UserSearchLogsModal>(await _userSearchLogsRepository.Get(Id).Result.FirstOrDefaultAsync());
        }
    }
}
