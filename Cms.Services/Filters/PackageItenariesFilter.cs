namespace Cms.Services.Filters
{
    public class PackageItenariesFilter : CommonFilter
    {
        public string PortalIds { get; set; }
        public string PackageName { get; set; }
        public int? PackageId { get; set; }
        public string DayOfItenary {  get; set; }
    }
}
