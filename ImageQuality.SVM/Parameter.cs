using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuality.SVM
{
    public enum SvmType
    {
        /// <summary>
        /// C-SVC.
        /// </summary>
        C_SVC,
        /// <summary>
        /// nu-SVC.
        /// </summary>
        NU_SVC,
        /// <summary>
        /// one-class SVM
        /// </summary>
        ONE_CLASS,
        /// <summary>
        /// epsilon-SVR
        /// </summary>
        EPSILON_SVR,
        /// <summary>
        /// nu-SVR
        /// </summary>
        NU_SVR
    }

    /// <summary>
    /// Contains the various kernel types this library can use.
    /// </summary>
    public enum KernelType
    {
        /// <summary>
        /// Linear: u'*v
        /// </summary>
        LINEAR,
        /// <summary>
        /// Polynomial: (gamma*u'*v + coef0)^degree
        /// </summary>
        POLY,
        /// <summary>
        /// Radial basis function: exp(-gamma*|u-v|^2)
        /// </summary>
        RBF,
        /// <summary>
        /// Sigmoid: tanh(gamma*u'*v + coef0)
        /// </summary>
        SIGMOID,
        /// <summary>
        /// Precomputed kernel
        /// </summary>
        PRECOMPUTED,
    };

    /// <summary>
    /// This class contains the various parameters which can affect the way in which an SVM
    /// is learned.  Unless you know what you are doing, chances are you are best off using
    /// the default values.
    /// </summary>
    [Serializable]
    public class Parameter:ICloneable
    {
        // <summary>
        /// Default Constructor.  Gives good default values to all parameters.
        /// </summary>
        public Parameter()
        {
            SvmType = SvmType.C_SVC;
            KernelType = KernelType.RBF;
            Degree = 3;
            Gamma = 0;
            Coefficient0 = 0;
            Nu = 0.5;
            CacheSize = 40;
            C = 1;
            EPS = 1e-3;
            P = 0.1;
            Shrinking = true;
            Probability = false;
            Weights = new Dictionary<int, double>();

            
        }

        public SvmType SvmType { get; set; }
        public KernelType KernelType { get; set; }
        public int Degree { get; set; }
        public double Gamma { get; set; }
        public double Coefficient0 { get; set; }
        public double CacheSize { get; set; }
        public double EPS { get; set; }
        public double C { get; set; }
        public Dictionary<int,double> Weights { get; private set; }
        public double Nu { get; set; }
        public double P { get; set; }

        public bool Shrinking { get; set; }
        public bool Probability { get; set; }

        public override bool Equals(object obj)
        {
            Parameter other = obj as Parameter;
            if (other == null)
                return false;

            return other.C == C &&
                other.CacheSize == CacheSize &&
                other.Coefficient0 == Coefficient0 &&
                other.Degree == Degree &&
                other.EPS == EPS &&
                other.Gamma == Gamma &&
                other.KernelType == KernelType &&
                other.Nu == Nu &&
                other.P == P &&
                other.Probability == Probability &&
                other.Shrinking == Shrinking &&
                other.SvmType == SvmType &&
                other.Weights.ToArray().IsEqual(Weights.ToArray());
        }

        public override int GetHashCode()
        {
            return C.GetHashCode() +
                CacheSize.GetHashCode() +
                Coefficient0.GetHashCode() +
                Degree.GetHashCode() +
                EPS.GetHashCode() +
                Gamma.GetHashCode() +
                KernelType.GetHashCode() +
                Nu.GetHashCode() +
                P.GetHashCode() +
                Probability.GetHashCode() +
                Shrinking.GetHashCode() +
                SvmType.GetHashCode() +
                Weights.ToArray().ComputeHashCode();

        }

        #region IClonable Members
        public object Clone()
        {
            return base.MemberwiseClone();
        }
        #endregion
    }
}
