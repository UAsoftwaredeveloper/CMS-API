using Cms.Services.Models.Common;

namespace Cms.Services.Models.MasterAirlines
{
    public class MasterAirlinesModal : EntityModal
    {
        public string MasterID { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
        public int Rating { get; set; }
        public bool IsLcc { get; set; }
    }

}
