using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.TemplateCategory;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class TemplateCategoryService : ITemplateCategoryService
    {
        public ITemplateCategoryRepository _sectionRepository;
        public IMapper _mapper;
        public TemplateCategoryService(ITemplateCategoryRepository usersRepository, IMapper mapper)
        {
            _sectionRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CreateTemplateCategoryModal> CreateTemplateCategory(CreateTemplateCategoryModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _sectionRepository.Insert(_mapper.Map<CreateTemplateCategoryModal, TemplateCategory>(modal));
                return _mapper.Map<TemplateCategory, CreateTemplateCategoryModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<UpdateTemplateCategoryModal> UpdateTemplateCategory(UpdateTemplateCategoryModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _sectionRepository.Update(_mapper.Map<UpdateTemplateCategoryModal, TemplateCategory>(modal));
                return _mapper.Map<TemplateCategory, UpdateTemplateCategoryModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<TemplateCategoryModal>> GetAllTemplateCategory(TemplateCategoryFilter filter)
        {
            if (filter != null)
            {
                var result =
                _sectionRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.CategoryName, $"%{filter.Query}%"));

                return await Task.FromResult(_mapper.Map<PaginatedList<TemplateCategory>, PaginatedList<TemplateCategoryModal>>(new PaginatedList<TemplateCategory>(result.ToList
                    (), filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _sectionRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<TemplateCategory>, PaginatedList<TemplateCategoryModal>>(new PaginatedList<TemplateCategory>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _sectionRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<TemplateCategoryModal> GetById(int Id)
        {
            return _mapper.Map<TemplateCategory, TemplateCategoryModal>(await _sectionRepository.Get(Id).Result.FirstOrDefaultAsync());

        }
    }
}
