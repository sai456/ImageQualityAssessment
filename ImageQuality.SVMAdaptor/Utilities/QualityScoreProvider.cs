using ImageQuality.SVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using static ImageQuality.Model.KeyStore;

namespace ImageQuality.SVMAdaptor.Utilities
{
    public static class QualityScoreProvider
    {
        private static SVM.Model _model;
        private static RangeTransform Transform { set; get; }

        private static readonly string SvmModelPath = Path.Combine(BasePath.BaseDirectory,Location.Model);

        internal static async Task<double> GetQualityScoreAsync(IList<double> brisqueFeatures)
        {
            var problem = new Problem(1, new double[1] { 1 }, ToNodes(brisqueFeatures), brisqueFeatures.Count);
            if (Transform == null)
                Transform = GetRangeTransform();
            if (_model == null)
                _model = SVM.Model.Read(MyStreamReader.GetMemoryStream(SvmModelPath));
            var scaled = Transform.Scale(problem);
            return Math.Round(Math.Abs(_model.Predict(scaled.X[0])), 2);
        }

        private static Node[][] ToNodes(IList<double>brisqueFeatures)
        {
            Node[] nodes = new Node[brisqueFeatures.Count];
            for(int i =0;i<brisqueFeatures.Count;i++)
            {
                nodes[i] = new Node(i + 1, brisqueFeatures[i]);
            }
            return new Node[1][] { nodes };
        }

        private static RangeTransform GetRangeTransform()
        {
            var minValues = new double[36]
                               {
                                    0.336999 ,0.019667 ,0.230000 ,-0.125959 ,0.000167 ,0.000616 ,0.231000 ,-0.125873 ,0.000165 ,0.000600 ,0.241000 ,-0.128814 ,0.000179 ,0.000386 ,0.243000 ,-0.133080 ,0.000182 ,0.000421 ,0.436998 ,0.016929 ,0.247000 ,-0.200231 ,0.000104 ,0.000834 ,0.257000 ,-0.200017 ,0.000112 ,0.000876 ,0.257000 ,-0.155072 ,0.000112 ,0.000356 ,0.258000 ,-0.154374 ,0.000117 ,0.000351
                               };
            var maxValues = new double[36]
                                {
                                            9.999411, 0.807472, 1.644021, 0.202917, 0.712384, 0.468672, 1.644021, 0.169548, 0.713132, 0.467896, 1.553016, 0.101368, 0.687324, 0.533087, 1.554016, 0.101000, 0.689177, 0.533133, 3.639918, 0.800955, 1.096995, 0.175286, 0.755547, 0.399270, 1.095995, 0.155928, 0.751488, 0.402398, 1.041992, 0.093209, 0.623516, 0.532925, 1.042992, 0.093714, 0.621958, 0.534484
                                };
            return new RangeTransform(minValues, maxValues, -1, 1);
        }
    }
}
