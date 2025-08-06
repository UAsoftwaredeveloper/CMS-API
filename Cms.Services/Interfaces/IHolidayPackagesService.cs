using Cms.Services.Filters;
using Cms.Services.Models.HolidayPackages;
using DataManager.DataClasses;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface IHolidayPackagesService
    {
        Task<HolidayPackagesModal> CreatePackages(HolidayPackagesModal modal);
        Task<PaginatedList<HolidayPackages_Trails>> GetAllHolidayPackages_Trails(HolidayPackagesFilter filter);
        Task<PaginatedList<HolidayPackagesModal>> GetAllPackages(HolidayPackagesFilter filter);
        Task<HolidayPackagesModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<HolidayPackagesModal> UpdatePackages(HolidayPackagesModal modal);
    }
}