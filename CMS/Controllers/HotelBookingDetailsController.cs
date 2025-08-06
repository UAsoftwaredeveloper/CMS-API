using Cms.Services.Filters.HotelAdmin;
using Cms.Services.Interfaces.HotelAdmin;
using Cms.Services.Models.HotelAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class HotelBookingDetailsController : ControllerBase
    {
        private readonly IHotelBookingDetailsService _hotelBookingDetailsService;
        public HotelBookingDetailsController(IHotelBookingDetailsService HotelBookingDetailsService)
        {
            _hotelBookingDetailsService = HotelBookingDetailsService ?? throw new ArgumentNullException(nameof(HotelBookingDetailsService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] HotelBookingDetailsFilter filter)
        {
            try
            {
                return Ok(await _hotelBookingDetailsService.GetAllHotelBookingDetails(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpGet]
        public async Task<ActionResult> GetAllByUser([FromQuery] HotelBookingDetailsFilter filter)
        {
            try
            {
                return Ok(await _hotelBookingDetailsService.GetUsersAllHotelBookingDetails(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpGet]
        public async Task<ActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await _hotelBookingDetailsService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}
