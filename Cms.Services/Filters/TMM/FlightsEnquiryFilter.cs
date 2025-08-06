namespace Cms.Services.Filters.TMM
{
    public class FlightsEnquiryFilter:CommonFilter
    {
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Ref_No { get; set; }
        public string DeviceType { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string DepartureDate { get; set; }
        public string CabinType { get; set; }
        public string TravellerCount { get; set; }
        public string PageType { get; set; }
        public int? PortalID { get; set; }

    }
}
