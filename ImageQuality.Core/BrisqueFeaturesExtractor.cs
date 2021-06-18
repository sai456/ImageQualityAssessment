using ImageQuality.Model;
using MathNet.Numerics;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQuality.Core
{
    public class BrisqueFeaturesExtractor : IImageFeaturesExtractor
    {
        public async Task<(List<double> features, string size)> ComputeImageFeaturesAsync(string fileName)
        {
            Cv2.SetNumThreads(0);
            using(var image = Cv2.ImRead(fileName,ImreadModes.Color))
            {
                var imageHeight = image.Size().Height;
                var imageWidth = image.Size().Width;
                if (imageHeight == 0 || imageWidth == 0)
                    return  (new List<double>(), GetImageSize(imageHeight,imageWidth));
                return (await CalculateBrisqueFeatures(image), GetImageSize(imageHeight,imageWidth));
            }
        }

        private async Task<List<double>> CalculateBrisqueFeatures(Mat image)
        {
            Cv2.SetNumThreads(0);
            var brisqueFeatures = new List<double>();

            Mat originalImage = new Mat(image.Size(), MatType.CV_64FC1, 1);
            Cv2.CvtColor(image, originalImage, ColorConversionCodes.BGR2GRAY);
            image.Dispose();
            Mat originalImageGrayScale = new Mat(originalImage.Size(), MatType.CV_64FC1, 1);
            originalImage.ConvertTo(originalImageGrayScale, MatType.CV_64FC1, 1);
            originalImage.Dispose();
            int scaleNum = 2;
            for(int itr_scale = 1; itr_scale <= scaleNum; itr_scale++)
            {
                Size newImageSize = new Size((originalImageGrayScale.Cols / Math.Pow(2, itr_scale - 1)), originalImageGrayScale.Rows / Math.Pow(2, itr_scale - 1));
                Mat imagescaledDistance = new Mat();
                Cv2.Resize(originalImageGrayScale, imagescaledDistance, newImageSize, 0, 0, InterpolationFlags.Cubic);
                imagescaledDistance.ConvertTo(imagescaledDistance, MatType.CV_64FC1, 1.0 / 255.0);
                Mat meanImage = new Mat(imagescaledDistance.Size(), MatType.CV_64FC1, 1.0 / 255.0);
                Cv2.GaussianBlur(imagescaledDistance, meanImage, new Size(7, 7), 1.166);
                Mat meanSquareImage = new Mat();
                Cv2.Pow(meanImage, 2.0, meanSquareImage);
                Mat sigma = new Mat();
                Cv2.Multiply(imagescaledDistance, imagescaledDistance, sigma);
                Cv2.GaussianBlur(sigma, sigma, new Size(7, 7), 1.166);
                Cv2.Subtract(sigma, meanSquareImage, sigma);
                Cv2.Pow(sigma, 0.5, sigma);
                Cv2.Add(sigma, new Scalar(1.0 / 255), sigma);
                Mat structuredDistance = new Mat(imagescaledDistance.Size(), MatType.CV_64FC1, 1);
                Cv2.Subtract(imagescaledDistance, meanImage, structuredDistance);
                Cv2.Divide(structuredDistance, sigma, structuredDistance);

                double lsigma_best = 0, rsigma_best = 0, gamma_best = 0.0;

                structuredDistance = AGGDFit(structuredDistance, ref lsigma_best, ref rsigma_best, ref gamma_best);
                brisqueFeatures.Add(gamma_best);
                brisqueFeatures.Add((lsigma_best * lsigma_best + rsigma_best * rsigma_best) / 2.0);
                int[,] shifts = new int[,]
                {
                            { 0,1},
                            { 1,0},
                            { 1,1},
                            {1,-1 }
                };
                for (int itr_shift = 1; itr_shift <= 4; itr_shift++)
                {

                    Mat shiftestructuredDistance = new Mat(imagescaledDistance.Size(), MatType.CV_64FC1, 1);
                    double[,] originalArray = structuredDistance.ToDoubleArray();
                    double[,] shiftedArray = shiftestructuredDistance.ToDoubleArray();
                    List<int> Rows = Enumerable.Range(0, structuredDistance.Rows).ToList();
                    Parallel.ForEach(Rows, i => {

                        for (int j = 0; j < structuredDistance.Cols; j++)
                        {
                            if (i + shifts[itr_shift - 1, 0] >= 0 && i + shifts[itr_shift - 1, 0] < structuredDistance.Rows && j + shifts[itr_shift - 1, 1] >= 0 && j + shifts[itr_shift - 1, 1] < structuredDistance.Cols)

                            {

                                shiftedArray[i, j] = originalArray[i + shifts[itr_shift - 1, 0], j + shifts[itr_shift - 1, 1]];

                            }
                            else
                                shiftedArray[i, j] = 0;
                        }
                    });


                    shiftestructuredDistance = shiftestructuredDistance.ToMat(shiftedArray);
                    Cv2.Multiply(structuredDistance, shiftestructuredDistance, shiftestructuredDistance);

                    shiftestructuredDistance = AGGDFit(shiftestructuredDistance, ref lsigma_best, ref rsigma_best, ref gamma_best);
                    double constant = Math.Sqrt((SpecialFunctions.Gamma(1 / gamma_best)) / (SpecialFunctions.Gamma(3 / gamma_best)));
                    double meanparam = (rsigma_best - lsigma_best) * (SpecialFunctions.Gamma(2 / gamma_best) / SpecialFunctions.Gamma(1 / gamma_best)) * constant;

                    //feature parameters
                    brisqueFeatures.Add(gamma_best);
                    brisqueFeatures.Add(meanparam);
                    brisqueFeatures.Add(Math.Pow(lsigma_best, 2));
                    brisqueFeatures.Add(Math.Pow(rsigma_best, 2));

                }

            }
            return brisqueFeatures;

        }


        //Assymetric generalised guassian distribution
        private Mat AGGDFit(Mat image, ref double lsigma_best, ref double rsigma_best, ref double gamma_best)
        {
            double[,] ImArr = image.ToDoubleArray();
            double poscount = 0, negcount = 0;
            double possqsum = 0, negsqsum = 0, abssum = 0;
            double prevgamma = 0;
            double prevdiff = 1E10;
            float sampling = 0.001f;
            

            for(int i=0;i<image.Rows;i++)
            {
                for(int j=0;j<image.Cols;j++)
                {
                    double pt = ImArr[i, j];

                    if (pt > 0)
                    {
                        poscount++;
                        possqsum += pt * pt;
                        abssum += pt;
                    }
                    else if(pt < 0)
                    {
                        negcount++;
                        negsqsum += pt * pt;
                        abssum -= pt;
                    }
                }
            }
            lsigma_best = Math.Pow(negsqsum / negcount, 0.5);
            rsigma_best = Math.Pow(possqsum / poscount, 0.5);

            double gammahat = lsigma_best / rsigma_best;
            long totalcount = (image.Cols) * (image.Rows);
            double rhat = Math.Pow(abssum / totalcount, 2) / ((negsqsum + possqsum) / totalcount);
            double rhatnorm = rhat * (Math.Pow(gammahat, 3) + 1) * (gammahat + 1) / Math.Pow(Math.Pow(gammahat, 2) + 1, 2);

            for (float gam = 0.2f; gam < 10; gam += sampling)
            {
                double r_gamma = SpecialFunctions.Gamma(2 / gam) * SpecialFunctions.Gamma(2 / gam) / (SpecialFunctions.Gamma(1 / gam) * SpecialFunctions.Gamma(3 / gam));
                double diff = Math.Abs(r_gamma - rhatnorm);
                if (diff > prevdiff) break;
                prevdiff = diff;
                prevgamma = gam;
            }
            gamma_best = prevgamma;
            return image;

        }

        private string GetImageSize(int imageHeight,int imageWidth)
        {
            if (imageHeight <= 200 || imageWidth <= 200)
                return KeyStore.ImageSize.Small;
            if (imageWidth <= 500 || imageHeight <= 500)
                return KeyStore.ImageSize.Medium;
            return KeyStore.ImageSize.Large;
        }
    }
}
