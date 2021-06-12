using ImageQuality.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQuality.SVMAdaptor
{
    public static class RequestedImageHandler
    {
        public static RequestedImage GetRequestedImageForNullOrEmptyURL(string url)
        {
            return new RequestedImage() { Url = url, Error = ErrorHandler.GetEmptyUrlError() };
        }

        public static RequestedImage GetRequestedImageForDownloadSuccess(string url, string filePath)
        {
            return new RequestedImage() { Url = url, IsDownloaded = true, FilePath = filePath };
        }

        public static RequestedImage GetRequestedImageForDownloadFailure(string url, string filePath)
        {
            return new RequestedImage() { Url = url, FilePath = filePath, Error = ErrorHandler.GetDownloadFailedError() };
        }

        public static RequestedImage GetRequestedImage(string url, string filePath, BaseApplicationException ex)
        {
            return new RequestedImage() { Url = url, FilePath = filePath, Error = ErrorHandler.GetError(ex) };
        }

    }
}
