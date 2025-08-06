namespace DataManager.DataClasses
{
    public class UserSearchLogs : Entity
    {
        public string SearchText { get; set; }
        public string SearchResults { get; set; }
        public string VisitedPageType { get; set; }
        public string VisitedPageName { get; set; }
        public string VisitedPageUrl { get; set; }
        public string IpAddress { get; set; }
        public string DeviceType { get; set; }
        public string DeviceDetails { get; set; }
        public int? UserId { get; set; }
        public int? PortalId { get; set; }
    }
}
