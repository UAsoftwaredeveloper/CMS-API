using Cms.Services.Models.Common;
using Cms.Services.Models.OpenAPIDataModel.PackageItenaries;
using Cms.Services.Models.PackageItenaries;
using System;
using System.Collections.Generic;

namespace Cms.Services.Models.HolidayPackages
{
    public class HolidayPackagesModal : EntityModal
    {
        /// <summary>
        /// portals where package will shows.
        /// </summary>
        public string PortalIds { get; set; }
        /// <summary>
        /// Templates where packgae will shows
        /// </summary>
        public string Url { get; set; }
        public string Keywords { get; set; }
        public string Title { get; set; }
        public string Point_of_Attraction { get; set; }
        public string MetaTitle { get; set; }
        public string Metakeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaData { get; set; }
        /// <summary>
        /// Name of package
        /// </summary>
        public string PackageName { get; set; }
        /// <summary>
        /// in nights
        /// </summary>
        public int Number_of_Nights { get; set; }
        public int Number_of_Days { get; set; }
        /// <summary>
        /// Origin package start city name
        /// </summary>
        public string OriginCityName { get; set; }
        /// <summary>
        /// Origin package start city code
        /// </summary>
        public string OriginCityCode { get; set; }
        /// <summary>
        /// Destination City Name
        /// </summary>
        public string DestinationCityName { get; set; }
        /// <summary>
        /// Destination City code
        /// </summary>
        public string DestinationCityCode { get; set; }
        /// <summary>
        /// Origin package start Country name
        /// </summary>
        public string OriginCountryName { get; set; }
        /// <summary>
        /// Origin package start city code
        /// </summary>
        public string OriginCountryCode { get; set; }
        /// <summary>
        /// Destination Country Name
        /// </summary>
        public string DestinationCountryName { get; set; }
        /// <summary>
        /// Destination Country code
        /// </summary>
        public string DestinationCountryCode { get; set; }
        public string LocationTitle { get; set; }
        /// <summary>
        /// currency for amount
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// package value
        /// </summary>
        public decimal? Amount { get; set; }
       /// <summary>
       /// Excursion starting amount
       /// </summary>
       public decimal? ExcursionStartingPrice { get; set; }
        /// <summary>
        /// image data
        /// </summary>
        public string ImageUrls { get; set; }
        /// <summary>
        /// Itenaries and hotel details
        /// </summary>
        public string PackageDetails { get; set; }
        public string ShortDescription { get; set; }
        public string ReferenceId { get; set; }
        public decimal PaxCount { get; set; }
        public decimal MarkUp { get; set; }

        public decimal StarRatings { get; set; }
        public string Exclusions { get; set; }
        public string Inclusions { get; set; }
        public string Conditions { get; set; }
        public string ImportantPolicies { get; set; }
        public string Misclenouse1 { get; set; }
        public string Misclenouse2 { get; set; }
        public string Misclenouse3 { get; set; }
        public string Misclenouse4 { get; set; }
        public string Misclenouse5 { get; set; }
        public string Discount{ get; set; }
        public string PackageIncluded { get; set; }
        public string SubTitle { get; set; }
        public string PackageTheme { get; set; }
        public bool? Approved { get; set; }
        public bool? ShowOnHomePage { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public virtual List<PackageItenariesModal> PackageItenaries { get; set; } = null;

    }
}
