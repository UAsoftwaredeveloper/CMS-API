namespace Cms.Services.Filters
{
    public class MenuMasterFilter : CommonFilter
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Roles { get; set; }
        public string ParentMenu { get; set; }

    }
}
