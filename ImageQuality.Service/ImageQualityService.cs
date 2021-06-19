using ImageQuality.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageQuality.Service
{
    public class ImageQualityService : IImageQualityService
    {
        private IImageQualityComponent _imageQualityComponent;
        public ImageQualityService(IImageQualityComponent imageQualityComponent)
        {
            _imageQualityComponent = imageQualityComponent;
        }

        public async Task<ImagesQualitiesResponse> GetQualitiesAsync(ImagesQualitiesRequest request)
        {
            Validations.EnsureValid(request, new ImagesQualitiesRequestValidator());
            var result = await _imageQualityComponent.GetQualitiesAsync(request?.Urls);
            return result.ToImagesQualitiesResponse();
        }
    }
}
