using ImageQuality.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQuality.Service
{
    public static class ImageTranslator
    {
        public static Contracts.Image ToImageContract(this Image image)
        {
            return image == null
                ? null
                : new Contracts.Image()
                {
                    Url = image.Url,
                    Status = image.Status.ToString(),
                    Quality = image.Quality,
                    QualityScore = image.QualityScore,
                    Size = image.Size,
                    Error = image.Error.ToErrorContract()
                };
        }
    }
}
