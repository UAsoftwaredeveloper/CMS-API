using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.DataClasses
{
    [Table("AirportDetails")]
    public class AirportDetails:Entity
    {
        public string AirportCode {get;set;}
        public string AirportName {get;set;}
        public string CountryCode {get;set;}
        public string CountryName {get;set;}
        public string StateCode {get;set;}
        public string StateName {get;set;}
        public string CityCode  {get;set;}
        public string CityName  { get; set; }

    }
}
