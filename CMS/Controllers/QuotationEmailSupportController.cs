using Cms.Services.Filters.FrontEnd;
using Cms.Services.Filters.TMM;
using Cms.Services.Interfaces.TMM;
using Cms.Services.Models.SectionType;
using Cms.Services.Models.TMMModals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class QuotationEmailSupportController : ControllerBase
    {
        private readonly IQuotationEmailSupportService _quotationEmailSupportService;
        public QuotationEmailSupportController(IQuotationEmailSupportService QuotationEmailSupportService)
        {
            _quotationEmailSupportService = QuotationEmailSupportService ?? throw new ArgumentNullException(nameof(QuotationEmailSupportService));
        }
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] QuotationEmailSupportFilter filter)
        {
            return Ok(await _quotationEmailSupportService.GetAllQuotationEmailSupport(filter));
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetAllByUsers([FromQuery] QuotationEmailSupportFilter filter)
        {
            try
            {
                return Ok(await _quotationEmailSupportService.GetAllQuotationEmailSupportPublic(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] QuotationEmailSupportModal modal)
        {
            if (modal == null)
            {
                throw new ArgumentNullException(nameof(modal));
            }
            else
            {
                try
                {
                    return Ok(await _quotationEmailSupportService.CreateQuotationEmailSupport(modal));
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

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]QuotationEmailSupportModal modal)
        {
            try
            {
                return Ok(await _quotationEmailSupportService.CreateQuotationEmailSupport(modal));
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
                return Ok(await _quotationEmailSupportService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }

    }
}
