using ImageQuality.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQuality.SVMAdaptor
{
    public static class ErrorHandler
    {
        public static Error GetEmptyUrlError()
        {
            return new Error() { Code = Convert.ToInt32(FaultCodes.NullOrEmptyImageUrl), Message = FaultMessages.NullOrEmptyImageUrl };
        }
        public static Error GetDownloadFailedError()
        {
            return new Error() { Code = Convert.ToInt32(FaultCodes.DownloadFailure), Message = FaultMessages.DownloadFailure };
        }
        public static Error GetError(BaseApplicationException ex)
        {
            if (ex == null)
                return null;
            return new Error() { Code = Convert.ToInt32(ex.ErrorCode), Message = ex.Message };
        }
    }
}
