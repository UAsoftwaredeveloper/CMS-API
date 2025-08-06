using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.DataClasses
{
    [Table("Portals")]
    public class Portals:Entity
    {
        public string Name { get; set; }    
        public string PortalCode { get; set; }    
        public string Description { get; set; }

    }
}
