using Cms.Services.Models.Common;
using Cms.Services.Models.Portals;
using Cms.Services.Models.TemplateDetails;
using System;

namespace Cms.Services.Models.FlightDealManagement
{
    public class UpdateFlightDealManagementModal : UpdateModal
    {
        public int PortalId { get; set; }
        public string DealType { get; set; }
        public string AirlineName { get; set; }
        public string AirlineCode { get; set; }
        public string OriginName { get; set; }
        public string Origin { get; set; }
        public string DestinationName { get; set; }
        public string Destination { get; set; }
        public string TripType { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal DealAmount { get; set; }
        public string ClassType { get; set; }
        public string ImageUrls {  get; set; }
        public int DisplayOrder { get; set; }
    }
}
