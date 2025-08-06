using System;

namespace Cms.Services.Models.TMMModals
{
    public class UsersModal
    {
        public int Id { get; set; }
        public string AuthProvider { get; set; } = string.Empty;
        public string ProviderKey { get; set; } = string.Empty;
        public string AuthKey { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string EmailVerificationCode { get; set; } = string.Empty;
        public DateTime? EmailVerificationCodeExpiry { get; set; }
        public string PhoneVerificationCode { get; set; } = string.Empty;
        public DateTime? PhoneVerificationCodeExpiry { get; set; }
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string FacebookAuthToken { get; set; } = string.Empty;
        public string GoogleAuthToken { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        public string TimeZone { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string DOB { get; set; } = string.Empty;
        public string DOAnniversary { get; set; } = string.Empty;
        public string PassportDetails { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string zip { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public bool RememberMe { get; set; } = true;
        public bool Active { get; set; } = true;
        public bool EmailVerified { get; set; } = false;
        public bool PhoneVerified { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = string.Empty;
        public string UpdatedBy { get; set; } = string.Empty;
        public string ProfilePhotoUrl { get; set; }
    }
}
