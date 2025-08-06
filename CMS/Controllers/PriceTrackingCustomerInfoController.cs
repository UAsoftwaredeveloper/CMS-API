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
    public class PriceTrackingCustomerInfoController : ControllerBase
    {
        private readonly IPriceTrackingCustomerInfoService _priceTrackingCustomerInfoService;
        public PriceTrackingCustomerInfoController(IPriceTrackingCustomerInfoService priceTrackingCustomerInfoService)
        {
            _priceTrackingCustomerInfoService = priceTrackingCustomerInfoService ?? throw new ArgumentNullException(nameof(priceTrackingCustomerInfoService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] PriceTrackingCustomerInfoFilter filter)
        {
            try
            {
                return Ok(await _priceTrackingCustomerInfoService.GetAllPriceTrackingCustomerInfo(filter));
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
                return Ok(await _priceTrackingCustomerInfoService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}
