using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.DataClasses
{
    [Table("Users")]
    public class Users:Entity
    {
        [ForeignKey(nameof(UserRole))]
        public int? RoleId {  get; set; }
       
        [StringLength(200)]
        public string UserName {  get; set; }
       
        public string Password { get; set; }
       
        public string Email { get; set; }
       
        public string Phone { get; set; }
       
        public string PhoneNumber { get; set; }

        public virtual UserRole UserRole { get; set; }

    }
}
