using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.TMMDbClasses
{
    [Table("HotelSearchDetails")]
    public class HotelSearchDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string SearchGUID { get; set; }
        public string Destination { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int? RoomCount { get; set; }
        public int? AdultCount { get; set; }
        public int? ChildCount { get; set; }
        public DateTime? SearchDate { get; set; }
        public string IPAddress { get; set; }
        public string DeviceType { get; set; }
        public string UTM_Source { get; set; }
        public int? PortalID { get; set; }

    }
}
