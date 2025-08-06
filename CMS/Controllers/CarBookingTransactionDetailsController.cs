using Cms.Services.Filters.TransferAdmin;
using Cms.Services.Interfaces.TransferAdmin;
using Cms.Services.Models.TransferAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CarBookingTransactionDetailsController : ControllerBase
    {
        private readonly ICarBookingTransactionDetailsService _carBookingTransactionDetailsService;
        public CarBookingTransactionDetailsController(ICarBookingTransactionDetailsService carBookingTransactionDetailsService)
        {
            _carBookingTransactionDetailsService = carBookingTransactionDetailsService??throw new ArgumentNullException(nameof(carBookingTransactionDetailsService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery]CarBookingTransactionDetailsFilter filter)
        {
           try{ return Ok(await _carBookingTransactionDetailsService.GetAllCarBookingTransactionDetails(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpGet]
        public async Task<ActionResult> GetAllByUser([FromQuery]CarBookingTransactionDetailsFilter filter)
        {
            try{return Ok(await _carBookingTransactionDetailsService.GetUsersAllCarBookingTransactionDetails(filter));
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
            try{return Ok(await _carBookingTransactionDetailsService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}
