using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class SectionRepository:Repository<Section>,ISectionRepository
    {
        public SectionRepository(CMSDBContext context) : base(context) { }
    }
}
