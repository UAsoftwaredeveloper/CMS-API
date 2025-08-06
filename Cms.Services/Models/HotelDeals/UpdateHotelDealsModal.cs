using Cms.Services.Models.Common;
using System;

namespace Cms.Services.Models.HotelDeals
{
    public class UpdateHotelDealsModal : UpdateModal
    {
        public int PortalId { get; set; }
        public string HotelId { get; set; }
        public string HotelCode { get; set; }
        public string HotelName { get; set; }
        public string DealType { get; set; }
        public string LocationTitle { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public decimal Price { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        /// <summary>
        /// Check-in From Date
        /// </summary>
        public DateTime? From { get; set; }
        /// <summary>
        /// Check-in To Date
        /// </summary>
        public DateTime? To { get; set; }
        public int StarRating { get; set; } = 0;
        public string ImageUrls { get; set; }
        public int DisplayOrder { get; set; }
    }
}
