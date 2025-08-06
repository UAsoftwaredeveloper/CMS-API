using Cms.Services.Filters.ActivityAdmin;
using Cms.Services.Interfaces.ActivityAdmin;
using Cms.Services.Models.ActivityAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ActivityBookingDetailsController : ControllerBase
    {
        private readonly IActivityBookingDetailsService _activityBookingDetailsService;
        public ActivityBookingDetailsController(IActivityBookingDetailsService activityBookingDetailsService)
        {
            _activityBookingDetailsService = activityBookingDetailsService ?? throw new ArgumentNullException(nameof(activityBookingDetailsService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] ActivityBookingDetailsFilter filter)
        {
            try
            {

                return Ok(await _activityBookingDetailsService.GetAllActivityBookingDetails(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetAllByUser([FromQuery] ActivityBookingDetailsFilter filter)
        {
            try
            {
                return Ok(await _activityBookingDetailsService.GetUsersAllActivityBookingDetails(filter));
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
                return Ok(await _activityBookingDetailsService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}
