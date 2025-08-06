using System.ComponentModel.DataAnnotations.Schema;
using Cms.Services.Models.Common;
using Cms.Services.Models.TemplateMaster;

namespace Cms.Services.Models.TemplateDetails
{
    public class CreateTemplateDetailsModal : CreateModal
    {

        public int? TemplateId { get; set; }
        public int? PortalId { get; set; }
        /// <summary>
        /// for blog purpose only
        /// </summary>
        public string TemplateCategory { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaData { get; set; }
        public string PageName { get; set; }
        public string PageCode { get; set; }
        public string FromName { get; set; }
        public string FromCode { get; set; }
        public string ToName { get; set; }
        public string ToCode { get; set; }
        public string Url { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string FromCountryCode { get; set; }
        public string FromCountryName { get; set; }
        public string ToCountryCode { get; set; }
        public string ToCountryName { get; set; }
        public bool UseSlider { get; set; }

        public bool UseSliderOnDesktop { get; set; }

        public bool UseSliderOnMobile { get; set; }

        public bool ShowOnHomePage { get; set; }

        public bool ShowAirLines { get; set; }

        public bool ShowDestinations { get; set; }

        public bool ShowThemes { get; set; }

        public bool ShowDeals { get; set; }
        public bool ShowBlogs { get; set; }
        public bool ShowHolidayPackages { get; set; }
        public string ImageUrls { get; set; }

        public bool ShowSectionOverView { get; set; }

        public bool ShowBreadcrumb { get; set; }
        public bool ShowTempratureWidget { get; set; }
        public bool? Approved { get; set; }
    }

}
