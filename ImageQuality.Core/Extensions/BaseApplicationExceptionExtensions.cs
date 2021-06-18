using ImageQuality.Model;
using System;

namespace ImageQuality.Core
{
    public static class BaseApplicationExceptionExtensions
    {
        public static Error ToError(this BaseApplicationException baseApplicationException)
        {
            return baseApplicationException == null
                ? null
                : new Error()
                {
                    Code = Convert.ToInt32(baseApplicationException.ErrorCode),
                    Message = baseApplicationException.Message
                };
        }
    }
}
