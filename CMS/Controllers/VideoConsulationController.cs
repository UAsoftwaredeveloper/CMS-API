using Cms.Services.Filters.TMM;
using Cms.Services.Interfaces.TMM;
using Cms.Services.Models.TMMModals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class VideoConsulationController : ControllerBase
    {
        private readonly IVideoConsulationService _videoConsulationService;
        public VideoConsulationController(IVideoConsulationService videoConsulationService)
        {
            _videoConsulationService = videoConsulationService ?? throw new ArgumentNullException(nameof(videoConsulationService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] VideoConsulationFilter filter)
        {
            try
            {
                return Ok(await _videoConsulationService.GetAllVideoConsulation(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                if (ex.Message == "409")
                    return Conflict();
                else
                    return Problem(ex.Message, ex.StackTrace, 500);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetUserAll([FromQuery] VideoConsulationFilter filter)
        {
            try
            {
                return Ok(await _videoConsulationService.GetUsersAllVideoConsulation(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                if (ex.Message == "409")
                    return Conflict();
                else
                    return Problem(ex.Message, ex.StackTrace, 500);
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await _videoConsulationService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                if (ex.Message == "409")
                    return Conflict();
                else
                    return Problem(ex.Message, ex.StackTrace, 500);
            }
        }
    }
}
