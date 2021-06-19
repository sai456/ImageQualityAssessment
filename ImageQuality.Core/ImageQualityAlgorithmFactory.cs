using ImageQuality.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQuality.Core
{
    public class ImageQualityAlgorithmFactory
    {
        public static IImageFeaturesExtractor GetExtractor(string featuresExtractorName)
        {
            IImageFeaturesExtractor featuresExtractor = null;
            switch (featuresExtractorName?.ToLower())
            {
                case KeyStore.ImageFeaturesExtractor.BrisqueAlgorithm:
                    featuresExtractor = new BrisqueFeaturesExtractor();
                    break;
                // To do need to add other algorithms for calculation image quality
            }
            return featuresExtractor;
        }
    }
}
