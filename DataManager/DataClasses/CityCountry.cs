using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.DataClasses
{
    [Table("City_Country")]
    public class CityCountry : Entity
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
