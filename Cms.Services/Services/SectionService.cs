using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.Section;
using CMS.Repositories.Interfaces;
using CMS.Repositories.Repositories;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class SectionService : ISectionService
    {
        public ISectionRepository _sectionRepository;
        public IMapper _mapper;
        public SectionService(ISectionRepository usersRepository, IMapper mapper)
        {
            _sectionRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CreateSectionModal> CreateSection(CreateSectionModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {


                var result = await _sectionRepository.Insert(_mapper.Map<CreateSectionModal, Section>(modal));
                if (result.Id > 0)
                {
                    await _sectionRepository.Insert(_mapper.Map<Section_Trails>(result));
                }
                return _mapper.Map<Section, CreateSectionModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UpdateSectionModal> UpdateSection(UpdateSectionModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _sectionRepository.Update(_mapper.Map<UpdateSectionModal, Section>(modal));
                if (result.Id > 0)
                {
                    await _sectionRepository.Insert(_mapper.Map<Section_Trails>(result));
                }
                return _mapper.Map<Section, UpdateSectionModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<SectionModal>> GetAllSection(SectionFilter filter)
        {
            if (filter != null)
            {
                var result =
                _sectionRepository.GetAll(deleted: false).Include(x => x.SectionType).Include(x => x.TemplateDetails).Include(x => x.TemplateDetails.TemplateMaster).Include(x => x.TemplateDetails.Portal)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.Active != null, x => x.Active == filter.Active && x.TemplateDetails.Active == filter.Active)
                .WhereIf(filter.TemplateId != null && filter.TemplateId.Value > 0, x => x.TemplatDetailsId == filter.TemplateId)
                .WhereIf(!string.IsNullOrEmpty(filter.PortalName), x => EF.Functions.Like(x.TemplateDetails.Portal.Name, $"%{filter.PortalName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.TemplateType), x => EF.Functions.Like(x.TemplateDetails.TemplateMaster.Name, $"%{filter.TemplateType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.TemplateName), x => EF.Functions.Like(x.TemplateDetails.Name, $"%{filter.TemplateName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Title), x => EF.Functions.Like(x.Title, $"%{filter.Title}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.TemplateDetails.Title, $"%{filter.Query}%")
                || EF.Functions.Like(x.Title, $"%{filter.Query}%"));
                return await Task.FromResult(_mapper.Map<PaginatedList<Section>, PaginatedList<SectionModal>>(new PaginatedList<Section>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _sectionRepository.GetAll(deleted: false).Include(x => x.SectionType).Include(x => x.TemplateDetails).Include(x => x.TemplateDetails.Portal).Include(x => x.TemplateDetails.TemplateMaster);
                return await Task.FromResult(_mapper.Map<PaginatedList<Section>, PaginatedList<SectionModal>>(new PaginatedList<Section>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<PaginatedList<Section_Trails>> GetAllSection_Trails(SectionFilter filter)
        {
            if (filter != null)
            {
                var result =
                _sectionRepository.GetAllSection_Trails(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.Active != null, x => x.Active == filter.Active)
                .WhereIf(filter.TemplateId != null && filter.TemplateId.Value > 0, x => x.TemplatDetailsId == filter.TemplateId)
                .WhereIf(!string.IsNullOrEmpty(filter.Title), x => EF.Functions.Like(x.Title, $"%{filter.Title}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Caption, $"%{filter.Query}%")
                || EF.Functions.Like(x.Title, $"%{filter.Query}%"));
                return await Task.FromResult(new PaginatedList<Section_Trails>(result, filter.PageSize, filter.PageNumber));
            }
            else
            {
                var result =
                _sectionRepository.GetAllSection_Trails(deleted: false);
                return await Task.FromResult(new PaginatedList<Section_Trails>(result, filter.PageSize, filter.PageNumber));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _sectionRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                await _sectionRepository.Insert(_mapper.Map<Section_Trails>(result));
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<SectionModal> GetById(int Id)
        {
            var result = await _sectionRepository.GetAll(true).Include(x => x.SectionType).Include(x => x.TemplateDetails).Include(x => x.TemplateDetails.TemplateMaster).Include(x => x.TemplateDetails.Portal).FirstOrDefaultAsync(x => x.Id == Id);
            return _mapper.Map<Section, SectionModal>(result);

        }
    }
}
