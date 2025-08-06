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
    public class QuotationEmailSupportService : IQuotationEmailSupportService
    {
        private readonly IQuotationEmailSupportRepository _quotationEmailSupportRepository;
        private readonly IMapper _mapper;
        public QuotationEmailSupportService(IQuotationEmailSupportRepository quotationEmailSupportRepository, IMapper mapper)
        {
            _quotationEmailSupportRepository = quotationEmailSupportRepository ?? throw new ArgumentNullException(nameof(quotationEmailSupportRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<QuotationEmailSupportModal>> GetAllQuotationEmailSupport(QuotationEmailSupportFilter filter)
        {
            try
            {
                if (filter != null)
                {
                    var result =
                    _quotationEmailSupportRepository.GetAll(deleted: false)
                    .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                    .WhereIf(filter.PortalId > 0, x => x.PortalId == filter.PortalId)
                    .WhereIf(filter.FromDate != null, x => x.CreatedOn >= filter.FromDate.Value)
                    .WhereIf(filter.ToDate != null, x => x.CreatedOn <= filter.ToDate.Value)
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerEmail), x => EF.Functions.Like(x.CustomerEmail, $"%{filter.CustomerEmail}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerPhone), x => EF.Functions.Like(x.CustomerPhone, $"%{filter.CustomerPhone}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.DeviceType), x => EF.Functions.Like(x.DeviceType, $"%{filter.DeviceType}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.Destination), x =>
                    EF.Functions.Like(x.DestinationAirport, $"%{filter.Destination}%")
                    || EF.Functions.Like(x.DestinationAirportCode, $"%{filter.Destination}%")
                    || EF.Functions.Like(x.DestinationAirportCityCode, $"%{filter.Destination}%")
                    || EF.Functions.Like(x.DestinationAirportCityName, $"%{filter.Destination}%")
                    || EF.Functions.Like(x.DestinationAirportCountryCode, $"%{filter.Destination}%")
                    || EF.Functions.Like(x.DestinationAirportCountryName, $"%{filter.Destination}%")
                    )
                    .WhereIf(!string.IsNullOrEmpty(filter.Origine), x =>
                    EF.Functions.Like(x.OrigineAirport, $"%{filter.Origine}%")
                    || EF.Functions.Like(x.OrigineAirportCode, $"%{filter.Origine}%")
                    || EF.Functions.Like(x.OrigineAirportCityCode, $"%{filter.Origine}%")
                    || EF.Functions.Like(x.OrigineAirportCityName, $"%{filter.Origine}%")
                    || EF.Functions.Like(x.OrigineAirportCountryCode, $"%{filter.Origine}%")
                    || EF.Functions.Like(x.OrigineAirportCountryName, $"%{filter.Origine}%")
                    )
                    .WhereIf(!string.IsNullOrEmpty(filter.Query), x => 
                    EF.Functions.Like(x.CustomerEmail, $"%{filter.Query}%")
                   || EF.Functions.Like(x.UserEmail, $"%{filter.Query}%")
                    ).OrderByDescending(x => x.Id);

                    return await Task.FromResult(_mapper.Map<PaginatedList<QuotationEmailSupport>, PaginatedList<QuotationEmailSupportModal>>(new PaginatedList<QuotationEmailSupport>(result.ToList
                        (), filter.PageSize, filter.PageNumber)));
                }
                else
                {
                    var result =
                    _quotationEmailSupportRepository.GetAll(deleted: false);
                    return await Task.FromResult(_mapper.Map<PaginatedList<QuotationEmailSupport>, PaginatedList<QuotationEmailSupportModal>>(new PaginatedList<QuotationEmailSupport>(result, filter.PageSize, filter.PageNumber)));
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex); }

        }
        public async Task<QuotationEmailSupportModal> CreateQuotationEmailSupport(QuotationEmailSupportModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _quotationEmailSupportRepository.Insert(_mapper.Map<QuotationEmailSupportModal, QuotationEmailSupport>(modal));
                if (result.Id > 0)
                {
                    return _mapper.Map<QuotationEmailSupport, QuotationEmailSupportModal>(result);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<PaginatedList<QuotationEmailSupportModal>> GetAllQuotationEmailSupportPublic(QuotationEmailSupportFilter filter)
        {
            if (filter != null)
            {
                var query = _quotationEmailSupportRepository.GetAll(deleted: false)
                    .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                    .WhereIf(filter.PortalId > 0, x => x.PortalId == filter.PortalId)
                    .WhereIf(filter.CreatedBy!=null, x => x.CreatedBy == filter.CreatedBy)
                    .WhereIf(filter.CreatedBy!=null, x => x.UserId == filter.CreatedBy)
                    .WhereIf(filter.FromDate != null, x => x.CreatedOn >= filter.FromDate.Value)
                    .WhereIf(filter.ToDate != null, x => x.CreatedOn <= filter.ToDate.Value)
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerEmail), x => EF.Functions.Like(x.CustomerEmail, $"%{filter.CustomerEmail}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CustomerPhone), x => EF.Functions.Like(x.CustomerPhone, $"%{filter.CustomerPhone}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.Destination), x =>
                    EF.Functions.Like(x.DestinationAirport, $"%{filter.Destination}%")
                    || EF.Functions.Like(x.DestinationAirportCode, $"%{filter.Destination}%")
                    || EF.Functions.Like(x.DestinationAirportCityCode, $"%{filter.Destination}%")
                    || EF.Functions.Like(x.DestinationAirportCityName, $"%{filter.Destination}%")
                    || EF.Functions.Like(x.DestinationAirportCountryCode, $"%{filter.Destination}%")
                    || EF.Functions.Like(x.DestinationAirportCountryName, $"%{filter.Destination}%")
                    )
                    .WhereIf(!string.IsNullOrEmpty(filter.Origine), x =>
                    EF.Functions.Like(x.OrigineAirport, $"%{filter.Origine}%")
                    || EF.Functions.Like(x.OrigineAirportCode, $"%{filter.Origine}%")
                    || EF.Functions.Like(x.OrigineAirportCityCode, $"%{filter.Origine}%")
                    || EF.Functions.Like(x.OrigineAirportCityName, $"%{filter.Origine}%")
                    || EF.Functions.Like(x.OrigineAirportCountryCode, $"%{filter.Origine}%")
                    || EF.Functions.Like(x.OrigineAirportCountryName, $"%{filter.Origine}%")
                    )
                    .WhereIf(!string.IsNullOrEmpty(filter.Query), x =>
                    EF.Functions.Like(x.CustomerEmail, $"%{filter.Query}%")
                   || EF.Functions.Like(x.UserEmail, $"%{filter.Query}%"));
                if (filter.SortDescending != null && filter.SortDescending.Value)
                    query = query.OrderByDescending(x => x.CreatedOn);
                if (filter.SortAscending != null && filter.SortAscending.Value)
                    query = query.OrderBy(x => x.CreatedOn);
                return await Task.FromResult(_mapper.Map<PaginatedList<QuotationEmailSupport>, PaginatedList<QuotationEmailSupportModal>>(new PaginatedList<QuotationEmailSupport>(query.ToList(), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _quotationEmailSupportRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<QuotationEmailSupport>, PaginatedList<QuotationEmailSupportModal>>(new PaginatedList<QuotationEmailSupport>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<QuotationEmailSupportModal> UpdateCustomerReviewRatings(QuotationEmailSupportModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _quotationEmailSupportRepository.Update(_mapper.Map<QuotationEmailSupportModal, QuotationEmailSupport>(modal));
                if (result.Id > 0)
                {
                    return _mapper.Map<QuotationEmailSupport, QuotationEmailSupportModal>(result);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<QuotationEmailSupportModal> GetById(int Id)
        {
            return _mapper.Map<QuotationEmailSupport, QuotationEmailSupportModal>(await _quotationEmailSupportRepository.Entites().FirstOrDefaultAsync(x => x.Id == Id));

        }
    }
}
