using Cms.Services.Models.Common;

namespace Cms.Services.Models.SectionContent
{
    public class UpdateSectionContentModal : UpdateModal
    {

        public int? SectionId { get; set; }

        public string ContentHeading { get; set; }
        public string Title { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public string ImageUrls { get; set; }

        public string HyperLink { get; set; }
        public int DisplayOrder { get; set; }
    }
}
