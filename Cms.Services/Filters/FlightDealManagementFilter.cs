namespace Cms.Services.Filters
{
    public class FlightDealManagementFilter:CommonFilter
    {
        public int? TemplateDetailsId { get; set; }
        public int? PortalId { get; set; }
        public string DealType { get; set; }
        public string AirlineName { get; set; }
        public string AirlineCode { get; set; }
        public string OriginName { get; set; }
        public string Origin { get; set; }
        public string DestinationName { get; set; }
        public string Destination { get; set; }
        public string TripType { get; set; }
        public decimal? DealAmountFrom { get; set; }
        public decimal? DealAmountTo { get; set; }
        public decimal? DealAmount { get; set; }
        public string ClassType { get; set; }
        public string TemplateType { get; set; }
        public string TemplateName { get; set; }
        public string PortalCode { get; set; }
        public string PortalName { get; set; }
        
    }
}
