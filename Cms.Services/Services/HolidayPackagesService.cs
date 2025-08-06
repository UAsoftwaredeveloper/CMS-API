using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.HolidayPackages;
using CMS.Repositories.Interfaces;
using CMS.Repositories.Repositories;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class HolidayPackagesService : IHolidayPackagesService
    {
        public IHolidayPackagesRepository _holidayPackageRepository;
        public IMapper _mapper;
        public HolidayPackagesService(IHolidayPackagesRepository holidayPackagesRepository, IMapper mapper)
        {
            _holidayPackageRepository = holidayPackagesRepository ?? throw new ArgumentNullException(nameof(holidayPackagesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<HolidayPackagesModal> CreatePackages(HolidayPackagesModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _holidayPackageRepository.Insert(_mapper.Map<HolidayPackagesModal, HolidayPackages>(modal));
                if (result.Id > 0)
                {
                    await _holidayPackageRepository.Insert(_mapper.Map<HolidayPackages_Trails>(result));
                }
                return _mapper.Map<HolidayPackages, HolidayPackagesModal>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<HolidayPackagesModal> UpdatePackages(HolidayPackagesModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _holidayPackageRepository.Update(_mapper.Map<HolidayPackagesModal, HolidayPackages>(modal));
                if (result.Id > 0)
                {
                    await _holidayPackageRepository.Insert(_mapper.Map<HolidayPackages_Trails>(result));
                }
                return _mapper.Map<HolidayPackages, HolidayPackagesModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<HolidayPackagesModal>> GetAllPackages(HolidayPackagesFilter filter)
        {
            if (filter != null)
            {
                var result =
                _holidayPackageRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.Active != null, x => x.Active == filter.Active)
                .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved)
                .WhereIf(filter.Number_of_Nights > 0, x => x.Number_of_Nights == filter.Number_of_Nights)
                .WhereIf(filter.Number_of_Days > 0, x => x.Number_of_Days == filter.Number_of_Days)
                .WhereIf(filter.StarRatings > 0, x => x.StarRatings == filter.StarRatings)
                .WhereIf(!string.IsNullOrEmpty(filter.PortalIds), x => x.PortalIds == filter.PortalIds)
                .WhereIf(!string.IsNullOrEmpty(filter.Url), x => EF.Functions.Like(x.Url, $"%{filter.Url}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Keywords), x => EF.Functions.Like(x.Keywords, $"%{filter.Keywords}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.LocationTitle), x => EF.Functions.Like(x.LocationTitle, $"%{filter.LocationTitle}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Title), x => EF.Functions.Like(x.Title, $"%{filter.Title}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Keywords), x => EF.Functions.Like(x.Keywords, $"%{filter.Keywords}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PackageName), x => EF.Functions.Like(x.PackageName, $"%{filter.PackageName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.OriginCityName), x => EF.Functions.Like(x.OriginCityName, $"%{filter.OriginCityName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DestinationCityName), x => EF.Functions.Like(x.DestinationCityName, $"%{filter.DestinationCityName}%"))
                .WhereIf(filter.Active != null && filter.Active.Value, x => x.Active == filter.Active)
                .WhereIf(filter.Amount != null && filter.Amount.Value > 0, x => x.Amount == filter.Amount.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.PortalIds, $"%{filter.Query}%")
                || EF.Functions.Like(x.PackageName, $"%{filter.Query}%")
                || EF.Functions.Like(x.ReferenceId, $"%{filter.Query}%")
                || EF.Functions.Like(x.Keywords, $"%{filter.Query}%"))
                ;

                return await Task.FromResult(_mapper.Map<PaginatedList<HolidayPackages>, PaginatedList<HolidayPackagesModal>>(new PaginatedList<HolidayPackages>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _holidayPackageRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<HolidayPackages>, PaginatedList<HolidayPackagesModal>>(new PaginatedList<HolidayPackages>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<PaginatedList<HolidayPackages_Trails>> GetAllHolidayPackages_Trails(HolidayPackagesFilter filter)
        {
            if (filter != null)
            {
                var result =
                _holidayPackageRepository.GetAllHolidayPackages_Trails(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.Active != null, x => x.Active == filter.Active)
                .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved)
                .WhereIf(filter.Number_of_Nights > 0, x => x.Number_of_Nights == filter.Number_of_Nights)
                .WhereIf(filter.Number_of_Days > 0, x => x.Number_of_Days == filter.Number_of_Days)
                .WhereIf(filter.StarRatings > 0, x => x.StarRatings == filter.StarRatings)
                .WhereIf(!string.IsNullOrEmpty(filter.PortalIds), x => x.PortalIds == filter.PortalIds)
                .WhereIf(!string.IsNullOrEmpty(filter.Url), x => EF.Functions.Like(x.Url, $"%{filter.Url}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Keywords), x => EF.Functions.Like(x.Keywords, $"%{filter.Keywords}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.LocationTitle), x => EF.Functions.Like(x.LocationTitle, $"%{filter.LocationTitle}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Title), x => EF.Functions.Like(x.Title, $"%{filter.Title}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Keywords), x => EF.Functions.Like(x.Keywords, $"%{filter.Keywords}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PackageName), x => EF.Functions.Like(x.PackageName, $"%{filter.PackageName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.OriginCityName), x => EF.Functions.Like(x.OriginCityName, $"%{filter.OriginCityName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DestinationCityName), x => EF.Functions.Like(x.DestinationCityName, $"%{filter.DestinationCityName}%"))
                .WhereIf(filter.Active != null && filter.Active.Value, x => x.Active == filter.Active)
                .WhereIf(filter.Amount != null && filter.Amount.Value > 0, x => x.Amount == filter.Amount.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.PortalIds, $"%{filter.Query}%")
                || EF.Functions.Like(x.PackageName, $"%{filter.Query}%")
                || EF.Functions.Like(x.Keywords, $"%{filter.Query}%"))
                ;

                return await Task.FromResult(new PaginatedList<HolidayPackages_Trails>(result, filter.PageSize, filter.PageNumber));
            }
            else
            {
                var result =
                _holidayPackageRepository.GetAllHolidayPackages_Trails(deleted: false);
                return await Task.FromResult(new PaginatedList<HolidayPackages_Trails>(result, filter.PageSize, filter.PageNumber));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _holidayPackageRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                await _holidayPackageRepository.Insert(_mapper.Map<HolidayPackages_Trails>(result));
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<HolidayPackagesModal> GetById(int Id)
        {
            return _mapper.Map<HolidayPackages, HolidayPackagesModal>(await _holidayPackageRepository.Get(Id).Result.FirstOrDefaultAsync());
        }
    }
}
