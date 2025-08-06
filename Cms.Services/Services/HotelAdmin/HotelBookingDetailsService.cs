using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters.HotelAdmin;
using Cms.Services.Interfaces.HotelAdmin;
using Cms.Services.Models.HotelAdmin;
using CMS.Repositories.Interfaces.HotelAdmin;
using DataManager.HotelAdmin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services.HotelAdmin
{
    public class HotelBookingDetailsService : IHotelBookingDetailsService
    {
        private readonly IHotelBookingDetailsRepository _hotelBookingRepository;
        private readonly IMapper _mapper;
        public HotelBookingDetailsService(IHotelBookingDetailsRepository repository, IMapper mapper)
        {
            _hotelBookingRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<HotelBookingDetailsModal>> GetAllHotelBookingDetails(HotelBookingDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _hotelBookingRepository.GetAll(deleted: false)
                .WhereIf(filter.BookingID != null, x => x.BookingID == filter.BookingID)
                .WhereIf(filter.AffiliateID !=null , x => x.AffiliateID == filter.AffiliateID)
                .WhereIf(filter.SupplierID !=null , x => x.SupplierID == filter.SupplierID)
                .WhereIf(filter.Created_By !=null , x => x.CreatedBy == filter.Created_By)
                .WhereIf(filter.Markup != null , x => x.Markup == filter.Markup)
                .WhereIf(filter.TotalPrice != null , x => x.TotalPrice == filter.TotalPrice)
                .WhereIf(filter.FromDate !=null, x => x.Created_On >= filter.FromDate.Value)
                .WhereIf(filter.ToDate !=null, x => x.Created_On <= filter.ToDate.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.BillerEmailId), x => EF.Functions.Like(x.BillerEmailId, $"%{filter.BillerEmailId}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Destination), x => EF.Functions.Like(x.Destination, $"%{filter.Destination}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.BookingStatus), x => EF.Functions.Like(x.BookingStatus, $"%{filter.BookingStatus}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.HotelCode), x => EF.Functions.Like(x.HotelCode, $"%{filter.HotelCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.HotelName), x => EF.Functions.Like(x.HotelName, $"%{filter.HotelName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.RepriceKey), x => EF.Functions.Like(x.RepriceKey, $"%{filter.RepriceKey}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SearchKey), x => EF.Functions.Like(x.SearchKey, $"%{filter.SearchKey}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SearchReq), x => EF.Functions.Like(x.SearchReq, $"%{filter.SearchReq}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Destination, $"%{filter.Query}%")).OrderByDescending(x => x.Created_On);

                return await Task.FromResult(_mapper.Map<PaginatedList<HotelBookingDetails>, PaginatedList<HotelBookingDetailsModal>>(new PaginatedList<HotelBookingDetails>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _hotelBookingRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<HotelBookingDetails>, PaginatedList<HotelBookingDetailsModal>>(new PaginatedList<HotelBookingDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<PaginatedList<HotelBookingDetailsModal>> GetUsersAllHotelBookingDetails(HotelBookingDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _hotelBookingRepository.GetAll(deleted: false)
                .WhereIf(filter.BookingID != null, x => x.BookingID == filter.BookingID)
                .WhereIf(filter.Markup != null, x => x.Markup == filter.Markup)
                .WhereIf(filter.TotalPrice != null, x => x.TotalPrice == filter.TotalPrice)
                .WhereIf(filter.FromDate != null, x => x.Created_On >= filter.FromDate.Value)
                .WhereIf(filter.ToDate != null, x => x.Created_On <= filter.ToDate.Value)
                .WhereIf(filter.Completed != null && filter.Completed.Value, x => x.CheckInDate < DateTime.Today)
                .WhereIf(filter.UpComing != null && filter.UpComing.Value, x => x.CheckInDate >= DateTime.Today)
                .WhereIf(filter.Created_By != null && !string.IsNullOrEmpty(filter.BillerEmailId), x => x.CreatedBy == filter.Created_By || EF.Functions.Like(x.BillerEmailId, $"%{filter.BillerEmailId}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Destination), x => EF.Functions.Like(x.Destination, $"%{filter.Destination}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.BookingStatus), x => EF.Functions.Like(x.BookingStatus, $"%{filter.BookingStatus}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.HotelCode), x => EF.Functions.Like(x.HotelCode, $"%{filter.HotelCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.HotelName), x => EF.Functions.Like(x.HotelName, $"%{filter.HotelName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.RepriceKey), x => EF.Functions.Like(x.RepriceKey, $"%{filter.RepriceKey}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SearchKey), x => EF.Functions.Like(x.SearchKey, $"%{filter.SearchKey}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SearchReq), x => EF.Functions.Like(x.SearchReq, $"%{filter.SearchReq}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Destination, $"%{filter.Query}%"));
                if (filter.SortDescending != null && filter.SortDescending.Value)
                    result = result.OrderByDescending(x => x.CheckInDate);
                if (filter.SortAscending != null && filter.SortAscending.Value)
                    result = result.OrderBy(x => x.CheckInDate);

                return await Task.FromResult(_mapper.Map<PaginatedList<HotelBookingDetails>, PaginatedList<HotelBookingDetailsModal>>(new PaginatedList<HotelBookingDetails>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _hotelBookingRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<HotelBookingDetails>, PaginatedList<HotelBookingDetailsModal>>(new PaginatedList<HotelBookingDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<HotelBookingDetailsModal> GetById(int Id)
        {
            return _mapper.Map<HotelBookingDetails, HotelBookingDetailsModal>(await _hotelBookingRepository.Entites().FirstOrDefaultAsync(x => x.BookingID == Id));

        }
    }
}
