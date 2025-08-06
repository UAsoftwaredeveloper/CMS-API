using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cms.Services.Models.Common;

namespace Cms.Services.Models.CarHireDeals
{
    public class CreateCarHireDealsModel:CreateModal
    {
        public int? PortalId { get; set; }
        public string DealType { get; set; }
        public string DealName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public decimal PricePerDay { get; set; }
        public string ImageUrls { get; set; }
        public int DisplayOrder { get; set; }
        public string CountryCode { get; set; }
        public string OriginLongitude { get; set; }
        public string DestinationLongitude { get; set; }
        public string OriginLatitude { get; set; }
        public string DestinationLatitude { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string OriginCityCode { get; set; }
        public string DestinationCityCode { get; set; }
        public string AirportCode { get; set; }
    }
}
