using Cms.Services.Filters.HotelAdmin;
using Cms.Services.Models.HotelAdmin;
using System.Threading.Tasks;

namespace Cms.Services.Interfaces.HotelAdmin
{
    public interface IHotelBookingDetailsService
    {
        Task<PaginatedList<HotelBookingDetailsModal>> GetAllHotelBookingDetails(HotelBookingDetailsFilter filter);
        Task<HotelBookingDetailsModal> GetById(int Id);
        Task<PaginatedList<HotelBookingDetailsModal>> GetUsersAllHotelBookingDetails(HotelBookingDetailsFilter filter);
    }
}