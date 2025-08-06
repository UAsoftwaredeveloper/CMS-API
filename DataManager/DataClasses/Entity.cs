using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.DataClasses
{
    public abstract class Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        public bool? Active { get; set; }=true;
       
        public bool? Deleted { get; set; } = false;
       
        public DateTime CreatedOn { get; set; }
       
        public DateTime? ModifiedOn { get; set; }
       
        public int? CreatedBy { get; set; }
       
        public int? ModifiedBy { get; set; }
    }
}
