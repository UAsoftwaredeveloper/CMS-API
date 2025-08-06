using System.ComponentModel.DataAnnotations.Schema;
using Cms.Services.Models.Common;

namespace Cms.Services.Models.TemplateConfiguration
{
    public class UpdateTemplateConfigurationModal : UpdateModal
    {

        public int? TemplateDetailsId { get; set; }

        public string ButtonForColor { get; set; }

        public string ButtonBackgroundColor { get; set; }

        public string HeadingColor { get; set; }

        public string LabelColor { get; set; }

        public string HyperLinkColor { get; set; }

        public bool Mobile { get; set; } = true;

        public bool Desktop { get; set; } = true;

    }
}
