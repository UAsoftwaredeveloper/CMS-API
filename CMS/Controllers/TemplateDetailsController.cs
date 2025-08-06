using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.TemplateDetails;
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
    public class TemplateDetailsController : ControllerBase
    {
        private readonly ITemplateDetailsService _TemplateDetailsService;
        public TemplateDetailsController(ITemplateDetailsService TemplateDetailsService)
        {
            _TemplateDetailsService = TemplateDetailsService ?? throw new ArgumentNullException(nameof(TemplateDetailsService));
        }
        [HttpGet]
        [RequestSizeLimit(long.MaxValue)]

        public IActionResult GetAll([FromQuery] TemplateDetailsFilter filter)
        {
            try
            {
                var result = _TemplateDetailsService.GetAllTemplateDetails(filter).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }
        }
        [HttpGet]
        public IActionResult GetAllTrails([FromQuery] TemplateDetailsFilter filter)
        {
            try
            {
                var result = _TemplateDetailsService.GetAllTemplateDetails_Trails(filter).Result;
                return Ok(result);
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
            try{return Ok(await _TemplateDetailsService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateTemplateDetailsModal modal)
        {
            try{if (modal == null)
            {
                throw new ArgumentNullException(nameof(modal));
            }
            else
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    throw new UnauthorizedAccessException();
                }
                else
                {
                    modal.CreatedBy = Convert.ToInt32(userId);
                }

                return Ok(await _TemplateDetailsService.CreateTemplateDetails(modal));
                }
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
        [HttpPost]
        public async Task<ActionResult> CreateDuplicate(UpdateTemplateDetailsModal modal)
        {
            if (modal == null)
            {
                throw new ArgumentNullException(nameof(modal));
            }
            else
            {
                try
                {
                    var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                    if (userId == null)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        modal.ModifiedBy = Convert.ToInt32(userId);
                    }

                    return Ok(await _TemplateDetailsService.CreateDuplicateTemplateDetails(modal));
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
        [HttpPut]
        public async Task<ActionResult> Update(UpdateTemplateDetailsModal modal)
        {
            if (modal == null)
            {
                throw new ArgumentNullException(nameof(modal));
            }
            else
            {
                try
                {
                    var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                    if (userId == null)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {
                        modal.ModifiedBy = Convert.ToInt32(userId);
                    }

                    return Ok(await _TemplateDetailsService.UpdateTemplateDetails(modal));
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
        [HttpDelete]
        public async Task<ActionResult> Delete(int Id)
        {
            try{if (Id < 1)
            {
                throw new ArgumentNullException(nameof(Id));
            }
            else
            {
                return Ok(await _TemplateDetailsService.SoftDelete(Id));
            }
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
