using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace ImageQuality.SVM
{
    internal static class TemporaryCulture
    {
        private static CultureInfo _culture;

        public static void Start()
        {
            _culture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        public static void Stop()
        {
            Thread.CurrentThread.CurrentCulture = _culture;
        }
    }
}
