using Cms.Services.Filters;
using Cms.Services.Models.OpenAPIDataModel.TemplateDetails;
using Cms.Services.Models.TemplateDetails;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface ITemplateDetailsDataService
    {
        Task<List<SiteMapDetail>> GetAllSiteMapDetails(TemplateDetailsDataFilter filter);
        Task<List<TemplateDetailsData>> GetAllTemplateDetails(TemplateDetailsDataFilter filter);
        Task<TemplateDetailsData> GetSingleAllTemplateDetails(TemplateDetailsDataFilter filter);
    }
}