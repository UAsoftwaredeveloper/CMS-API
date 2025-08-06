using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.UserRoleMenuPermissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class UserRoleMenuPermissionController : ControllerBase
    {
        private readonly IUserRoleMenuPermissionService _UserRoleMenuPermissionService;
        public UserRoleMenuPermissionController(IUserRoleMenuPermissionService UserRoleMenuPermissionService)
        {
            _UserRoleMenuPermissionService = UserRoleMenuPermissionService ?? throw new ArgumentNullException(nameof(UserRoleMenuPermissionService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] UserRoleMenuPermissionFilter filter)
        {
            try
            {
                return Ok(await _UserRoleMenuPermissionService.GetAllUserRoleMenuPermission(filter));
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
        public async Task<ActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await _UserRoleMenuPermissionService.GetById(Id));
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
        public async Task<ActionResult> Create(UserRoleMenuPermissionModal modal)
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

                    return Ok(await _UserRoleMenuPermissionService.CreateUserRoleMenuPermission(modal));
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
        public async Task<IActionResult> BulkCreateUpdate(List<UserRoleMenuPermissionModal> modal)
        {
            if (modal == null)
            {
                return Problem(JsonSerializer.Serialize(new ArgumentNullException(nameof(modal))));
            }
            else
            {
                var createSuccess = true;
                var updateSuccess = true;
                var createError = "";
                var updateError = "";
                var createdResponse = new List<UserRoleMenuPermissionModal>();
                var updatedResponse = new List<UserRoleMenuPermissionModal>();
                try
                {
                    var createData = modal.Where(x => x.Id == 0).ToList();
                    var updateData = modal.Where(x => x.Id > 0).ToList();
                    var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                    if (userId == null)
                    {
                        throw new UnauthorizedAccessException();
                    }
                    else
                    {

                        if (updateData != null && updateData.Count > 0)
                        {
                            updateData.ForEach(x => x.ModifiedBy = Convert.ToInt32(userId));

                        }
                        if (createData != null && createData.Count > 0)
                        {
                            createData.ForEach(x => x.CreatedBy = Convert.ToInt32(userId));

                        }
                    }

                    try
                    {

                        createdResponse = await _UserRoleMenuPermissionService.CreateUserRoleMenuPermissionList(createData);
                        createSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        createSuccess = false;
                        createError = ex.Message;
                    }
                    try
                    {
                        updatedResponse = await _UserRoleMenuPermissionService.UpdateUserRoleMenuPermissionList(updateData);
                        updateSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        updateSuccess = false;
                        updateError = ex.Message;
                    }
                    var resultedData = new
                    {

                        updateSucess = updateSuccess,
                        updateError = updateError,
                        createSuccess = createSuccess,
                        createError = createError,
                        updateResponse = updatedResponse,
                        createdResponse = createdResponse,
                    };

                    return Ok(resultedData);
                }
                catch (Exception ex)
                {
                    if (ex.Message == "409")
                        return Conflict();
                    else
                    {
                        var resultedData = new
                        {

                            updateSucess = updateSuccess,
                            updateError = updateError,
                            createSuccess = createSuccess,
                            createError = createError,
                            updateResponse = updatedResponse,
                            createdResponse = createdResponse,
                            otherError = ex.Message,
                        };
                        return BadRequest(resultedData);
                    }
                }
            }
        }
        [HttpPut]
        public async Task<ActionResult> Update(UserRoleMenuPermissionModal modal)
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

                    return Ok(await _UserRoleMenuPermissionService.UpdateUserRoleMenuPermission(modal));
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
                    return Ok(await _UserRoleMenuPermissionService.SoftDelete(Id));
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
