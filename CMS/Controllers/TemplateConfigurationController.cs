using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.TemplateConfiguration;
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
    public class TemplateConfigurationController : ControllerBase
    {
        private readonly ITemplateConfigurationService _TemplateConfigurationService;
        public TemplateConfigurationController(ITemplateConfigurationService TemplateConfigurationService)
        {
            _TemplateConfigurationService = TemplateConfigurationService ?? throw new ArgumentNullException(nameof(TemplateConfigurationService));
        }
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] TemplateConfigurationFilter filter)
        {
            try
            {
                return Ok(await _TemplateConfigurationService.GetAllTemplateConfiguration(filter));
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
                return Ok(await _TemplateConfigurationService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateTemplateConfigurationModal modal)
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

                    return Ok(await _TemplateConfigurationService.CreateTemplateConfiguration(modal));
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
        public async Task<ActionResult> Update(UpdateTemplateConfigurationModal modal)
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

                    return Ok(await _TemplateConfigurationService.UpdateTemplateConfiguration(modal));
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
            try
            {
                if (Id < 1)
                {
                    throw new ArgumentNullException(nameof(Id));
                }
                else
                {
                    return Ok(await _TemplateConfigurationService.SoftDelete(Id));
                }
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}
