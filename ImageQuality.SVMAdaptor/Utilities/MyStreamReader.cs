using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            catch (System.OutOfMemoryException) { throw new NotImplementedException(); }
            catch (System.IO.IOException) { throw new NotImplementedException(); }
            catch (System.ArgumentNullException) { throw new NotImplementedException(); }
            catch { throw new NotImplementedException(); }
        }
    }
}
