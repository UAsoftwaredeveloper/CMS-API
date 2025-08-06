namespace Cms.Services.Filters
{
    public class TemplateDetailsFilter:CommonFilter
    {
        public string TemplateType { get; set; }
        public int? TemplateTypeId { get; set; }
        public int? PortalId { get; set; }
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public string MetaData { get; set; }

        public string BannerImageUrls { get; set; }

        public bool UseSlider { get; set; }

        public bool UseSliderOnDesktop { get; set; }

        public bool UseSliderOnMobile { get; set; }

        public bool ShowOnHomePage { get; set; }

        public int ItemsInRow { get; set; }

        public bool ShowAirLines { get; set; }

        public bool ShowDestinations { get; set; }

        public bool ShowThemes { get; set; }

        public bool ShowDeals { get; set; }

        public bool ShowSectionOverView { get; set; }

        public bool ShowTempratureWidget { get; set; }
        public bool? Approved { get; set; }

    }
}
