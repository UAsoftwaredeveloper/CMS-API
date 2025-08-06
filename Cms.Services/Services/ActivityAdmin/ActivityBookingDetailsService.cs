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
    public class ActivityBookingDetailsService : IActivityBookingDetailsService
    {
        private readonly IActivityBookingDetailsRepository _activityBookingRepository;
        private readonly IMapper _mapper;
        public ActivityBookingDetailsService(IActivityBookingDetailsRepository activityBookingRepository, IMapper mapper)
        {
            _activityBookingRepository = activityBookingRepository ?? throw new ArgumentNullException(nameof(activityBookingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<ActivityBookingDetailsModal>> GetAllActivityBookingDetails(ActivityBookingDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _activityBookingRepository.GetAll(deleted: false)
                .WhereIf(filter.BookingID != null, x => x.BookingID == filter.BookingID)
                .WhereIf(filter.AffiliateID != null && filter.AffiliateID > 0, x => x.AffiliateID == filter.AffiliateID)
                .WhereIf(filter.CreatedBy != null, x => x.CreatedBy == filter.CreatedBy)
                .WhereIf(filter.SupplierID != null && filter.SupplierID > 0, x => x.SupplierID == filter.SupplierID)
                .WhereIf(filter.FromDate != null, x => x.Created_On >= filter.FromDate.Value)
                .WhereIf(filter.ToDate != null, x => x.Created_On <= filter.ToDate.Value)
                .WhereIf(filter.ActivityDt != null, x => x.ActivityDt == filter.ActivityDt)
                .WhereIf(!string.IsNullOrEmpty(filter.Destination), x => EF.Functions.Like(x.Destination, $"%{filter.Destination}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.ActivityType), x => EF.Functions.Like(x.ActivityType, $"%{filter.ActivityType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.BillerEmailId), x => EF.Functions.Like(x.BillerEmailId, $"%{filter.BillerEmailId}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.BillerPhone), x => EF.Functions.Like(x.BillerPhone, $"%{filter.BillerPhone}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.BookingStatus), x => EF.Functions.Like(x.BookingStatus, $"%{filter.BookingStatus}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CategoryName), x => EF.Functions.Like(x.CategoryName, $"%{filter.CategoryName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Currency), x => EF.Functions.Like(x.Currency, $"%{filter.Currency}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SearchKey), x => EF.Functions.Like(x.SearchKey, $"%{filter.SearchKey}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SearchReq), x => EF.Functions.Like(x.SearchReq, $"%{filter.SearchReq}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x =>
                EF.Functions.Like(x.CountryCode, $"%{filter.Query}%") ||
                EF.Functions.Like(x.Destination, $"%{filter.Query}%")).OrderByDescending(x => x.Created_On);

                return await Task.FromResult(_mapper.Map<PaginatedList<ActivityBookingDetails>, PaginatedList<ActivityBookingDetailsModal>>(new PaginatedList<ActivityBookingDetails>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _activityBookingRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<ActivityBookingDetails>, PaginatedList<ActivityBookingDetailsModal>>(new PaginatedList<ActivityBookingDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<PaginatedList<ActivityBookingDetailsModal>> GetUsersAllActivityBookingDetails(ActivityBookingDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _activityBookingRepository.GetAll(deleted: false)
    .WhereIf(filter?.BookingID != null, x => x.BookingID == filter.BookingID)
    .WhereIf(filter?.FromDate != null, x => x.Created_On >= filter.FromDate.Value)
    .WhereIf(filter?.ToDate != null, x => x.Created_On <= filter.ToDate.Value)
    .WhereIf(filter?.Completed == true, x => x.ActivityDt < DateTime.Today)
    .WhereIf(filter?.UpComing == true, x => x.ActivityDt >= DateTime.Today)
    .WhereIf(!string.IsNullOrEmpty(filter?.Destination), x => EF.Functions.Like(x.Destination, $"%{filter.Destination}%"))
    .WhereIf(filter?.Created_By != null && !string.IsNullOrEmpty(filter?.BillerEmailId),
        x => x.CreatedBy == filter.Created_By || EF.Functions.Like(x.BillerEmailId, $"%{filter.BillerEmailId}%"))
    .WhereIf(!string.IsNullOrEmpty(filter?.ActivityType), x => EF.Functions.Like(x.ActivityType, $"%{filter.ActivityType}%"))
    .WhereIf(!string.IsNullOrEmpty(filter?.BillerPhone), x => EF.Functions.Like(x.BillerPhone, $"%{filter.BillerPhone}%"))
    .WhereIf(!string.IsNullOrEmpty(filter?.BookingStatus), x => EF.Functions.Like(x.BookingStatus, $"%{filter.BookingStatus}%"))
    .WhereIf(!string.IsNullOrEmpty(filter?.SearchKey), x => EF.Functions.Like(x.SearchKey, $"%{filter.SearchKey}%"))
    .WhereIf(!string.IsNullOrEmpty(filter?.SearchReq), x => EF.Functions.Like(x.SearchReq, $"%{filter.SearchReq}%"))
    .WhereIf(!string.IsNullOrEmpty(filter?.Query), x =>
        EF.Functions.Like(x.CountryCode, $"%{filter.Query}%") ||
        EF.Functions.Like(x.Destination, $"%{filter.Query}%"));
                if (filter.SortDescending != null && filter.SortDescending.Value)
                    result = result.OrderByDescending(x => x.ActivityDt);
                if (filter.SortAscending != null && filter.SortAscending.Value)
                    result = result.OrderBy(x => x.ActivityDt);

                return await Task.FromResult(_mapper.Map<PaginatedList<ActivityBookingDetails>, PaginatedList<ActivityBookingDetailsModal>>(new PaginatedList<ActivityBookingDetails>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _activityBookingRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<ActivityBookingDetails>, PaginatedList<ActivityBookingDetailsModal>>(new PaginatedList<ActivityBookingDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<ActivityBookingDetailsModal> GetById(int Id)
        {
            return _mapper.Map<ActivityBookingDetails, ActivityBookingDetailsModal>(await _activityBookingRepository.Entites().FirstOrDefaultAsync(x => x.BookingID == Id));

        }
    }
}
