using System;

namespace Cms.Services.Filters.TMM
{
    public class PriceTrackingCustomerInfoFilter : CommonFilter
    {
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string DeviceType { get; set; }
        public string OriginCode { get; set; }
        public string DestinationCode { get; set; }
        public DateTime? DepartureDateTo { get; set; }
        public DateTime? ReturnDateTo { get; set; }
        public DateTime? DepartureDateFrom { get; set; }
        public DateTime? ReturnDateFrom { get; set; }
        public string CabinClass { get; set; }


    }
}
