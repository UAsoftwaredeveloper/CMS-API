using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.DataClasses
{
    public class SectionType:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
