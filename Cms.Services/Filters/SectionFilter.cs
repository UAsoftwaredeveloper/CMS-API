namespace Cms.Services.Filters
{
    public class SectionFilter:CommonFilter
    {
        public string TemplateName { get; set; }
        public string TemplateType { get; set; }
        public string PortalName { get; set; }
        

        public string Title { get; set; }

        public int DisplayOrder { get; set; }
        public bool ShowOnHomePage { get; set; }
        public int? TemplateId {  get; set; }
    }
}
