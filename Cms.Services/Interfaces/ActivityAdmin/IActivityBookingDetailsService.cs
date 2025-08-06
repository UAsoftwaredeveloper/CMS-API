using Cms.Services.Filters.ActivityAdmin;
using Cms.Services.Models.ActivityAdmin;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.ActivityAdmin
{
    public interface IActivityBookingDetailsService
    {
        Task<PaginatedList<ActivityBookingDetailsModal>> GetAllActivityBookingDetails(ActivityBookingDetailsFilter filter);
        Task<ActivityBookingDetailsModal> GetById(int Id);
        Task<PaginatedList<ActivityBookingDetailsModal>> GetUsersAllActivityBookingDetails(ActivityBookingDetailsFilter filter);
    }
}