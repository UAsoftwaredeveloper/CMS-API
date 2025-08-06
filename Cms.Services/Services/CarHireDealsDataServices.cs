using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CMS.Repositories.Interfaces;
using Cms.Services.Interfaces;
using Cms.Services.Filters;
using Cms.Services.Models.OpenAPIDataModel.HotelDealsData;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using Cms.Services.Models.OpenAPIDataModel.CarHireDealsData;
using Cms.Services.Extensions;

namespace Cms.Services.Services
{
    public class CarHireDealsDataServices : ICarHireDealsDataServices
    {
        private readonly ICarHireDealsRepository _hotelDealsDataRepository;
        private readonly IMapper _mapper;
        private readonly IDataCachingService _dataCachingService;

        public CarHireDealsDataServices(ICarHireDealsRepository hotelDealsRepository, IMapper mapper, IDataCachingService dataCachingService)
        {
            _hotelDealsDataRepository = hotelDealsRepository ?? throw new ArgumentNullException(nameof(hotelDealsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataCachingService = dataCachingService;


        }

        public async Task<List<CarHireDealsData>> GetAllDealManagement(CarHireDealFilter filter)
        {
            //var dealDetailKey = "hotelDealDetail";
            var dealDetailKey = "CarHireDealDetail";
            var hasDatainCache = _dataCachingService.IsKeyAvailable(dealDetailKey);
            if (filter != null && hasDatainCache)
            {
                var response = _dataCachingService.PullDataFromCache<List<CarHireDeals>>(dealDetailKey).AsQueryable();
                var result = response
                 .Where(x => x.From >= DateTime.UtcNow)
                 .WhereIf(filter.Id != null && filter.Id > 0, x => x.Id == filter.Id)
                 .WhereIf(filter.PortalId != null && filter.PortalId > 0, x => x.PortalId == filter.PortalId)
                 .WhereIf(!string.IsNullOrEmpty(filter.PortalName), x => x.Portal.Name.ToLower().Contains(filter.PortalName))
                 .WhereIf(!string.IsNullOrEmpty(filter.PortalCode), x => x.Portal.PortalCode.ToLower().Contains(filter.PortalCode))
                 .WhereIf(!string.IsNullOrEmpty(filter.DealType), x => filter.DealType.ToLower().Contains(x.DealType.ToLower()))
                 .ToList();
                if (result != null && result.Count > 0)
                {
                    return await Task.FromResult(_mapper.Map<List<CarHireDeals>, List<CarHireDealsData>>(result));

                }
                else
                {
                    var data = new List<CarHireDeals>();
                    var filterDBData = _hotelDealsDataRepository.GetAll(deleted: false).Include(x => x.Portal)
                .Where(x => x.From >= DateTime.UtcNow)
                .WhereIf(true, x => x.Active == true)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PortalId > 0, x => x.PortalId == filter.PortalId)
                .WhereIf(!string.IsNullOrEmpty(filter.PortalName), x => EF.Functions.Like(x.Portal.Name, filter.PortalName))
                .WhereIf(!string.IsNullOrEmpty(filter.PortalCode), x => EF.Functions.Like(x.Portal.PortalCode, filter.PortalCode))
                .WhereIf(!string.IsNullOrEmpty(filter.DealType), x => EF.Functions.Like(x.DealType, $"%{filter.DealType}%"))
                .ToList();

                    if (filterDBData.Count > 0)
                    {
                        hasDatainCache = _dataCachingService.PushDataInCache(filterDBData, dealDetailKey);
                    }
                    return await Task.FromResult(_mapper.Map<List<CarHireDeals>, List<CarHireDealsData>>(filterDBData));

                }

            }
            else if (!hasDatainCache && filter != null)
            {
                var flterDBData = _hotelDealsDataRepository.GetAll(deleted: false).Include(x => x.Portal)
                .Where(x => x.From >= DateTime.UtcNow)
                .WhereIf(true, x => x.Active == true)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PortalId > 0, x => x.PortalId == filter.PortalId)
                .WhereIf(!string.IsNullOrEmpty(filter.PortalName), x => x.Portal.Name == filter.PortalName)
                .WhereIf(!string.IsNullOrEmpty(filter.PortalCode), x => x.Portal.PortalCode == filter.PortalCode)
                .WhereIf(!string.IsNullOrEmpty(filter.DealType), x => EF.Functions.Like(x.DealType, $"%{filter.DealType}%"))
                .ToList();
                hasDatainCache = _dataCachingService.PushDataInCache(flterDBData, dealDetailKey);
                return await Task.FromResult(_mapper.Map<List<CarHireDeals>, List<CarHireDealsData>>(flterDBData));
            }
            else
            {
                var flterDBData = _hotelDealsDataRepository.GetAll(deleted: false).Include(x => x.Portal)
                .Where(x => x.From >= DateTime.UtcNow).ToList();
                return await Task.FromResult(_mapper.Map<List<CarHireDeals>, List<CarHireDealsData>>(flterDBData));
            }
        }


    }
}
