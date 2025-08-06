using Cms.Services.Filters;
using Cms.Services.Models.OpenAPIDataModel.TemplateConfiguration;
using Cms.Services.Models.TemplateConfiguration;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface ITemplateConfigurationService
    {
        Task<CreateTemplateConfigurationModal> CreateTemplateConfiguration(CreateTemplateConfigurationModal modal);
        Task<PaginatedList<TemplateConfigurationModal>> GetAllTemplateConfiguration(TemplateConfigurationFilter filter);
        Task<TemplateConfigurationModal> GetById(int Id);
        Task<bool> SoftDelete(int Id);
        Task<UpdateTemplateConfigurationModal> UpdateTemplateConfiguration(UpdateTemplateConfigurationModal modal);
    }
}