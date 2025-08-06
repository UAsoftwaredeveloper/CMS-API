using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.MasterAirlines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class MasterAirlinesController : ControllerBase
    {
        private readonly IMasterAirlinesService _MasterAirlinesService;
        public MasterAirlinesController(IMasterAirlinesService MasterAirlinesService)
        {
            _MasterAirlinesService = MasterAirlinesService ?? throw new ArgumentNullException(nameof(MasterAirlinesService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] MasterAirlinesFilter filter)
        {
            try
            {
                return Ok(await _MasterAirlinesService.GetAllMasterAirlines(filter));
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
                return Ok(await _MasterAirlinesService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                if (Id < 1)
                {
                    throw new ArgumentNullException(nameof(Id));
                }
                else
                {
                    return Ok(await _MasterAirlinesService.SoftDelete(Id));
                }
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}
