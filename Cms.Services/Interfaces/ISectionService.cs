using Cms.Services.Filters;
using Cms.Services.Models.OpenAPIDataModel.Section;
using Cms.Services.Models.Section;
using DataManager.DataClasses;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface ISectionService
    {
        Task<CreateSectionModal> CreateSection(CreateSectionModal modal);
        Task<PaginatedList<SectionModal>> GetAllSection(SectionFilter filter);
        Task<PaginatedList<Section_Trails>> GetAllSection_Trails(SectionFilter filter);
        Task<SectionModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<UpdateSectionModal> UpdateSection(UpdateSectionModal modal);
    }
}