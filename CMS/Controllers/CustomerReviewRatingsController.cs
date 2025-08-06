using Cms.Services.Filters.FrontEnd;
using Cms.Services.Filters.TMM;
using Cms.Services.Interfaces.TMM;
using Cms.Services.Models.SectionType;
using Cms.Services.Models.TMMModals;
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
    public class CustomerReviewRatingsController : ControllerBase
    {
        private readonly ICustomerReviewRatingsService _customerReviewRatingsService;
        public CustomerReviewRatingsController(ICustomerReviewRatingsService CustomerReviewRatingsService)
        {
            _customerReviewRatingsService = CustomerReviewRatingsService ?? throw new ArgumentNullException(nameof(CustomerReviewRatingsService));
        }
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] CustomerReviewRatingsFilter filter)
        {
            return Ok(await _customerReviewRatingsService.GetAllCustomerReviewRatings(filter));
        }
        [HttpGet]
        public async Task<ActionResult> GetAllByUsers([FromQuery] CustomerReviewRatingsFilter filter)
        {
            try
            {
                return Ok(await _customerReviewRatingsService.GetAllCustomerReviewRatingsPublic(filter));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] CustomerReviewRatingsModal modal)
        {
            if (modal == null)
            {
                throw new ArgumentNullException(nameof(modal));
            }
            else
            {
                try
                {
                    return Ok(await _customerReviewRatingsService.UpdateCustomerReviewRatings(modal));
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
        public async Task<ActionResult> Create([FromBody]CustomerReviewRatingsModal modal)
        {
            try
            {
                return Ok(await _customerReviewRatingsService.CreateCustomerReviewRatings(modal));
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
            try
            {
                return Ok(await _customerReviewRatingsService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }

    }
}
