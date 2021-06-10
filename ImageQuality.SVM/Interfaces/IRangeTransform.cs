using System;
using System.Collections.Generic;
using System.Text;

namespace ImageQuality.SVM.Interfaces
{
    public interface IRangeTransform
    {
        double Transform(double input, int index);
        Node[] Transform(Node[] input);
    }
}
