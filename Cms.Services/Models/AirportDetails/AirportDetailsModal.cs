using Cms.Services.Models.Common;

namespace Cms.Services.Models.AirportDetails
{
    public class AirportDetailsModal : EntityModal
    {
        public string AirportCode { get; set; }
        public string AirportName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
    }

}
