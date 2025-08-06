using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.OpenAPIDataModel.TemplateDetails;
using Cms.Services.Models.TemplateDetails;
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
    public class TemplateDetailsDataService : ITemplateDetailsDataService
    {
        private readonly ITemplateDetailsRepository _templateDetailsRepository;
        private readonly ISectionContentRepository _sectionContentRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IHolidayPackagesRepository _holidayPackagesRepository;
        private readonly IMapper _mapper;
        private readonly IDataCachingService _dataCachingService;

        public TemplateDetailsDataService(ITemplateDetailsRepository usersRepository, IMapper mapper, ISectionRepository sectionRepository, IDataCachingService dataCachingService, ISectionContentRepository sectionContentRepository, IHolidayPackagesRepository holidayPackagesRepository)
        {
            _templateDetailsRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _sectionRepository = sectionRepository ?? throw new ArgumentNullException(nameof(sectionRepository));
            _dataCachingService = dataCachingService;
            _sectionContentRepository = sectionContentRepository ?? throw new ArgumentNullException(nameof(sectionContentRepository));
            _holidayPackagesRepository = holidayPackagesRepository ?? throw new ArgumentNullException(nameof(holidayPackagesRepository));
        }
        public async Task<List<TemplateDetailsData>> GetAllTemplateDetails(TemplateDetailsDataFilter filter)
        {
            var templateDetailKey = GenerateShortCacheKey("templateDetail", filter);
            var hasDatainCache = _dataCachingService.IsKeyAvailable(templateDetailKey);
            filter.Active = true;
            if (filter != null && hasDatainCache)
            {
                var result = _dataCachingService.PullDataFromCache<List<TemplateDetails>>(templateDetailKey).AsQueryable()
                .WhereIf(filter.Approved != null && filter.Approved.Value, x => x.Approved == filter.Approved)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.portalId > 0, x => x.PortalId == filter.portalId)
                .WhereIf(filter.TemplateId > 0, x => x.TemplateId == filter.TemplateId)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.Showonhomepage != null, x => x.ShowOnHomePage == filter.Showonhomepage.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.Url), x => x.Url.ToLower().Contains(filter.Url.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(filter.TemplateName), x => x.Url.ToLower() == (filter.TemplateName.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(filter.TemplateName), x => x.Name.ToLower() == (filter.TemplateName.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(filter.TemplateCategory), x => x.TemplateCategory.ToLower() == (filter.TemplateCategory.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(filter.portalName), x => x.Portal.Name.ToLower() == (filter.portalName.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(filter.portalCode), x => x.Portal.PortalCode.ToLower() == (filter.portalCode.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(filter.TemplateType), x => x.TemplateMaster.Name.ToLower() == (filter.TemplateType.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => x.Title.ToLower().Contains(filter.Query.ToLower())
                || x.TemplateMaster.Name.ToLower().Contains(filter.Query.ToLower())).OrderByDescending(x => x.Id).ToList();
                if (result != null && result.Count > 0)
                {
                    return await Task.FromResult(_mapper.Map<List<TemplateDetails>, List<TemplateDetailsData>>(result));

                }
                else
                {
                    var data = new List<TemplateDetails>();
                    var flterDBData = _templateDetailsRepository.GetAll(deleted: false).Include(x => x.TemplateMaster).Include(x => x.Portal)
                            .WhereIf(filter.Approved != null && filter.Approved.Value, x => x.Approved == filter.Approved)
                            .Where(x => x.Active == true)
                            .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                            .WhereIf(filter.portalId > 0, x => x.PortalId == filter.portalId)
                            .WhereIf(filter.TemplateId > 0, x => x.TemplateId == filter.TemplateId)
                            .WhereIf(filter.Showonhomepage != null, x => x.ShowOnHomePage == filter.Showonhomepage.Value)
                            .WhereIf(!string.IsNullOrEmpty(filter.Url), x => EF.Functions.Like(x.Url, $"%{filter.Url}%"))
                            .WhereIf(!string.IsNullOrEmpty(filter.TemplateName), x => EF.Functions.Like(x.Url, $"%{filter.TemplateName}%"))
                            .WhereIf(!string.IsNullOrEmpty(filter.TemplateName), x => EF.Functions.Like(x.Name, $"%{filter.TemplateName}%"))
                            .WhereIf(!string.IsNullOrEmpty(filter.TemplateCategory), x => EF.Functions.Like(x.TemplateCategory, $"%{filter.TemplateCategory}%"))
                            .WhereIf(!string.IsNullOrEmpty(filter.portalName), x => EF.Functions.Like(x.Portal.Name, $"%{filter.portalName}%"))
                            .WhereIf(!string.IsNullOrEmpty(filter.portalCode), x => EF.Functions.Like(x.Portal.PortalCode, $"%{filter.portalCode}%"))
                           .WhereIf(!string.IsNullOrEmpty(filter.TemplateType), x => EF.Functions.Like(x.TemplateMaster.Name, $"%{filter.TemplateType}%"))
                            .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Title, $"%{filter.Query}%")
                            || EF.Functions.Like(x.TemplateMaster.Name, $"%{filter.Query}%")).OrderByDescending(x => x.Id).ToList();
                    foreach (var item in flterDBData)
                    {
                        if (filter.IncludeSimilarItems != null && filter.IncludeSimilarItems.Value)
                        {
                            var query = _templateDetailsRepository.GetAll(false).Where(x => x.TemplateMaster.Name == filter.TemplateType).
                                WhereIf(filter.Approved != null && filter.Approved.Value, x => x.Approved == x.Approved).OrderByDescending(x => x.Id);
                            var similarAllItems = new List<TemplateDetails>();

                            var categories = item.TemplateCategory.Split(' ', ' ').OrderBy(x => x);
                            foreach (var itemCate in categories)
                            {
                                var pageNames = string.Concat(",", similarAllItems.Select(x => x.Url).Distinct());
                                var items = query.Where(x => x.TemplateCategory.ToLower().Contains(itemCate.ToLower()) && !pageNames.ToLower().Contains(x.PageName.ToLower()))
                                                            .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved).OrderByDescending(x => x.Id);
                                similarAllItems.AddRange(items.ToList());
                            }
                            if (similarAllItems != null && similarAllItems.Count > 0)
                            {
                                item.SimilarTemplatesData = new PaginatedList<TemplateDetails>(similarAllItems, filter.SimilarItems.Value, 1).Result;
                            }
                        }

                        List<Section> sections = new List<Section>();
                        var section2 = _sectionRepository.GetAll(false).Include(x => x.SectionType).Where(x => x.TemplatDetailsId == item.Id && x.Active == true).ToList();
                        if (section2 != null)
                            foreach (var item2 in section2)
                            {
                                var sectionDetails = _sectionContentRepository.GetAll(false).Where(x => x.SectionId == item2.Id && x.Active == true).ToList();
                                item2.SectionContents = sectionDetails;
                                if (sectionDetails.Count > 0)
                                    sections.Add(item2);
                            }
                        if (sections.Count > 0)
                        {
                            item.Sections = sections;
                            data.Add(item);
                        }
                    }
                    if (data.Count > 0)
                    {
                        result.AddRange(data);
                        if (DependancyRegistrar.UseCacheForTemplateDetails)
                            hasDatainCache = _dataCachingService.PushDataInCache(result, templateDetailKey);
                    }
                    return await Task.FromResult(_mapper.Map<List<TemplateDetails>, List<TemplateDetailsData>>(data));

                }

            }
            else if (!hasDatainCache && filter != null)
            {
                var data = new List<TemplateDetails>();
                var flterDBData = _templateDetailsRepository.GetAll(deleted: false).Include(x => x.TemplateMaster).Include(x => x.Portal)
                        .Where(x => x.Active == true)
                        .WhereIf(filter.Approved != null && filter.Approved.Value, x => x.Approved == filter.Approved)
                        .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                        .WhereIf(filter.portalId > 0, x => x.PortalId == filter.portalId)
                        .WhereIf(filter.TemplateId > 0, x => x.TemplateId == filter.TemplateId)
                        .WhereIf(filter.Showonhomepage != null, x => x.ShowOnHomePage == filter.Showonhomepage.Value)
                        .WhereIf(!string.IsNullOrEmpty(filter.Url), x => EF.Functions.Like(x.Url, $"%{filter.Url}%"))
                        .WhereIf(!string.IsNullOrEmpty(filter.TemplateName), x => EF.Functions.Like(x.Url, $"%{filter.TemplateName}%"))
                        .WhereIf(!string.IsNullOrEmpty(filter.TemplateName), x => EF.Functions.Like(x.Name, $"%{filter.TemplateName}%"))
                        .WhereIf(!string.IsNullOrEmpty(filter.portalName), x => EF.Functions.Like(x.Portal.Name, $"%{filter.portalName}%"))
                        .WhereIf(!string.IsNullOrEmpty(filter.portalCode), x => EF.Functions.Like(x.Portal.PortalCode, $"%{filter.portalCode}%"))
                        .WhereIf(!string.IsNullOrEmpty(filter.TemplateType), x => EF.Functions.Like(x.TemplateMaster.Name, $"%{filter.TemplateType}%"))
                        .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Title, $"%{filter.Query}%")
                        || EF.Functions.Like(x.TemplateMaster.Name, $"%{filter.Query}%")).OrderByDescending(x => x.Id).ToList();
                foreach (var item in flterDBData)
                {
                    if (filter.IncludeSimilarItems != null && filter.IncludeSimilarItems.Value)
                    {
                        var query = _templateDetailsRepository.GetAll(false)
                            .Where(x => x.TemplateMaster.Name == filter.TemplateType);
                        var similarAllItems = new List<TemplateDetails>();

                        var categories = item.TemplateCategory.Split(' ', ' ').OrderBy(x => x);
                        foreach (var itemCate in categories)
                        {
                            var pageNames = string.Concat(",", similarAllItems.Select(x => x.Url).Distinct());
                            var items = query.Where(x => x.TemplateCategory.ToLower().Contains(itemCate.ToLower()) && !pageNames.ToLower().Contains(x.PageName.ToLower()))
                                                        .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved).OrderByDescending(x => x.Id);
                            similarAllItems.AddRange(items.ToList());
                        }
                        if (similarAllItems != null && similarAllItems.Count > 0)
                        {
                            item.SimilarTemplatesData = new PaginatedList<TemplateDetails>(similarAllItems, filter.SimilarItems.Value, 1).Result;
                        }
                    }
                    List<Section> sections = new List<Section>();
                    var section2 = _sectionRepository.GetAll(false).Include(x => x.SectionType).Where(x => x.TemplatDetailsId == item.Id && x.Active == true).ToList();
                    if (section2 != null)
                        foreach (var item2 in section2)
                        {
                            var sectionDetails = _sectionContentRepository.GetAll(false).Where(x => x.SectionId == item2.Id && x.Active == true).ToList();
                            item2.SectionContents = sectionDetails;
                            if (sectionDetails.Count > 0)
                                sections.Add(item2);
                        }
                    if (sections.Count > 0)
                    {
                        item.Sections = sections;
                        data.Add(item);
                    }
                }
                if (data.Count > 0)
                {

                    if (DependancyRegistrar.UseCacheForTemplateDetails)
                        hasDatainCache = _dataCachingService.PushDataInCache(data, templateDetailKey);
                }
                return await Task.FromResult(_mapper.Map<List<TemplateDetails>, List<TemplateDetailsData>>(data));
            }
            else
            {
                var data = new List<TemplateDetails>();
                var flterDBData = _templateDetailsRepository.GetAll(deleted: false).Include(x => x.TemplateMaster).Include(x => x.Portal)
                                    .Where(x => x.Active == true).OrderByDescending(x=>x.Id).ToList();
                foreach (var item in flterDBData)
                {
                    List<Section> sections = new List<Section>();
                    var section2 = _sectionRepository.GetAll(false).Include(x => x.SectionType).Where(x => x.TemplatDetailsId == item.Id && x.Active == true).ToList();
                    if (section2 != null)
                        foreach (var item2 in section2)
                        {
                            var sectionDetails = _sectionContentRepository.GetAll(false).Where(x => x.SectionId == item2.Id && x.Active == true).ToList();
                            item2.SectionContents = sectionDetails;
                            if (sectionDetails.Count > 0)
                                sections.Add(item2);
                        }
                    if (sections.Count > 0)
                    {
                        item.Sections = sections;
                        data.Add(item);
                    }
                }
                if (data.Count > 0)
                {

                    if (DependancyRegistrar.UseCacheForTemplateDetails)
                        hasDatainCache = _dataCachingService.PushDataInCache(data, templateDetailKey);
                }
                return await Task.FromResult(_mapper.Map<List<TemplateDetails>, List<TemplateDetailsData>>(data));
            }
        }
        public async Task<List<SiteMapDetail>> GetAllSiteMapDetails(TemplateDetailsDataFilter filter)
        {
            var flterDBDataQuery = _templateDetailsRepository.GetAll(deleted: false)
                .Include(x => x.TemplateMaster)
                .Where(x => x.Active == true)
                .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved)
                //.WhereIf(filter.portalId>0, x => x.PortalId == filter.portalId)
                .Select(x => new SiteMapDetail
                {
                    Name = x.PageName,
                    Url = x.Url,
                    TemplateType = x.TemplateMaster.Name
                });

            var flterPackageDBDataQuery = _holidayPackagesRepository.GetAll(deleted: false)
                .Where(x => x.Active == true)
                .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved)
                //.WhereIf(!string.IsNullOrEmpty(filter.portalCode), x => x.PortalIds.Split(',', ' ').Contains("2") || x.PortalIds == filter.portalCode)
                .Select(x => new SiteMapDetail
                {
                    Name = x.PackageName,
                    Url = x.Url,
                    TemplateType = "HolidayPackages"
                });

            // Fetch data asynchronously
            var flterDBData = await flterDBDataQuery.ToListAsync();
            var flterPackageDBData = await flterPackageDBDataQuery.ToListAsync();

            // Merge results
            flterDBData.AddRange(flterPackageDBData);

            return flterDBData;
        }
        public async Task<TemplateDetailsData> GetSingleAllTemplateDetails(TemplateDetailsDataFilter filter)
        {

            var templateDetailKey = GenerateShortCacheKey("templateDetailSingle", filter);
            var hasDatainCache = _dataCachingService.IsKeyAvailable(templateDetailKey);
            if (filter != null && hasDatainCache)
            {
                var response = _dataCachingService.PullDataFromCache<List<TemplateDetails>>(templateDetailKey).AsQueryable();
                var result = response.WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .Where(x => x.Active == true)
                .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved)
                .WhereIf(filter.portalId > 0, x => x.PortalId == filter.portalId)
                .WhereIf(filter.TemplateId > 0, x => x.TemplateId == filter.TemplateId)
                .WhereIf(filter.Showonhomepage != null, x => x.ShowOnHomePage == filter.Showonhomepage.Value)
                .WhereIf(!string.IsNullOrEmpty(filter.Url), x => x.Url.ToLower().Contains(filter.Url.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(filter.TemplateName), x => x.Url.ToLower().Contains(filter.TemplateName.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(filter.TemplateName), x => x.Name.ToLower().Contains(filter.TemplateName.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(filter.portalName), x => x.Portal.Name.ToLower().Contains(filter.portalName.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(filter.portalCode), x => x.Portal.PortalCode.ToLower().Contains(filter.portalCode.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(filter.TemplateType), x => x.TemplateMaster.Name.ToLower().Contains(filter.TemplateType.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => x.Title.ToLower().Contains(filter.Query.ToLower())
                || x.TemplateMaster.Name.ToLower().Contains(filter.Query.ToLower()))
                .WhereIf(filter.Id != null && filter.Id > 0, x => x.Id == filter.Id).OrderByDescending(x => x.Id).ToList();
                if (result != null && result.Count > 0)
                {
                    return await Task.FromResult(_mapper.Map<TemplateDetails, TemplateDetailsData>(result.FirstOrDefault()));

                }
                else
                {
                    var data = new List<TemplateDetails>();
                    var flterDBData = _templateDetailsRepository.GetAll(deleted: false).Include(x => x.TemplateMaster).Include(x => x.Portal)
                        .Where(x => x.Active == true)
                        .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved)
                        .WhereIf(filter.Id != null && filter.Id > 0, x => x.Id == filter.Id)
                        .WhereIf(filter.portalId > 0, x => x.PortalId == filter.portalId)
                        .WhereIf(filter.TemplateId > 0, x => x.TemplateId == filter.TemplateId)
                        .WhereIf(filter.Showonhomepage != null, x => x.ShowOnHomePage == filter.Showonhomepage.Value)
                        .WhereIf(!string.IsNullOrEmpty(filter.Url), x => x.Url == filter.Url)
                        .WhereIf(!string.IsNullOrEmpty(filter.TemplateName), x => x.Name == filter.TemplateName)
                        .WhereIf(!string.IsNullOrEmpty(filter.portalName), x => x.Portal.Name == filter.portalName)
                        .WhereIf(!string.IsNullOrEmpty(filter.portalCode), x => x.Portal.PortalCode == filter.portalCode)
                        .WhereIf(!string.IsNullOrEmpty(filter.TemplateType), x => x.TemplateMaster.Name == filter.TemplateType)
                        .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Title, $"%{filter.Query}%")
                        || EF.Functions.Like(x.TemplateMaster.Name, $"%{filter.Query}%")).OrderByDescending(x => x.Id).ToList();
                    foreach (var item in flterDBData)
                    {
                        if (filter.IncludeSimilarItems != null)
                        {
                            var query = _templateDetailsRepository.GetAll(false).Where(x => x.Active == true)
                                .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved)
                                .WhereIf(filter.IncludeSimilarItems != null && filter.IncludeSimilarItems.Value,
                                x => x.Active == true && x.TemplateMaster.Name == filter.TemplateType && x.Id != item.Id && x.PortalId == item.PortalId).OrderByDescending(x => x.Id);
                            var similarAllItems = new List<TemplateDetails>();

                            var categories = item.TemplateCategory.Split(' ', ' ').OrderBy(x => x);
                            foreach (var itemCate in categories)
                            {
                                var pageNames = string.Concat(",", similarAllItems.Select(x => x.Url).Distinct());
                                var items = query.Where(x => x.Active == true && x.TemplateCategory.ToLower().Contains(itemCate.ToLower()) && !pageNames.ToLower().Contains(x.PageName.ToLower()));
                                similarAllItems.AddRange(items.ToList());
                            }
                            if (similarAllItems != null && similarAllItems.Count > 0)
                            {
                                item.SimilarTemplatesData = new PaginatedList<TemplateDetails>(similarAllItems, filter.SimilarItems.Value, 1).Result;
                            }
                        }
                        List<Section> sections = new List<Section>();
                        var section2 = _sectionRepository.GetAll(false).Include(x => x.SectionType).Where(x => x.TemplatDetailsId == item.Id && x.Active == true).ToList();
                        if (section2 != null)
                            foreach (var item2 in section2)
                            {
                                var sectionDetails = _sectionContentRepository.GetAll(false).Where(x => x.SectionId == item2.Id && x.Active == true).ToList();
                                item2.SectionContents = sectionDetails;
                                if (sectionDetails.Count > 0)
                                    sections.Add(item2);
                            }
                        if (sections.Count > 0)
                        {
                            item.Sections = sections;
                            data.Add(item);
                        }
                    }
                    if (data.Count > 0)
                    {
                        result.AddRange(data);
                        if (DependancyRegistrar.UseCacheForTemplateDetails)
                            hasDatainCache = _dataCachingService.PushDataInCache(result, templateDetailKey);
                    }
                    return await Task.FromResult(_mapper.Map<TemplateDetails, TemplateDetailsData>(data.FirstOrDefault()));

                }

            }
            else if (!hasDatainCache && filter != null)
            {
                var data = new List<TemplateDetails>();
                var flterDBData = _templateDetailsRepository.GetAll(deleted: false).Include(x => x.TemplateMaster).Include(x => x.Portal)
                      .Where(x => x.Active == true)
                      .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                      .WhereIf(filter.portalId > 0, x => x.PortalId == filter.portalId)
                      .WhereIf(filter.TemplateId > 0, x => x.TemplateId == filter.TemplateId)
                      .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved)
                      .WhereIf(filter.Showonhomepage != null, x => x.ShowOnHomePage == filter.Showonhomepage.Value)
                      .WhereIf(!string.IsNullOrEmpty(filter.Url), x => x.Url == filter.Url)
                      .WhereIf(!string.IsNullOrEmpty(filter.TemplateName), x => x.Name == filter.TemplateName)
                      .WhereIf(!string.IsNullOrEmpty(filter.portalName), x => x.Portal.Name == filter.portalName)
                      .WhereIf(!string.IsNullOrEmpty(filter.portalCode), x => x.Portal.PortalCode == filter.portalCode)
                      .WhereIf(!string.IsNullOrEmpty(filter.TemplateType), x => x.TemplateMaster.Name == filter.TemplateType)
                      .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Title, $"%{filter.Query}%")
                        || EF.Functions.Like(x.TemplateMaster.Name, $"%{filter.Query}%")).ToList();
                foreach (var item in flterDBData)
                {
                    var query = _templateDetailsRepository.GetAll(false)
                        .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved)
                        .WhereIf(filter.IncludeSimilarItems != null && filter.IncludeSimilarItems.Value,
                        x => x.Active == true && x.TemplateMaster.Name == filter.TemplateType && x.Id != item.Id && x.PortalId == item.PortalId);
                    var similarAllItems = new List<TemplateDetails>();

                    var categories = item.TemplateCategory.Split(' ', ' ').OrderBy(x => x);
                    foreach (var itemCate in categories)
                    {
                        var pageNames = string.Concat(",", similarAllItems.Select(x => x.Url).Distinct());
                        var items = query.Where(x => x.Active == true && x.TemplateCategory.ToLower().Contains(itemCate.ToLower()) && !pageNames.ToLower().Contains(x.PageName.ToLower())).OrderByDescending(x => x.Id);
                        similarAllItems.AddRange(items.ToList());
                    }
                    if (similarAllItems != null && similarAllItems.Count > 0)
                    {
                        item.SimilarTemplatesData = new PaginatedList<TemplateDetails>(similarAllItems, filter.SimilarItems.Value, 1).Result;
                    }
                    List<Section> sections = new List<Section>();
                    var section2 = _sectionRepository.GetAll(false).Include(x => x.SectionType).Where(x => x.TemplatDetailsId == item.Id && x.Active == true).ToList();
                    if (section2 != null)
                        foreach (var item2 in section2)
                        {
                            var sectionDetails = _sectionContentRepository.GetAll(false).Where(x => x.SectionId == item2.Id && x.Active == true).ToList();
                            item2.SectionContents = sectionDetails;
                            if (sectionDetails.Count > 0)
                                sections.Add(item2);
                        }
                    if (sections.Count > 0)
                    {
                        item.Sections = sections;
                        data.Add(item);
                    }
                }
                if (data.Count > 0)
                {

                    if (DependancyRegistrar.UseCacheForTemplateDetails)
                        hasDatainCache = _dataCachingService.PushDataInCache(data, templateDetailKey);
                }
                return await Task.FromResult(_mapper.Map<TemplateDetails, TemplateDetailsData>(data.FirstOrDefault()));
            }
            else
            {
                var data = new List<TemplateDetails>();
                var flterDBData = _templateDetailsRepository.GetAll(deleted: false).Include(x => x.TemplateMaster).Include(x => x.Portal)
                        .Include(x => x.Sections).Where(x => x.Active == true).OrderByDescending(x => x.Id)
                        .ToList();
                foreach (var item in flterDBData)
                {
                    List<Section> sections = new List<Section>();
                    var section2 = _sectionRepository.GetAll(false).Include(x => x.SectionType).Where(x => x.TemplatDetailsId == item.Id && x.Active == true).ToList();
                    if (section2 != null)
                        foreach (var item2 in section2)
                        {
                            var sectionDetails = _sectionContentRepository.GetAll(false).Where(x => x.SectionId == item2.Id).ToList();
                            item2.SectionContents = sectionDetails;
                            if (sectionDetails.Count > 0)
                                sections.Add(item2);
                        }
                    if (sections.Count > 0)
                    {
                        item.Sections = sections;
                        data.Add(item);
                    }
                }
                if (data.Count > 0)
                {

                    if (DependancyRegistrar.UseCacheForTemplateDetails)
                        hasDatainCache = _dataCachingService.PushDataInCache(data, templateDetailKey);
                }
                return await Task.FromResult(_mapper.Map<TemplateDetails, TemplateDetailsData>(data.FirstOrDefault()));
            }
        }
        private string GenerateShortCacheKey(string schema, TemplateDetailsDataFilter filter)
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
