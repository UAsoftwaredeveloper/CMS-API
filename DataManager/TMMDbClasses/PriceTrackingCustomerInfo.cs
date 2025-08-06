using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.TMMDbClasses
{
    [Table("PriceTrackingCustomerInfo")]
    public class PriceTrackingCustomerInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerIpAddress { get; set; }
        public string CustomerDeviceType { get; set; }
        public string OriginCode { get; set; }
        public string DestinationCode { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string CabinClass { get; set; }
        public decimal? EmailOTP { get; set; }
        public decimal? MobileOTP { get; set; }
        public DateTime? EmailOTPExpiryTime { get; set; }
        public DateTime? MobilOTPExpiryTime { get; set; }
        public bool? IsEmailOtpSent { get; set; }
        public bool? IsMobileOtpSent { get; set; }
        public bool? EmailVerified { get; set; }
        public bool? MobileVerified { get; set; }
        public bool? TrackPrice { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal? TriggerPrice { get; set; }
        public string CurrencyType { get; set; }
        public int? CreatedBy { get; set; }


    }
}
