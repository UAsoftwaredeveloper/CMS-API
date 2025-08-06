using Cms.Services.Models.Common;
using Cms.Services.Models.HolidayPackages;

namespace Cms.Services.Models.PackageItenaries
{
    public class PackageItenariesModal : EntityModal
    {
        public string Title { get; set; }
        public int? PackageId { get; set; }
        public string ItenaryType { get; set; }
        public decimal? Nights { get; set; }
        public string BoardBasis { get; set; }
        public string RoomsType { get; set; }
        public string HotelName { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }
        public string Duration { get; set; }
        public string Highlights { get; set; }
        public string Overview { get; set; }
        public string FullDetails { get; set; }
        public string SightSeeingLocations { get; set; }
        public string Distance { get; set; }
        public string ImageUrls { get; set; }
        public string GoLocation { get; set; }
        public int? RoomCount { get; set; }
        public bool? IsSightSeeing { get; set; }
        public bool? IsHotel { get; set; }
        public bool? IsFlight { get; set; }
        public virtual HolidayPackagesModal HolidayPackages { get; set; }=null;

    }
}
