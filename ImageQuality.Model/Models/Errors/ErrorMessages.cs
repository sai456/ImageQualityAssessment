using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQuality.Model
{
    public static partial class ErrorMessages
    {
        public static string ValidationFailure()
        {
            return FaultMessages.ValidationFailure;
        }
        public static string MissingRequestHeaders(List<string> headerNames)
        {
            return string.Format(FaultMessages.MissingRequestHeaders, string.Join(", ", headerNames));
        }
        public static string InvalidRequestHeaders(List<string> headerNames)
        {
            return string.Format(FaultMessages.InvalidRequestHeaders, string.Join(", ", headerNames));
        }
        public static string InvalidFieldValue(string path)
        {
            return string.Format(FaultMessages.InvalidFieldValue, path);
        }
        public static string MissingField(string path)
        {
            return string.Format(FaultMessages.MissingField, path);
        }
    }
}
