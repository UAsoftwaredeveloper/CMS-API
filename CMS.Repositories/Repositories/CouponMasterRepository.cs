using CMS.Repositories.Interfaces;
using DataManager;
using DataManager.DataClasses;

namespace CMS.Repositories.Repositories
{
    public class CouponMasterRepository : Repository<CouponMaster>, ICouponMasterRepository
    {
        public CouponMasterRepository(CMSDBContext cMSDBContext):base(cMSDBContext) { }
    }
}
