using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class CustomerReviewRatingsRepository : Repository<CustomerReviewRatings>, ICustomerReviewRatingsRepository
    {
        public CustomerReviewRatingsRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
