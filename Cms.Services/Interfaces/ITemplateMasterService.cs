using Cms.Services.Filters;
using Cms.Services.Models.TemplateMaster;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces
{
    public interface ITemplateMasterService
    {
        Task<CreateTemplateMasterModal> CreateTemplateMaster(CreateTemplateMasterModal modal);
        Task<PaginatedList<TemplateMasterModal>> GetAllTemplateMaster(TemplateMasterFilter filter);
        Task<TemplateMasterModal> GetById(int id);
        Task<bool> SoftDelete(int Id);
        Task<UpdateTemplateMasterModal> UpdateTemplateMaster(UpdateTemplateMasterModal modal);
    }
}