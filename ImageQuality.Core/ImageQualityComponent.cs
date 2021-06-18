using ImageQuality.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImageQuality.Core
{
    public class ImageQualityComponent : IImageQualityComponent
    {
        private readonly IImageQualityAdapter _imageQualityAdapter;
        private readonly IImageFeaturesExtractor _featuresExtractor;

        public ImageQualityComponent(IImageQualityAdapter imageQualityAdapter, IImageFeaturesExtractor featuresExtractor)
        {
            _imageQualityAdapter = imageQualityAdapter;
            _featuresExtractor = featuresExtractor;
        }

        public async Task<List<Image>> GetQualitiesAsync(List<string> urls)
        {
            var images = new List<Image>();
            var requestedImages = await _imageQualityAdapter.DownloadImagesAsync(urls);
            foreach( var requestedImage in requestedImages)
            {
                if(requestedImage.IsDownloaded)
                {
                    try
                    {
                        var (features, size) = await _featuresExtractor.ComputeImageFeaturesAsync(requestedImage.FilePath);
                        double brisqueScore = await _imageQualityAdapter.GetQualityScoreAsync(features);
                        var image = new Image(requestedImage.Url, GetQualityFromBrisqueScore(brisqueScore),100-brisqueScore,size);
                        images.Add(image);
                        continue; 
                    }
                    catch(BaseApplicationException ex)
                    {
                        requestedImage.Error = ex.ToError();
                    }
                    catch(Exception ex)
                    {
                        requestedImage.AddError(ex);
                    }
                    finally
                    {
                        _imageQualityAdapter.DeleteImage(requestedImage.FilePath);
                    }
                }
                images.Add(requestedImage.ToImage(Status.Failed));
            }
            return images;
        }

        public string GetQualityFromBrisqueScore(double brisqueScore)
        {
            return brisqueScore <= 40
                ? KeyStore.ImageQuality.High
                : brisqueScore > 40 && brisqueScore <= 70
                        ? KeyStore.ImageQuality.Medium
                        : KeyStore.ImageQuality.Low;
        }
    }
}
