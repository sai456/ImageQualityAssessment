using ImageQuality.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageQuality.Controllers
{
    [Route("api/v1.0/images")]
    [ApiController]
    public class ImageController : Controller
    {
        private IImageQualityService _imageQualityService;
        public ImageController(IImageQualityService imageQualityService)
        {
            _imageQualityService = imageQualityService;
        }
        [HttpPost("qualities")]
        public async Task<ActionResult> GetQualitiesAsync([FromBody] ImagesQualitiesRequest request)
        {
            var result = await _imageQualityService.GetQualitiesAsync(request);
            return result == null ? (ActionResult)NotFound() : Ok(result);
        }
    }
}
