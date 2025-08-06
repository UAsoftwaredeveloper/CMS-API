using Cms.Services.Filters;
using Cms.Services.Models.OpenAPIDataModel.HolidayPackages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cms.Services.Services
{
    public interface IHolidayPackagesDataService
    {
        Task<List<HolidayPackagesData>> GetAll(HolidayPackagesFilter filter);
        Task<HolidayPackagesData> GetPackageByUrlAll(HolidayPackagesFilter filter);
        Task<List<HolidayPackagesData>> GetThemeSpecificAll(HolidayPackagesFilter filter);
    }
}