using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageQuality.Service
{
    public interface IImageQualityService
    {
        Task<ImagesQualitiesResponse> GetQualitiesAsync(ImagesQualitiesRequest request);
    }
}
