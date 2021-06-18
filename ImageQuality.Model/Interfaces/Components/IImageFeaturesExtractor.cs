using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageQuality.Model
{
    public interface IImageFeaturesExtractor
    {
        Task<(List<Double> features,string size)>ComputeImageFeaturesAsync(string fileName);
    }
}
