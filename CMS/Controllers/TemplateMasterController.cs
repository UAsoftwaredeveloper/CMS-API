using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.TemplateMaster;
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
    public class TemplateMasterController : ControllerBase
    {
        private readonly ITemplateMasterService _TemplateMasterService;
        public TemplateMasterController(ITemplateMasterService TemplateMasterService)
        {
            _TemplateMasterService = TemplateMasterService ?? throw new ArgumentNullException(nameof(TemplateMasterService));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TemplateMasterFilter filter)
        {
            try
            {
                return Ok(await _TemplateMasterService.GetAllTemplateMaster(filter));
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
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await _TemplateMasterService.GetById(Id));
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
        public async Task<IActionResult> Create(CreateTemplateMasterModal modal)
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
                        modal.CreatedBy = Convert.ToInt32(userId);
                    }

                    return Ok(await _TemplateMasterService.CreateTemplateMaster(modal));
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
        public async Task<IActionResult> Update(UpdateTemplateMasterModal modal)
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

                    return Ok(await _TemplateMasterService.UpdateTemplateMaster(modal));
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
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                if (Id < 1)
                {
                    throw new ArgumentNullException(nameof(Id));
                }
                else
                {
                    return Ok(await _TemplateMasterService.SoftDelete(Id));
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
