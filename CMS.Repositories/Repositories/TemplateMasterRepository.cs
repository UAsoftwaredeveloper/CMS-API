using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class TemplateMasterRepository:Repository<TemplateMaster>,ITemplateMasterRepository
    {

        public TemplateMasterRepository(CMSDBContext cMSDBContext):base(cMSDBContext) 
        {
            
        }
    }
}
