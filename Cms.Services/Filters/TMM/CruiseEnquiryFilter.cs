using System;

namespace Cms.Services.Filters.TMM
{
    public class CruiseEnquiryFilter:CommonFilter
    {
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string NeedAccessibleCabin { get; set; }
        public string Airports { get; set; }
        public int RoomAdultCount { get; set; }
        public int RoomChildCount { get; set; }
        public string SelectedCabin { get; set; }
        public string SelectedCabinCount { get; set; }
        public string CruiseLineCode { get; set; }
        public string CruiseLineName { get; set; }
        public string ShipCode { get; set; }
        public string ShipName { get; set; }
        public string Departure { get; set; }
        public string Ref_No { get; set; }
        public int PortalID { get; set; }
        public string Duration { get; set; }
    }
}
