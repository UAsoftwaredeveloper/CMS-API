using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.TemplateDetails;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class TemplateDetailsService : ITemplateDetailsService
    {
        public ITemplateDetailsRepository _templateDetailsRepository;
        private readonly ISectionContentRepository _sectionContentRepository;
        private readonly ISectionRepository _sectionRepository;

        public IMapper _mapper;
        public TemplateDetailsService(ITemplateDetailsRepository usersRepository, IMapper mapper, ISectionContentRepository sectionContentRepository, ISectionRepository sectionRepository)
        {
            _templateDetailsRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _sectionRepository = sectionRepository ?? throw new ArgumentNullException(nameof(_sectionRepository));
            _sectionContentRepository = sectionContentRepository ?? throw new ArgumentNullException(nameof(sectionContentRepository));
        }
        public async Task<CreateTemplateDetailsModal> CreateTemplateDetails(CreateTemplateDetailsModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var exist = await _templateDetailsRepository.GetAll(false).AnyAsync(x => (x.PortalId == modal.PortalId && x.TemplateId == modal.TemplateId) && (x.Url == modal.Url || x.Name == modal.Name));
                if (exist)
                    throw new DuplicateNameException("409");
                var result = await _templateDetailsRepository.Insert(_mapper.Map<CreateTemplateDetailsModal, TemplateDetails>(modal));
                if (result.Id > 0)
                {
                    await _templateDetailsRepository.Insert(_mapper.Map<TemplateDetails_Trails>(result));
                }
                return _mapper.Map<TemplateDetails, CreateTemplateDetailsModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<TemplateDetailsModal> CreateDuplicateTemplateDetails(UpdateTemplateDetailsModal updatemodal)
        {
            TemplateDetails modal = new TemplateDetails();
            if (updatemodal != null && updatemodal.Id < 0)
                throw new ArgumentOutOfRangeException(nameof(updatemodal));
            var exist = await _templateDetailsRepository.GetAll(false).AnyAsync(x => (x.PortalId == modal.PortalId && x.TemplateId == modal.TemplateId) && (x.Url == modal.Url || x.Name == modal.Name));
            if (exist)
                throw new DuplicateNameException("409");

            modal = _templateDetailsRepository.GetAll(false).Include(x => x.TemplateConfigurations).FirstOrDefault(x => x.Id == updatemodal.Id);

            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var sections = _sectionRepository.GetAll(false).Where(x => x.TemplatDetailsId == modal.Id).ToList();
                List<Section> sections2 = new List<Section>();
                foreach (var section in sections)
                {
                    var sectionDetails = _sectionContentRepository.GetAll(false).Where(x => x.SectionId == section.Id).ToList();
                    sectionDetails.ForEach(x => { x.Id = 0; x.CreatedOn = DateTime.UtcNow; x.CreatedBy = updatemodal.ModifiedBy; });
                    section.Id = 0;
                    section.SectionContents = sectionDetails;
                    section.CreatedOn = DateTime.UtcNow;
                    section.CreatedBy = updatemodal.ModifiedBy;
                    sections2.Add(section);
                }
                modal.Sections = sections2;
                modal.Active = false;
                modal.Id = 0;
                modal.Url = updatemodal.Url;
                modal.Title = updatemodal.Title;
                modal.Name = updatemodal.Name;
                modal.PageCode = updatemodal.PageCode;
                modal.PageName = updatemodal.PageName;
                modal.ToName = updatemodal.ToName;
                modal.ToCode = updatemodal.ToCode;
                modal.PortalId = updatemodal.PortalId;
                modal.TemplateId = updatemodal.TemplateId;
                modal.TemplateCategory = updatemodal.TemplateCategory;
                modal.Approved = false;
                modal.TemplateConfigurations.ForEach(x => { x.Id = 0; x.CreatedOn = DateTime.UtcNow; x.CreatedBy = updatemodal.ModifiedBy; });
                var result = await _templateDetailsRepository.Insert(modal);
                if (result.Id > 0)
                {
                    foreach (var section in result.Sections)
                    {
                        foreach (var sectionDetails in section.SectionContents)
                            await _templateDetailsRepository.InsertDuplicate(_mapper.Map<TemplateDetails_Trails>(result), _mapper.Map<List<Section_Trails>>(section), _mapper.Map<List<SectionContent_Trails>>(sectionDetails));
                    }


                }

                return _mapper.Map<TemplateDetails, TemplateDetailsModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UpdateTemplateDetailsModal> UpdateTemplateDetails(UpdateTemplateDetailsModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var exist = await _templateDetailsRepository.GetAll(false).FirstOrDefaultAsync(x => x.Id == modal.Id && x.PortalId == modal.PortalId && x.TemplateId == modal.TemplateId);
                if (exist != null && (exist.Url != modal.Url || exist.Name != modal.Name))
                {
                    //modal.Url = exist.Url;
                    modal.Name = exist.Name;
                }
                var result = await _templateDetailsRepository.Update(_mapper.Map<UpdateTemplateDetailsModal, TemplateDetails>(modal));
                if (result.Id > 0)
                {
                    await _templateDetailsRepository.Insert(_mapper.Map<TemplateDetails_Trails>(result));
                }

                return _mapper.Map<TemplateDetails, UpdateTemplateDetailsModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<TemplateDetails_Trails>> GetAllTemplateDetails_Trails(TemplateDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _templateDetailsRepository.GetAllTemplateDetails_Trails(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.TemplateTypeId != null && filter.TemplateTypeId.Value > 0, x => x.TemplateId.Value == filter.TemplateTypeId.Value)
                .WhereIf(filter.PortalId != null && filter.PortalId.Value > 0, x => x.PortalId.Value == filter.PortalId.Value)
                .WhereIf(filter.Active != null, x => x.Active.Value == filter.Active.Value)
                .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved)
                .WhereIf(!string.IsNullOrEmpty(filter.Title), x => EF.Functions.Like(x.Title, $"%{filter.Title}%") || EF.Functions.Like(x.Name, $"%{filter.Title}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Title, $"%{filter.Query}%")
                || EF.Functions.Like(x.Title, $"%{filter.Query}%")
                || EF.Functions.Like(x.Url, $"%{filter.Query}%")
                || EF.Functions.Like(x.Name, $"%{filter.Query}%"));
                return await Task.FromResult(new PaginatedList<TemplateDetails_Trails>(result, filter.PageSize, filter.PageNumber));
            }
            else
            {
                var result =
                _templateDetailsRepository.GetAllTemplateDetails_Trails(deleted: false);
                return await Task.FromResult(new PaginatedList<TemplateDetails_Trails>(result, filter.PageSize, filter.PageNumber));
            }
        }
        public async Task<PaginatedList<TemplateDetailsModal>> GetAllTemplateDetails(TemplateDetailsFilter filter)
        {
            if (filter != null)
            {
                var result =
                _templateDetailsRepository.GetAll(deleted: false).Include(x => x.TemplateMaster).Include(x => x.Portal)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.TemplateTypeId != null && filter.TemplateTypeId.Value > 0, x => x.TemplateId.Value == filter.TemplateTypeId.Value)
                .WhereIf(filter.PortalId != null && filter.PortalId.Value > 0, x => x.PortalId.Value == filter.PortalId.Value)
                .WhereIf(filter.Active != null, x => x.Active.Value == filter.Active.Value)
                .WhereIf(filter.Approved != null, x => x.Approved == filter.Approved)
                .WhereIf(!string.IsNullOrEmpty(filter.Title), x => EF.Functions.Like(x.Title, $"%{filter.Title}%") || EF.Functions.Like(x.Name, $"%{filter.Title}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.TemplateType), x => x.TemplateMaster.Name==filter.TemplateType)
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Title, $"%{filter.Query}%")
                || EF.Functions.Like(x.TemplateMaster.Name, $"%{filter.Query}%")
                || EF.Functions.Like(x.TemplateMaster.Name, $"%{filter.Query}%")
                || EF.Functions.Like(x.Title, $"%{filter.Query}%")
                || EF.Functions.Like(x.Url, $"%{filter.Query}%")
                || EF.Functions.Like(x.Name, $"%{filter.Query}%"));
                return await Task.FromResult(_mapper.Map<PaginatedList<TemplateDetails>, PaginatedList<TemplateDetailsModal>>(new PaginatedList<TemplateDetails>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _templateDetailsRepository.GetAll(deleted: false).Include(x => x.TemplateMaster).Include(x => x.Portal);
                return await Task.FromResult(_mapper.Map<PaginatedList<TemplateDetails>, PaginatedList<TemplateDetailsModal>>(new PaginatedList<TemplateDetails>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _templateDetailsRepository.Delete(Id);
            if ((bool)result.Deleted)
            {

                await _templateDetailsRepository.Insert(_mapper.Map<TemplateDetails_Trails>(result));
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<TemplateDetailsModal> GetById(int Id)
        {
            return _mapper.Map<TemplateDetails, TemplateDetailsModal>(await _templateDetailsRepository.Get(Id).Result.Include(x => x.Portal).Include(x => x.TemplateMaster).FirstOrDefaultAsync());

        }
    }
}
