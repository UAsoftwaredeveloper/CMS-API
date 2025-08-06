using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface ICustomerReviewRatingsService
    {
        Task<CustomerReviewRatingsModal> CreateCustomerReviewRatings(CustomerReviewRatingsModal modal);
        Task<PaginatedList<CustomerReviewRatingsModal>> GetAllCustomerReviewRatings(CustomerReviewRatingsFilter filter);
        Task<PaginatedList<CustomerReviewRatingsData>> GetAllCustomerReviewRatingsPublic(CustomerReviewRatingsFilter filter);
        Task<CustomerReviewRatingsModal> GetById(long Id);
        Task<CustomerReviewRatingsModal> UpdateCustomerReviewRatings(CustomerReviewRatingsModal modal);
    }
}