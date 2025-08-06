using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.Portals;
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
    public class PortalController : ControllerBase
    {
        private readonly IPortalService _PortalService;
        public PortalController(IPortalService PortalService)
        {
            _PortalService = PortalService ?? throw new ArgumentNullException(nameof(PortalService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] PortalFilter filter)
        {
            try
            {
                return Ok(await _PortalService.GetAllPortal(filter));
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
                return Ok(await _PortalService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpPost]
        public async Task<ActionResult> Create(CreatePortalModal modal)
        {
            try
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
                        return Ok(await _PortalService.CreatePortal(modal));
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "409")
                            return Conflict();
                        else
                            throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpPut]
        public async Task<ActionResult> Update(UpdatePortalModal modal)
        {
            try
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

                        return Ok(await _PortalService.UpdatePortal(modal));
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "409")
                            return Conflict();
                        else
                            throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
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
                    return Ok(await _PortalService.SoftDelete(Id));
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
