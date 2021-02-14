using MathNet.Numerics.LinearAlgebra;
using System;
namespace PerleyML_Core.Networking.MathNet
{
    /// <summary>
    /// Provides a series of static methods to aid in matrix creation 
    /// provides some activation functionality and prediction matrix prep
    /// </summary>
    public class MatrixManager
    {
        /// <summary>
        /// simple method to turn 2d double array into a matrix.
        /// </summary>
        /// <returns>The array.</returns>
        /// <param name="array">Array.</param>
        public static Matrix<double> MatrixArray(double[,] array)
        {
            return Matrix<double>.Build.DenseOfArray(array);
        }

        /// <summary>
        /// this multiplies the matrices together and returns result. again simple.
        /// </summary>
        /// <returns>The product.</returns>
        /// <param name="M1">M1.</param>
        /// <param name="M2">M2.</param>
        public static Matrix<double> dotProduct(Matrix<double> M1, Matrix<double> M2)
        {
            return M1 * M2;
        }

        /// <summary>
        /// builds a prediction matrix from random values generated at creation.
        /// </summary>
        /// <returns>The prediction matrix.</returns>
        /// <param name="Rows">Rows.</param>
        /// <param name="Columns">Columns.</param>
        /// <param name="MaxOutput">Max output.</param>
        /// <param name="MinOutput">Minimum output.</param>
        public static Matrix<double> BuildPredictionMatrix(int Rows, int Columns, double MaxOutput, double MinOutput )
        {
            Random r = new Random();
            double[,] m = new double[Rows, Columns];

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    double Scores = r.NextDouble() * (MaxOutput - MinOutput) + MinOutput;
                    m[i, j] = Scores;
                }
            }

            return Matrix<double>.Build.DenseOfArray(m);
        }

        /// <summary>
        /// returns the hyperbolic tangent of x
        /// </summary>
        /// <returns>The activate.</returns>
        /// <param name="x">Value.</param>
        public static Matrix<double> TanhMethod(Matrix<double> x, bool derivative)
        {
            if (derivative)
            {
                var sumsqr = x * x;
                return x.PointwiseMultiply(1 - sumsqr);
            }
            else
            {
                return x.PointwiseTanh();
            }
        }

        /// <summary>
        /// sigmoid method implementation
        /// </summary>
        /// <returns>The method.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="derivative">If set to <c>true</c> derivative.</param>
        public static Matrix<double> SigmoidMethod(Matrix<double> x, bool derivative)
        {
            if (derivative)
            {
                var scal = 1 - x;
                var rtnval = x.PointwiseMultiply(scal);
                return rtnval;
            }
            else
            {
                var pointw = x.PointwiseExp();
                //Console.WriteLine("Math.Net: " + pointw.ToMatrixString());
                var div = 1 / pointw;
                var add = 1 + div;
                var rtnval = 1 / add;
                return rtnval;
            }
        }
    }
}