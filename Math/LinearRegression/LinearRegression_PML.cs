using System.Collections.Generic;
using System.Linq;

namespace PerleyML_Core.Math.LinearRegression
{
    public class LinearRegression_PML : RegressionBase
    {
        public LinearRegression_PML(double[] _y_data, double[] _x_data)
        {
            this.x_data = _x_data;
            this.y_data = _y_data;
        }

        /// <summary>
        /// use linear regression to analyze dataset
        /// </summary>
        /// <returns>The regression analysis.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public (double variance, 
            double standard_deviation, 
            double correlation, 
            double slope, 
            double y_intercept) Linear_Regression_Analysis(double[] x, double[] y)
        {
            var variance = this.GetVariance(x);
            var standardDev = this.GetStandardDeviation(x);
            var correlation = this.GetCorrelation(x, y);
            var slope = this.GetSlope(x, y, correlation);
            var yIntercept = this.GetYIntercept(x, y, slope);

            return (variance, standardDev, correlation, slope, yIntercept);
        }

        /// <summary>
        /// gets the variance of a dataset
        /// </summary>
        /// <returns>The variance.</returns>
        /// <param name="data_set">Data set.</param>
        private double GetVariance(double[] data_set)
        {
            var mean = this.GetMean(data_set);
            List<double> xMinusAverage = this.Scalar_Subtract(data_set, mean).ToList<double>();
            double sumOfSquares = Square_Data(xMinusAverage.ToArray());
            double variance = sumOfSquares / (data_set.Length - 1);
            return variance;
        }

        /// <summary>
        /// gets the standard deviation from a dataset
        /// </summary>
        /// <returns>The standard deviation.</returns>
        /// <param name="data_set">Data set.</param>
        private double GetStandardDeviation(double[] data_set)
        {
            var _var = GetVariance(data_set);
            return _var * _var;
        }

        /// <summary>
        /// solve using single regression
        /// // -1 == perfect downhill relation 
        /// -0.60+ == strong downhill relation 
        /// 0 = no relation
        /// + 0.60+ == strong uphill relation
        /// +1 == perfect uphill relation
        /// </summary>
        /// <returns>The forb0b1.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        private double GetCorrelation(double[] x, double[] y)
        {
            double xmean = this.GetMean(x);
            double ymean = this.GetMean(y);
            double standardDevX = this.GetStandardDeviation(x);
            double standardDevY = this.GetStandardDeviation(y);
            double[] xMinusMean = Scalar_Subtract(x, xmean);
            double[] yMinusMean = Scalar_Subtract(y, ymean);
            double[] multiplyXY = Elemental_Multiply(xMinusMean, yMinusMean);
            double totalXY = Elemental_Addition(multiplyXY);
            var sumOfMeans = totalXY / (xmean * ymean);
            var correlation = sumOfMeans / (x.Length - 1);
            return correlation;
        }

        /// <summary>
        /// Gets the slope from a correlation
        /// </summary>
        /// <returns>The slope.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="correlation">Correlation.</param>
        private double GetSlope(double[] x, double[] y, double correlation)
        {
            double xmean = this.GetMean(x);
            double ymean = this.GetMean(y);
            var meanDiv = ymean / xmean;
            var slope = correlation * meanDiv;
            return slope;
        }

        /// <summary>
        /// finds the yintercept of the data set
        /// </summary>
        /// <returns>The YI ntercept.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="slope">Slope.</param>
        private double GetYIntercept(double[] x, double[] y, double slope)
        {
            double xmean = this.GetMean(x);
            double ymean = this.GetMean(y);

            var slopeMultX = slope * xmean;
            var subFromY = ymean - slopeMultX;
            return subFromY;
        }

        /// <summary>
        /// predict future output
        /// </summary>
        /// <returns>The prediction.</returns>
        /// <param name="yIntercept">Y intercept.</param>
        /// <param name="xVal">X value.</param>
        public double MakePrediction(double yIntercept, double xVal)
        {
            return (yIntercept * xVal) + 1;
        }
    }
}
