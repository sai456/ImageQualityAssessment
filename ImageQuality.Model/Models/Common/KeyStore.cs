using System;

namespace Tavisca.Content.ImageQuality
{
    public static class KeyStore
    {
        public static readonly string ApplicationName = "image_quality_service";
       
        public static class Api
        {
            public static readonly string Image = "images";
        }
        public static class Verb
        {
            public static readonly string Qualities = "qualities";
        }
        public static class ImageFeaturesExtractor
        {
            public const string BrisqueAlgorithm = "brisque";
        }
        public static class Location
        {
            public static readonly string Model = "allmodel";
            public static readonly string Images = "Images";
        }
        public static class BasePath
        {
            public static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }
        public static class ImageSize
        {
            public static readonly string Large = "Large";
            public static readonly string Medium = "Medium";
            public static readonly string Small = "Small";
        }
        public static class ImageQuality
        {
            public static readonly string High = "High";
            public static readonly string Medium = "Medium";
            public static readonly string Low = "Low";
        }
      
    }
}
