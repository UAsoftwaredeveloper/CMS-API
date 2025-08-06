using Cms.Services.Filters;
using Cms.Services.Models.OpenAPIDataModel.TemplateDetails;
using Cms.Services.Models.TemplateDetails;
using DataManager.DataClasses;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface ITemplateDetailsService
    {
        Task<TemplateDetailsModal> CreateDuplicateTemplateDetails(UpdateTemplateDetailsModal modal);
        Task<CreateTemplateDetailsModal> CreateTemplateDetails(CreateTemplateDetailsModal modal);
        Task<PaginatedList<TemplateDetailsModal>> GetAllTemplateDetails(TemplateDetailsFilter filter);
        Task<PaginatedList<TemplateDetails_Trails>> GetAllTemplateDetails_Trails(TemplateDetailsFilter filter);
        Task<TemplateDetailsModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<UpdateTemplateDetailsModal> UpdateTemplateDetails(UpdateTemplateDetailsModal modal);
    }
}