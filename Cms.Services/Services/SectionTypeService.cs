using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.SectionType;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class SectionTypeService : ISectionTypeService
    {
        public ISectionTypeRepository _sectionTypeRepository;
        public IMapper _mapper;
        public SectionTypeService(ISectionTypeRepository usersRepository, IMapper mapper)
        {
            _sectionTypeRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<CreateSectionTypeModal> CreateSectionType(CreateSectionTypeModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var exist = await _sectionTypeRepository.GetAll(false).AnyAsync(x => x.Name == modal.Name);
                if (exist)
                    throw new DuplicateNameException("409");

                var result = await _sectionTypeRepository.Insert(_mapper.Map<CreateSectionTypeModal, SectionType>(modal));
                return _mapper.Map<SectionType, CreateSectionTypeModal>(result);
            }
            catch (Exception)
            {
                throw;

            }
        }
        public async Task<UpdateSectionTypeModal> UpdateSectionType(UpdateSectionTypeModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {

                var result = await _sectionTypeRepository.Update(_mapper.Map<UpdateSectionTypeModal, SectionType>(modal));
                return _mapper.Map<SectionType, UpdateSectionTypeModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<SectionTypeModal>> GetAllSectionType(SectionTypeFilter filter)
        {
            if (filter != null)
            {
                var result =
                _sectionTypeRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.Active !=null, x => x.Active == filter.Active)
                .WhereIf(!string.IsNullOrEmpty(filter.Name), x => EF.Functions.Like(x.Name, $"%{filter.Name}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Name, $"%{filter.Query}%")
                || EF.Functions.Like(x.Description, $"%{filter.Query}%"))
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id);
                return await Task.FromResult(_mapper.Map<PaginatedList<SectionType>, PaginatedList<SectionTypeModal>>(new PaginatedList<SectionType>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _sectionTypeRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<SectionType>, PaginatedList<SectionTypeModal>>(new PaginatedList<SectionType>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _sectionTypeRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<SectionTypeModal> GetById(int Id)
        {
            return _mapper.Map<SectionType,SectionTypeModal>(await _sectionTypeRepository.Get(Id).Result.FirstOrDefaultAsync());

        }
    }
}
