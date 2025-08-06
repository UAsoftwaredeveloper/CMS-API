namespace Cms.Services.Filters
{
    public class SectionContentFilter:CommonFilter
    {
        public int SectionId {  get; set; }
        public string SectionType { get; set; }
        public string SectionTitle { get; set; }
        public string TemplateName { get; set; }
        public string TemplateType { get; set; }
        public string PortalName { get; set; }

        public string Title { get; set; }

        public int DisplayOrder { get; set; }
        public bool ShowOnHomePage { get; set; }
    }
}
