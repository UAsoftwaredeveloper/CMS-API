using Cms.Services.Filters;
using Cms.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class HolidayPackagesDataController : ControllerBase
    {
        private readonly IHolidayPackagesDataService _holidayPackagesDataService;
        public HolidayPackagesDataController(IHolidayPackagesDataService holidayPackagesDataService)
        {
            _holidayPackagesDataService = holidayPackagesDataService ?? throw new ArgumentNullException(nameof(holidayPackagesDataService));
        }
        [HttpGet]
        [RequestSizeLimit(long.MaxValue)]
        [DisableRequestSizeLimit]

        public IActionResult GetAll([FromQuery] HolidayPackagesFilter filter)
        {
            try
            {
                var result = _holidayPackagesDataService.GetAll(filter).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpGet]
        public IActionResult GetThemeSpecificHolidayPackages([FromQuery] HolidayPackagesFilter filter)
        {
            try
            {
                var result = _holidayPackagesDataService.GetThemeSpecificAll(filter).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpGet]
        [RequestSizeLimit(long.MaxValue)]
        [DisableRequestSizeLimit]
        public IActionResult GetPackageByUrl([FromQuery] HolidayPackagesFilter filter)
        {
            try
            {
                var result = _holidayPackagesDataService.GetPackageByUrlAll(filter).Result;
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
