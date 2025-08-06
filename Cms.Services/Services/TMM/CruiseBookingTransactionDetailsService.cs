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
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Cms.Services.Services.TMM
{
    public class CruiseBookingTransactionDetailsService : ICruiseBookingTransactionDetailsService
    {
        private readonly ICruiseBookingTransactionDetailsRepository _subscribeRepository;
        private readonly IMapper _mapper;
        public CruiseBookingTransactionDetailsService(ICruiseBookingTransactionDetailsRepository usersRepository, IMapper mapper)
        {
            _subscribeRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<CruiseBookingTransactionDetailsModal>> GetAllCruiseBookingTransactionDetails(CruiseBookingTransactionDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _subscribeRepository.GetAll(deleted: false)
                .WhereIf(filter.PortalID != null, x => x.PortalID == filter.PortalID)
                .WhereIf(filter.IsBooked != null, x => x.IsBooked == filter.IsBooked)
                .WhereIf(filter.Created_By != null, x => x.CreatedBy == filter.Created_By)
                .WhereIf(filter.FromDate != null, x => x.InsertedOn >= filter.FromDate.Value)
                .WhereIf(filter.ToDate != null, x => x.InsertedOn <= filter.ToDate.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.BillerEmailId), x => EF.Functions.Like(x.BillerEmailId, $"%{filter.BillerEmailId}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.BillerPhone), x => EF.Functions.Like(x.BillerPhone, $"%{filter.BillerPhone}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.BookingMode), x => EF.Functions.Like(x.BookingMode, $"%{filter.BookingMode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.BillerCity), x => EF.Functions.Like(x.BillerCity, $"%{filter.BillerCity}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CardType), x => EF.Functions.Like(x.CardType, $"%{filter.CardType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CruiseLineName), x => EF.Functions.Like(x.CruiseLineName, $"%{filter.CruiseLineName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CruiseTitle), x => EF.Functions.Like(x.CruiseTitle, $"%{filter.CruiseTitle}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CouponCode), x => EF.Functions.Like(x.CouponCode, $"%{filter.CouponCode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CustRefNo), x => EF.Functions.Like(x.CustRefNo, $"%{filter.CustRefNo}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Duration), x => EF.Functions.Like(x.Duration, $"%{filter.Duration}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DeviceType), x => EF.Functions.Like(x.DeviceType, $"%{filter.DeviceType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PNRNo), x => EF.Functions.Like(x.PNRNo, $"%{filter.PNRNo}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.ShipName), x => EF.Functions.Like(x.ShipName, $"%{filter.ShipName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SearchGuid), x => EF.Functions.Like(x.SearchGuid, $"%{filter.SearchGuid}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.ToCurrencyType), x => EF.Functions.Like(x.ToCurrencyType, $"%{filter.ToCurrencyType}%")).OrderByDescending(x => x.TransactionId);

                return await Task.FromResult(_mapper.Map<PaginatedList<CruiseBookingTransactionDetails>, PaginatedList<CruiseBookingTransactionDetailsModal>>(new PaginatedList<CruiseBookingTransactionDetails>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _subscribeRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<CruiseBookingTransactionDetails>, PaginatedList<CruiseBookingTransactionDetailsModal>>(new PaginatedList<CruiseBookingTransactionDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<PaginatedList<CruiseBookingTransactionDetailsModal>> GetUsersAllCruiseBookingTransactionDetails(CruiseBookingTransactionDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _subscribeRepository.GetAll(deleted: false)
                .WhereIf(filter.TransactionId != null&&filter.TransactionId.Value>0, x => x.TransactionId == filter.TransactionId)
                .WhereIf(filter.PortalID != null, x => x.PortalID == filter.PortalID)
                .WhereIf(filter.IsBooked != null, x => x.IsBooked == filter.IsBooked)
                .WhereIf(filter.FromDate != null, x => x.InsertedOn >= filter.FromDate.Value)
                .WhereIf(filter.ToDate != null, x => x.InsertedOn <= filter.ToDate.Value)
                .WhereIf(filter.Completed != null && filter.Completed.Value, x => x.DepartureDt < DateTime.Now)
                .WhereIf(filter.UpComing != null && filter.UpComing.Value, x => x.DepartureDt >= DateTime.Now)
                .WhereIf(filter.Created_By != null && !string.IsNullOrEmpty(filter.BillerEmailId), x => x.CreatedBy == filter.Created_By || EF.Functions.Like(x.BillerEmailId, $"%{filter.BillerEmailId}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.BookingMode), x => EF.Functions.Like(x.BookingMode, $"%{filter.BookingMode}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CruiseLineName), x => EF.Functions.Like(x.CruiseLineName, $"%{filter.CruiseLineName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CruiseTitle), x => EF.Functions.Like(x.CruiseTitle, $"%{filter.CruiseTitle}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Duration), x => EF.Functions.Like(x.Duration, $"%{filter.Duration}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PNRNo), x => EF.Functions.Like(x.PNRNo, $"%{filter.PNRNo}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.ShipName), x => EF.Functions.Like(x.ShipName, $"%{filter.ShipName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SearchGuid), x => EF.Functions.Like(x.SearchGuid, $"%{filter.SearchGuid}%"));
                if (filter.SortDescending != null && filter.SortDescending.Value)
                    result = result.OrderByDescending(x => x.DepartureDt);
                if (filter.SortAscending != null && filter.SortAscending.Value)
                    result = result.OrderBy(x => x.DepartureDt);
                else
                    result = result.OrderByDescending(x => x.TransactionId);

                return await Task.FromResult(_mapper.Map<PaginatedList<CruiseBookingTransactionDetails>, PaginatedList<CruiseBookingTransactionDetailsModal>>(new PaginatedList<CruiseBookingTransactionDetails>(result.ToList
                        (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _subscribeRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<CruiseBookingTransactionDetails>, PaginatedList<CruiseBookingTransactionDetailsModal>>(new PaginatedList<CruiseBookingTransactionDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<CruiseBookingTransactionDetailsModal> GetById(int Id)
        {
            return _mapper.Map<CruiseBookingTransactionDetails, CruiseBookingTransactionDetailsModal>(await _subscribeRepository.Entites().FirstOrDefaultAsync(x => x.TransactionId == Id));

        }
    }
}
