using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.SectionContent;
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
    public class SectionContentAuditTrailsController : ControllerBase
    {
        private readonly ISectionContentAuditTrailsService _SectionContentService;
        public SectionContentAuditTrailsController(ISectionContentAuditTrailsService SectionContentService)
        {
            _SectionContentService = SectionContentService ?? throw new ArgumentNullException(nameof(SectionContentService));
        }
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] SectionContentFilter filter)
        {
            try
            {
                return Ok(await _SectionContentService.GetAllSectionContent(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}
