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
    public class GroupTravelFlightEnqueryDetailsService : IGroupTravelFlightEnqueryDetailsService
    {
        private readonly IGroupTravelFlightEnqueryDetailsRepository _flightSearchDetailsRepository;
        private readonly IMapper _mapper;
        public GroupTravelFlightEnqueryDetailsService(IGroupTravelFlightEnqueryDetailsRepository flightSearchDetailsRepository, IMapper mapper)
        {
            _flightSearchDetailsRepository = flightSearchDetailsRepository ?? throw new ArgumentNullException(nameof(flightSearchDetailsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<GroupTravelFlightEnqueryDetailsModal>> GetAllGroupTravelFlightEnqueryDetails(GroupTravelFlightEnqueryDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _flightSearchDetailsRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.TripType != null, x => x.TripType == filter.TripType)
                .WhereIf(filter.PortalID != null, x => x.PortalID == filter.PortalID)
                .WhereIf(filter.DepartDateFrom != null, x => x.DepartDate >= filter.DepartDateFrom)
                .WhereIf(filter.DepartDateTo != null, x => x.DepartDate <= filter.DepartDateTo)
                .WhereIf(filter.ReturnDateFrom != null, x => x.ReturnDate >= filter.ReturnDateFrom)
                .WhereIf(filter.ReturnDateTo != null, x => x.ReturnDate <= filter.ReturnDateTo)
                .WhereIf(filter.FromDate != null, x => x.SearchDate >= filter.FromDate)
                .WhereIf(filter.ToDate != null, x => x.SearchDate <= filter.ToDate)
                .WhereIf(!string.IsNullOrEmpty(filter.CabinClass), x => x.CabinClass == filter.CabinClass)
                .WhereIf(!string.IsNullOrEmpty(filter.FullName), x =>EF.Functions.Like($"{x.FirstName} {x.LastName}",$"%{filter.FullName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.EmailId), x => EF.Functions.Like(x.EmailId,$"%{filter.EmailId}"))
                .WhereIf(!string.IsNullOrEmpty(filter.Depart), x => x.Depart == filter.Depart)
                .WhereIf(!string.IsNullOrEmpty(filter.Depart), x => x.Depart == filter.Depart)
                .WhereIf(!string.IsNullOrEmpty(filter.Return), x => x.Return == filter.Return)
                .WhereIf(!string.IsNullOrEmpty(filter.UTM_Source), x => x.UTM_Source == filter.UTM_Source)
                .WhereIf(!string.IsNullOrEmpty(filter.DeviceType), x => x.DeviceType == filter.DeviceType)
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x =>
                EF.Functions.Like(x.CabinClass, $"%{filter.Query}%") ||
                EF.Functions.Like(x.Return, $"%{filter.Query}%") ||
                EF.Functions.Like(x.Depart, $"%{filter.Query}%") ||
                EF.Functions.Like(x.DeviceType, $"%{filter.Query}%") ||
                EF.Functions.Like(x.UTM_Source, $"%{filter.Query}%") ||
                EF.Functions.Like(x.Query, $"%{filter.Query}%") ||
                EF.Functions.Like(x.CabinClass, $"%{filter.Query}%")).OrderByDescending(x=>x.Id);

                return await Task.FromResult(_mapper.Map<PaginatedList<GroupTravelFlightEnqueryDetails>, PaginatedList<GroupTravelFlightEnqueryDetailsModal>>(new PaginatedList<GroupTravelFlightEnqueryDetails>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _flightSearchDetailsRepository.GetAll(deleted: false).OrderByDescending(x=>x.Id);
                return await Task.FromResult(_mapper.Map<PaginatedList<GroupTravelFlightEnqueryDetails>, PaginatedList<GroupTravelFlightEnqueryDetailsModal>>(new PaginatedList<GroupTravelFlightEnqueryDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<GroupTravelFlightEnqueryDetailsModal> GetById(int Id)
        {
            return _mapper.Map<GroupTravelFlightEnqueryDetails, GroupTravelFlightEnqueryDetailsModal>(await _flightSearchDetailsRepository.Entites().FirstOrDefaultAsync(x => x.Id == Id));

        }
    }
}
