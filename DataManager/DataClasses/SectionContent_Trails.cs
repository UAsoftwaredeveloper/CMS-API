using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.DataClasses
{
    [Table("SectionContent_Trails")]
    public class SectionContent_Trails
    {
        public int Id { get; set; }

        public bool? Active { get; set; } = true;

        public bool? Deleted { get; set; } = false;

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }
        public int? SectionId {  get; set; }
       
        public string Title { get; set; }
        public string ContentHeading { get; set; }
       
        public string ShortDescription {  get; set; }
       
        public string FullDescription {  get; set; }
       
        public string ImageUrls {  get; set; }
       
        public string HyperLink { get; set; }
       
        public bool ShowOnHomePage { get; set; }
        public int DisplayOrder { get; set; }
    }
}
