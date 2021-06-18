using ImageQuality.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQuality.Core
{
    public static class RequestedImageExtensions
    {
        public static Image ToImage(this RequestedImage requestedImage, Status status)
        {
            return requestedImage == null
                ? null
                : new Image() { Url = requestedImage.Url, Error = requestedImage.Error, Status = status };
        }
        public static RequestedImage AddError(this RequestedImage requestedImage, Exception ex)
        {
            return requestedImage?.SetError(ex);
        }

        private static RequestedImage SetError(this RequestedImage requestedImage, Exception ex)
        {
            requestedImage.Error = ex != null
                ? new Error() { Code = Convert.ToInt32(FaultCodes.QualityError), Message = ex.ToString() }
                : null;
            return requestedImage;
        }
    }
}
