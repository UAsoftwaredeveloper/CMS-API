using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.OpenAPIDataModel.HolidayPackages;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class HolidayPackagesDataService : IHolidayPackagesDataService
    {
        private readonly IHolidayPackagesRepository _HolidayPackagesDataRepository;
        private readonly IPackageItenariesRepository _PackageItenariesDataRepository;
        private readonly IMapper _mapper;
        private readonly IDataCachingService _dataCachingService;

        public HolidayPackagesDataService(IHolidayPackagesRepository HolidayPackagesRepository, IPackageItenariesRepository PackageItenaries, IMapper mapper, IDataCachingService dataCachingService)
        {
            _HolidayPackagesDataRepository = HolidayPackagesRepository ?? throw new ArgumentNullException(nameof(HolidayPackagesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dataCachingService = dataCachingService;
            _PackageItenariesDataRepository = PackageItenaries ?? throw new ArgumentNullException(nameof(PackageItenaries));
        }
        public async Task<List<HolidayPackagesData>> GetAll(HolidayPackagesFilter filter)
        {
            var dealDetailKey = GenerateShortCacheKey("holidayPackageDetails", filter);
            if (filter != null)
            {

                string portalId = string.IsNullOrEmpty(filter.PortalIds) || filter.PortalIds.Contains("2") ? "0" : filter.PortalIds;
                if (!string.IsNullOrEmpty(filter.DestinationCityName))
                {
                    dealDetailKey = string.Format(dealDetailKey, portalId, filter.DestinationCityName);
                }
                else if (!string.IsNullOrEmpty(filter.CountryName))
                {
                    dealDetailKey = string.Format(dealDetailKey, portalId, filter.CountryName);
                }
                else if (!string.IsNullOrEmpty(filter.Keywords))
                {
                    dealDetailKey = string.Format(dealDetailKey, portalId, "keywords:" + filter.Keywords);
                }
                else
                {
                    dealDetailKey = string.Format(dealDetailKey, portalId, "All");

                }

            }
            var hasDatainCache = _dataCachingService.IsKeyAvailable(dealDetailKey);
            if (filter != null && hasDatainCache)
            {
                var response = _dataCachingService.PullDataFromCache<List<HolidayPackages>>(dealDetailKey).AsQueryable();
                var result = response
                 .Where(x => x.ValidTo.Value.Date >= DateTime.Now.Date)
                 .WhereIf(filter.ShowOnHomePage != null && filter.ShowOnHomePage.Value, x => x.ShowOnHomePage == filter.ShowOnHomePage)
                 .WhereIf(filter.Approved != null && filter.Approved.Value, x => x.Approved == filter.Approved)
                 .WhereIf(filter.Id != null && filter.Id > 0, x => x.Id == filter.Id)
                 .WhereIf(filter.PortalIds != null, x => x.PortalIds.Split(',', ' ').Contains("2") || x.PortalIds == filter.PortalIds)
                 .WhereIf(!string.IsNullOrEmpty(filter.PackageName), x => x.PackageName.Contains(filter.PackageName))
                 .WhereIf(!string.IsNullOrEmpty(filter.Theme), x => x.PackageTheme.ToLower().Contains(filter.Theme.ToLower()) == true)
                 .WhereIf(!string.IsNullOrEmpty(filter.OriginCityName), x => x.OriginCityName == filter.OriginCityName)
                 .WhereIf(!string.IsNullOrEmpty(filter.DestinationCityName), x => filter.DestinationCityName.ToLower().Contains(x.DestinationCityName) || x.DestinationCountryName.ToLower().Contains(filter.DestinationCityName))
                 .WhereIf(!string.IsNullOrEmpty(filter.CountryName), x => filter.CountryName.ToLower().Contains(x.DestinationCountryName) || x.DestinationCountryName.ToLower().Contains(filter.CountryName))
                .WhereIf(!string.IsNullOrEmpty(filter.Keywords), x => x.Keywords.Split(',', ' ').Any(xx => xx.ToLower() == filter.Keywords.ToLower()))
                .WhereIf(filter.Number_of_Nights > 0, x => x.Number_of_Nights == filter.Number_of_Nights)
                .WhereIf(filter.Number_of_Days > 0, x => x.Number_of_Days == filter.Number_of_Days).Take(filter.PageSize)
                 .ToList();
                if (result != null && result.Count > 0)
                {
                    return await Task.FromResult(_mapper.Map<List<HolidayPackages>, List<HolidayPackagesData>>(result));

                }
                else
                {
                    var data = new List<HolidayPackages>();
                    var filterDBData = _HolidayPackagesDataRepository.GetAll(deleted: false)
                .Where(x => x.ValidTo.Value.Date >= DateTime.Now.Date)
                .WhereIf(true, x => x.Active == true)
                .WhereIf(filter.ShowOnHomePage != null && filter.ShowOnHomePage.Value, x => x.ShowOnHomePage == filter.ShowOnHomePage)
                .WhereIf(filter.Approved != null && filter.Approved.Value, x => x.Approved == filter.Approved)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PortalIds != null, x => EF.Functions.Like(x.PortalIds, "2") || x.PortalIds == filter.PortalIds)
                .WhereIf(!string.IsNullOrEmpty(filter.PackageName), x => EF.Functions.Like(x.PackageName, filter.PackageName))
                .WhereIf(!string.IsNullOrEmpty(filter.Theme), x => EF.Functions.Like(x.PackageTheme.ToLower(), $"%{filter.Theme.ToLower()}%") == true)
                .WhereIf(!string.IsNullOrEmpty(filter.OriginCityName), x => EF.Functions.Like(x.OriginCityName, filter.OriginCityName))
                .WhereIf(!string.IsNullOrEmpty(filter.DestinationCityName), x => EF.Functions.Like(x.DestinationCityName, $"%{filter.DestinationCityName}%") || EF.Functions.Like(x.DestinationCountryName, $"%{filter.DestinationCityName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CountryName), x => EF.Functions.Like(x.DestinationCountryName, $"{filter.CountryName}") || EF.Functions.Like(x.DestinationCityName, $"%{filter.CountryName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Keywords), x => x.Keywords.Split(',', ' ').Any(xx => xx.ToLower() == filter.Keywords.ToLower()))
                .WhereIf(filter.Number_of_Nights > 0, x => x.Number_of_Nights == filter.Number_of_Nights)
                .WhereIf(filter.Number_of_Days > 0, x => x.Number_of_Days == filter.Number_of_Days).Take(filter.PageSize)
                .ToList();
                    if (filterDBData.Count > 0)
                    {
                        filterDBData.ForEach(item =>
                        {
                            if (item.Id > 0)
                            {
                                var itenaryQuery = _PackageItenariesDataRepository.GetAll(false).Where(x => x.PackageId == item.Id && x.Active != null && x.Active == true);
                                item.PackageItenaries = itenaryQuery.ToList();
                            }
                        });
                        result.AddRange(filterDBData);
                        if (DependancyRegistrar.UseCacheForHolidayPackages)
                            hasDatainCache = _dataCachingService.PushDataInCache(result, dealDetailKey);
                    }
                    return await Task.FromResult(_mapper.Map<List<HolidayPackages>, List<HolidayPackagesData>>(filterDBData));

                }

            }
            else if (!hasDatainCache && filter != null)
            {
                var portalIds = filter.PortalIds.Split(',');
                if (filter.PortalIds.Contains("2") || filter.PortalIds.Contains("0"))
                {
                    portalIds = null;
                }
                var filterDBData = _HolidayPackagesDataRepository.GetAll(deleted: false)
                    .Where(x => x.ValidTo.Value.Date >= DateTime.Now.Date)
                    .Where(x => x.Active == true)
                    .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                    .WhereIf(filter.ShowOnHomePage != null && filter.ShowOnHomePage.Value, x => x.ShowOnHomePage == filter.ShowOnHomePage)
                    .WhereIf(filter.Approved != null && filter.Approved.Value, x => x.Approved == filter.Approved)
                    .WhereIf(!string.IsNullOrEmpty(filter.PackageName), x => EF.Functions.Like(x.PackageName, filter.PackageName))
                    .WhereIf(!string.IsNullOrEmpty(filter.Theme), x => EF.Functions.Like(x.PackageTheme.ToLower(), $"%{filter.Theme.ToLower()}%") == true)
                    .WhereIf(!string.IsNullOrEmpty(filter.OriginCityName), x => EF.Functions.Like(x.OriginCityName, filter.OriginCityName))
                    .WhereIf(!string.IsNullOrEmpty(filter.DestinationCityName), x => EF.Functions.Like(x.DestinationCityName, $"%{filter.DestinationCityName}%") || EF.Functions.Like(x.DestinationCountryName, $"%{filter.DestinationCityName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CountryName), x => EF.Functions.Like(x.DestinationCountryName, $"{filter.CountryName}") || EF.Functions.Like(x.DestinationCityName, $"%{filter.CountryName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.Keywords), x => EF.Functions.Like(x.Keywords, $"%{filter.Keywords}%"))
                    .WhereIf(filter.Number_of_Nights > 0, x => x.Number_of_Nights == filter.Number_of_Nights)
                    .WhereIf(filter.Number_of_Days > 0, x => x.Number_of_Days == filter.Number_of_Days).Take(filter.PageSize)
                    .ToList();
                if (portalIds != null)
                {
                    // Perform in-memory filtering for portalIds
                    filterDBData = filterDBData
                        .Where(x => portalIds.Any(id => x.PortalIds.Split(',').Contains(id)))
                        .ToList();
                }
                if (filterDBData.Count > 0)
                {
                    filterDBData.ForEach(item =>
                    {
                        if (item.Id > 0)
                        {
                            var itenaryQuery = _PackageItenariesDataRepository.GetAll(false).Where(x => x.PackageId == item.Id && x.Active != null && x.Active == true);
                            item.PackageItenaries = itenaryQuery.ToList();
                        }
                    });
                }
                if (DependancyRegistrar.UseCacheForHolidayPackages)
                    hasDatainCache = _dataCachingService.PushDataInCache(filterDBData, dealDetailKey);
                return await Task.FromResult(_mapper.Map<List<HolidayPackages>, List<HolidayPackagesData>>(filterDBData));
            }
            else
            {
                var filterDBData = _HolidayPackagesDataRepository.GetAll(deleted: false)
                .WhereIf(filter.ShowOnHomePage != null && filter.ShowOnHomePage.Value, x => x.ShowOnHomePage == filter.ShowOnHomePage)
                .Where(x => x.ValidFrom >= DateTime.UtcNow).ToList();
                if (filterDBData.Count > 0)
                {
                    filterDBData.ForEach(item =>
                    {
                        if (item.Id > 0)
                        {
                            var itenaryQuery = _PackageItenariesDataRepository.GetAll(false).Where(x => x.PackageId == item.Id && x.Active != null && x.Active == true);
                            item.PackageItenaries = itenaryQuery.ToList();
                        }
                    });
                }
                return await Task.FromResult(_mapper.Map<List<HolidayPackages>, List<HolidayPackagesData>>(filterDBData));
            }
        }
        public async Task<List<HolidayPackagesData>> GetThemeSpecificAll(HolidayPackagesFilter filter)
        {
            var dealDetailKey = GenerateShortCacheKey("holidayPackageDetails", filter);
            if (filter != null)
            {

                string portalId = string.IsNullOrEmpty(filter.PortalIds) || filter.PortalIds.Contains("2") ? "0" : filter.PortalIds;
                if (!string.IsNullOrEmpty(filter.DestinationCityName))
                {
                    dealDetailKey = string.Format(dealDetailKey, portalId, filter.DestinationCityName);
                }
                else if (!string.IsNullOrEmpty(filter.CountryName))
                {
                    dealDetailKey = string.Format(dealDetailKey, portalId, filter.CountryName);
                }
                else if (!string.IsNullOrEmpty(filter.Keywords))
                {
                    dealDetailKey = string.Format(dealDetailKey, portalId, "keywords:" + filter.Keywords);
                }
                else
                {
                    dealDetailKey = string.Format(dealDetailKey, portalId, "All");

                }

            }
            var hasDatainCache = _dataCachingService.IsKeyAvailable(dealDetailKey);
            if (filter != null && hasDatainCache)
            {
                var response = _dataCachingService.PullDataFromCache<List<HolidayPackages>>(dealDetailKey).AsQueryable();
                var result = response
                 .Where(x => x.ValidTo.Value.Date >= DateTime.Now.Date)
                 .WhereIf(filter.ShowOnHomePage != null && filter.ShowOnHomePage.Value, x => x.ShowOnHomePage == filter.ShowOnHomePage)
                 .WhereIf(filter.Approved != null && filter.Approved.Value, x => x.Approved == filter.Approved)
                 .WhereIf(filter.Id != null && filter.Id > 0, x => x.Id == filter.Id)
                 .WhereIf(filter.PortalIds != null, x => x.PortalIds.Split(',', ' ').Contains("2") || x.PortalIds == filter.PortalIds)
                 .WhereIf(!string.IsNullOrEmpty(filter.PackageName), x => x.PackageName.Contains(filter.PackageName))
                 .WhereIf(!string.IsNullOrEmpty(filter.Theme), x => x.PackageTheme.ToLower().StartsWith(filter.Theme.ToLower()) == true)
                 .WhereIf(!string.IsNullOrEmpty(filter.OriginCityName), x => x.OriginCityName == filter.OriginCityName)
                 .WhereIf(!string.IsNullOrEmpty(filter.DestinationCityName), x => filter.DestinationCityName.ToLower().Contains(x.DestinationCityName) || x.DestinationCountryName.ToLower().Contains(filter.DestinationCityName))
                 .WhereIf(!string.IsNullOrEmpty(filter.CountryName), x => filter.CountryName.ToLower().Contains(x.DestinationCountryName) || x.DestinationCountryName.ToLower().Contains(filter.CountryName))
                .WhereIf(!string.IsNullOrEmpty(filter.Keywords), x => x.Keywords.Split(',', ' ').Any(xx => xx.ToLower() == filter.Keywords.ToLower()))
                .WhereIf(filter.Number_of_Nights > 0, x => x.Number_of_Nights == filter.Number_of_Nights)
                .WhereIf(filter.Number_of_Days > 0, x => x.Number_of_Days == filter.Number_of_Days).Take(filter.PageSize)
                 .ToList();
                if (result == null || result.Count < filter.MinPrimaryThemeQuantity)
                {
                    result = response
                                     .Where(x => x.ValidTo.Value.Date >= DateTime.Now.Date)
                                     .WhereIf(filter.ShowOnHomePage != null && filter.ShowOnHomePage.Value, x => x.ShowOnHomePage == filter.ShowOnHomePage)
                                     .WhereIf(filter.Approved != null && filter.Approved.Value, x => x.Approved == filter.Approved)
                                     .WhereIf(filter.Id != null && filter.Id > 0, x => x.Id == filter.Id)
                                     .WhereIf(filter.PortalIds != null, x => x.PortalIds.Split(',', ' ').Contains("2") || x.PortalIds == filter.PortalIds)
                                     .WhereIf(!string.IsNullOrEmpty(filter.PackageName), x => x.PackageName.Contains(filter.PackageName))
                                     .WhereIf(!string.IsNullOrEmpty(filter.Theme), x => x.PackageTheme.ToLower().Contains(filter.Theme.ToLower()) == true)
                                     .WhereIf(!string.IsNullOrEmpty(filter.OriginCityName), x => x.OriginCityName == filter.OriginCityName)
                                     .WhereIf(!string.IsNullOrEmpty(filter.DestinationCityName), x => filter.DestinationCityName.ToLower().Contains(x.DestinationCityName) || x.DestinationCountryName.ToLower().Contains(filter.DestinationCityName))
                                     .WhereIf(!string.IsNullOrEmpty(filter.CountryName), x => filter.CountryName.ToLower().Contains(x.DestinationCountryName) || x.DestinationCountryName.ToLower().Contains(filter.CountryName))
                                    .WhereIf(!string.IsNullOrEmpty(filter.Keywords), x => x.Keywords.Split(',', ' ').Any(xx => xx.ToLower() == filter.Keywords.ToLower()))
                                    .WhereIf(filter.Number_of_Nights > 0, x => x.Number_of_Nights == filter.Number_of_Nights)
                                    .WhereIf(filter.Number_of_Days > 0, x => x.Number_of_Days == filter.Number_of_Days).Take(filter.PageSize)
                                     .ToList();
                }
                if (result != null && result.Count > 0)
                {
                    return await Task.FromResult(_mapper.Map<List<HolidayPackages>, List<HolidayPackagesData>>(result));

                }
                else
                {
                    var data = new List<HolidayPackages>();
                    var filterDBData = _HolidayPackagesDataRepository.GetAll(deleted: false)
                .Where(x => x.ValidTo.Value.Date >= DateTime.Now.Date)
                .WhereIf(true, x => x.Active == true)
                .WhereIf(filter.ShowOnHomePage != null && filter.ShowOnHomePage.Value, x => x.ShowOnHomePage == filter.ShowOnHomePage)
                .WhereIf(filter.Approved != null && filter.Approved.Value, x => x.Approved == filter.Approved)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PortalIds != null, x => EF.Functions.Like(x.PortalIds, "2") || x.PortalIds == filter.PortalIds)
                .WhereIf(!string.IsNullOrEmpty(filter.PackageName), x => EF.Functions.Like(x.PackageName, filter.PackageName))
                .WhereIf(!string.IsNullOrEmpty(filter.Theme), x => EF.Functions.Like(x.PackageTheme.ToLower(), $"{filter.Theme.ToLower()}%") == true)
                .WhereIf(!string.IsNullOrEmpty(filter.OriginCityName), x => EF.Functions.Like(x.OriginCityName, filter.OriginCityName))
                .WhereIf(!string.IsNullOrEmpty(filter.DestinationCityName), x => EF.Functions.Like(x.DestinationCityName, $"%{filter.DestinationCityName}%") || EF.Functions.Like(x.DestinationCountryName, $"%{filter.DestinationCityName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.CountryName), x => EF.Functions.Like(x.DestinationCountryName, $"{filter.CountryName}") || EF.Functions.Like(x.DestinationCityName, $"%{filter.CountryName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Keywords), x => x.Keywords.Split(',', ' ').Any(xx => xx.ToLower() == filter.Keywords.ToLower()))
                .WhereIf(filter.Number_of_Nights > 0, x => x.Number_of_Nights == filter.Number_of_Nights)
                .WhereIf(filter.Number_of_Days > 0, x => x.Number_of_Days == filter.Number_of_Days).Take(filter.PageSize)
                .ToList();
                    if (filterDBData == null || filterDBData.Count < filter.MinPrimaryThemeQuantity)
                    {
                        filterDBData = _HolidayPackagesDataRepository.GetAll(deleted: false)
                        .Where(x => x.ValidTo.Value.Date >= DateTime.Now.Date)
                        .WhereIf(true, x => x.Active == true)
                        .WhereIf(filter.Approved != null && filter.Approved.Value, x => x.Approved == filter.Approved)
                        .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                        .WhereIf(filter.PortalIds != null, x => EF.Functions.Like(x.PortalIds, "2") || x.PortalIds == filter.PortalIds)
                        .WhereIf(!string.IsNullOrEmpty(filter.PackageName), x => EF.Functions.Like(x.PackageName, filter.PackageName))
                        .WhereIf(!string.IsNullOrEmpty(filter.Theme), x => EF.Functions.Like(x.PackageTheme.ToLower(), $"%{filter.Theme.ToLower()}%") == true)
                        .WhereIf(!string.IsNullOrEmpty(filter.OriginCityName), x => EF.Functions.Like(x.OriginCityName, filter.OriginCityName))
                        .WhereIf(!string.IsNullOrEmpty(filter.DestinationCityName), x => EF.Functions.Like(x.DestinationCityName, $"%{filter.DestinationCityName}%") || EF.Functions.Like(x.DestinationCountryName, $"%{filter.DestinationCityName}%"))
                        .WhereIf(!string.IsNullOrEmpty(filter.CountryName), x => EF.Functions.Like(x.DestinationCountryName, $"{filter.CountryName}") || EF.Functions.Like(x.DestinationCityName, $"%{filter.CountryName}%"))
                        .WhereIf(!string.IsNullOrEmpty(filter.Keywords), x => x.Keywords.Split(',', ' ').Any(xx => xx.ToLower() == filter.Keywords.ToLower()))
                        .WhereIf(filter.Number_of_Nights > 0, x => x.Number_of_Nights == filter.Number_of_Nights)
                        .WhereIf(filter.Number_of_Days > 0, x => x.Number_of_Days == filter.Number_of_Days)
                        .ToList();
                    }
                    if (filterDBData.Count > 0)
                    {
                        filterDBData.ForEach(item =>
                        {
                            if (item.Id > 0)
                            {
                                var itenaryQuery = _PackageItenariesDataRepository.GetAll(false).Where(x => x.PackageId == item.Id && x.Active != null && x.Active == true);
                                item.PackageItenaries = itenaryQuery.ToList();
                            }
                        });
                        result.AddRange(filterDBData);
                        if (DependancyRegistrar.UseCacheForHolidayPackages)
                            hasDatainCache = _dataCachingService.PushDataInCache(result, dealDetailKey);
                    }
                    return await Task.FromResult(_mapper.Map<List<HolidayPackages>, List<HolidayPackagesData>>(filterDBData));

                }

            }
            else if (!hasDatainCache && filter != null)
            {
                var portalIds = filter.PortalIds?.Split(',');
                if (filter.PortalIds.Contains("2") || filter.PortalIds.Contains("0"))
                {
                    portalIds = null;
                }
                var filterDBData = _HolidayPackagesDataRepository.GetAll(deleted: false)
                    .Where(x => x.ValidTo.Value.Date >= DateTime.Now.Date)
                    .Where(x => x.Active == true)
                    .WhereIf(filter.ShowOnHomePage != null && filter.ShowOnHomePage.Value, x => x.ShowOnHomePage == filter.ShowOnHomePage)
                    .WhereIf(filter.Approved != null && filter.Approved.Value, x => x.Approved == filter.Approved)
                    .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                    .WhereIf(filter.PortalIds != null, x => EF.Functions.Like(x.PortalIds, "2") || x.PortalIds == filter.PortalIds)
                    .WhereIf(!string.IsNullOrEmpty(filter.PackageName), x => EF.Functions.Like(x.PackageName, filter.PackageName))
                    .WhereIf(!string.IsNullOrEmpty(filter.Theme), x => EF.Functions.Like(x.PackageTheme.ToLower(), $"{filter.Theme.ToLower()}%") == true)
                    .WhereIf(!string.IsNullOrEmpty(filter.OriginCityName), x => EF.Functions.Like(x.OriginCityName, filter.OriginCityName))
                    .WhereIf(!string.IsNullOrEmpty(filter.DestinationCityName), x => EF.Functions.Like(x.DestinationCityName, $"%{filter.DestinationCityName}%") || EF.Functions.Like(x.DestinationCountryName, $"%{filter.DestinationCityName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CountryName), x => EF.Functions.Like(x.DestinationCountryName, $"{filter.CountryName}") || EF.Functions.Like(x.DestinationCityName, $"%{filter.CountryName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.Keywords), x => EF.Functions.Like(x.Keywords, $"%{filter.Keywords}%"))
                    .WhereIf(filter.Number_of_Nights > 0, x => x.Number_of_Nights == filter.Number_of_Nights)
                    .WhereIf(filter.Number_of_Days > 0, x => x.Number_of_Days == filter.Number_of_Days).Take(filter.PageSize)
                    .ToList();
                if (filterDBData == null || filterDBData.Count < filter.MinPrimaryThemeQuantity)
                {
                    filterDBData = _HolidayPackagesDataRepository.GetAll(deleted: false)
                    .Where(x => x.ValidTo.Value.Date >= DateTime.Now.Date)
                    .WhereIf(true, x => x.Active == true)
                    .WhereIf(filter.ShowOnHomePage != null && filter.ShowOnHomePage.Value, x => x.ShowOnHomePage == filter.ShowOnHomePage)
                    .WhereIf(filter.Approved != null && filter.Approved.Value, x => x.Approved == filter.Approved)
                    .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                    .WhereIf(filter.PortalIds != null, x => EF.Functions.Like(x.PortalIds, "2") || x.PortalIds == filter.PortalIds)
                    .WhereIf(!string.IsNullOrEmpty(filter.PackageName), x => EF.Functions.Like(x.PackageName, filter.PackageName))
                    .WhereIf(!string.IsNullOrEmpty(filter.Theme), x => EF.Functions.Like(x.PackageTheme.ToLower(), $"%{filter.Theme.ToLower()}%") == true)
                    .WhereIf(!string.IsNullOrEmpty(filter.OriginCityName), x => EF.Functions.Like(x.OriginCityName, filter.OriginCityName))
                    .WhereIf(!string.IsNullOrEmpty(filter.DestinationCityName), x => EF.Functions.Like(x.DestinationCityName, $"%{filter.DestinationCityName}%") || EF.Functions.Like(x.DestinationCountryName, $"%{filter.DestinationCityName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.CountryName), x => EF.Functions.Like(x.DestinationCountryName, $"{filter.CountryName}") || EF.Functions.Like(x.DestinationCityName, $"%{filter.CountryName}%"))
                    .WhereIf(!string.IsNullOrEmpty(filter.Keywords), x => x.Keywords.Split(',', ' ').Any(xx => xx.ToLower() == filter.Keywords.ToLower()))
                    .WhereIf(filter.Number_of_Nights > 0, x => x.Number_of_Nights == filter.Number_of_Nights)
                    .WhereIf(filter.Number_of_Days > 0, x => x.Number_of_Days == filter.Number_of_Days).Take(filter.PageSize)
                    .ToList();
                }
                if (portalIds != null)
                {
                    // Perform in-memory filtering for portalIds
                    filterDBData = filterDBData
                        .Where(x => portalIds.Any(id => x.PortalIds.Split(',').Contains(id)))
                        .ToList();
                }
                if (filterDBData.Count > 0)
                {
                    filterDBData.ForEach(item =>
                    {
                        if (item.Id > 0)
                        {
                            var itenaryQuery = _PackageItenariesDataRepository.GetAll(false).Where(x => x.PackageId == item.Id && x.Active != null && x.Active == true);
                            item.PackageItenaries = itenaryQuery.ToList();
                        }
                    });
                }
                if (DependancyRegistrar.UseCacheForHolidayPackages)
                    hasDatainCache = _dataCachingService.PushDataInCache(filterDBData, dealDetailKey);
                return await Task.FromResult(_mapper.Map<List<HolidayPackages>, List<HolidayPackagesData>>(filterDBData));
            }
            else
            {
                var filterDBData = _HolidayPackagesDataRepository.GetAll(deleted: false)
                .WhereIf(filter.ShowOnHomePage != null && filter.ShowOnHomePage.Value, x => x.ShowOnHomePage == filter.ShowOnHomePage)
                .Where(x => x.ValidFrom >= DateTime.UtcNow).ToList();
                if (filterDBData.Count > 0)
                {
                    filterDBData.ForEach(item =>
                    {
                        if (item.Id > 0)
                        {
                            var itenaryQuery = _PackageItenariesDataRepository.GetAll(false).Where(x => x.PackageId == item.Id && x.Active != null && x.Active == true);
                            item.PackageItenaries = itenaryQuery.ToList();
                        }
                    });
                }
                return await Task.FromResult(_mapper.Map<List<HolidayPackages>, List<HolidayPackagesData>>(filterDBData));
            }
        }
        public async Task<HolidayPackagesData> GetPackageByUrlAll(HolidayPackagesFilter filter)
        {
            var dealDetailKey = GenerateShortCacheKey("holidayPackageDetailsSingle", filter);
            var hasDatainCache = _dataCachingService.IsKeyAvailable(dealDetailKey);
            if (filter != null && hasDatainCache)
            {
                var response = _dataCachingService.PullDataFromCache<List<HolidayPackages>>(dealDetailKey).AsQueryable();
                var result = response
                .Where(x => x.ValidTo.Value.Date >= DateTime.Now.Date)
                .Where(x => x.Active == true)
                .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved)
                .WhereIf(filter.Id != null && filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.PortalIds != null, x => x.PortalIds.Split(',', ' ').Contains("2") || x.PortalIds.Split(',', ' ').Contains(filter.PortalIds))
                .WhereIf(!string.IsNullOrEmpty(filter.Url), x => x.Url.ToLower() == filter.Url.ToLower())
                .WhereIf(!string.IsNullOrEmpty(filter.Theme), x => EF.Functions.Like(x.PackageTheme.ToLower(), $"%{filter.Theme.ToLower()}%") == true)
                .ToList();
                if (result != null && result.Count > 0)
                {

                    result.ForEach(item =>
                    {
                        if (item.Id > 0)
                        {
                            var itenaryQuery = _PackageItenariesDataRepository.GetAll(false).Where(x => x.PackageId == item.Id && x.Active != null && x.Active == true);
                            item.PackageItenaries = itenaryQuery.ToList();
                        }
                    });

                    return await Task.FromResult(_mapper.Map<HolidayPackages, HolidayPackagesData>(result.FirstOrDefault()));

                }
                else
                {
                    var query = _HolidayPackagesDataRepository.GetAll(false);
                    var similarAllItems = new List<HolidayPackages>();
                    var searchedDetail = await query.Where(x => x.ValidTo.Value.Date >= DateTime.Now.Date)
                        .WhereIf(true, x => x.Active == true)
                        .WhereIf(filter.Approved != null && filter.Approved.Value, x => x.Approved == filter.Approved)
                        .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                        .WhereIf(!string.IsNullOrEmpty(filter.Theme), x => EF.Functions.Like(x.PackageTheme.ToLower(), $"%{filter.Theme.ToLower()}%") == true)
                        .WhereIf(filter.PortalIds != null, x => EF.Functions.Like(x.PortalIds, "%2%") || x.PortalIds == filter.PortalIds)
                        .WhereIf(!string.IsNullOrEmpty(filter.Url), x => x.Url.ToLower() == filter.Url.ToLower()).FirstOrDefaultAsync();

                    if (searchedDetail.Id > 0)
                    {
                        var itenaryQuery = _PackageItenariesDataRepository.GetAll(false).Where(x => x.PackageId == searchedDetail.Id && x.Active != null && x.Active == true);
                        searchedDetail.PackageItenaries = itenaryQuery.ToList();
                    }
                    var keywords = searchedDetail.Keywords.Split(' ', ' ').OrderBy(x => x);
                    foreach (var keyword in keywords)
                    {
                        var pageNames = string.Concat(",", searchedDetail.Url);
                        var items = query
                            .Where(x => x.Active == true && x.Keywords.ToLower().Contains(keyword.ToLower()) && !pageNames.ToLower().Contains(x.Url.ToLower()));
                        similarAllItems.AddRange(items.ToList());
                    }
                    if (similarAllItems != null && similarAllItems.Count > 0)
                    {
                        searchedDetail.SimilarPackages = new PaginatedList<HolidayPackages>(similarAllItems, filter.SimilarItems, 1).Result;
                    }
                    result.Add(searchedDetail);
                    if (DependancyRegistrar.UseCacheForHolidayPackages)
                        hasDatainCache = _dataCachingService.PushDataInCache(result, dealDetailKey);

                    return await Task.FromResult(_mapper.Map<HolidayPackages, HolidayPackagesData>(searchedDetail));


                }

            }
            else if (!hasDatainCache && filter != null)
            {
                var result = new List<HolidayPackages>();
                var query = _HolidayPackagesDataRepository.GetAll(false);
                var similarAllItems = new List<HolidayPackages>();
                var searchedDetail = await query.Where(x => x.ValidTo.Value.Date >= DateTime.Now.Date)
                    .WhereIf(true, x => x.Active == true)
                    .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved)
                    .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                    .WhereIf(filter.PortalIds != null, x => EF.Functions.Like(x.PortalIds, "%2%") || x.PortalIds == filter.PortalIds)
                    .WhereIf(!string.IsNullOrEmpty(filter.Theme), x => EF.Functions.Like(x.PackageTheme.ToLower(), $"%{filter.Theme.ToLower()}%") == true)
                    .WhereIf(!string.IsNullOrEmpty(filter.Url), x => x.Url.ToLower() == filter.Url.ToLower()).FirstOrDefaultAsync();

                if (searchedDetail != null && searchedDetail.Id > 0)
                {
                    var itenaryQuery = _PackageItenariesDataRepository.GetAll(false).Where(x => x.PackageId == searchedDetail.Id && x.Active != null && x.Active == true);
                    searchedDetail.PackageItenaries = itenaryQuery.ToList();
                }
                if (filter.IncludeSimilarItems)
                {
                    var keywords = searchedDetail.Keywords.Split(' ', ' ').OrderBy(x => x);
                    foreach (var keyword in keywords)
                    {
                        var pageNames = string.Concat(",", searchedDetail.Url);
                        var items = query.Where(x => x.Active == true && x.Keywords.ToLower().Contains(keyword.ToLower()) && !pageNames.ToLower().Contains(x.Url.ToLower()))
                            .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved);
                        similarAllItems.AddRange(items.ToList());
                    }
                    if (similarAllItems != null && similarAllItems.Count > 0)
                    {
                        var itenaryQuery = _PackageItenariesDataRepository.GetAll(false).Where(x => x.PackageId == searchedDetail.Id && x.Active != null && x.Active == true);
                        searchedDetail.PackageItenaries = itenaryQuery.ToList();
                        searchedDetail.SimilarPackages = new PaginatedList<HolidayPackages>(similarAllItems, filter.SimilarItems, 1).Result;
                    }
                }
                result.Add(searchedDetail);
                if (DependancyRegistrar.UseCacheForHolidayPackages)
                    hasDatainCache = _dataCachingService.PushDataInCache(result, dealDetailKey);

                return await Task.FromResult(_mapper.Map<HolidayPackages, HolidayPackagesData>(searchedDetail));

            }
            else
            {
                var flterDBData = await _HolidayPackagesDataRepository.GetAll(deleted: false)
                .Where(x => x.ValidFrom >= DateTime.UtcNow).FirstOrDefaultAsync();
                if (flterDBData.Id > 0)
                {
                    var itenaryQuery = _PackageItenariesDataRepository.GetAll(false).Where(x => x.PackageId == flterDBData.Id && x.Active != null && x.Active == true);
                    flterDBData.PackageItenaries = itenaryQuery.ToList();
                }
                return await Task.FromResult(_mapper.Map<HolidayPackages, HolidayPackagesData>(flterDBData));
            }
        }
        private string GenerateShortCacheKey(string schema, HolidayPackagesFilter filter)
        {
            string jsonString = JsonSerializer.Serialize(filter);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(jsonString));
                return $"{schema}:{Convert.ToBase64String(hashBytes)}";
            }
        }
    }
}
