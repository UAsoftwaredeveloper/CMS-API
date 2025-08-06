using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters.TransferAdmin;
using Cms.Services.Interfaces.TransferAdmin;
using Cms.Services.Models.TransferAdmin;
using CMS.Repositories.Interfaces.TransferAdmin;
using DataManager.TransferAdmin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services.TransferAdmin
{
    public class CarBookingTransactionDetailsService : ICarBookingTransactionDetailsService
    {
        private readonly ICarBookingTransactionDetailsRepository _repository;
        private readonly IMapper _mapper;
        public CarBookingTransactionDetailsService(ICarBookingTransactionDetailsRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<CarBookingTransactionDetailsModal>> GetAllCarBookingTransactionDetails(CarBookingTransactionDetailsFilter filter)
        {
            try
            {

                if (filter != null)
                {
                    var result =
                    _repository.GetAll(deleted: false)
                    .WhereIf(filter.PortalID != null, x => x.PortalID == filter.PortalID)
                    .WhereIf(filter.AffiliateID != null, x => x.AffiliateID == filter.AffiliateID)
                    .WhereIf(filter.SupplierID != null, x => x.SupplierID == filter.SupplierID)
                    .WhereIf(filter.Created_By != null, x => x.CreatedBy == filter.Created_By)
                    .WhereIf(filter.FromDate != null, x => x.Created_On >= filter.FromDate.Value)
                    .WhereIf(filter.ToDate != null, x => x.Created_On <= filter.ToDate.Value)
                    .WhereIf(!string.IsNullOrEmpty(filter.BillerEmailId), x => EF.Functions.Like(x.BillerEmailId, $"%{filter.BillerEmailId}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.BookingStatus), x => EF.Functions.Like(x.BookingStatus, $"%{filter.BookingStatus}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.RepriceKey), x => EF.Functions.Like(x.RepriceKey, $"%{filter.RepriceKey}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.SearchKey), x => EF.Functions.Like(x.SearchKey, $"%{filter.SearchKey}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.SearchReq), x => EF.Functions.Like(x.SearchReq, $"%{filter.SearchReq}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.TransferTypeName), x => EF.Functions.Like(x.TransferTypeName, $"%{filter.TransferTypeName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.TravelType), x => EF.Functions.Like(x.TravelType, $"%{filter.TravelType}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.TripType), x => EF.Functions.Like(x.TripType, $"%{filter.TripType}%")).OrderByDescending(x => x.Created_On);

                    return await Task.FromResult(_mapper.Map<PaginatedList<CarBookingTransactionDetails>, PaginatedList<CarBookingTransactionDetailsModal>>(new PaginatedList<CarBookingTransactionDetails>(result.ToList
                        (), filter.PageSize, filter.PageNumber)));
                }
                else
                {
                    var result =
                    _repository.GetAll(deleted: false);
                    return await Task.FromResult(_mapper.Map<PaginatedList<CarBookingTransactionDetails>, PaginatedList<CarBookingTransactionDetailsModal>>(new PaginatedList<CarBookingTransactionDetails>(result, filter.PageSize, filter.PageNumber)));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<PaginatedList<CarBookingTransactionDetailsModal>> GetUsersAllCarBookingTransactionDetails(CarBookingTransactionDetailsFilter filter)
        {
            try
            {

                if (filter != null)
                {
                    var result =
                    _repository.GetAll(deleted: false)
                    .WhereIf(filter.PortalID != null, x => x.PortalID == filter.PortalID)
                    .WhereIf(filter.FromDate != null, x => x.Created_On >= filter.FromDate.Value)
                    .WhereIf(filter.ToDate != null, x => x.Created_On <= filter.ToDate.Value)
                    .WhereIf(filter.Created_By != null && !string.IsNullOrEmpty(filter.BillerEmailId), x => x.CreatedBy == filter.Created_By || EF.Functions.Like(x.BillerEmailId, $"%{filter.BillerEmailId}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.BookingStatus), x => EF.Functions.Like(x.BookingStatus, $"%{filter.BookingStatus}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.RepriceKey), x => EF.Functions.Like(x.RepriceKey, $"%{filter.RepriceKey}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.SearchKey), x => EF.Functions.Like(x.SearchKey, $"%{filter.SearchKey}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.SearchReq), x => EF.Functions.Like(x.SearchReq, $"%{filter.SearchReq}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.TransferTypeName), x => EF.Functions.Like(x.TransferTypeName, $"%{filter.TransferTypeName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.TravelType), x => EF.Functions.Like(x.TravelType, $"%{filter.TravelType}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.TripType), x => EF.Functions.Like(x.TripType, $"%{filter.TripType}%")).OrderByDescending(x => x.Created_On);

                    return await Task.FromResult(_mapper.Map<PaginatedList<CarBookingTransactionDetails>, PaginatedList<CarBookingTransactionDetailsModal>>(new PaginatedList<CarBookingTransactionDetails>(result.ToList
                        (), filter.PageSize, filter.PageNumber)));
                }
                else
                {
                    var result =
                    _repository.GetAll(deleted: false);
                    return await Task.FromResult(_mapper.Map<PaginatedList<CarBookingTransactionDetails>, PaginatedList<CarBookingTransactionDetailsModal>>(new PaginatedList<CarBookingTransactionDetails>(result, filter.PageSize, filter.PageNumber)));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CarBookingTransactionDetailsModal> GetById(int Id)
        {
            return _mapper.Map<CarBookingTransactionDetails, CarBookingTransactionDetailsModal>(await _repository.Entites().FirstOrDefaultAsync(x => x.BookingID == Id));

        }
    }
}
