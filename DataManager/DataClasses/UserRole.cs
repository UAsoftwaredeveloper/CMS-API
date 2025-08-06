using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.DataClasses
{
    [Table("Role")]
    public class UserRole:Entity
    {
        public string Name {  get; set; }

    }
}
