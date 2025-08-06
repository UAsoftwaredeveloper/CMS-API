using System;

namespace Cms.Services.Filters.TMM
{
    public class VideoConsulationFilter : CommonFilter
    {
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Date { get; set; }
        public bool? SortAscending { get; set; }
        public bool? SortDescending { get; set; }
        public bool? UpComing { get; set; }
        public bool? Completed { get; set; }

    }
}
