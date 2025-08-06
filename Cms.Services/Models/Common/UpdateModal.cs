using System;

namespace Cms.Services.Models.Common
{
    public abstract class UpdateModal
    {
        public int Id { get; set; }
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; } = false;
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
