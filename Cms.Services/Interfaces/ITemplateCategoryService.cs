using Cms.Services.Filters;
using Cms.Services.Models.TemplateCategory;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface ITemplateCategoryService
    {
        Task<CreateTemplateCategoryModal> CreateTemplateCategory(CreateTemplateCategoryModal modal);
        Task<PaginatedList<TemplateCategoryModal>> GetAllTemplateCategory(TemplateCategoryFilter filter);
        Task<TemplateCategoryModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<UpdateTemplateCategoryModal> UpdateTemplateCategory(UpdateTemplateCategoryModal modal);
    }
}