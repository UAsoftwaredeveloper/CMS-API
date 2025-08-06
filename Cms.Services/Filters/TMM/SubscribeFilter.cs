namespace Cms.Services.Filters.TMM
{
    public class SubscribeFilter:CommonFilter
    {
        public string Email { get; set; }
        public int PortalID { get; set; }
        public int? PageId { get; set; }
        public string PageName { get; set; }

    }
}
