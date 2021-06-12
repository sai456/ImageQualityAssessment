using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQuality.Model
{
    public class RequestedImage
    {
        public string Url { get; set; }
        public string FilePath { get; set; }
        public bool isDownloaded { get; set; }
        public Error Error { get; set; }

    }
}
