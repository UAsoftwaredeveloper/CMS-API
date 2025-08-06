using Cms.Services.Models.Common;

namespace Cms.Services.Models.TemplateCategory
{
    public class CreateTemplateCategoryModal:CreateModal
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
