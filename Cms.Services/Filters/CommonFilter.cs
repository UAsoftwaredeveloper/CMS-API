using System;

namespace Cms.Services.Filters
{
    public abstract class CommonFilter:PaginationFilter
    {
        public int? Id {  get; set; }
        public int? CreatedBy {  get; set; }
        public int? ModifiedBy {  get; set; }
        public string Query { get; set; }
        public bool? Deleted { get; set; }
        public bool? Active { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        
    }
}
