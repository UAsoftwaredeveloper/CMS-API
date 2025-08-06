using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class PackageItenariesRepository : Repository<PackageItenaries>, IPackageItenariesRepository
    {
        public PackageItenariesRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
