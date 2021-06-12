using ImageQuality.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQuality.SVMAdaptor.Helpers
{
    public static class RequestedImageHandler
    {
        public static RequestedImage GetRequestedImageForNullOrEmptyURL(string url)
        {
            return new RequestedImage() { Url =url,Error=}
        }
    }
}
