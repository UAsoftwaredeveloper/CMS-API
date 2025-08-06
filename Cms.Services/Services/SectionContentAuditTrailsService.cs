using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.SectionContent;
using CMS.Repositories.Interfaces;
using CMS.Repositories.Repositories;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class SectionContentAuditTrailsService : ISectionContentAuditTrailsService
    {
        public ISectionContentRepository _sectionContentRepository;
        public IMapper _mapper;
        public SectionContentAuditTrailsService(ISectionContentRepository usersRepository, IMapper mapper)
        {
            _sectionContentRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PaginatedList<SectionContent_Trails>> GetAllSectionContent(SectionContentFilter filter)
        {
            if (filter != null)
            {
                var result =
                _sectionContentRepository.GetAllSectionContentTrails(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.SectionId > 0, x => x.SectionId == filter.SectionId)
                .WhereIf(!string.IsNullOrEmpty(filter.Title), x => EF.Functions.Like(x.ContentHeading, $"%{filter.Title}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.ContentHeading, $"%{filter.Query}%")
                || EF.Functions.Like(x.Title, $"%{filter.Query}%"));
                return await Task.FromResult(new PaginatedList<SectionContent_Trails>(result, filter.PageSize, filter.PageNumber));
            }
            else
            {
                var result =
                _sectionContentRepository.GetAllSectionContentTrails(deleted: false);
                return await Task.FromResult(new PaginatedList<SectionContent_Trails>(result, filter.PageSize, filter.PageNumber));
            }
            }
    }
}
