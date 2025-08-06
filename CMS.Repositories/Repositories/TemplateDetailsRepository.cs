using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class TemplateDetailsRepository:Repository<TemplateDetails>, ITemplateDetailsRepository
    {
        public TemplateDetailsRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
