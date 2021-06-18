using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageQuality.Model
{
    public interface IImageQualityAdapter
    {
        Task<List<RequestedImage>> DownloadImagesAsync(List<string> urls);
        Task<double> GetQualityScoreAsync(IList<double> brisqueFeatures);
        bool DeleteImage(string filePath);
    }
}
