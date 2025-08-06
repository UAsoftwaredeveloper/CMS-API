using Cms.Services.Models.Common;

namespace Cms.Services.Models.Portals
{
    public class UpdatePortalModal:UpdateModal
    {
        public string Name {  get; set; }
        public string PortalCode {  get; set; }
        public string Description { get; set; }
    }
}
