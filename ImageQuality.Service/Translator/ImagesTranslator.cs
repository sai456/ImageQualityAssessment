using ImageQuality.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuality.Service
{
    public static class ImagesTranslator
    {
        public static ImagesQualitiesResponse ToImagesQualitiesResponse(this List<Image> images)
        {
            return images == null
                ? null
                : new ImagesQualitiesResponse()
                {
                    Images = images?.Select(image => image.ToImageContract()).ToList()
                };
        }
    }
}
