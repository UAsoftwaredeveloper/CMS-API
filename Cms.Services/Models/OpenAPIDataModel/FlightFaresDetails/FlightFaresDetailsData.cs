using Cms.Services.Models.Common;
using System.Collections.Generic;

namespace Cms.Services.Models.OpenAPIDataModel.FlightFaresDetails
{
    public class FlightFaresDetailsData : EntityModal
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
        public string OutBoundClass { get; set; }
        public string OutBoundCabinClassCode { get; set; }
        public string OutBoundCabinClassName { get; set; }
        public int? OutBoundNoSeats { get; set; }
        public int? OutBoundFltNum { get; set; }
        public string OutBoundEquipType { get; set; }
        public string OutBoundActualTime { get; set; }
        public string OutBoundElapsedTime { get; set; }
        public string OutBoundDepTerminal { get; set; }
        public string OutBoundDepAirpCode { get; set; }
        public string OutBoundDepAirpName { get; set; }
        public string OutBoundDepCityName { get; set; }
        public string OutBoundDepCityCode { get; set; }
        public string OutBoundDepStateName { get; set; }
        public string OutBoundDepCountryName { get; set; }
        public string OutBoundDepCountryCode { get; set; }
        public string OutBoundDepDate { get; set; }
        public string OutBoundDepTime { get; set; }
        public string OutBoundArrTerminal { get; set; }
        public string OutBoundArrAirpCode { get; set; }
        public string OutBoundArrAirpName { get; set; }
        public string OutBoundArrCityName { get; set; }
        public string OutBoundArrCityCode { get; set; }
        public string OutBoundArrStateName { get; set; }
        public string OutBoundArrCountryName { get; set; }
        public string OutBoundArrCountryCode { get; set; }
        public string OutBoundArrDate { get; set; }
        public string OutBoundArrTime { get; set; }
        public string OutBoundBaggageInfo { get; set; }
        public string InBoundClass { get; set; }
        public string InBoundCabinClassCode { get; set; }
        public string InBoundCabinClassName { get; set; }
        public int? InBoundNoSeats { get; set; }
        public int? InBoundFltNum { get; set; }
        public string InBoundEquipType { get; set; }
        public string InBoundActualTime { get; set; }
        public string InBoundElapsedTime { get; set; }
        public string InBoundDepTerminal { get; set; }
        public string InBoundDepAirpCode { get; set; }
        public string InBoundDepAirpName { get; set; }
        public string InBoundDepCityName { get; set; }
        public string InBoundDepCityCode { get; set; }
        public string InBoundDepStateName { get; set; }
        public string InBoundDepCountryName { get; set; }
        public string InBoundDepCountryCode { get; set; }
        public string InBoundDepDate { get; set; }
        public string InBoundDepTime { get; set; }
        public string InBoundArrTerminal { get; set; }
        public string InBoundArrAirpCode { get; set; }
        public string InBoundArrAirpName { get; set; }
        public string InBoundArrCityName { get; set; }
        public string InBoundArrCityCode { get; set; }
        public string InBoundArrStateName { get; set; }
        public string InBoundArrCountryName { get; set; }
        public string InBoundArrCountryCode { get; set; }
        public string InBoundArrDate { get; set; }
        public string InBoundArrTime { get; set; }
        public string InBoundBaggageInfo { get; set; }
    }
}
