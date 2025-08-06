using Cms.Services.Filters;
using Cms.Services.Models.SectionType;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface ISectionTypeService
    {
        Task<CreateSectionTypeModal> CreateSectionType(CreateSectionTypeModal modal);
        Task<PaginatedList<SectionTypeModal>> GetAllSectionType(SectionTypeFilter filter);
        Task<SectionTypeModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<UpdateSectionTypeModal> UpdateSectionType(UpdateSectionTypeModal modal);
    }
}