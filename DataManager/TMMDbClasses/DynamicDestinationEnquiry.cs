using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.TMMDbClasses
{
    [Table("DynamicDestinationEnquiry")]
    public class DynamicDestinationEnquiry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Comments { get; set; }
        public string SearchedText { get; set; }
        public int? UserId { get; set; }
        public int? PortalId { get; set; }
        public string RefrenceId { get; set; }
        public string DeviceType { get; set; }
        public string CustomerIp { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public int? CreatedBy { get; set; } = -1;
        public bool Deleted { get; set; } = false;
    }
}
