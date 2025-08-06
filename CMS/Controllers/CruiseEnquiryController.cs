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
    public class CruiseEnquiryController : ControllerBase
    {
        private readonly ICruiseEnquiryService _cruiseEnquiryService;
        public CruiseEnquiryController(ICruiseEnquiryService cruiseEnquiryService)
        {
            _cruiseEnquiryService = cruiseEnquiryService ?? throw new ArgumentNullException(nameof(cruiseEnquiryService));
        }
        [AllowAnonymous]

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] CruiseEnquiryFilter filter)
        {
            try
            {
                return Ok(await _cruiseEnquiryService.GetAllCruiseEnquiry(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [AllowAnonymous]

        [HttpGet]
        public async Task<ActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await _cruiseEnquiryService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}
