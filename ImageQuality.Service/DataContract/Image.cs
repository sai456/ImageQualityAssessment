using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ImageQuality.Service.Contracts
{
    public class Image
    {
        public string Url { get; set; }
        public string Status { get; set; }
        public string Quality { get; set; }
        public double QualityScore { get; set; }
        public string Size { get; set; }
        [DataMember(Name = "Error", EmitDefaultValue = false)]
        public Error Error { get; set; }
    }
}
