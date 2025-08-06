namespace Cms.Services.Filters.TMM
{
    public class EnqueryPageDetailsFilter : CommonFilter
    {
        public string Email { get; set; }
        public int? PageId { get; set; }
        public string PageName { get; set; }
        public string PageUrl { get; set; }
        public string PageType { get; set; } = string.Empty;

    }
}
