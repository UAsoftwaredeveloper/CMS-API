namespace Cms.Services.Models.TMMModals
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class QuotationEmailSupportModal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15)]
        public string CustomerPhone { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerEmail { get; set; }

        [Required]
        [StringLength(100)]
        public string OrigineAirport { get; set; }

        [StringLength(10)]
        public string OrigineAirportCode { get; set; }

        [StringLength(10)]
        public string OrigineAirportCityCode { get; set; }

        [StringLength(100)]
        public string OrigineAirportCityName { get; set; }

        //public string OrigineAirportCountryCode { get; set; }

        [NotMapped]
        public string OrigineAirportCountryName { get; set; }

        [Required]
        [StringLength(100)]
        public string DestinationAirport { get; set; }

        //public string DestinationAirportCode { get; set; }
        //public string DestinationAirportCityCode { get; set; }
        //public string DestinationAirportCityName { get; set; }
        //public string DestinationAirportCountryCode { get; set; }
        //public string DestinationAirportCountryName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Adult count must be at least 1.")]
        public int AdultCount { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Child count cannot be negative.")]
        public int? ChildCount { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Day count must be at least 1.")]
        public int DayCount { get; set; }

        [StringLength(300)]
        public string EnqueryDetails { get; set; }
        public string CustomerIp { get; set; }
        public string DeviceType { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }

        public int? CreatedBy { get; set; }

        public int? PortalId { get; set; }
        public int? UserId { get; set; }
        public string UserEmail { get; set; }
    }
}
