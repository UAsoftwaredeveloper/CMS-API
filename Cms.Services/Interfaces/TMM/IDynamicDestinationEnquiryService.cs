using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IDynamicDestinationEnquiryService
    {
        Task<PaginatedList<DynamicDestinationEnquiryModal>> GetAllDynamicDestinationEnquiry(DynamicDestinationEnquiryFilter filter);
        Task<DynamicDestinationEnquiryModal> GetById(int Id);
    }
}