namespace Cms.Services.Filters
{
    public class DummyVacationPackageMasterFilter:CommonFilter
    {
        public string Country { get; set; }
        public string Continent { get; set; }
        public string City { get; set; }
        public string PackageType { get; set; }
    }
}
