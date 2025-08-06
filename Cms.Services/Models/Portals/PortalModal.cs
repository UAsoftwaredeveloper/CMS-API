using Cms.Services.Models.Common;

namespace Cms.Services.Models.Portals
{
    public class PortalModal:EntityModal
    {
        public string Name {  get; set; }
        public string PortalCode {  get; set; }
        public string Description { get; set; }
    }
}
