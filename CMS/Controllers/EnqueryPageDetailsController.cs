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
    public class EnqueryPageDetailsController : ControllerBase
    {
        private readonly IEnqueryPageDetailsService _enqueryPageDetailsService;
        public EnqueryPageDetailsController(IEnqueryPageDetailsService EnqueryPageDetailsService)
        {
            _enqueryPageDetailsService = EnqueryPageDetailsService??throw new ArgumentNullException(nameof(EnqueryPageDetailsService));
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery]EnqueryPageDetailsFilter filter)
        {
           try{ return Ok(await _enqueryPageDetailsService.GetAllEnqueryPageDetails(filter));
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
           try{ return Ok(await _enqueryPageDetailsService.GetById(Id));
            }
            catch (Exception ex)
            {
                Log.Log.Error("Exception: " + ex.ToString(), Log.Log.GetCurrentNameSpace(), Log.Log.GetCurrentClass(), Log.Log.GetCurrentMethod());
                return Problem(ex.Message, ex.StackTrace, 500);
            }

        }
    }
}
