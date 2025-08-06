using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class TemplateConfigurationRepository:Repository<TemplateConfiguration>,ITemplateConfigurationRepository
    {

        public TemplateConfigurationRepository(CMSDBContext cMSDBContext):base(cMSDBContext) 
        {
            
        }
    }
}
