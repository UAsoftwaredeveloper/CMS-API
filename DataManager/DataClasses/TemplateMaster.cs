using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DataManager.DataClasses
{
    [Table(name:"TemplateMaster")]
    public class TemplateMaster:Entity
    {
       
        public string Name { get; set; }
       
        public string Description { get; set; }
        [ForeignKey(nameof(Portals))]
        public int? PortalId {  get; set; }
        public string LanguageType {  get; set; }
        public virtual Portals Portal { get; set; }
        
    }
}
