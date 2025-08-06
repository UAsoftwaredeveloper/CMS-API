using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.OpenAPIDataModel.FlightDealManagement;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class FlightDealManagementDataService : IFlightDealManagementDataService
    {
        private readonly IFlightDealManagementRepository _flightDealManagementRepository;
        private readonly IMapper _mapper;
        private readonly IDataCachingService _dataCachingService;

        public FlightDealManagementDataService(IFlightDealManagementRepository flightDealManagementRepository, IMapper mapper, IDataCachingService dataCachingService)
        {
            _flightDealManagementRepository = flightDealManagementRepository ?? throw new ArgumentNullException(nameof(flightDealManagementRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataCachingService = dataCachingService;
        }
        public async Task<List<FlightDealManagementData>> GetAllDealManagement(FlightDealManagementFilter filter)
        {
            var dealDetailKey = "fightDealDetail";
            var hasDatainCache = _dataCachingService.IsKeyAvailable(dealDetailKey);
            if (filter != null && hasDatainCache)
            {
                var response = _dataCachingService.PullDataFromCache<List<FlightDealManagement>>(dealDetailKey).AsQueryable();
                var result = response
                 .Where(x => x.DepartureDate >= DateTime.UtcNow)
                 .WhereIf(filter.Id != null && filter.Id > 0, x => x.Id == filter.Id)
                 .WhereIf(filter.PortalId != null && filter.PortalId > 0, x => x.PortalId == filter.PortalId)
                 .WhereIf(!string.IsNullOrEmpty(filter.PortalName), x => x.Portal.Name.ToLower().Contains(filter.PortalName))
                 .WhereIf(!string.IsNullOrEmpty(filter.PortalCode), x => x.Portal.PortalCode.ToLower().Contains(filter.PortalCode))
                 .WhereIf(!string.IsNullOrEmpty(filter.TripType), x => x.TripType.ToLower().Contains(filter.TripType))
                 .WhereIf(!string.IsNullOrEmpty(filter.ClassType), x => x.ClassType.ToLower().Contains(filter.ClassType))
                 .WhereIf(!string.IsNullOrEmpty(filter.DealType), x => filter.DealType.ToLower().Contains(x.DealType.ToLower()))
                 .ToList();
                if (result != null && result.Count > 0)
                {
                    return await Task.FromResult(_mapper.Map<List<FlightDealManagement>, List<FlightDealManagementData>>(result));

                }
                else
                {
                    var data = new List<FlightDealManagement>();
                    var filterDBData = _flightDealManagementRepository.GetAll(deleted: false).Include(x => x.Portal)
                .Where(x => x.DepartureDate >= DateTime.UtcNow)
                .WhereIf(true, x => x.Active == true)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PortalId > 0, x => x.PortalId == filter.PortalId)
                .WhereIf(!string.IsNullOrEmpty(filter.PortalName), x => EF.Functions.Like(x.Portal.Name, filter.PortalName))
                .WhereIf(!string.IsNullOrEmpty(filter.PortalCode), x => EF.Functions.Like(x.Portal.PortalCode, filter.PortalCode))
                 .WhereIf(!string.IsNullOrEmpty(filter.DealType), x => filter.DealType.ToLower().Contains(x.DealType.ToLower()))
                .ToList();

                    if (filterDBData.Count > 0)
                    {
                        hasDatainCache = _dataCachingService.PushDataInCache(filterDBData, dealDetailKey);
                    }
                    return await Task.FromResult(_mapper.Map<List<FlightDealManagement>, List<FlightDealManagementData>>(filterDBData));

                }

            }
            else if (!hasDatainCache && filter != null)
            {
                var flterDBData = _flightDealManagementRepository.GetAll(deleted: false).Include(x => x.Portal)
                .WhereIf(true, x => x.Active == true)
                .Where(x => x.DepartureDate >= DateTime.UtcNow)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PortalId > 0, x => x.PortalId == filter.PortalId)
                .WhereIf(!string.IsNullOrEmpty(filter.PortalName), x => x.Portal.Name == filter.PortalName)
                .WhereIf(!string.IsNullOrEmpty(filter.PortalCode), x => x.Portal.PortalCode == filter.PortalCode)
                .WhereIf(!string.IsNullOrEmpty(filter.TripType), x => x.TripType == filter.TripType)
                .WhereIf(!string.IsNullOrEmpty(filter.ClassType), x => x.ClassType == filter.ClassType)
                 .WhereIf(!string.IsNullOrEmpty(filter.DealType), x => filter.DealType.ToLower().Contains(x.DealType.ToLower()))
                .ToList();
                if (flterDBData != null && flterDBData.Count > 0)
                    hasDatainCache = _dataCachingService.PushDataInCache(flterDBData, dealDetailKey);
                return await Task.FromResult(_mapper.Map<List<FlightDealManagement>, List<FlightDealManagementData>>(flterDBData));
            }
            else
            {
                var flterDBData = _flightDealManagementRepository.GetAll(deleted: false).Include(x => x.Portal).Where(x => x.DepartureDate >= DateTime.UtcNow).ToList();
                return await Task.FromResult(_mapper.Map<List<FlightDealManagement>, List<FlightDealManagementData>>(flterDBData));
            }
        }
    }
}
