using Cms.Services.Filters;
using Cms.Services.Models.PackageItenaries;
using DataManager.DataClasses;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface IPackageItenariesService
    {
        Task<PackageItenariesModal> CreatePackages(PackageItenariesModal modal);
        Task<PaginatedList<PackageItenaries_Trails>> GetAllPackageItenaries_Trails(PackageItenariesFilter filter);
        Task<PaginatedList<PackageItenariesModal>> GetAllPackages(PackageItenariesFilter filter);
        Task<PackageItenariesModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<PackageItenariesModal> UpdatePackages(PackageItenariesModal modal);
    }
}