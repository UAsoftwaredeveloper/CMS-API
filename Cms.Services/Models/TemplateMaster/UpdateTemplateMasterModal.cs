using Cms.Services.Models.Common;

namespace Cms.Services.Models.TemplateMaster
{
    public class UpdateTemplateMasterModal : UpdateModal
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PortalId { get; set; }
        public string LanguageType { get; set; }
    }
}
