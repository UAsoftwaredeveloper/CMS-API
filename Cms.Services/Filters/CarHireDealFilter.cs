namespace Cms.Services.Filters
{
    public class CarHireDealFilter:CommonFilter
    {
        public int? TemplateDetailsId { get; set; }
        public int? PortalId { get; set; }
        public string DealType { get; set; }
        public string DealName { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal? DealAmountFrom { get; set; }
        public decimal? DealAmountTo { get; set; }
        public decimal? DealAmount { get; set; }
        public string TemplateType { get; set; }
        public string TemplateName { get; set; }
        public string PortalCode { get; set; }
        public string PortalName { get; set; }
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
