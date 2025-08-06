using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters.ActivityAdmin;
using Cms.Services.Interfaces.ActivityAdmin;
using Cms.Services.Models.ActivityAdmin;
using CMS.Repositories.Interfaces.ActivityAdmin;
using DataManager.ActivityAdmin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services.ActivityAdmin
{
    public class ActivitySearchLogsService : IActivitySearchLogsService
    {
        private readonly IActivitySearchLogsRepository _activitySearchLogsRepository;
        private readonly IMapper _mapper;
        public ActivitySearchLogsService(IActivitySearchLogsRepository activitySearchLogsRepository, IMapper mapper)
        {
            _activitySearchLogsRepository = activitySearchLogsRepository ?? throw new ArgumentNullException(nameof(activitySearchLogsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<ActivitySearchLogsModal>> GetAllActivitySearchLogs(ActivitySearchLogsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _activitySearchLogsRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.AffiliateId !=null && filter.AffiliateId > 0, x => x.AffiliateId == filter.AffiliateId)
                .WhereIf(filter.TotalPax !=null && filter.TotalPax > 0, x => x.TotalPax == filter.TotalPax)
                .WhereIf(filter.FromDate !=null, x => x.CreatedOn >= filter.FromDate.Value)
                .WhereIf(filter.ToDate !=null, x => x.CreatedOn <= filter.ToDate.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.Destination), x => EF.Functions.Like(x.Destination, $"%{filter.Destination}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CityCode), x => EF.Functions.Like(x.CityCode, $"%{filter.CityCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CountryCode), x => EF.Functions.Like(x.CountryCode, $"%{filter.CountryCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.cultureID), x => EF.Functions.Like(x.cultureID, $"%{filter.cultureID}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Currency), x => EF.Functions.Like(x.Currency, $"%{filter.Currency}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.HotelCode), x => EF.Functions.Like(x.HotelCode, $"%{filter.HotelCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Nationality), x => EF.Functions.Like(x.Nationality, $"%{filter.Nationality}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.deviceType), x => EF.Functions.Like(x.deviceType, $"%{filter.deviceType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Destination, $"%{filter.Query}%")).OrderByDescending(x => x.CreatedOn);

                return await Task.FromResult(_mapper.Map<PaginatedList<ActivitySearchLogs>, PaginatedList<ActivitySearchLogsModal>>(new PaginatedList<ActivitySearchLogs>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _activitySearchLogsRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<ActivitySearchLogs>, PaginatedList<ActivitySearchLogsModal>>(new PaginatedList<ActivitySearchLogs>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<ActivitySearchLogsModal> GetById(int Id)
        {
            return _mapper.Map<ActivitySearchLogs, ActivitySearchLogsModal>(await _activitySearchLogsRepository.Entites().FirstOrDefaultAsync(x => x.Id == Id));

        }
    }
}
