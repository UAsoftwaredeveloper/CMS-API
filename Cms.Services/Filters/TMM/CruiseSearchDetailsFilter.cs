namespace Cms.Services.Filters.TMM
{
    public class CruiseSearchDetailsFilter:CommonFilter
    {
        public string sea { get; set;}
        public string package { get; set;}
        public string area { get; set;}
        public string cruiseline { get; set;}
        public string ship { get; set;}
        public string duration { get; set;}
        public string departure { get; set;}
        public string arrival { get; set;}
        public string DeviceType { get; set;}
        public int? PortalID { get; set; }
        public string UTM_Campaign { get; set; }

    }
}
