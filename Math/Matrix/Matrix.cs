using System;
using System.Collections.Generic;
namespace PerleyML_Core.Math.Matrix
{
    public class Matrix
    {
        private double[,] value { get; set; }
        public Matrix(double[,] matrix)
        {
            this.value = matrix;
        }

        public Matrix(int matrixRows, int matrixCols, double maxOutput, double minOutput){
            // build matrix with random values
            this.value = this.BuildPredictionMatrix_Dimmensional(matrixRows, matrixCols, maxOutput, minOutput);
        }

        public Matrix(double[][] matrix){
            this.value = Matrix.ToDimmensional(matrix);
        }

        #region Transpose
            public static double[][] Transpose_Jagged(double[][] original){
            List<double[]> newMat = new List<double[]>();
            for (int i = 0; i < original[0].Length; i++)
            {
                List<double> newd = new List<double>();
                for (int j = 0; j < original.Length; j++)
                {
                    newd.Add(original[j][i]);
                }
                newMat.Add(newd.ToArray());
            }
            return newMat.ToArray();
        }

        public static double[,] Transpose_Dimmensional(double[,] original){
            double[,] newMat = new double[original.GetUpperBound(1) + 1, original.GetUpperBound(0) + 1];

            for (int i = 0; i < original.GetUpperBound(1) + 1; i++)
            {
                for (int j = 0; j < original.GetUpperBound(0) + 1; j++)
                {
                    newMat[i,j] = original[j,i];
                }
            }

            return newMat;
        }
        #endregion

        #region Divide
        /// <summary>
        /// divides a matrix by a scalar
        /// </summary>
        /// <returns>The divide scalar.</returns>
        /// <param name="matrix">Matrix.</param>
        /// <param name="scalar">Scalar.</param>
        public double[,] Matrix_Divide_Scalar(double scalar, bool from_right)
        {
            if (from_right)
            {
                double[,] newMat = new double[this.value.GetUpperBound(0), this.value.GetUpperBound(1)];
                for (int i = 0; i < this.value.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < this.value.GetUpperBound(1) + 1; j++)
                    {
                        newMat[i, j] = this.value[i, j] / scalar;
                    }
                }
                return newMat;
            }
            else
            {
                double[,] newMat = new double[this.value.GetUpperBound(0), this.value.GetUpperBound(1)];
                for (int i = 0; i < this.value.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < this.value.GetUpperBound(1) + 1; j++)
                    {
                        newMat[i, j] = scalar / this.value[i, j];
                    }
                }
                return newMat;
            }
        }
        #endregion
        #region Subtract
        /// <summary>
        /// subtracts a scalar from the left.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="original"></param>
        /// <returns></returns>
        public double[,] Matrix_Subtract_Scalar(double scalar, bool from_right)
        {
            if (!from_right)
            {
                double[,] newD = new double[this.value.GetUpperBound(0) + 1, this.value.GetUpperBound(1) + 1];
                for (int i = 0; i < this.value.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < this.value.GetUpperBound(1) + 1; j++)
                    {
                        newD[i, j] = scalar - this.value[i, j];
                    }
                }
                return newD;
            }
            else
            {
                double[,] newD = new double[this.value.GetUpperBound(0) + 1, this.value.GetUpperBound(1) + 1];
                for (int i = 0; i < this.value.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < this.value.GetUpperBound(1) + 1; j++)
                    {
                        newD[i, j] = this.value[i, j] - scalar;
                    }
                }
                return newD;
            }
        }

        /// <summary>
        /// subtracts one matrx from another
        /// </summary>
        /// <returns>The subtract.</returns>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        public static double[,] Matrix_Subtract(double[,] left, double[,] right)
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
        #endregion
        #region Addition
        /// <summary>
        /// add a scalar from the right to a matrix
        /// </summary>
        /// <returns>The addition scalar.</returns>
        /// <param name="original">Original.</param>
        /// <param name="scalar">Scalar.</param>
        public double[,] Matrix_Addition_Scalar(double scalar, bool from_right)
        {
            if (from_right)
            {
                double[,] newD = new double[this.value.GetUpperBound(0) + 1, this.value.GetUpperBound(1) + 1];

                for (int i = 0; i < this.value.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < this.value.GetUpperBound(1) + 1; j++)
                    {
                        newD[i, j] = this.value[i, j] + scalar;
                    }
                }

                return newD;
            }
            else
            {
                double[,] newD = new double[this.value.GetUpperBound(0) + 1, this.value.GetUpperBound(1) + 1];

                for (int i = 0; i < this.value.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < this.value.GetUpperBound(1) + 1; j++)
                    {
                        newD[i, j] = scalar + this.value[i, j];
                    }
                }

                return newD;
            }
        }

        /// <summary>
        /// adds 2 matrices together
        /// </summary>
        /// <returns>The addition.</returns>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        public static double[,] Matrix_Addition(double[,] left, double[,] right)
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
        #endregion
        #region Multiply
        /// <summary>
        /// returns a matrix with same dimmensions as provided matrices.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static double[,] PointwiseMultiply(double[,] left, double[,] right)
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
        public static double[,] Dotproduct(double[,] left, double[,] right)
        {
            //matrix dimmesnions agree
            double[][] jagMatLeft = Matrix.ToJagged(left);
            double[][] jagMatRight = Matrix.ToJagged(right);

            double[][] tranRight = Matrix.Transpose_Jagged(jagMatRight);

            List<double[]> endMat = new List<double[]>();
            for (int i = 0; i < jagMatLeft.Length; i++)
            {
                double[] line = Matrix.BuildMatrixRow(jagMatLeft[i], tranRight);
                endMat.Add(line);
            }

            var end = Matrix.ToDimmensional(endMat.ToArray());

            return end;
        }

        /// <summary>
        /// multiply a scalar by a matrix
        /// multiply from the left
        /// </summary>
        /// <returns>The multiply scalar.</returns>
        /// <param name="scalar">Scalar.</param>
        /// <param name="original">Original.</param>
        public double[,] Matrix_Multiply_Scalar(double scalar, bool from_right)
        {
            if (!from_right)
            {
                double[,] newD = new double[this.value.GetUpperBound(0) + 1, this.value.GetUpperBound(1) + 1];
                for (int i = 0; i < newD.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < newD.GetUpperBound(1) + 1; j++)
                    {
                        newD[i, j] = scalar * this.value[i, j];
                    }
                }
                return newD;
            }
            else
            {
                double[,] newD = new double[this.value.GetUpperBound(0) + 1, this.value.GetUpperBound(1) + 1];
                for (int i = 0; i < newD.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < newD.GetUpperBound(1) + 1; j++)
                    {
                        newD[i, j] = this.value[i, j] * scalar;
                    }
                }
                return newD;
            }
        }
        #endregion
        #region misc
        private double[,] BuildPredictionMatrix_Dimmensional(int Rows, int Columns, double MaxOutput, double MinOutput){
            int rand = new Random().Next(-1, 1);            
            double[,] m = new double[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    double Scores = rand * (MaxOutput - MinOutput) + MinOutput;
                    m[i, j] = Scores;
                }
            }
            return m;
        }
        public double[,] getValue(){
            return this.value;
        }
        
        public double[][] getJagged(){
            return Matrix.ToJagged(this.value);
        }
        //builds a row for a new matrix in a dotproduct.
        private static double[] BuildMatrixRow(double[] leftElement, double[][] rightMatrix)
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
            var reTrans = Matrix.Transpose_Jagged(rtnval.ToArray());

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
        public static double[][] ToJagged(double[,] original)
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
        public static double[,] ToDimmensional(double[][] original)
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
        #endregion
    }
}
