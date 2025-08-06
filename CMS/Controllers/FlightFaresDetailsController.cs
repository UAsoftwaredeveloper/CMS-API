using Cms.Services.Filters;
using Cms.Services.Interfaces;
using Cms.Services.Models.FlightFaresDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class FlightFaresDetailsController : ControllerBase
    {
        private readonly IFlightFaresDetailsService _FlightFaresDetailsService;
        public FlightFaresDetailsController(IFlightFaresDetailsService FlightFaresDetailsService)
        {
            _FlightFaresDetailsService = FlightFaresDetailsService ?? throw new ArgumentNullException(nameof(FlightFaresDetailsService));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] FlightFaresDetailsFilter filter)
        {
            try
            {
                return Ok(await _FlightFaresDetailsService.GetAllFlightFaresDetails(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                return Ok(await _FlightFaresDetailsService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Create(FlightFaresDetailsModal modal)
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

                    return Ok(await _FlightFaresDetailsService.CreateFlightFaresDetails(modal));
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
        public async Task<IActionResult> BulkCreateUpdate(List<FlightFaresDetailsModal> modal)
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
                var createdResponse = new List<FlightFaresDetailsModal>();
                var updatedResponse = new List<FlightFaresDetailsModal>();
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

                        createdResponse = await _FlightFaresDetailsService.CreateFlightFaresDetailsList(createData);
                        createSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        createSuccess = false;
                        createError = ex.Message;
                    }
                    try
                    {
                        updatedResponse = await _FlightFaresDetailsService.UpdateFlightFaresDetailsList(updateData);
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
                    Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
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
        public async Task<IActionResult> Update(FlightFaresDetailsModal modal)
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

                    return Ok(await _FlightFaresDetailsService.UpdateFlightFaresDetails(modal));
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
                    return Ok(await _FlightFaresDetailsService.SoftDelete(Id));
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
