using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static ImageQuality.Model.Errors;

namespace ImageQuality.SVMAdaptor.Utilities
{
    public class MyStreamReader
    {
        public static MemoryStream GetMemoryStream(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return new MemoryStream(Encoding.UTF8.GetBytes(reader.ReadToEnd() ?? ""));
                }
            }
            catch (System.OutOfMemoryException) { throw ServerSide.OutOfMemoryWhileReadingStream(); }
            catch (System.IO.IOException) { throw ServerSide.IOErrorWhileReadingStream(); }
            catch (System.ArgumentNullException) { throw ServerSide.BufferNullToCreateMemoryStream(); }
            catch { throw ServerSide.StreamReadingFailure(); }
        }
    }
}
