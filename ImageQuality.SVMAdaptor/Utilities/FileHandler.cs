using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static ImageQuality.Model.Errors;

namespace ImageQuality.SVMAdaptor.Utilities
{
    public class FileHandler
    {
        public static async Task<bool> Download(string url, string path)
        {
            try
            {
                using (var client = new WebClient())
                {
                    await client.DownloadFileTaskAsync(url, path);
                    return true;
                }
            }
            catch (System.ArgumentNullException) { throw ServerSide.DownloadFailure("Null value for url or path"); }
            catch (System.Net.WebException ex) { throw ServerSide.UrlDownloadFailure(ex.Message + "|" + ex.ToString() + "|" + path); }
            catch (System.InvalidOperationException) { throw ServerSide.FileInUseError(); }
            catch (Exception ex) { throw new Exception(); }
        }

        public static bool DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
