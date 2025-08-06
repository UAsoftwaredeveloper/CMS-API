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
    public class SectionContentService : ISectionContentService
    {
        public ISectionContentRepository _sectionContentRepository;
        public IMapper _mapper;
        public SectionContentService(ISectionContentRepository usersRepository, IMapper mapper)
        {
            _sectionContentRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CreateSectionContentModal> CreateSectionContent(CreateSectionContentModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _sectionContentRepository.Insert(_mapper.Map<CreateSectionContentModal, SectionContent>(modal));
                if (result.Id > 0)
                {
                    await _sectionContentRepository.Insert(_mapper.Map<SectionContent_Trails>(result));
                }
                return _mapper.Map<SectionContent, CreateSectionContentModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UpdateSectionContentModal> UpdateSectionContent(UpdateSectionContentModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _sectionContentRepository.Update(_mapper.Map<UpdateSectionContentModal, SectionContent>(modal));
                if (result.Id > 0)
                {
                    await _sectionContentRepository.Insert(_mapper.Map<SectionContent_Trails>(result));
                }
                return _mapper.Map<SectionContent, UpdateSectionContentModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<SectionContentModal>> GetAllSectionContent(SectionContentFilter filter)
        {
            if (filter != null)
            {
                var result =
                _sectionContentRepository.GetAll(deleted: false).Include(x => x.Section).Include(x => x.Section.TemplateDetails).Include(x => x.Section.TemplateDetails.TemplateMaster).Include(x => x.Section.TemplateDetails.Portal).Include(x => x.Section.SectionType)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.Active != null, x => x.Active == filter.Active && x.Section.Active == filter.Active && x.Section.TemplateDetails.Active == filter.Active)
                .WhereIf(filter.SectionId > 0, x => x.SectionId == filter.SectionId)
                .WhereIf(!string.IsNullOrEmpty(filter.PortalName), x => EF.Functions.Like(x.Section.TemplateDetails.Portal.Name, $"%{filter.PortalName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.TemplateType), x => EF.Functions.Like(x.Section.TemplateDetails.TemplateMaster.Name, $"%{filter.TemplateType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.TemplateName), x => EF.Functions.Like(x.Section.TemplateDetails.Name, $"%{filter.TemplateName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SectionType), x => EF.Functions.Like(x.Section.SectionType.Name, $"%{filter.SectionType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.SectionTitle), x => EF.Functions.Like(x.Section.Title, $"%{filter.SectionTitle}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Title), x => EF.Functions.Like(x.ContentHeading, $"%{filter.Title}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Section.Title, $"%{filter.Query}%")
                || EF.Functions.Like(x.Title, $"%{filter.Query}%"));
                return await Task.FromResult(_mapper.Map<PaginatedList<SectionContent>, PaginatedList<SectionContentModal>>(new PaginatedList<SectionContent>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _sectionContentRepository.GetAll(deleted: false).Include(x => x.Section).Include(x => x.Section.TemplateDetails).Include(x => x.Section.TemplateDetails.TemplateMaster).Include(x => x.Section.TemplateDetails.Portal).Include(x => x.Section.SectionType);
                return await Task.FromResult(_mapper.Map<PaginatedList<SectionContent>, PaginatedList<SectionContentModal>>(new PaginatedList<SectionContent>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _sectionContentRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                await _sectionContentRepository.Insert(_mapper.Map<SectionContent_Trails>(result));
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<SectionContentModal> GetById(int Id)
        {
            return _mapper.Map<SectionContent, SectionContentModal>(await _sectionContentRepository.Get(Id).Result.Include(x => x.Section).Include(x => x.Section.SectionType).Include(x => x.Section.TemplateDetails).FirstOrDefaultAsync());

        }
    }
}
