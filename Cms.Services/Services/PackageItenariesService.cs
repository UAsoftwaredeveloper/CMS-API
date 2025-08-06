using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.PackageItenaries;
using CMS.Repositories.Interfaces;
using CMS.Repositories.Repositories;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class PackageItenariesService : IPackageItenariesService
    {
        public IPackageItenariesRepository _packageItenariesRepository;
        public IMapper _mapper;
        public PackageItenariesService(IPackageItenariesRepository PackageItenariesRepository, IMapper mapper)
        {
            _packageItenariesRepository = PackageItenariesRepository ?? throw new ArgumentNullException(nameof(PackageItenariesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<PackageItenariesModal> CreatePackages(PackageItenariesModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _packageItenariesRepository.Insert(_mapper.Map<PackageItenariesModal, PackageItenaries>(modal));
                if (result.Id > 0)
                {
                    await _packageItenariesRepository.Insert(_mapper.Map<PackageItenaries_Trails>(result));
                }
                return _mapper.Map<PackageItenaries, PackageItenariesModal>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<PackageItenariesModal> UpdatePackages(PackageItenariesModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _packageItenariesRepository.Update(_mapper.Map<PackageItenariesModal, PackageItenaries>(modal));
                if (result.Id > 0)
                {
                    await _packageItenariesRepository.Insert(_mapper.Map<PackageItenaries_Trails>(result));
                }
                return _mapper.Map<PackageItenaries, PackageItenariesModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<PackageItenariesModal>> GetAllPackages(PackageItenariesFilter filter)
        {
            if (filter != null)
            {
                var result =
                _packageItenariesRepository.GetAll(deleted: false).Include(x => x.HolidayPackages)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(!string.IsNullOrEmpty(filter.PortalIds), x => x.HolidayPackages.PortalIds == filter.PortalIds)
                .WhereIf(!string.IsNullOrEmpty(filter.PackageName), x => EF.Functions.Like(x.HolidayPackages.PackageName, $"%{filter.PackageName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.DayOfItenary), x => EF.Functions.Like(x.Title, $"%{filter.DayOfItenary}%"))
                .WhereIf(filter.PackageId != null && filter.PackageId.Value>0, x => x.PackageId == filter.PackageId)
                .WhereIf(filter.Active != null , x => x.Active == filter.Active)
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Title, $"%{filter.Query}%"));

                return await Task.FromResult(_mapper.Map<PaginatedList<PackageItenaries>, PaginatedList<PackageItenariesModal>>(new PaginatedList<PackageItenaries>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _packageItenariesRepository.GetAll(deleted: false);
                return await Task.FromResult(_mapper.Map<PaginatedList<PackageItenaries>, PaginatedList<PackageItenariesModal>>(new PaginatedList<PackageItenaries>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<PaginatedList<PackageItenaries_Trails>> GetAllPackageItenaries_Trails(PackageItenariesFilter filter)
        {
            if (filter != null)
            {
                var result =
                _packageItenariesRepository.GetAllPackageItenaries_Trails(deleted: false)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(!string.IsNullOrEmpty(filter.DayOfItenary), x => EF.Functions.Like(x.Title, $"%{filter.DayOfItenary}%"))
                .WhereIf(filter.PackageId != null && filter.PackageId.Value>0, x => x.PackageId == filter.PackageId)
                .WhereIf(filter.Active != null , x => x.Active == filter.Active)
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.Title, $"%{filter.Query}%"));

                return await Task.FromResult(new PaginatedList<PackageItenaries_Trails>(result, filter.PageSize, filter.PageNumber));
            }
            else
            {
                var result =
                _packageItenariesRepository.GetAllPackageItenaries_Trails(deleted: false);
                return await Task.FromResult(new PaginatedList<PackageItenaries_Trails>(result, filter.PageSize, filter.PageNumber));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _packageItenariesRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                    await _packageItenariesRepository.Insert(_mapper.Map<PackageItenaries_Trails>(result));
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<PackageItenariesModal> GetById(int Id)
        {
            return _mapper.Map<PackageItenaries, PackageItenariesModal>(await _packageItenariesRepository.GetAll(false).Include(x=>x.HolidayPackages).FirstOrDefaultAsync(x=>x.Id==Id));
        }
    }
}
