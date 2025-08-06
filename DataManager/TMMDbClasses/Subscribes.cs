using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
namespace DataManager.TMMDbClasses
{
    [Table("Subscribes")]
    public class Subscribe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Column("ID")]
        public int ID { get; set; }
        [Column("UserId")]
        [ForeignKey(nameof(Users))]
        public int UserId { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("CreationTime")]
        public DateTime CreationTime { get; set; }
        [Column("PortalID")]
        public int PortalID { get; set; }
        [Column("PageId")]
        public int? PageId { get; set; }
        [Column("DeviceType")]
        public string DeviceType { get; set; }
        [Column("ServiceType")]
        public string ServiceType { get; set; }
        [Column("SubscriptionType")]
        public string SubscriptionType { get; set; }
        [Column("PageName")]
        public string PageName { get; set; }
        [Column("Location")]
        public string Location { get; set; }
        [Column("BrochureName")]
        public string BrochureName { get; set; }
        [Column("IsActive")]
        public bool IsActive { get; set; }
        public Users Users { get; set; }

    }
    [Table("Contactus")]
    public class ContactUs
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int ID { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Comment")]
        public string Comment { get; set; }
        [Column("CreationTime")]
        public DateTime CreationTime { get; set; }
        [Column("Ref_No")]
        public string Ref_No { get; set; }
        [Column("PortalID")]
        public int PortalID { get; set; }
        [Column("MobileNo")]
        public string MobileNo { get; set; }

    }
}
