using System;

namespace Cms.Services.Filters.TMM
{
    public class HotelSearchDetailsFilter : CommonFilter
    {
        public string Destination { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? CheckInFrom { get; set; }
        public DateTime? CheckInTo { get; set; }
        public DateTime? CheckOutFrom { get; set; }
        public DateTime? CheckOutTo { get; set; }
        public int? RoomCount { get; set; }
        public int? AdultCount { get; set; }
        public int? ChildCount { get; set; }
        public string DeviceType { get; set; }
        public string UTM_Source { get; set; }
        public string UTM_Campaign { get; set; }
        public int? PortalID { get; set; }

    }
}
