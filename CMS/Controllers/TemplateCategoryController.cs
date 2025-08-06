using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.TemplateCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class TemplateCategoryController : ControllerBase
    {
        private readonly ITemplateCategoryService _TemplateCategoryService;
        public TemplateCategoryController(ITemplateCategoryService TemplateCategoryService)
        {
            _TemplateCategoryService = TemplateCategoryService ?? throw new ArgumentNullException(nameof(TemplateCategoryService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] TemplateCategoryFilter filter)
        {
            try
            {
                return Ok(await _TemplateCategoryService.GetAllTemplateCategory(filter));
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
                return Ok(await _TemplateCategoryService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateTemplateCategoryModal modal)
        {
            try
            {
                if (modal == null)
                {
                    throw new ArgumentNullException(nameof(modal));
                }
                else
                {
                    return Ok(await _TemplateCategoryService.CreateTemplateCategory(modal));
                }
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpPut]
        public async Task<ActionResult> Update(UpdateTemplateCategoryModal modal)
        {
            try
            {
                if (modal == null)
                {
                    throw new ArgumentNullException(nameof(modal));
                }
                else
                {
                    return Ok(await _TemplateCategoryService.UpdateTemplateCategory(modal));
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
                    return Ok(await _TemplateCategoryService.SoftDelete(Id));
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
