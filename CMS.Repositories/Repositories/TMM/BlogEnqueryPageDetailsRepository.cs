using CMS.Repositories.Interfaces.TMM;
using DataManager;
using DataManager.TMMDbClasses;

namespace CMS.Repositories.Repositories.TMM
{
    public class BlogEnqueryPageDetailsRepository : Repository<BlogEnqueryPageDetails>, IBlogEnqueryPageDetailsRepository
    {
        public BlogEnqueryPageDetailsRepository(TMMDBContext tMMDBContext) : base(tMMDBContext) { }
    }
}
