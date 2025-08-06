using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IFlightsEnquiryService
    {
        Task<PaginatedList<FlightsEnquiryModal>> GetAllFlightsEnquiry(FlightsEnquiryFilter filter);
        Task<FlightsEnquiryModal> GetById(int Id);
    }
}