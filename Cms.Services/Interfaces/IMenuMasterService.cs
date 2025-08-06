using Cms.Services.Filters;
using Cms.Services.Models.MenuMaster;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface IMenuMasterService
    {
        Task<MenuMasterModal> CreateMenu(MenuMasterModal modal);
        Task<PaginatedList<MenuMasterModal>> GetAllMenus(MenuMasterFilter filter);
        Task<MenuMasterModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<MenuMasterModal> UpdateMenu(MenuMasterModal modal);
    }
}