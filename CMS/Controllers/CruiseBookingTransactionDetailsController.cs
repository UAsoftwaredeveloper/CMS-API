using Cms.Services.Filters.TMM;
using Cms.Services.Interfaces.TMM;
using Cms.Services.Models.TMMModals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class CruiseBookingTransactionDetailsController : ControllerBase
    {
        private readonly ICruiseBookingTransactionDetailsService _cruiseBookingTransactionDetailsService;
        public CruiseBookingTransactionDetailsController(ICruiseBookingTransactionDetailsService cruiseBookingTransactionDetailsService)
        {
            _cruiseBookingTransactionDetailsService = cruiseBookingTransactionDetailsService ?? throw new ArgumentNullException(nameof(cruiseBookingTransactionDetailsService));
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] CruiseBookingTransactionDetailsFilter filter)
        {
            try
            {
                return Ok(await _cruiseBookingTransactionDetailsService.GetAllCruiseBookingTransactionDetails(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpGet]
        public async Task<ActionResult> GetAllByUser([FromQuery] CruiseBookingTransactionDetailsFilter filter)
        {
            try
            {
                return Ok(await _cruiseBookingTransactionDetailsService.GetUsersAllCruiseBookingTransactionDetails(filter));
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
                return Ok(await _cruiseBookingTransactionDetailsService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}
