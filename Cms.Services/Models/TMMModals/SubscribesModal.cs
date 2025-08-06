using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cms.Services.Models.TMMModals
{
    public class SubscribeModal
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public DateTime CreationTime { get; set; }
        public int PortalID { get; set; }
        public int? PageId { get; set; }
        public string DeviceType { get; set; }
        public string ServiceType { get; set; }
        public string SubscriptionType { get; set; }
        public string PageName { get; set; }
        public string Location { get; set; }
        public string BrochureName { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public UsersModal Users {  get; set; }
    }

    public class ContactUsModal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public DateTime CreationTime { get; set; }
        public string Ref_No { get; set; }  
        public int PortalID { get; set; }
        public string MobileNo { get; set; }

    }
}
