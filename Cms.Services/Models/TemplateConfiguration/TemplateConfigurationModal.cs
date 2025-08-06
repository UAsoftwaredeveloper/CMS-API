using System.ComponentModel.DataAnnotations.Schema;
using Cms.Services.Models.Common;
using Cms.Services.Models.OpenAPIDataModel.TemplateDetails;
using Cms.Services.Models.TemplateDetails;

namespace Cms.Services.Models.TemplateConfiguration
{
    public class TemplateConfigurationModal : EntityModal
    {

        public int? TemplateDetailsId { get; set; }

        public string ButtonForColor { get; set; }

        public string ButtonBackgroundColor { get; set; }

        public string HeadingColor { get; set; }

        public string LabelColor { get; set; }

        public string HyperLinkColor { get; set; }

        public bool Mobile { get; set; } = true;

        public bool Desktop { get; set; } = true;
        public virtual TemplateDetailsModal TemplateDetails { get; set; }

    }
}
