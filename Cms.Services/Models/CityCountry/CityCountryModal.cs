using Cms.Services.Models.Common;

namespace Cms.Services.Models.CityCountry
{
    public class CityCountryModal : EntityModal
    {
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string CityCode { get; set; }
        public string CountryCode { get; set; }
    }

}
