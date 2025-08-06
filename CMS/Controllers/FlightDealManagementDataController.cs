using Cms.Services.Filters;
using Cms.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class FlightDealManagementDataController : ControllerBase
    {
        private readonly IFlightDealManagementDataService _DealManagementService;
        public FlightDealManagementDataController(IFlightDealManagementDataService DealManagementService)
        {
            _DealManagementService = DealManagementService ?? throw new ArgumentNullException(nameof(DealManagementService));
        }
        [HttpGet]
        [RequestSizeLimit(long.MaxValue)]
        [DisableRequestSizeLimit]

        public IActionResult GetAll([FromQuery] FlightDealManagementFilter filter)
        {
            try
            {
                var result = _DealManagementService.GetAllDealManagement(filter).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}
