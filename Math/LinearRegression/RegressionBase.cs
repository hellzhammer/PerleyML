using System;
using System.Collections.Generic;
using System.Linq;

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
        /// Gets the median f a given datast
        /// </summary>
        /// <returns>The median.</returns>
        /// <param name="data_set">Data set.</param>
        public double GetMedian(double[] data_set) 
        {
            Array.Sort(data_set);
            //is odd
            if (data_set.Length % 2 != 0)
            {
                //remove 1 item to make even and divide by 2.
                var midIndex = (data_set.Length - 1) / 2;
                // readd removed index to give actual result
                midIndex += 1;
                //return the adjusted index
                var median = data_set[midIndex];
                return median;
            }
            // is even
            else
            {
                int indexHalf = data_set.Length / 2;
                var item1 = data_set[indexHalf];
                var item2 = data_set[indexHalf + 1];
                var median = (item1 + item2) / 2;
                return median; 
            }
        }

        /// <summary>
        /// Finds the variace in a given data set
        /// </summary>
        /// <returns>The variance.</returns>
        /// <param name="dataset">Dataset.</param>
        public double GetVariance(double[] dataset,  double dataSquared)
        {
            var variance = dataSquared / (dataset.Length - 1);
            return variance;
        }

        /// <summary>
        /// finds the standard deviation of a given variance
        /// </summary>
        /// <returns>The standard dev.</returns>
        /// <param name="setVariance">Set variance.</param>
        public double GetStandardDev(double setVariance)
        {
            // just squaring the variance
            var sqaured = setVariance * setVariance;
            return sqaured;
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
