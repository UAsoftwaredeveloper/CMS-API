using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface ICruiseEnquiryService
    {
        Task<PaginatedList<CruiseEnquiryModal>> GetAllCruiseEnquiry(CruiseEnquiryFilter filter);
        Task<CruiseEnquiryModal> GetById(int Id);
    }
}