using Cms.Services.Filters;
using DataManager.DataClasses;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface ISectionContentAuditTrailsService
    {
        Task<PaginatedList<SectionContent_Trails>> GetAllSectionContent(SectionContentFilter filter);
    }
}