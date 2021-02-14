using System;
using System.Collections.Generic;
namespace PerleyML_Core.Math.Matrix
{
    public class Matrix_Math
    {
        /// <summary>
        /// return a matrix after applying the exponent function
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public double[,] Pointwise_Exponent(double[,] original)
        {
            var newMatrix = original;

            for (var x = 0; x < original.GetUpperBound(0) + 1; x++)
            {
                for (var y = 0; y < original.GetUpperBound(1) + 1; y++)
                {
                    newMatrix[x, y] = System.Math.Pow(System.Math.E, newMatrix[x, y]); 
                }
            }

            return newMatrix;
        }

        /// <summary>
        /// divides a scalar by a matrix
        /// </summary>
        /// <returns>The divide scalar.</returns>
        /// <param name="scalar">Scalar.</param>
        /// <param name="matrix">Matrix.</param>
        public double[,] Matrix_Divide_Scalar(double scalar, double[,] matrix){
            double[,] newMat = new double[matrix.GetUpperBound(0), matrix.GetUpperBound(1)];
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < matrix.GetUpperBound(1) + 1; j++)
                {
                    newMat[i,j] = scalar / matrix[i,j];                     
                }
            }
            return newMat;
        }

        /// <summary>
        /// divides a matrix by a scalar
        /// </summary>
        /// <returns>The divide scalar.</returns>
        /// <param name="matrix">Matrix.</param>
        /// <param name="scalar">Scalar.</param>
        public double[,] Matrix_Divide_Scalar(double[,] matrix, double scalar){
            double[,] newMat = new double[matrix.GetUpperBound(0), matrix.GetUpperBound(1)];
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < matrix.GetUpperBound(1) + 1; j++)
                {
                    newMat[i,j] = matrix[i,j] / scalar;                     
                }
            }
            return newMat;
        }

        /// <summary>
        /// subtract a scalar from the right.
        /// </summary>
        /// <param name="original"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public double[,] Matrix_Subtract_Scalar(double[,] original, double scalar)
        {
            double[,] newD = new double[original.GetUpperBound(0) + 1, original.GetUpperBound(1) + 1];
            for (int i = 0; i < original.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < original.GetUpperBound(1) + 1; j++)
                {
                    newD[i, j] = original[i, j] - scalar;
                }
            }
            return newD;
        }

        /// <summary>
        /// subtracts a scalar from the left.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="original"></param>
        /// <returns></returns>
        public double[,] Matrix_Subtract_Scalar(double scalar, double[,] original)
        {
            double[,] newD = new double[original.GetUpperBound(0) + 1, original.GetUpperBound(1) + 1];
            for (int i = 0; i < original.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < original.GetUpperBound(1) + 1; j++)
                {
                    newD[i, j] = scalar - original[i, j];
                }
            }
            return newD;
        }

        /// <summary>
        /// subtracts one matrx from another
        /// </summary>
        /// <returns>The subtract.</returns>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        public double[,] Matrix_Subtract(double[,] left, double[,] right)
        {
            if (left.GetUpperBound(0) == right.GetUpperBound(0) && left.GetUpperBound(1) == right.GetUpperBound(1))
            {
                double[,] newD = new double[left.GetUpperBound(0) + 1, left.GetUpperBound(1) + 1];
                for (int i = 0; i < left.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < left.GetUpperBound(1) + 1; j++)
                    {
                        newD[i, j] = left[i, j] - right[i, j];
                    }
                }
                return newD;
            }
            return null;
        }

        /// <summary>
        /// returns a matrix with same dimmensions as provided matrices.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public double[,] PointwiseMultiply(double[,] left, double[,] right)
        {
            double[,] rtnval = new double[left.GetUpperBound(0) + 1, left.GetUpperBound(1) + 1];
            for (int i = 0; i < left.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < left.GetUpperBound(1) + 1; j++)
                {
                    rtnval[i, j] = left[i, j] * right[i, j];
                }
            }
            return rtnval;
        }

        /// <summary>
        /// get the dotproduct of 2 matrices
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public double[,] Dotproduct(double[,] left, double[,] right)
        {
            //matrix dimmesnions agree
            Transpose_Matrix tp = new Transpose_Matrix();

            double[][] jagMatLeft = ToJagged(left);
            double[][] jagMatRight = ToJagged(right);

            double[][] tranRight = tp.Transpose_Jagged(jagMatRight);

            List<double[]> endMat = new List<double[]>();
            for (int i = 0; i < jagMatLeft.Length; i++)
            {
                double[] line = BuildMatrixRow(jagMatLeft[i], tranRight);
                endMat.Add(line);
            }

            var end = ToDimmensional(endMat.ToArray());

            return end;
        }

        //builds a row for a new matrix in a dotproduct.
        private double[] BuildMatrixRow(double[] leftElement, double[][] rightMatrix)
        {
            List<double[]> rtnval = new List<double[]>();
            for (int e = 0; e < leftElement.Length; e++)
            {
                List<double> vals = new List<double>();
                for (int i = 0; i < rightMatrix.Length; i++)
                {
                    vals.Add(leftElement[e] * rightMatrix[i][e]);
                }
                rtnval.Add(vals.ToArray());
            }
            Transpose_Matrix tm = new Transpose_Matrix();
            var reTrans = tm.Transpose_Jagged(rtnval.ToArray());

            List<double> EndLine = new List<double>();
            for (int i = 0; i < reTrans.Length; i++)
            {
                double endVal = 0;
                for (int j = 0; j < reTrans[0].Length; j++)
                {
                    endVal += reTrans[i][j];
                }
                EndLine.Add(endVal);
            }

            return EndLine.ToArray();
        }

        /// <summary>
        /// returns a jagged version of the array == double[][]
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public double[][] ToJagged(double[,] original)
        {
            List<double[]> newD = new List<double[]>();
            for (int i = 0; i < original.GetUpperBound(0) + 1; i++)
            {
                List<double> ds = new List<double>();
                for (int y = 0; y < original.GetUpperBound(1) + 1; y++)
                {
                    ds.Add(original[i, y]);
                }
                newD.Add(ds.ToArray());
            }
            return newD.ToArray();
        }

        /// <summary>
        /// return a multi dimmensional version of the matrix == double[,]
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public double[,] ToDimmensional(double[][] original)
        {
            double[,] newD = new double[original.Length, original[0].Length];
            for (int i = 0; i < original.Length; i++)
            {
                for (int y = 0; y < original[0].Length; y++)
                {
                    newD[i, y] = original[i][y];
                }
            }
            return newD;
        }

        /// <summary>
        /// multiply a matrix by a scalar
        /// </summary>
        /// <returns>The multiply scalar.</returns>
        /// <param name="original">Original.</param>
        /// <param name="scalar">Scalar.</param>
        public double[,] Matrix_Multiply_Scalar(double[,] original, double scalar)
        {
            double[,] newD = new double[original.GetUpperBound(0) + 1, original.GetUpperBound(1) + 1];
            for (int i = 0; i < newD.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < newD.GetUpperBound(1) + 1; j++)
                {
                    newD[i, j] = original[i, j] * scalar;
                }
            }
            return newD;
        }

        /// <summary>
        /// multiply a scalar by a matrix
        /// </summary>
        /// <returns>The multiply scalar.</returns>
        /// <param name="scalar">Scalar.</param>
        /// <param name="original">Original.</param>
        public double[,] Matrix_Multiply_Scalar(double scalar, double[,] original)
        {
            double[,] newD = new double[original.GetUpperBound(0) + 1, original.GetUpperBound(1) + 1];
            for (int i = 0; i < newD.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < newD.GetUpperBound(1) + 1; j++)
                {
                    newD[i, j] = scalar * original[i, j];
                }
            }
            return newD;
        }

        /// <summary>
        /// add a scalar to a matrix
        /// </summary>
        /// <returns>The addition scalar.</returns>
        /// <param name="scalar">Scalar.</param>
        /// <param name="original">Original.</param>
        public double[,] Matrix_Addition_Scalar(double scalar, double[,] original)
        {
            double[,] newD = new double[original.GetUpperBound(0) + 1, original.GetUpperBound(1) + 1];

            for (int i = 0; i < original.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < original.GetUpperBound(1) + 1; j++)
                {
                    newD[i, j] = scalar * original[i, j];
                }
            }

            return newD;
        }

        /// <summary>
        /// add a scalar from the right to a matrix
        /// </summary>
        /// <returns>The addition scalar.</returns>
        /// <param name="original">Original.</param>
        /// <param name="scalar">Scalar.</param>
        public double[,] Matrix_Addition_Scalar(double[,] original, double scalar)
        {
            double[,] newD = new double[original.GetUpperBound(0) + 1, original.GetUpperBound(1) + 1];

            for (int i = 0; i < original.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < original.GetUpperBound(1) + 1; j++)
                {
                    newD[i, j] = original[i, j] * scalar;
                }
            }

            return newD;
        }

        /// <summary>
        /// adds 2 matrices together
        /// </summary>
        /// <returns>The addition.</returns>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        public double[,] Matrix_Addition(double[,] left, double[,] right)
        {
            if (left.GetUpperBound(0) == right.GetUpperBound(0) && left.GetUpperBound(1) == right.GetUpperBound(1))
            {
                double[,] newD = new double[left.GetUpperBound(0) + 1, left.GetUpperBound(1) + 1];

                for (int i = 0; i < left.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < left.GetUpperBound(1) + 1; j++)
                    {
                        newD[i, j] = left[i, j] + right[i, j];
                    }
                }

                return newD;
            }

            return null;
        }
    }
}