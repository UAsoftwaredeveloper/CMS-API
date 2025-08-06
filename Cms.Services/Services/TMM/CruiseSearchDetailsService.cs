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
    public class CruiseSearchDetailsService : ICruiseSearchDetailsService
    {
        private readonly ICruiseSearchDetailsRepository _cruiseSearchDetailsRepository;
        private readonly IMapper _mapper;
        public CruiseSearchDetailsService(ICruiseSearchDetailsRepository cruiseSearchDetailsRepository, IMapper mapper)
        {
            _cruiseSearchDetailsRepository = cruiseSearchDetailsRepository ?? throw new ArgumentNullException(nameof(cruiseSearchDetailsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<CruiseSearchDetailsModal>> GetAllCruiseSearchDetails(CruiseSearchDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _cruiseSearchDetailsRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PortalID != null, x => x.PortalID == filter.PortalID)
                .WhereIf(filter.FromDate != null, x => x.SearchDate >= filter.FromDate)
                .WhereIf(filter.ToDate != null, x => x.SearchDate <= filter.ToDate)
                .WhereIf(!string.IsNullOrEmpty(filter.cruiseline), x => EF.Functions.Like(x.cruiseline, $"%{filter.cruiseline}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.departure), x => EF.Functions.Like(x.departure, $"%{filter.departure}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.arrival), x => EF.Functions.Like(x.arrival, $"%{filter.arrival}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.duration), x => EF.Functions.Like(x.duration, $"%{filter.duration}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.sea), x => EF.Functions.Like(x.sea, $"%{filter.sea}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.area), x => EF.Functions.Like(x.area, $"%{filter.area}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.ship), x => EF.Functions.Like(x.ship, $"%{filter.ship}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DeviceType), x => EF.Functions.Like(x.DeviceType, $"%{filter.DeviceType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.package), x => EF.Functions.Like(x.package, $"%{filter.package}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x =>
                EF.Functions.Like(x.cruiseline, $"%{filter.Query}%") ||
                EF.Functions.Like(x.departure, $"%{filter.Query}%") ||
                EF.Functions.Like(x.arrival, $"%{filter.Query}%") ||
                EF.Functions.Like(x.duration, $"%{filter.Query}%") ||
                EF.Functions.Like(x.sea, $"%{filter.Query}%") ||
                EF.Functions.Like(x.area, $"%{filter.Query}%") ||
                EF.Functions.Like(x.ship, $"%{filter.Query}%") ||
                EF.Functions.Like(x.DeviceType, $"%{filter.Query}%") ||
                EF.Functions.Like(x.package, $"%{filter.Query}%")).OrderByDescending(x => x.Id);

                return await Task.FromResult(_mapper.Map<PaginatedList<CruiseSearchDetails>, PaginatedList<CruiseSearchDetailsModal>>(new PaginatedList<CruiseSearchDetails>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _cruiseSearchDetailsRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<CruiseSearchDetails>, PaginatedList<CruiseSearchDetailsModal>>(new PaginatedList<CruiseSearchDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }

        public async Task<CruiseSearchDetailsModal> GetById(int Id)
        {
            return _mapper.Map<CruiseSearchDetails, CruiseSearchDetailsModal>(await _cruiseSearchDetailsRepository.Entites().FirstOrDefaultAsync(x => x.Id == Id));

        }
    }
}
