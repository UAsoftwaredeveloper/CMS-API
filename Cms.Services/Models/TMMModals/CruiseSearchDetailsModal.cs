using System;

namespace Cms.Services.Models.TMMModals
{
    public class CruiseSearchDetailsModal
    {
        public long Id { get; set;}
        public string SearchGUID { get; set;}
        public string sea { get; set;}
        public string package { get; set;}
        public string area { get; set;}
        public string cruiseline { get; set;}
        public string ship { get; set;}
        public string duration { get; set;}
        public string departure { get; set;}
        public string arrival { get; set;}
        public DateTime? SearchDate { get; set;}
        public string IPAddress { get; set;}
        public string DeviceType { get; set;}
        public int? PortalID { get; set; }

    }
}
