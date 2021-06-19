using ImageQuality.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQuality.Service
{
    public static class ErrorTranslator
    {
        public static Contracts.Error ToErrorContract(this Error error)
        {
            return error == null
                 ? null
                 : new Contracts.Error()
                 {
                     Code = error.Code,
                     Message = error.Message
                 };
        }
    }
}
