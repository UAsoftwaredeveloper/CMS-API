using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.DataClasses
{
    [Table("TemplateCategory")]
    public class TemplateCategory : Entity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }

    }
}
