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
    public class ActivitySearchLogsController : ControllerBase
    {
        private readonly IActivitySearchLogsService _activitySearchLogsService;
        public ActivitySearchLogsController(IActivitySearchLogsService ActivitySearchLogsService)
        {
            _activitySearchLogsService = ActivitySearchLogsService ?? throw new ArgumentNullException(nameof(ActivitySearchLogsService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] ActivitySearchLogsFilter filter)
        {
            try
            {
                return Ok(await _activitySearchLogsService.GetAllActivitySearchLogs(filter));
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

                return Ok(await _activitySearchLogsService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}
