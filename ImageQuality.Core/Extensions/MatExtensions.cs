using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQuality.Core
{
    public static class MatExtensions
    {
        public static double[,] ToDoubleArray(this Mat image)
        {
            if (image == null)
                return null;
            double[,] array = new double[image.Rows, image.Cols];
            var Rows = Enumerable.Range(0, image.Rows);
            var Cols = Enumerable.Range(0, image.Cols);
            Parallel.ForEach(Rows, i =>
            {

                foreach (var j in Cols)
                {
                    array[i, j] = image.At<double>(i, j);
                }
      
            });

            return array;
        }

        public static Mat ToMat(this Mat structdis, double[,] shiftArray)
        {
            if (shiftArray == null || structdis == null || shiftArray.Length < structdis.Cols * structdis.Rows)
                return null;
            Mat mat = new Mat(structdis.Size(), MatType.CV_64FC1, 1);
            var Rows = Enumerable.Range(0, structdis.Rows);
            var Cols = Enumerable.Range(0, structdis.Cols);
            Parallel.ForEach(Rows, i =>
            {
                foreach (var j in Cols)
                {
                    mat.Set<double>(i, j, shiftArray[i, j]);
                }
            });
            return mat;
        }
    }
}
