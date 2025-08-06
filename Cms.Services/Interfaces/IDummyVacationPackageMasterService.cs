using Cms.Services.Filters;
using Cms.Services.Models.DummyVacationPackageMaster;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface IDummyVacationPackageMasterService
    {
        Task<DummyVacationPackageMasterModal> CreateDummyVacationPackageMaster(DummyVacationPackageMasterModal modal);
        Task<PaginatedList<DummyVacationPackageMasterModal>> GetAllDummyVacationPackageMaster(DummyVacationPackageMasterFilter filter);
        Task<DummyVacationPackageMasterModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<DummyVacationPackageMasterModal> UpdateDummyVacationPackageMaster(DummyVacationPackageMasterModal modal);
    }
}