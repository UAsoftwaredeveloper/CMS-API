using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cms.Services.Models.Portals;

namespace Cms.Services.Models.OpenAPIDataModel.CarHireDealsData
{
    public class CarHireDealsData
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
        public string OriginName { get; set; }
        public string DestinationName { get; set; }
        public virtual PortalModal Portal { get; set; }
    }
}
