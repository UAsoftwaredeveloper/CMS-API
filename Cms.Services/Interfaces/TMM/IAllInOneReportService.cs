using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals.AllInOne;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IAllInOneReportService
    {
        Task<PaginatedList<GenericTmmReportModel>> GetAllEnquiryReportSearchAsync(GeneralReportsFilter filter);
    }
}