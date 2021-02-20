using System.Collections.Generic;

namespace PerleyML_Core.Math.LinearRegression
{
    public class LinearRegression_OLS : RegressionBase
    {
        public LinearRegression_OLS(double[] _y_data, double[] _x_data)
        {
            this.x_data = _x_data;
            this.y_data = _y_data;
        }

        /// <summary>
        /// Solve for B1
        /// </summary>
        /// <returns>The b1.</returns>
        /// <param name="squaredTotal">Squared total.</param>
        /// <param name="xyTotal">Xy total.</param>
        private double GetA(double squaredTotal, double xyTotal)
        {
            return xyTotal / squaredTotal;
        }

        /// <summary>
        /// Solve for B0
        /// </summary>
        /// <returns>The BN ot.</returns>
        /// <param name="ymean">Ymean.</param>
        /// <param name="xmean">Xmean.</param>
        /// <param name="b1">B1.</param>
        private double GetB(double ymean, double xmean, double b1)
        {
            var sum = b1 * xmean;
            var b0 = ymean - sum;
            return b0;
        }

        /// <summary>
        /// solve using single regression
        /// </summary>
        /// <returns>The forb0b1.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public (double x_mean, double y_mean, double sumOfSquares, double Variance, double StandardDev, double sumOfProducts, double a, double b) Solve_OrdinaryLeastSquares()
        {
            double xmean = this.GetMean(this.x_data);
            double ymean = this.GetMean(this.y_data);

            double[] xMinusMean = this.Scalar_Subtract(this.x_data, xmean);
            double[] yMinusMean = this.Scalar_Subtract(this.y_data, ymean);

            double _sumOfSquares = this.Square_Data(xMinusMean);
            double variance = this.GetVariance(this.x_data, _sumOfSquares);
            double standardDev = this.GetStandardDev(variance);

            double[] multiplyXY = this.Elemental_Multiply(xMinusMean, yMinusMean);

            double _sumOfProducts = this.Elemental_Addition(multiplyXY);
            //var sig = 1 / (1 + (1 / _sumOfProducts));
            double a = this.GetA(_sumOfSquares, _sumOfProducts);
            double b = this.GetB(ymean, xmean, a);

            return (xmean, ymean, _sumOfSquares, variance, standardDev, _sumOfProducts, a, b);
        }

        /// <summary>
        /// solve for rsquared from single regression
        /// </summary>
        /// <returns>The for RS quared.</returns>
        /// <param name="b1">B1.</param>
        /// <param name="b0">B0.</param>
        public double SolveForRSquared(double b1, double b0)
        {
            double ymean = this.GetMean(this.y_data);

            double[] yMinusMean = this.Scalar_Subtract(this.y_data, ymean);
            double yMeanTotal = this.Square_Data(yMinusMean);

            List<double> Yvals = new List<double>();
            for (int i = 0; i < this.x_data.Length; i++)
            {
                Yvals.Add(b0 + b1 * this.x_data[i]);
            }
            List<double> YvalsMinusMean = new List<double>();
            for (int i = 0; i < Yvals.Count; i++)
            {
                //Math.Round(d, 2); 
                YvalsMinusMean.Add(System.Math.Round(System.Math.Round(Yvals[i], 1) - ymean, 1));
            }
            double YvalsSquared = this.Square_Data(YvalsMinusMean.ToArray());

            return System.Math.Round(YvalsSquared, 1) / System.Math.Round(yMeanTotal, 1);
        }

        /// <summary>
        /// solve for standard error
        /// from single regression
        /// </summary>
        /// <returns>The for standard error.</returns>
        /// <param name="b1">B1.</param>
        /// <param name="b0">B0.</param>
        public double SolveForStandardError(double b1, double b0)
        {
            double ymean = this.GetMean(this.y_data);
            double[] yMinusMean = this.Scalar_Subtract(this.y_data, ymean);
            double yMeanSq = this.Square_Data(yMinusMean);

            List<double> yEstVals = new List<double>();
            for (int i = 0; i < this.x_data.Length; i++)
            {
                yEstVals.Add(b0 + b1 * this.x_data[i]);
            }

            List<double> yEstMinusY = new List<double>();
            for (int i = 0; i < yEstVals.Count; i++)
            {
                yEstMinusY.Add(yEstVals[i] - this.y_data[i]);
            }

            List<double> yEstMinusSquared = new List<double>();
            for (int i = 0; i < yEstMinusY.Count; i++)
            {
                yEstMinusSquared.Add(yEstMinusY[i] * yEstMinusY[i]);
            }

            double tot = 0;
            for (int i = 0; i < yEstMinusSquared.Count; i++)
            {
                tot += yEstMinusSquared[i];
            }

            double observations = yEstMinusY.Count - 2;
            var sqrt1 = System.Math.Sqrt(System.Math.Round(tot, 1) / observations);
            return sqrt1;
        }

        /*
        public void Logistic_Analasys(double[] y, double[] x)
        {
            double xmean = this.GetMean(x);
            double ymean = this.GetMean(y);

            double[] xMinusMean = Scalar_Subtract(x, xmean);
            double[] yMinusMean = Scalar_Subtract(y, ymean);

            double _sumOfSquares = Square_Data(xMinusMean);
            double variance = this.GetVariance(x, _sumOfSquares);
            double standardDev = this.GetStandardDev(variance);

            double[] multiplyXY = Elemental_Multiply(xMinusMean, yMinusMean);

            double _sumOfProducts = Elemental_Addition(multiplyXY);
            var sigmoid = this.Sigmoid(_sumOfProducts);
            //double a = GetA(_sumOfSquares, _sumOfProducts);
            //double b = GetB(ymean, xmean, a);

            //return (xmean, ymean, _sumOfSquares, variance, standardDev, _sumOfProducts, a, b);
        }
        */

        /*
        /// <summary>
        /// performs a multiple regression analasys
        /// </summary>
        /// <returns>The forb0bi.</returns>
        /// <param name="y">The y coordinate.</param>
        /// <param name="x">The x coordinate.</param>
        public static (double Total, List<(double x_mean, double x_square, double b0, double b1, double total)> Values) MultipleRegression(double[] y, double[][] x)
        {
            List<(double x_mean, double x_square, double b0, double b1, double total)> bvals = new List<(double x_mean, double x_square, double b0, double b1, double total)>();

            for (int i = 0; i < x.Length; i++)
            {
                LinearRegression_PML pml = new LinearRegression_PML(y, x[i]);

                double ymean = pml.GetMean(y);
                double[] yMinusMean = pml.Scalar_Subtract(y, ymean);

                double xmean = pml.GetMean(x[i]);
                double[] xMinusMean = pml.Scalar_Subtract(x[i], xmean);
                double xSquared = pml.Square_Data(xMinusMean);
                double[] multiplyXY = pml.Elemental_Multiply(xMinusMean, yMinusMean);
                double totalXY = pml.Elemental_Addition(multiplyXY);
                double b1 = pml.GetB1(xSquared, totalXY);
                double b0 = pml.GetBNot(ymean, xmean, b1);
                double total = b0 * b1;
                bvals.Add((xmean, xSquared, b0, b1, total));
            }

            double Y = 0;
            for (int i = 0; i < bvals.Count; i++)
            {
                Y += bvals[i].total;
            }

            return (Y, bvals);
        }
        */
    }
}
