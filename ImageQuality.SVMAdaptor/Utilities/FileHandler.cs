using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
            catch (System.ArgumentNullException)
            {
                throw new ArgumentNullException();
            }
            catch (System.Net.WebException ex) {
                throw new WebException();
            }
            catch (System.InvalidOperationException) {
                throw new InvalidOperationException();
            }
            catch (Exception ex) {
                throw new Exception();
            }
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
