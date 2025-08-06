using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class SectionContentRepository:Repository<SectionContent>, ISectionContentRepository
    {
        public SectionContentRepository(CMSDBContext context) : base(context) { }
    }
}
