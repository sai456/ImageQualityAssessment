using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageQuality.SVM
{
    [Serializable]
    public class Model
    {
        internal Model()
        {

        }

        public Parameter Parameter { get; set; }
        public int NumberOfClasses { get; set; }
        public int SupportVectorCount { get; set; }
        public Node[][] SupportVectors { get; set; }
        public double[][] SupportVectorCoefficients { get; set; }
        public int[] SupportVectorIndices { get; set; }
        public double[] Rho { get; set; }
        public double[] PairwiseProbabilityA { get; set; }
        public double[] PairwiseProbabilityB { get; set; }
        public int[] ClassLabels { get; set; }
        public int[] NumberOfSVPerClass { get; set; }
        public override bool Equals(object obj)
        {
            Model test = obj as Model;
            if (test == null)
                return false;
            bool same = ClassLabels.IsEqual(test.ClassLabels);
            same = same && NumberOfClasses == test.NumberOfClasses;
            same = same && NumberOfSVPerClass.IsEqual(test.NumberOfSVPerClass);

            if (PairwiseProbabilityA != null)
                same = same && PairwiseProbabilityA.IsEqual(test.PairwiseProbabilityA);
            if (PairwiseProbabilityB != null)
                same = same && PairwiseProbabilityB.IsEqual(test.PairwiseProbabilityB);
            same = same && Parameter.Equals(test.Parameter);
            same = same && Rho.IsEqual(test.Rho);
            same = same && SupportVectorCoefficients.IsEqual(test.SupportVectorCoefficients);
            same = same && SupportVectorCount == test.SupportVectorCount;
            same = same && SupportVectors.IsEqual(test.SupportVectors);

            return same;

        }

        public override int GetHashCode()
        {
            return ClassLabels.ComputeHashCode()+
                NumberOfClasses.GetHashCode()+
                NumberOfSVPerClass.ComputeHashCode() +
                PairwiseProbabilityA.ComputeHashCode() +
                PairwiseProbabilityB.ComputeHashCode() +
                Parameter.GetHashCode() +
                Rho.ComputeHashCode() +
                SupportVectorCoefficients.ComputeHashCode() +
                SupportVectorCount.GetHashCode() +
                SupportVectors.ComputeHashCode();
        }


        public static Model Read(Stream stream)
        {
            TemporaryCulture.Start();
            StreamReader input = new StreamReader(stream);
            //read Parameters
            Model model = new Model();
            Parameter parameter = new Parameter();
            model.Parameter = parameter;
            model.Rho = null;
            model.PairwiseProbabilityA = null;
            model.PairwiseProbabilityB = null;
            model.ClassLabels = null;
            model.NumberOfSVPerClass = null;

            bool headerFinished = false;
            while(!headerFinished)
            {
                string line = input.ReadLine();
                string cmd, arg;
                int splitIndex = line.IndexOf(' ');
                if(splitIndex>=0)
                {
                    cmd = line.Substring(0, splitIndex);
                    arg = line.Substring(splitIndex + 1);
                }
                else
                {
                    cmd = line;
                    arg = "";
                }
                arg = arg.ToLower();

                int i, n;
                #region Reading all neccessary parameters for Svm from allmodel file
                switch (cmd)
                {
                    case "svm_type":
                        parameter.SvmType = (SvmType)Enum.Parse(typeof(SvmType), arg.ToUpper());
                        break;
                    case "kernel_type":
                        if (arg == "polynomial")
                            arg = "poly";
                        parameter.KernelType = (KernelType)Enum.Parse(typeof(KernelType), arg.ToUpper());
                        break;
                    case "degree":
                        parameter.Degree = int.Parse(arg);
                        break;
                    case "gamma":
                        parameter.Gamma = double.Parse(arg);
                        break;
                    case "coef0":
                        parameter.Coefficient0 = double.Parse(arg);
                        break;

                    case "nr_class":
                        model.NumberOfClasses = int.Parse(arg);
                        break;

                    case "total_sv":
                        model.SupportVectorCount = int.Parse(arg);
                        break;
                    case "rho":
                        n = model.NumberOfClasses * (model.NumberOfClasses - 1) / 2;
                        model.Rho = new double[n];
                        string[] rhoParts = arg.Split();
                        for (i = 0; i < n; i++)
                            model.Rho[i] = double.Parse(rhoParts[i]);
                        break;
                    case "label":
                        n = model.NumberOfClasses;
                        model.ClassLabels = new int[n];
                        string[] labelParts = arg.Split();
                        for (i = 0; i < n; i++)
                            model.ClassLabels[i] = int.Parse(labelParts[i]);
                        break;

                    case "probA":
                        n = model.NumberOfClasses * (model.NumberOfClasses - 1) / 2;
                        model.PairwiseProbabilityA = new double[n];
                        string[] probAParts = arg.Split();
                        for (i = 0; i < n; i++)
                            model.PairwiseProbabilityA[i] = double.Parse(probAParts[i]);
                        break;

                    case "probB":
                        n = model.NumberOfClasses * (model.NumberOfClasses - 1) / 2;
                        model.PairwiseProbabilityB = new double[n];
                        string[] probBParts = arg.Split();
                        for (i = 0; i < n; i++)
                            model.PairwiseProbabilityB[i] = double.Parse(probBParts[i]);
                        break;

                    case "nr_sv":
                        n = model.NumberOfClasses;
                        model.NumberOfSVPerClass = new int[n];
                        string[] nrsvParts = arg.Split();
                        for (i = 0; i < n; i++)
                            model.NumberOfSVPerClass[i] = int.Parse(nrsvParts[i]);
                        break;

                    case "SV":
                        headerFinished = true;
                        break;

                    default:
                        throw new Exception("Unknown text in model file");

                }
                #endregion

            }
            #region Reading Support Vectors from all Models file
            int m = model.NumberOfClasses - 1;
            int l = model.SupportVectorCount;
            model.SupportVectorCoefficients = new double[m][];
            for (int i = 0; i < m; i++)
            {
                model.SupportVectorCoefficients[i] = new double[l];
            }
            model.SupportVectors = new Node[l][];

            for (int i = 0; i < l; i++)
            {
                string[] parts = input.ReadLine().Trim().Split();

                for (int k = 0; k < m; k++)
                    model.SupportVectorCoefficients[k][i] = double.Parse(parts[k]);
                int n = parts.Length - m;
                model.SupportVectors[i] = new Node[n];
                for (int j = 0; j < n; j++)
                {
                    string[] nodeParts = parts[m + j].Split(':');
                    model.SupportVectors[i][j] = new Node();
                    model.SupportVectors[i][j].Index = int.Parse(nodeParts[0]);
                    model.SupportVectors[i][j].Value = double.Parse(nodeParts[1]);
                }
            }
            #endregion

            TemporaryCulture.Stop();
            return model;

        }
        public static Model Read(string fileName)
        {
            FileStream input = File.OpenRead(fileName);
            try
            {
                return Read(input);
            }
            finally
            {
                input.Close();
            }
        }

        public static void Write(string fileName, Model model)
        {
            FileStream input = File.Open(fileName, FileMode.Create);
            try
            {
                Write(input, model);
            }
            finally
            {
                input.Close();
            }
        }
        public static void Write(Stream stream, Model model)
        {
            TemporaryCulture.Start();

            StreamWriter output = new StreamWriter(stream);

            Parameter param = model.Parameter;

            output.Write("svm_type {0}\n", param.SvmType);
            output.Write("kernel_type {0}\n", param.KernelType);

            if (param.KernelType == KernelType.POLY)
                output.Write("degree {0}\n", param.Degree);

            if (param.KernelType == KernelType.POLY || param.KernelType == KernelType.RBF || param.KernelType == KernelType.SIGMOID)
                output.Write("gamma {0:0.000000}\n", param.Gamma);

            if (param.KernelType == KernelType.POLY || param.KernelType == KernelType.SIGMOID)
                output.Write("coef0 {0:0.000000}\n", param.Coefficient0);

            int nr_class = model.NumberOfClasses;
            int l = model.SupportVectorCount;
            output.Write("nr_class {0}\n", nr_class);
            output.Write("total_sv {0}\n", l);

            {
                output.Write("rho");
                for (int i = 0; i < nr_class * (nr_class - 1) / 2; i++)
                    output.Write(" {0:0.000000}", model.Rho[i]);
                output.Write("\n");
            }

            if (model.ClassLabels != null)
            {
                output.Write("label");
                for (int i = 0; i < nr_class; i++)
                    output.Write(" {0}", model.ClassLabels[i]);
                output.Write("\n");
            }

            if (model.PairwiseProbabilityA != null)
            // regression has probA only
            {
                output.Write("probA");
                for (int i = 0; i < nr_class * (nr_class - 1) / 2; i++)
                    output.Write(" {0:0.000000}", model.PairwiseProbabilityA[i]);
                output.Write("\n");
            }
            if (model.PairwiseProbabilityB != null)
            {
                output.Write("probB");
                for (int i = 0; i < nr_class * (nr_class - 1) / 2; i++)
                    output.Write(" {0:0.000000}", model.PairwiseProbabilityB[i]);
                output.Write("\n");
            }

            if (model.NumberOfSVPerClass != null)
            {
                output.Write("nr_sv");
                for (int i = 0; i < nr_class; i++)
                    output.Write(" {0}", model.NumberOfSVPerClass[i]);
                output.Write("\n");
            }

            output.Write("SV\n");
            double[][] sv_coef = model.SupportVectorCoefficients;
            Node[][] SV = model.SupportVectors;

            for (int i = 0; i < l; i++)
            {
                for (int j = 0; j < nr_class - 1; j++)
                    output.Write("{0:0.000000} ", sv_coef[j][i]);

                Node[] p = SV[i];
                if (p.Length == 0)
                {
                    output.Write("\n");
                    continue;
                }
                if (param.KernelType == KernelType.PRECOMPUTED)
                    output.Write("0:{0:0.000000}", (int)p[0].Value);
                else
                {
                    output.Write("{0}:{1:0.000000}", p[0].Index, p[0].Value);
                    for (int j = 1; j < p.Length; j++)
                        output.Write(" {0}:{1:0.000000}", p[j].Index, p[j].Value);
                }
                output.Write("\n");
            }

            output.Flush();

            TemporaryCulture.Stop();
        }
    }
}
