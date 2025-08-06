using Cms.Services.Filters.TMM;
using Cms.Services.Models.TMMModals;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.TMM
{
    public interface IContactUsService
    {
        Task<PaginatedList<ContactUsModal>> GetAllContactUs(ContactUsFilter filter);
        Task<ContactUsModal> GetById(int Id);
    }
}