using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.Users;
using CMS.Models;
using CMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize]
    public class UserRegistrationController : ControllerBase
    {
        private readonly IUsersService _usersService;

        private readonly ITokenService _tokenService;
        public UserRegistrationController(IUsersService usersService, ITokenService tokenService)
        {
            try
            {

                _usersService = usersService ?? throw new ArgumentNullException("usersService");
                _tokenService = tokenService ?? throw new ArgumentNullException("tokenService");
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
            }
        }
        [AllowAnonymous]
        [Route("CreateUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUsersModal user)
        {
            try
            {
                var result = await _usersService.CreateUsers(user);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
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
        [Authorize]
        [Route("UpdateUser")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUsersModal usersModal)
        {
            try
            {
                var result = await _usersService.UpdateUsers(usersModal);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
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
        [Route("GetAll")]
        [HttpGet]

        public async Task<IActionResult> GetAll([FromQuery] UserFilter filter)
        {
            try
            {
                var result = await _usersService.GetAllUsers(filter);
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
        [Route("GetUserDetailsById")]
        [HttpGet]

        public async Task<IActionResult> GetUserDetailsById(int id)
        {
            try
            {
                var result = await _usersService.GetUsersModalById(id);
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

        [Route("Authentication")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authentication(UserLogin userLogin)
        {
            try
            {

                var user = await _usersService.Login(userLogin.UserName, userLogin.Password);
                if (user != null)
                {
                    var userToken = _tokenService.GenerateToken(user);


                    return Ok(userToken);
                }
                else
                {
                    return Unauthorized();

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("Delete")]
        [HttpDelete]

        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                return Ok(await _usersService.SoftDelete(Id));
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
