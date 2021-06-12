using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ImageQuality.Model
{
    public static partial class Errors
    {
        public static partial class ServerSide
        {
            public static BaseApplicationException FileCreationFailure()
            {
                return new BadRequestException(FaultCodes.FileCreationFailure, FaultMessages.FileCreationFailure, HttpStatusCode.InternalServerError);
            }

            public static BaseApplicationException OutOfMemoryWhileReadingStream()
            {
                return new BadRequestException(FaultCodes.OutOfMemoryWhileReadingStream, FaultMessages.OutOfMemoryWhileReadingStream, HttpStatusCode.InternalServerError);
            }

            public static BaseApplicationException StreamReadingFailure()
            {
                return new BadRequestException(FaultCodes.StreamReadingFailure, FaultMessages.StreamReadingFailure, HttpStatusCode.InternalServerError);
            }

            public static BaseApplicationException IOErrorWhileReadingStream()
            {
                return new BadRequestException(FaultCodes.IOErrorWhileReadingStream, FaultMessages.IOErrorWhileReadingStream, HttpStatusCode.InternalServerError);
            }

            public static BaseApplicationException BufferNullToCreateMemoryStream()
            {
                return new BadRequestException(FaultCodes.BufferNullToCreateMemoryStream, FaultMessages.BufferNullToCreateMemoryStream, HttpStatusCode.InternalServerError);
            }

            public static BaseApplicationException UrlDownloadFailure(string reason)
            {
                return new BadRequestException(FaultCodes.UrlDownloadFailure, FaultMessages.UrlDownloadFailure + ": " + reason, HttpStatusCode.BadRequest);
            }

            public static BaseApplicationException FileInUseError()
            {
                return new BadRequestException(FaultCodes.FileInUseError, FaultMessages.FileInUseError, HttpStatusCode.MethodNotAllowed);
            }

            public static BaseApplicationException NoBrisqueFeatures()
            {
                return new BadRequestException(FaultCodes.NoBrisqueFeatures, FaultMessages.NoBrisqueFeatures, HttpStatusCode.MethodNotAllowed);
            }

            public static BaseApplicationException DownloadFailure(string reason)
            {
                return new BadRequestException(FaultCodes.DownloadFailure, FaultMessages.DownloadFailure + " : " + reason, HttpStatusCode.InternalServerError);
            }

        }
    }
}
