using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.DataClasses
{
    [Table("SectionContent")]
    public class SectionContent:Entity
    {
       
        [ForeignKey(nameof(Section))]
        public int? SectionId {  get; set; }
       
        public string Title { get; set; }
        public string ContentHeading { get; set; }
       
        public string ShortDescription {  get; set; }
       
        public string FullDescription {  get; set; }
       
        public string ImageUrls {  get; set; }
       
        public string HyperLink { get; set; }
       
        public bool ShowOnHomePage { get; set; }
        public int DisplayOrder { get; set; }
        public virtual Section Section { get; set; }
    }
}
