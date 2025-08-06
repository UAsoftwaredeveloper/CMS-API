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
    public class DynamicDestinationEnquiryController : ControllerBase
    {
        private readonly IDynamicDestinationEnquiryService _DynamicDestinationEnquiryService;
        public DynamicDestinationEnquiryController(IDynamicDestinationEnquiryService DynamicDestinationEnquiryService)
        {
            _DynamicDestinationEnquiryService = DynamicDestinationEnquiryService ?? throw new ArgumentNullException(nameof(DynamicDestinationEnquiryService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] DynamicDestinationEnquiryFilter filter)
        {
            try
            {
                return Ok(await _DynamicDestinationEnquiryService.GetAllDynamicDestinationEnquiry(filter));
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
                return Ok(await _DynamicDestinationEnquiryService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}