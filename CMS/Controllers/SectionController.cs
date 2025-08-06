using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.Section;
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
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _SectionService;
        public SectionController(ISectionService SectionService)
        {
            _SectionService = SectionService ?? throw new ArgumentNullException(nameof(SectionService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] SectionFilter filter)
        {
            try
            {
                return Ok(await _SectionService.GetAllSection(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpGet]
        public async Task<ActionResult> GetAllTrails([FromQuery] SectionFilter filter)
        {
            try
            {
                return Ok(await _SectionService.GetAllSection_Trails(filter));
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
                return Ok(await _SectionService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateSectionModal modal)
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

                    return Ok(await _SectionService.CreateSection(modal));
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
        public async Task<ActionResult> Update(UpdateSectionModal modal)
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

                    return Ok(await _SectionService.UpdateSection(modal));
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
                    return Ok(await _SectionService.SoftDelete(Id));
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
