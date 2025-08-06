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
    [Authorize]
    public class CruiseSearchDetailsController : ControllerBase
    {
        private readonly ICruiseSearchDetailsService _cruiseSearchDetailsService;

        public CruiseSearchDetailsController(ICruiseSearchDetailsService cruiseSearchDetailsService)
        {
            _cruiseSearchDetailsService = cruiseSearchDetailsService ?? throw new ArgumentNullException(nameof(cruiseSearchDetailsService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] CruiseSearchDetailsFilter filter)
        {
            try
            {
                return Ok(await _cruiseSearchDetailsService.GetAllCruiseSearchDetails(filter));
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
                return Ok(await _cruiseSearchDetailsService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}
