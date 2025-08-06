namespace Cms.Services.Filters
{
    public class TemplateConfigurationFilter:CommonFilter
    {
        public string TemplateName { get; set; }
        public string ButtonForColor { get; set; }
        public string ButtonBackgroundColor { get; set; }
        public string HeadingColor { get; set; }
        public string LabelColor { get; set; }
        public string HyperLinkColor { get; set; }
        public bool Mobile { get; set; } = true;
        public bool Desktop { get; set; } = true;
    }
}
