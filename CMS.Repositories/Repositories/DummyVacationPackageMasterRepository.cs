using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class DummyVacationPackageMasterRepository:Repository<DummyVacationPackageMaster>,IDummyVacationPackageMasterRepository
    {
        public DummyVacationPackageMasterRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
