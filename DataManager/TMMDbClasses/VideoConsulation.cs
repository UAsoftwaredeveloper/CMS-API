using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.TMMDbClasses
{
    [Table("VideoConsulation")]
    public class VideoConsulation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public DateTime Created_On { get; set; }
        public string CustomerIp { get; set; }
        public string DeviceType { get; set; }
        public DateTime? ConsultationDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? PortalId { get; set; }
    }
}
