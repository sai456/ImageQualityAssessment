using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ImageQuality.SVM
{
    public class Node : IComparable<Node>
    {
        internal int _index;
        internal double _value;

        ///<summary>
        /// Default Constructor
        /// </summary>

        public Node()
        {

        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="index">The index of the value</param>
        /// <param name="value">The value to store</param>
        public Node(int index, double value)
        {
            _index = index;
            _value = value;
        }
        /// <summary>
        /// Index of this Node
        /// </summary>
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
            }
        }
        /// <summary>
        /// Value at Index
        /// </summary>
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// String representation of this Node as {index}:{value}.
        /// </summary>
        /// <returns>{index}:{value}</returns>

        public override string ToString()
        {
            return String.Format("{0}:{1}", _index, _value.Truncate());
        }

        public override bool Equals(object obj)
        {
            Node other = obj as Node;
            if (other == null)
                return false;

            return _index == other._index && _value.Truncate() == other._value.Truncate();
        }

        public override int GetHashCode()
        {
            return _index.GetHashCode() + _value.GetHashCode();
        }

        #region IComparable<Node> Members
        /// <summary>
        /// Compares this node with another
        /// </summary>
        /// <param name="other"> The node compares to</param>
        /// <returns>A positive number if this node is greater, a negative number if it is less than, or 0 if equal</returns>
        public int CompareTo(Node other)
        {
            return _index.CompareTo(other._index);
        }
        #endregion
    }
}
