using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQuality.Model
{
    [Serializable]
    public sealed class Info
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public Info(string code, string message)
        {
            if (String.IsNullOrEmpty(code))
                throw new ArgumentNullException(nameof(code));

            if (String.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            Code = code;
            Message = message;

        }
    }
}
