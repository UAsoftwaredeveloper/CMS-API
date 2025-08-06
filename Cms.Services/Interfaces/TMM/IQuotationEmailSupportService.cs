using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IQuotationEmailSupportService
    {
        Task<QuotationEmailSupportModal> CreateQuotationEmailSupport(QuotationEmailSupportModal modal);
        Task<PaginatedList<QuotationEmailSupportModal>> GetAllQuotationEmailSupport(QuotationEmailSupportFilter filter);
        Task<PaginatedList<QuotationEmailSupportModal>> GetAllQuotationEmailSupportPublic(QuotationEmailSupportFilter filter);
        Task<QuotationEmailSupportModal> GetById(int Id);
        Task<QuotationEmailSupportModal> UpdateCustomerReviewRatings(QuotationEmailSupportModal modal);
    }
}