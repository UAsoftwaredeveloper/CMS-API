using Cms.Services.Models.Common;
using Cms.Services.Models.Portals;
using System;

namespace Cms.Services.Models.CouponMaster
{
    public class CouponMasterModal: EntityModal
    {
        public int? PortalId { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public string CouponType { get; set; } // Percentage, Flat-Rate, BOGO
        public decimal DiscountValue { get; set; }
        public decimal DiscountMaxValue { get; set; }
        public string TermsAndCondition { get; set; } // terms about coupon.
        public string CouponName { get; set; } // Discount name.
        public string ImageUrls { get; set; } // image thumbs.
        public string ServiceCategory { get; set; } // Flights, Hotels, Car Rentals, etc.
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Eligibility { get; set; } // All, Signup, RegisteredUsers, etc.
        public decimal? MinBookingThreshold { get; set; }
        public decimal? MinOrderAmount { get; set; }
        public decimal? MaxOrderAmount { get; set; }
        public string Criteria { get; set; } // JSON serialized service-based criteria
        public bool IsMultiUse { get; set; } = false;
        public int? MaxUses { get; set; } // Null means unlimited
        public string Status { get; set; } // Active, Expired, Used
        public bool IsSpecial { get; set; }
        public bool? ShowOnHomePage { get; set; }
        public bool CallOnly { get; set; }
        public virtual PortalModal Portal { get; set; } = null;
    }
}
