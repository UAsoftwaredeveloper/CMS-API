using Cms.Services.Models.Common;

namespace Cms.Services.Models.DummyVacationPackageMaster
{
    public class DummyVacationPackageMasterModal : EntityModal
    {
        public string VacationPackageTitle { get; set; }
        public string Country { get; set; }
        public string Continent { get; set; }
        public string City { get; set; }
        public string PackageType { get; set; }
        public string Nights { get; set; }
        public string Highlights { get; set; }
        public string Audience { get; set; }
        public decimal? Price { get; set; }
        public string Discount { get; set; }
        public string PackageIncluded { get; set; }
    }
}
