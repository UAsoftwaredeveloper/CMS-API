namespace Cms.Services.Filters.TMM
{
    public class UsersFilter:CommonFilter
    {
        public string Name {  get; set; }
        public string CompanyName {  get; set; }
        public string Email {  get; set; }
        public bool? Google {  get; set; }
        public bool? Facebook {  get; set; }
        public bool? TMM {  get; set; }
        public bool? Live {  get; set; }
        public bool? EmailVerified {  get; set; }
        public bool? PhoneVerified {  get; set; }
        public bool? Company {  get; set; }

        
    }
}
