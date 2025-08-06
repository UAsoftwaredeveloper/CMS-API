using AutoMapper;
using Cms.Services.Extensions;
using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.MenuMaster;
using CMS.Repositories.Interfaces;
using DataManager.DataClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public class MenuMasterService : IMenuMasterService
    {
        public IMenuMasterRepository _menuMasterRepository;
        public IMapper _mapper;
        public MenuMasterService(IMenuMasterRepository menuMastersRepository, IMapper mapper)
        {
            _menuMasterRepository = menuMastersRepository ?? throw new ArgumentNullException(nameof(menuMastersRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<MenuMasterModal> CreateMenu(MenuMasterModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _menuMasterRepository.Insert(_mapper.Map<MenuMasterModal, MenuMaster>(modal));
                return _mapper.Map<MenuMaster, MenuMasterModal>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public async Task<MenuMasterModal> UpdateMenu(MenuMasterModal modal)
        {
            if (modal == null) throw new ArgumentNullException(nameof(modal));
            try
            {
                var result = await _menuMasterRepository.Update(_mapper.Map<MenuMasterModal, MenuMaster>(modal));
                return _mapper.Map<MenuMaster, MenuMasterModal>(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<PaginatedList<MenuMasterModal>> GetAllMenus(MenuMasterFilter filter)
        {
            if (filter != null)
            {
                var result =
                _menuMasterRepository.GetAll(deleted: false).Include(x => x.ParentMenu).Include(x => x.ChildMenus)
                .WhereIf(filter.Id > 0, x => x.Id == filter.Id)
                .WhereIf(filter.Active !=null, x => x.Active == filter.Active)
                .WhereIf(!string.IsNullOrEmpty(filter.Url), x => EF.Functions.Like(x.Url, $"%{filter.Url}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.ParentMenu), x => EF.Functions.Like(x.ParentMenu.Name, $"%{filter.ParentMenu}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.ControllerName), x => EF.Functions.Like(x.ControllerName, $"%{filter.ControllerName}%"))
                .WhereIf(!string.IsNullOrEmpty(filter.ActionName), x => EF.Functions.Like(x.ActionName, $"%{filter.ActionName}%"))
                .WhereIf(filter.Active != null && filter.Active.Value, x => x.Active == filter.Active)
                .WhereIf(!string.IsNullOrEmpty(filter.Query), x => EF.Functions.Like(x.ActionName, $"%{filter.Query}%")
                || EF.Functions.Like(x.ControllerName, $"%{filter.Query}%"));
                result = result.OrderBy(x => x.DisplayOrder);
                return await Task.FromResult(_mapper.Map<PaginatedList<MenuMaster>, PaginatedList<MenuMasterModal>>(new PaginatedList<MenuMaster>(result, filter.PageSize, filter.PageNumber)));
            }
            else
            {
                var result =
                _menuMasterRepository.GetAll(deleted: false).Include(x=>x.ParentMenu).Include(x=>x.ChildMenus);
                return await Task.FromResult(_mapper.Map<PaginatedList<MenuMaster>, PaginatedList<MenuMasterModal>>(new PaginatedList<MenuMaster>(result, filter.PageSize, filter.PageNumber)));
            }
        }
        public async Task<bool> SoftDelete(int Id)
        {
            var result = await _menuMasterRepository.Delete(Id);
            if ((bool)result.Deleted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<MenuMasterModal> GetById(int Id)
        {
            return _mapper.Map<MenuMaster, MenuMasterModal>(await _menuMasterRepository.Get(Id).Result.FirstOrDefaultAsync());
        }
    }
}
