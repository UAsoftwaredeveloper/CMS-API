using Cms.Services.Models.Common;
using Cms.Services.Models.OpenAPIDataModel.TemplateDetails;
using Cms.Services.Models.Portals;
using Cms.Services.Models.TemplateDetails;
using System.Collections.Generic;

namespace Cms.Services.Models.TemplateMaster
{
    public class TemplateMasterModal : EntityModal
    {
        public int? PortalId {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LanguageType { get; set; }
        public PortalModal Portal { get; set; }
        public List<TemplateDetailsModal> TemplateDetails { get;set; }

    }
}
