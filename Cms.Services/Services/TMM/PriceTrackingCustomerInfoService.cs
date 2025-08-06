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
    public class PriceTrackingCustomerInfoService : IPriceTrackingCustomerInfoService
    {
        private readonly IPriceTrackingCustomerInfoRepository _priceTrackingCustomerInfoRepository;
        private readonly IMapper _mapper;
        public PriceTrackingCustomerInfoService(IPriceTrackingCustomerInfoRepository priceTrackingCustomerInfoRepository, IMapper mapper)
        {
            _priceTrackingCustomerInfoRepository = priceTrackingCustomerInfoRepository ?? throw new ArgumentNullException(nameof(priceTrackingCustomerInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<PriceTrackingCustomerInfoModal>> GetAllPriceTrackingCustomerInfo(PriceTrackingCustomerInfoFilter filter)
        {
            try
            {

                if (filter != null)
                {
                    var result =
                    _priceTrackingCustomerInfoRepository.GetAll(deleted: false)
                    .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                    //.WhereIf(filter.PortalID > 0, x => x.PortalID == filter.PortalID)
                    .WhereIf(filter.FromDate != null, x => x.CreatedOn >= filter.FromDate.Value)
                    .WhereIf(filter.ToDate != null, x => x.CreatedOn <= filter.ToDate.Value)
                    .WhereIf(filter.DepartureDateFrom != null, x => x.DepartureDate >= filter.DepartureDateFrom.Value)
                    .WhereIf(filter.DepartureDateTo != null, x => x.DepartureDate <= filter.DepartureDateTo.Value)
                    .WhereIf(filter.ReturnDateFrom != null, x => x.ReturnDate >= filter.ReturnDateFrom.Value)
                    .WhereIf(filter.ReturnDateTo != null, x => x.ReturnDate <= filter.ReturnDateTo.Value)
                    .WhereIf(filter.CreatedBy != null && !string.IsNullOrEmpty(filter.CustomerEmail), x => x.CreatedBy == filter.CreatedBy || EF.Functions.Like(x.CustomerEmail, $"%{filter.CustomerEmail}%"))
                    .WhereIf(filter.CreatedBy== null && !string.IsNullOrEmpty(filter.CustomerEmail), x =>  EF.Functions.Like(x.CustomerEmail, $"%{filter.CustomerEmail}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerPhone), x => EF.Functions.Like(x.CustomerPhone, $"%{filter.CustomerPhone}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CabinClass), x => EF.Functions.Like(x.CabinClass, $"%{filter.CabinClass}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.DestinationCode), x => EF.Functions.Like(x.DestinationCode, $"%{filter.DestinationCode}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.OriginCode), x => EF.Functions.Like(x.OriginCode, $"%{filter.OriginCode}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.DeviceType), x => EF.Functions.Like(x.CustomerDeviceType, $"%{filter.DeviceType}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.CustomerEmail, $"%{filter.Query}%")).OrderByDescending(x => x.Id);

                    return await Task.FromResult(_mapper.Map<PaginatedList<PriceTrackingCustomerInfo>, PaginatedList<PriceTrackingCustomerInfoModal>>(new PaginatedList<PriceTrackingCustomerInfo>(result.ToList
                        (), filter.PageSize, filter.PageNumber)));
                }
                else
                {
                    var result =
                    _priceTrackingCustomerInfoRepository.GetAll(deleted: false);
                    return await Task.FromResult(_mapper.Map<PaginatedList<PriceTrackingCustomerInfo>, PaginatedList<PriceTrackingCustomerInfoModal>>(new PaginatedList<PriceTrackingCustomerInfo>(result, filter.PageSize, filter.PageNumber)));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<PriceTrackingCustomerInfoModal> GetById(int Id)
        {
            return _mapper.Map<PriceTrackingCustomerInfo, PriceTrackingCustomerInfoModal>(await _priceTrackingCustomerInfoRepository.Entites().FirstOrDefaultAsync(x => x.Id == Id));

        }
    }
}
