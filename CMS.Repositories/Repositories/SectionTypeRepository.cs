using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class SectionTypeRepository:Repository<SectionType>,ISectionTypeRepository
    {

        public SectionTypeRepository(CMSDBContext cMSDBContext):base(cMSDBContext) 
        {
            
        }
    }
}
