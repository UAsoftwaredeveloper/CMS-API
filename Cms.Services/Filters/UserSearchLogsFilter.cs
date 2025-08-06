namespace Cms.Services.Filters
{
    public class UserSearchLogsFilter:CommonFilter
    {
        public string PortalIds { get; set; }
        public string PageType { get; set; }
        public string PageUrl { get; set; }
        public string SearchedKeywords { get; set; }
    }
}
