using Cms.Services.Services;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cms.Services.Filters;
using Cms.Services.Models.OpenAPIDataModel.CarHireDealsData;
using Cms.Services.Interfaces;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CarHireDealsDataController : ControllerBase
    {
        
        private readonly ICarHireDealsDataServices _DealManagementService;
        public CarHireDealsDataController(ICarHireDealsDataServices DealManagementService)
        {
            _DealManagementService = DealManagementService ?? throw new ArgumentNullException(nameof(DealManagementService));
        }
        [HttpGet]
        [RequestSizeLimit(long.MaxValue)]
        [DisableRequestSizeLimit]
        public IActionResult GetAll([FromQuery] CarHireDealFilter filter)
        {
            try
            {
                var result = _DealManagementService.GetAllDealManagement(filter).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}
