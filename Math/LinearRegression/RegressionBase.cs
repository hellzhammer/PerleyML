using System;
using System.Collections.Generic;

namespace PerleyML_Core.Math.LinearRegression
{
    public class RegressionBase
    {
        public double[] x_data { get; set; }
        public double[] y_data { get; set; }

        /// <summary>
        /// gets the mean of a array
        /// </summary>
        /// <returns>The mean.</returns>
        /// <param name="data_set">Data set.</param>
        public double GetMean(double[] data_set)
        {
            double val = 0;
            for (int i = 0; i < data_set.Length; i++)
            {
                val += data_set[i];
            }
            var mean = val / data_set.Length;
            return mean;
        }

        /// <summary>
        /// adds two elements/arrays together
        /// </summary>
        /// <returns>The addition.</returns>
        /// <param name="dataset">Dataset.</param>
        public double Elemental_Addition(double[] dataset)
        {
            double rtnval = 0;
            for (int i = 0; i < dataset.Length; i++)
            {
                rtnval += dataset[i];
            }
            return rtnval;
        }

        /// <summary>
        /// squares an element/array
        /// </summary>
        /// <returns>The data.</returns>
        /// <param name="data_set">Data set.</param>
        public double Square_Data(double[] data_set)
        {
            List<double> _dset = new List<double>();
            for (int i = 0; i < data_set.Length; i++)
            {
                _dset.Add(data_set[i] * data_set[i]);
            }
            double rtnval = 0;
            for (int i = 0; i < _dset.Count; i++)
            {
                rtnval += _dset[i];
            }
            return rtnval;
        }

        /// <summary>
        /// subtracts a scalar from an array/element
        /// </summary>
        /// <returns>The subtract.</returns>
        /// <param name="data_set">Data set.</param>
        /// <param name="mean">Mean.</param>
        public double[] Scalar_Subtract(double[] data_set, double mean)
        {
            var rtnval = new List<double>();
            for (int i = 0; i < data_set.Length; i++)
            {
                rtnval.Add(data_set[i] - mean);
            }

            return rtnval.ToArray();
        }

        /// <summary>
        /// multiplies two elements/arrays together
        /// </summary>
        /// <returns>The multiply.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public double[] Elemental_Multiply(double[] x, double[] y)
        {
            List<double> rtnval = new List<double>();
            for (int i = 0; i < x.Length; i++)
            {
                rtnval.Add(x[i] * y[i]);
            }
            return rtnval.ToArray();
        }
    }
}
