using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ImageQuality.Model
{
    
    public static partial class Errors
    {
        public static partial class ClientSide
        {
            public static BaseApplicationException ValidationFailure()
            {
                return new BadRequestException(FaultCodes.ValidationFailure, FaultMessages.ValidationFailure, HttpStatusCode.BadRequest);
            }
        }
    }
    
}
