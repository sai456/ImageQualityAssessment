using System;
using System.Net;

namespace ImageQuality.Model
{
    [Serializable]
    public partial class BadRequestException : BaseApplicationException
    {
        public BadRequestException(string code, string message, HttpStatusCode httpStatusCode) : base(code, message, httpStatusCode) { }

        public BadRequestException(ErrorInfo info) : base(info.Code, info.Message, info.HttpStatusCode, info.Info) { }

        public BadRequestException(string message, string code, Exception inner) : base(message, code, inner) { }
    }
}
