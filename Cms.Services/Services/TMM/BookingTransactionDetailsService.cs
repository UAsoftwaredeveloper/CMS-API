using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters.FrontEnd;
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
    public class BookingTransactionDetailsService : IBookingTransactionDetailsService
    {
        private readonly IBookingTransactionDetailsRepository _bookingRepository;
        private readonly IMapper _mapper;
        public BookingTransactionDetailsService(IBookingTransactionDetailsRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<BookingTransactionDetailsModal>> GetAllBookingTransactionDetails(BookingTransactionDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _bookingRepository.GetAll(deleted: false).Include(x => x.BookingJourneyDetails).Include(x => x.BookingPaxDetails)
                .WhereIf(filter.PortalID != null, x => x.PortalID == filter.PortalID)
                .WhereIf(filter.Created_By != null, x => x.CreatedBy == filter.Created_By)
                .WhereIf(filter.IsBooked != null, x => x.IsBooked == filter.IsBooked)
                .WhereIf(filter.FromDate != null, x => x.InsertedOn >= filter.FromDate.Value)
                .WhereIf(filter.ToDate != null, x => x.InsertedOn <= filter.ToDate.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.BillerEmailId), x => EF.Functions.Like(x.BillerEmailId, $"%{filter.BillerEmailId}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.BillerName), x => EF.Functions.Like(x.BillerName, $"%{filter.BillerName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.BillerPhone), x => EF.Functions.Like(x.BillerPhone, $"%{filter.BillerPhone}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CabinClass), x => EF.Functions.Like(x.CabinClass, $"%{filter.CabinClass}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CouponCode), x => EF.Functions.Like(x.CouponCode, $"%{filter.CouponCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CustRefNo), x => EF.Functions.Like(x.CustRefNo, $"%{filter.CustRefNo}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Destination), x => EF.Functions.Like(x.Destination, $"%{filter.Destination}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DeviceType), x => EF.Functions.Like(x.DeviceType, $"%{filter.DeviceType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.FareType), x => EF.Functions.Like(x.FareType, $"%{filter.FareType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Origin), x => EF.Functions.Like(x.Origin, $"%{filter.Origin}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PNRNo), x => EF.Functions.Like(x.PNRNo, $"%{filter.PNRNo}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SearchGuid), x => EF.Functions.Like(x.SearchGuid, $"%{filter.SearchGuid}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SupplierName), x => EF.Functions.Like(x.SupplierName, $"%{filter.SupplierName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.UTMSource), x => EF.Functions.Like(x.UTMSource, $"%{filter.UTMSource}%")).OrderByDescending(x => x.TransactionId);

                return await Task.FromResult(_mapper.Map<PaginatedList<BookingTransactionDetails>, PaginatedList<BookingTransactionDetailsModal>>(new PaginatedList<BookingTransactionDetails>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _bookingRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<BookingTransactionDetails>, PaginatedList<BookingTransactionDetailsModal>>(new PaginatedList<BookingTransactionDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<PaginatedList<BookingTransactionDetailsModal>> GetAllBookingTransactionDetailsPublic(FlightBookingTransactionFilter filter)
        {
            if (filter != null)
            {
                var query =
                _bookingRepository
            .GetAll(deleted: false)
            .Include(x => x.BookingJourneyDetails)
            .Include(x => x.BookingPaxDetails)
            .Where(x=>x.BookingConfirmStatus!="Sold" && x.BookingConfirmStatus!= "PriceChange")
            .WhereIf(filter.PortalID != null, x => x.PortalID == filter.PortalID)
            .WhereIf(filter.IsBooked != null, x => x.IsBooked == filter.IsBooked)
            .WhereIf(filter.Completed != null && filter.Completed.Value, x => x.DepartDate < DateTime.Today)
            .WhereIf(filter.UpComing != null && filter.UpComing.Value, x => x.DepartDate >= DateTime.Today)
            .WhereIf(filter.FromDate != null, x => x.InsertedOn >= filter.FromDate.Value)
            .WhereIf(filter.ToDate != null, x => x.InsertedOn <= filter.ToDate.Value)
            .WhereIf(filter.Created_By != null || !string.IsNullOrEmpty(filter.BillerEmailId),
                x => (filter.Created_By != null && x.CreatedBy == filter.Created_By) ||
                     (!string.IsNullOrEmpty(filter.BillerEmailId) && EF.Functions.Like(x.BillerEmailId, $"%{filter.BillerEmailId}%")))
            .WhereIf(!string.IsNullOrEmpty(filter.Origin), x => EF.Functions.Like(x.Origin, $"%{filter.Origin}%"))
            .WhereIf(!string.IsNullOrEmpty(filter.Destination), x => EF.Functions.Like(x.Destination, $"%{filter.Destination}%"))
            .WhereIf(!string.IsNullOrEmpty(filter.PNRNo), x => EF.Functions.Like(x.PNRNo, $"%{filter.PNRNo}%"))
            .WhereIf(!string.IsNullOrEmpty(filter.SearchGuid), x => EF.Functions.Like(x.SearchGuid, $"%{filter.SearchGuid}%"))
            .WhereIf(!string.IsNullOrEmpty(filter.AirlineName),
                  x => x.BookingJourneyDetails.Any(bj => EF.Functions.Like(bj.AirlineName, $"%{filter.AirlineName}%")));

                if (filter.SortDescending != null && filter.SortDescending.Value)
                    query = query.OrderByDescending(x => x.DepartDate);
                else
                    query = query.OrderBy(x => x.DepartDate);
                return await Task.FromResult(_mapper.Map<PaginatedList<BookingTransactionDetails>, PaginatedList<BookingTransactionDetailsModal>>(new PaginatedList<BookingTransactionDetails>(query.ToList(), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _bookingRepository.GetAll(deleted: false).Include(x => x.BookingJourneyDetails)
            .Include(x => x.BookingPaxDetails);
                return await Task.FromResult(_mapper.Map<PaginatedList<BookingTransactionDetails>, PaginatedList<BookingTransactionDetailsModal>>(new PaginatedList<BookingTransactionDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<BookingTransactionDetailsModal> GetById(long Id)
        {
            return _mapper.Map<BookingTransactionDetails, BookingTransactionDetailsModal>(await _bookingRepository.Entites().Include(x => x.BookingJourneyDetails).Include(x => x.BookingPaxDetails).FirstOrDefaultAsync(x => x.TransactionId == Id));

        }
    }
}
