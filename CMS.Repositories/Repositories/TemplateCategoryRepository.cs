using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class TemplateCategoryRepository:Repository<TemplateCategory>, ITemplateCategoryRepository
    {
        public TemplateCategoryRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
