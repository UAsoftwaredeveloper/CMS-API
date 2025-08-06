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
    public class AllInOneInquirySearchReportController : ControllerBase
    {
        private readonly IAllInOneReportService _allInOneReportService;
        public AllInOneInquirySearchReportController(IAllInOneReportService AllInOneReportService)
        {
            _allInOneReportService = AllInOneReportService ?? throw new ArgumentNullException(nameof(AllInOneReportService));
        }
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] GeneralReportsFilter filter)
        {
            try
            {
                return Ok(await _allInOneReportService.GetAllEnquiryReportSearchAsync(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }
        }
    }
}