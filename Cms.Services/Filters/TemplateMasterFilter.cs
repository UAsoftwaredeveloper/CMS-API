namespace Cms.Services.Filters
{
    public class TemplateMasterFilter:CommonFilter
    {
        public string TemplateName {  get; set; }
        public string PortalName {  get; set; }
        public int? PortalId {  get; set; }
    }
}
