using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQuality.Model
{
    public class Image
    {
        public string Url { get; set; }
        public Status Status { get; set; }
        public string Quality { get; set; }

        public double QualityScore { get; set; }
        public string Size { get; set; }
        public Error Error { get; set; }
        public Image(string url,string quality,double qualityScore, string size)
        {
            Url = url;
            Status = Status.Success;
            Quality = quality;
            QualityScore = qualityScore;
            Size = size;
        }

        public Image() { }
    }
}
