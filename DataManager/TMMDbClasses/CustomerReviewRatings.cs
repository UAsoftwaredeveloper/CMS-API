using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataManager.TMMDbClasses
{
    [Table("CustomerReviewRatings")]
    public class CustomerReviewRatings
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string PageType { get; set; }
        public string FileType { get; set; }
        public string FileUrl { get; set; }
        public string UserType { get; set; }
        [ForeignKey(nameof(Users))]
        public int? UserId { get; set; }
        public int? PortalId { get; set; }
        public string emailId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Ratings { get; set; }
        public string ReviewComments { get; set; }
        public string IPAddress { get; set; }
        public string DeviceType { get; set; }
        public bool Approved { get; set; }
        public string Title { get; set; }
        public string BookingId { get; set; }
        public DateTime Created_On { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public virtual Users Users { get; set; }
    }
}
