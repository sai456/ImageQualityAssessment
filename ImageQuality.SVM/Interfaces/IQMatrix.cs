using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQuality.SVM.Interfaces
{
    public interface IQMatrix
    {
        float[] GetQ(int column, int len);
        double[] GetQD();
        void SwapIndex(int i, int j);
    }
}
