using Cms.Services.Filters.ActivityAdmin;
using Cms.Services.Interfaces.ActivityAdmin;
using Cms.Services.Models.ActivityAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;

using System.IO;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class GenerateQrCodeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetQr(string text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    return BadRequest("Text parameter is required.");
                }

                using (var qrGenerator = new QRCodeGenerator())
                {
                    var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                    var qrCode = new PngByteQRCode(qrCodeData);

                    // Generate QR code as PNG byte array
                    var qrCodeBytes = qrCode.GetGraphic(10);

                    // Return the QR code as a file result
                    return File(qrCodeBytes, "image/png");
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
