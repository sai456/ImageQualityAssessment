using ImageQuality.Model;
using ImageQuality.Model.Contracts;
using ImageQuality.SVMAdaptor.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static Tavisca.Content.ImageQuality.KeyStore;

namespace ImageQuality.SVMAdaptor
{
    public class ImageQualityAdapter : IImageQualityAdapter
    {
        public bool DeleteImage(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<List<RequestedImage>> DownloadImagesAsync(List<string> urls)
        {
            var requestedImages = new List<RequestedImage>() { };
            foreach(var url in urls)
            {
                RequestedImage requestedImage = null; 
                if(String.IsNullOrEmpty(url))
                {

                }
                else
                {
                    var filePath = Path.Combine(BasePath.BaseDirectory, Location.Images, Guid.NewGuid().ToString() + ".jpg");
                    requestedImage = await DownloadImageAsync(url, filePath);
                }
                requestedImages.Add(requestedImage);
            }
            return requestedImages;
        }

        private async Task<RequestedImage> DownloadImageAsync(string url,string filePath)
        {
            try
            {
                return await FileHandler.Download(url,filePath)?
            }
        }

        public Task<double> GetQualityScoreAsync(IList<double> brisqueFeatures)
        {
            throw new NotImplementedException();
        }
    }
}
