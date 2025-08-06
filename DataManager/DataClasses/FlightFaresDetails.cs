using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.DataClasses
{
    [Table(nameof(FlightFaresDetails))]
    public class FlightFaresDetails:Entity
    {
        public string Key { get; set; }
        public decimal? BaseFare { get; set; }
        public decimal? Taxes { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? markUp { get; set; }
        public decimal? GrandTotal { get; set; }
        public string Provider { get; set; }
        public bool? IsLcc { get; set; }
        public string ValCarrierCode { get; set; }
        public string ValCarrierName { get; set; }
        public string FareType { get; set; }
        public string Class { get; set; }
        public string CabinClassCode { get; set; }
        public string CabinClassName { get; set; }
        public int? NoSeats { get; set; }
        public int? FltNum { get; set; }
        public string EquipType { get; set; }
        public string ActualTime { get; set; }
        public string ElapsedTime { get; set; }
        public string DepTerminal { get; set; }
        public string DepAirpCode { get; set; }
        public string DepAirpName { get; set; }
        public string DepCityName { get; set; }
        public string DepCityCode { get; set; }
        public string DepStateName { get; set; }
        public string DepCountryName { get; set; }
        public string DepCountryCode { get; set; }
        public string DepDate { get; set; }
        public string DepTime { get; set; }
        public string ArrTerminal { get; set; }
        public string ArrAirpCode { get; set; }
        public string ArrAirpName { get; set; }
        public string ArrCityName { get; set; }
        public string ArrCityCode { get; set; }
        public string ArrStateName { get; set; }
        public string ArrCountryName { get; set; }
        public string ArrCountryCode { get; set; }
        public string ArrDate { get; set; }
        public string ArrTime { get; set; }
        public string BaggageInfo { get; set; }

    }
}
