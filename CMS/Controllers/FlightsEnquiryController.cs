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
    public class FlightsEnquiryController : ControllerBase
    {
        private readonly IFlightsEnquiryService _flightsEnquiryService;
        public FlightsEnquiryController(IFlightsEnquiryService FlightsEnquiryService)
        {
            _flightsEnquiryService = FlightsEnquiryService ?? throw new ArgumentNullException(nameof(FlightsEnquiryService));
        }
        [AllowAnonymous]

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] FlightsEnquiryFilter filter)
        {
            try
            {
                return Ok(await _flightsEnquiryService.GetAllFlightsEnquiry(filter));
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
                return Ok(await _flightsEnquiryService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}
