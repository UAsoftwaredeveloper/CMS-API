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
    public class DynamicDestinationEnquiryService : IDynamicDestinationEnquiryService
    {
        private readonly IDynamicDestinationEnquiryRepository _flightSearchDetailsRepository;
        private readonly IMapper _mapper;
        public DynamicDestinationEnquiryService(IDynamicDestinationEnquiryRepository flightSearchDetailsRepository, IMapper mapper)
        {
            _flightSearchDetailsRepository = flightSearchDetailsRepository ?? throw new ArgumentNullException(nameof(flightSearchDetailsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<DynamicDestinationEnquiryModal>> GetAllDynamicDestinationEnquiry(DynamicDestinationEnquiryFilter filter)
        {
            if (filter != null)
            {
                var result =
                _flightSearchDetailsRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PortalId != null, x => x.PortalId == filter.PortalId)
                .WhereIf(filter.FromDate != null, x => x.CreatedOn >= filter.FromDate)
                .WhereIf(filter.ToDate != null, x => x.CreatedOn <= filter.ToDate)
                .WhereIf(!string.IsNullOrEmpty(filter.CustomerName), x =>EF.Functions.Like($"{x.FirstName} {x.LastName}",$"%{filter.CustomerName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Email), x => EF.Functions.Like(x.Email,$"%{filter.Email}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Phone), x => x.PhoneNo == filter.Phone)
                .WhereIf(!string.IsNullOrEmpty(filter.RefrenceId), x => x.RefrenceId == filter.RefrenceId)
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x =>
                EF.Functions.Like($"{x.FirstName} {x.LastName}", $"%{filter.Query}%") ||
                EF.Functions.Like(x.Email, $"%{filter.Query}%") ||
                EF.Functions.Like(x.PhoneNo, $"%{filter.Query}%") ||
                EF.Functions.Like(x.RefrenceId, $"%{filter.Query}%")).OrderByDescending(x=>x.Id);

                return await Task.FromResult(_mapper.Map<PaginatedList<DynamicDestinationEnquiry>, PaginatedList<DynamicDestinationEnquiryModal>>(new PaginatedList<DynamicDestinationEnquiry>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _flightSearchDetailsRepository.GetAll(deleted: false).OrderByDescending(x=>x.Id);
                return await Task.FromResult(_mapper.Map<PaginatedList<DynamicDestinationEnquiry>, PaginatedList<DynamicDestinationEnquiryModal>>(new PaginatedList<DynamicDestinationEnquiry>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<DynamicDestinationEnquiryModal> GetById(int Id)
        {
            return _mapper.Map<DynamicDestinationEnquiry, DynamicDestinationEnquiryModal>(await _flightSearchDetailsRepository.Entites().FirstOrDefaultAsync(x => x.Id == Id));

        }
    }
}
