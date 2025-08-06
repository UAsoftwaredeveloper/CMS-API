using System;

namespace Cms.Services.Filters.TMM
{
    public class FlightSearchDetailsFilter : CommonFilter
    {
        public string Depart { get; set; }
        public string Return { get; set; }
        public DateTime? DepartDateFrom { get; set; }
        public DateTime? DepartDateTo { get; set; }
        public DateTime? ReturnDateFrom { get; set; }
        public DateTime? ReturnDateTo { get; set; }
        public string CabinClass { get; set; }
        public int? TripType { get; set; }
        public int? Adults { get; set; }
        public int? Children { get; set; }
        public int? Infants { get; set; }
        public string DeviceType { get; set; }
        public string UTM_Source { get; set; }
        public int? PortalID { get; set; }
        public string UTM_Campaign { get; set; }
        public string PaxType { get; set; }


    }
}
