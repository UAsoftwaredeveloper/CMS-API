using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.DummyVacationPackageMaster;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class DummyVacationPackageMasterService : IDummyVacationPackageMasterService
    {
        public IDummyVacationPackageMasterRepository _DummyVacationPackageMasterRepository;
        public IMapper _mapper;
        public DummyVacationPackageMasterService(IDummyVacationPackageMasterRepository DummyVacationPackageMasterRepository, IMapper mapper)
        {
            _DummyVacationPackageMasterRepository = DummyVacationPackageMasterRepository ?? throw new ArgumentNullException(nameof(DummyVacationPackageMasterRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<DummyVacationPackageMasterModal> CreateDummyVacationPackageMaster(DummyVacationPackageMasterModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _DummyVacationPackageMasterRepository.Insert(_mapper.Map<DummyVacationPackageMasterModal, DummyVacationPackageMaster>(modal));
                return _mapper.Map<DummyVacationPackageMaster, DummyVacationPackageMasterModal>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<DummyVacationPackageMasterModal> UpdateDummyVacationPackageMaster(DummyVacationPackageMasterModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _DummyVacationPackageMasterRepository.Update(_mapper.Map<DummyVacationPackageMasterModal, DummyVacationPackageMaster>(modal));
                return _mapper.Map<DummyVacationPackageMaster, DummyVacationPackageMasterModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<DummyVacationPackageMasterModal>> GetAllDummyVacationPackageMaster(DummyVacationPackageMasterFilter filter)
        {
            if (filter != null)
            {
                var result =
                _DummyVacationPackageMasterRepository.GetAll(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.Active !=null, x => x.Active == filter.Active)
                .WhereIf(!string.IsNullOrEmpty(filter.City), x => EF.Functions.Like(x.City, $"%{filter.City}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Continent), x => EF.Functions.Like(x.Continent.ToLower(), $"%{filter.Continent.ToLower()}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Country), x => EF.Functions.Like(x.Country, $"%{filter.Country}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.PackageType), x => EF.Functions.Like(x.PackageType, $"%{filter.PackageType}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.City, $"%{filter.Query}%")
                || EF.Functions.Like(x.Country, $"%{filter.Query}%")
                || EF.Functions.Like(x.Continent, $"%{filter.Query}%")
                || EF.Functions.Like(x.PackageType, $"%{filter.Query}%"))
                ;

                return await Task.FromResult(_mapper.Map<PaginatedList<DummyVacationPackageMaster>, PaginatedList<DummyVacationPackageMasterModal>>(new PaginatedList<DummyVacationPackageMaster>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _DummyVacationPackageMasterRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<DummyVacationPackageMaster>, PaginatedList<DummyVacationPackageMasterModal>>(new PaginatedList<DummyVacationPackageMaster>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _DummyVacationPackageMasterRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<DummyVacationPackageMasterModal> GetById(int Id)
        {
            return _mapper.Map<DummyVacationPackageMaster, DummyVacationPackageMasterModal>(await _DummyVacationPackageMasterRepository.Get(Id).Result.FirstOrDefaultAsync());
        }
    }
}
