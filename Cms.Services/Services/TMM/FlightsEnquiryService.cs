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
    public class FlightsEnquiryService : IFlightsEnquiryService
    {
        private readonly IFlightsEnquiryRepository _flightsEnquiryRepository;
        private readonly IMapper _mapper;
        public FlightsEnquiryService(IFlightsEnquiryRepository usersRepository, IMapper mapper)
        {
            _flightsEnquiryRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<FlightsEnquiryModal>> GetAllFlightsEnquiry(FlightsEnquiryFilter filter)
        {
            if (filter != null)
            {
                var result =
                _flightsEnquiryRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.ID == filter.Id)
                .WhereIf(filter.PortalID != null, x => x.PortalID == filter.PortalID)
                .WhereIf(filter.FromDate != null, x => x.CreationTime >= filter.FromDate.Value)
                .WhereIf(filter.ToDate != null, x => x.CreationTime <= filter.ToDate.Value)
                .WhereIf(filter.ToDate != null, x => x.CreationTime <= filter.ToDate.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.DepartureDate), x => x.DepDate1 == DateTime.ParseExact(filter.DepartureDate, "MM-dd-yyyy", null, System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd"))
                .WhereIf(!string.IsNullOrEmpty(filter.Email), x => EF.Functions.Like(x.Email, $"%{filter.Email}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => EF.Functions.Like(x.MobileNo, $"%{filter.MobileNo}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CabinType), x => EF.Functions.Like(x.CabinType, $"%{filter.CabinType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Origin), x => EF.Functions.Like(x.Origin1, $"%{filter.Origin}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Destination), x => EF.Functions.Like(x.Destination1, $"%{filter.Destination}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Ref_No), x => EF.Functions.Like(x.Ref_No, $"%{filter.Ref_No}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PageType), x => EF.Functions.Like(x.PageType, $"%{filter.PageType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DeviceType), x => EF.Functions.Like(x.DeviceType, $"%{filter.DeviceType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.TravellerCount), x => EF.Functions.Like(x.TravellerCount, $"%{filter.TravellerCount}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Email, $"%{filter.Query}%")).OrderByDescending(x => x.ID);

                return await Task.FromResult(_mapper.Map<PaginatedList<FlightsEnquiry>, PaginatedList<FlightsEnquiryModal>>(new PaginatedList<FlightsEnquiry>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _flightsEnquiryRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<FlightsEnquiry>, PaginatedList<FlightsEnquiryModal>>(new PaginatedList<FlightsEnquiry>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<FlightsEnquiryModal> GetById(int Id)
        {
            return _mapper.Map<FlightsEnquiry, FlightsEnquiryModal>(await _flightsEnquiryRepository.Entites().FirstOrDefaultAsync(x => x.ID == Id));

        }
    }
}
