using ImageQuality.Model;
using ImageQuality.Model;
using ImageQuality.SVMAdaptor.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static ImageQuality.Model.Errors;
using static ImageQuality.Model.KeyStore;

namespace ImageQuality.SVMAdaptor
{
    public class ImageQualityAdapter : IImageQualityAdapter
    {
        public bool DeleteImage(string filepath)
        {
            return FileHandler.DeleteFile(filepath);
        }

        public async Task<List<RequestedImage>> DownloadImagesAsync(List<string> urls)
        {
            var requestedImages = new List<RequestedImage>() { };
            foreach(var url in urls)
            {
                RequestedImage requestedImage = null; 
                if(String.IsNullOrEmpty(url))
                {
                    requestedImage = RequestedImageHandler.GetRequestedImageForNullOrEmptyURL(url);
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
                return await FileHandler.Download(url, filePath) ?
                    RequestedImageHandler.GetRequestedImageForDownloadSuccess(url, filePath):
                    RequestedImageHandler.GetRequestedImageForDownloadFailure(url, filePath);

            }
            catch(BaseApplicationException exception)
            {
                return RequestedImageHandler.GetRequestedImage(url, filePath, exception);
            }
        }

        public async Task<double> GetQualityScoreAsync(IList<double> brisqueFeatures)
        {
            if(brisqueFeatures?.Count>0)
            {
                return await QualityScoreProvider.GetQualityScoreAsync(brisqueFeatures);
            }
            throw ServerSide.NoBrisqueFeatures();
        }
    }
}
