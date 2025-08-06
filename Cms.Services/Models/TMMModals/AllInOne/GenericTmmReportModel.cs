using System;

namespace Cms.Services.Models.TMMModals.AllInOne
{
    public class GenericTmmReportModel
    {
        public int Id { get; set; }
        public string ReferenceNumber { get; set; }
        public string ReportName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string ServiceName { get; set; }
        public string ServiceType { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
