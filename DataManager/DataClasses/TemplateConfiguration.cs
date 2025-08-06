using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.DataClasses
{
    [Table(name: "TemplateConfiguration")]
    public class TemplateConfiguration:Entity
    {
       
        [ForeignKey(nameof(TemplateDetails))]
        public int? TemplateDetailsId {  get; set; }
       
        public string ButtonForColor { get; set; }
       
        public string ButtonBackgroundColor { get; set; }
       
        public string HeadingColor { get; set; }
       
        public string LabelColor { get; set; }
       
        public string HyperLinkColor { get; set; }
       
        public bool Mobile { get; set; } = true;
       
        public bool Desktop { get; set; } = true;
        public virtual TemplateDetails TemplateDetails { get; set; }

    }
}
