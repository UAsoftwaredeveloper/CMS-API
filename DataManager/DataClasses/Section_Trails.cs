using System;

namespace DataManager.DataClasses
{
    public class Section_Trails 
    {
        public int Id { get; set; }

        public bool? Active { get; set; } = true;

        public bool? Deleted { get; set; } = false;

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }
        public int? TemplatDetailsId { get; set; }
        public int? SectionTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FullDescription { get; set; }
        public bool UseSlider { get; set; } = false;
        public bool UseSliderOnDesktop { get; set; } = false;
        public bool UseSliderOnMobile { get; set; } = false;
        public bool ShowOnHomePage { get; set; } = true;
        public int DispalyOrder { get; set; } = 0;
        public int ItemsInRow { get; set; } = 1;
        public int ItemsInRowonMobile { get; set; } = 1;
        public bool IsCTASection { get; set; } = false;
        public string Caption {  get; set; }
        public string CaptionUrl {  get; set; }
    }
}
