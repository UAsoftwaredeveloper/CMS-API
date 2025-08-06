using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.TMMDbClasses
{
    [Table("CruiseEnquiry")]
    public class CruiseEnquiry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Comment { get; set; }
        public string NeedAccessibleCabin { get; set; }
        public string Airports { get; set; }
        public string NeedNewsletter { get; set; }
        public int RoomAdultCount { get; set; }
        public int RoomChildCount { get; set; }
        public string SelectedCabin { get; set; }
        public string SelectedCabinCount { get; set; }
        public string FlightGuidId { get; set; }
        public string MasterCruiseId { get; set; }
        public string CruiseLineCode { get; set; }
        public string CruiseLineName { get; set; }
        public string ShipCode { get; set; }
        public string ShipName { get; set; }
        public string Departure { get; set; }
        public string IPAddress { get; set; }
        public DateTime CreationTime { get; set; }
        public string Ref_No { get; set; }
        public int PortalID { get; set; }
        public string Duration { get; set; }
    }
}
