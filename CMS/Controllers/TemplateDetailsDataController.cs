using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.TemplateDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TemplateDetailsDataController : ControllerBase
    {
        private readonly ITemplateDetailsDataService _TemplateDetailsService;
        public TemplateDetailsDataController(ITemplateDetailsDataService TemplateDetailsService)
        {
            _TemplateDetailsService = TemplateDetailsService ?? throw new ArgumentNullException(nameof(TemplateDetailsService));
        }
        [HttpGet]
        [RequestSizeLimit(long.MaxValue)]
        [DisableRequestSizeLimit]

        public IActionResult GetAll([FromQuery] TemplateDetailsDataFilter filter)
        {
            try
            {
                var result = _TemplateDetailsService.GetAllTemplateDetails(filter).Result;
                return Ok(result);
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
        [HttpGet]
        public IActionResult GetSingleAll([FromQuery] TemplateDetailsDataFilter filter)
        {
            try
            {
                var result = _TemplateDetailsService.GetSingleAllTemplateDetails(filter).Result;
                return Ok(result);
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
        [HttpGet]
        public IActionResult GetAllSiteMapDetails([FromQuery] TemplateDetailsDataFilter filter)
        {
            try
            {
                var result = _TemplateDetailsService.GetAllSiteMapDetails(filter).Result;
               
                return Ok(result);
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
}