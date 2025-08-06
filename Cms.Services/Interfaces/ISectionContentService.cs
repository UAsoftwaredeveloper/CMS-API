using Cms.Services.Filters;
using Cms.Services.Models.OpenAPIDataModel.SectionContent;
using Cms.Services.Models.SectionContent;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface ISectionContentService
    {
        Task<CreateSectionContentModal> CreateSectionContent(CreateSectionContentModal modal);
        Task<PaginatedList<SectionContentModal>> GetAllSectionContent(SectionContentFilter filter);
        Task<SectionContentModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<UpdateSectionContentModal> UpdateSectionContent(UpdateSectionContentModal modal);
    }
}