using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageQuality.Model
{
    public interface IImageQualityComponent
    {
        Task<List<Image>> GetQualitiesAsync(List<string> urls);
    }
}
