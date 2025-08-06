using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IBlogEnqueryPageDetailsService
    {
        Task<PaginatedList<BlogEnqueryPageDetailsModal>> GetAllBlogEnqueryPageDetails(BlogEnqueryPageDetailsFilter filter);
        Task<BlogEnqueryPageDetailsModal> GetById(int Id);
    }
}