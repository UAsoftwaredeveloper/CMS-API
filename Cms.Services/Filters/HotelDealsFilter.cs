namespace Cms.Services.Filters
{
    public class HotelDealsFilter:CommonFilter
    {
        public int? TemplateDetailsId { get; set; }
        public int? PortalId { get; set; }
        public string DealType { get; set; }
        public decimal? DealAmountFrom { get; set; }
        public decimal? DealAmountTo { get; set; }
        public decimal? DealAmount { get; set; }
        public string TemplateType { get; set; }
        public string TemplateName { get; set; }
        public string PortalCode { get; set; }
        public string PortalName { get; set; }
        public string HotelCode {  get; set; }
        public string HotelName { get; set;}
        
    }
}
