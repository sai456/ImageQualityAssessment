using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQuality.Model.Models
{
    public static class ErrorCreator
    {
        public static Error GetEmptyUrlError()
        {
            return new Error();
        }

        internal static Error GetDownloadFailedError()
        {
            throw new NotImplementedException();
        }
        internal static Error GetError(BaseApplicationException ex)
        {
            throw new NotImplementedException();
        }
    }
}
