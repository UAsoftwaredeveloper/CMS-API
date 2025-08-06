using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.DataClasses
{
    [Table("MasterAirlines")]
    public class MasterAirlines : Entity
    {
        public string MasterID { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Code { get; set; }
        public int? Rating { get; set; }
        public bool IsLcc { get; set; }

    }
}
