using System;

namespace Cms.Services.Models.Common
{
    public abstract class CreateModal
    {
        public int Id { get; set; }
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; } = false;
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
    }
}
