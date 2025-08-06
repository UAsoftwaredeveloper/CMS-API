using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.DataClasses
{
    [Table(name: "PackageItenaries_Trails")]
    public class PackageItenaries_Trails
    {
        public int Id { get; set; }

        public bool? Active { get; set; } = true;

        public bool? Deleted { get; set; } = false;

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }

        public string Title { get; set; }
        public int PackageId { get; set; }
        public string ItenaryType { get; set; }
        public decimal Nights { get; set; }
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
    }
}
