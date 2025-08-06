using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class FlightFaresDetailsDataController : ControllerBase
    {
        private readonly IFlightFaresDetailsDataService _service;
        public FlightFaresDetailsDataController(IFlightFaresDetailsDataService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        [HttpPost]
        [Consumes("application/xml")]
        public async Task<IActionResult> SearchFlight([FromBody] FlightSearchDetails searchDetails)
        {
            try
            {
                var result = await _service.GetAllFlightFaresDetailsData(searchDetails);
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
