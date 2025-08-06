using Cms.Services.Filters.FrontEnd;
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
    public class BookingTransactionDetailsController : ControllerBase
    {
        private readonly IBookingTransactionDetailsService _bookingTransactionDetailsService;
        public BookingTransactionDetailsController(IBookingTransactionDetailsService BookingTransactionDetailsService)
        {
            _bookingTransactionDetailsService = BookingTransactionDetailsService??throw new ArgumentNullException(nameof(BookingTransactionDetailsService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery]BookingTransactionDetailsFilter filter)
        {
           try{ return Ok(await _bookingTransactionDetailsService.GetAllBookingTransactionDetails(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpGet]
        public async Task<ActionResult> UserWiseGetAll([FromQuery] FlightBookingTransactionFilter filter)
        {
            try{return Ok(await _bookingTransactionDetailsService.GetAllBookingTransactionDetailsPublic(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpGet]
        public async Task<ActionResult> GetById(long Id)
        {
            return Ok(await _bookingTransactionDetailsService.GetById(Id));
        }
    }
}
