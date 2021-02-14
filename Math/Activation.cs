using System;
using PerleyML_Core.Math.Matrix;

namespace PerleyML_Core.Math
{
    public class Activation
    {
        /// <summary>
        /// perform relu on an input
        /// </summary>
        /// <returns>The relu.</returns>
        /// <param name="input">Input.</param>
        /// <param name="deriv">If set to <c>true</c> deriv.</param>
        public double RELU(double input, bool deriv)
        {
            if (input > 0)
            {
                if (deriv)
                {
                    return 1;
                }
                else
                {
                    return input;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// perform the sigmoid function on a matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="deriv"></param>
        /// <returns></returns>
        public double[,] Sigmoid(double[,] matrix, bool deriv)
        {
            Matrix_Math math = new Matrix_Math();
            var newMatrix = matrix;

            if (!deriv)
            {
                double[,] pointwiseOut = math.Pointwise_Exponent(matrix);
                var div = math.Matrix_Divide_Scalar(1, pointwiseOut);
                var add = math.Matrix_Addition_Scalar(1, div);
                newMatrix = math.Matrix_Divide_Scalar(1, add);
            }
            else
            {
                var subMat = math.Matrix_Subtract_Scalar(1, matrix);
                newMatrix = math.PointwiseMultiply(matrix, subMat);
            }

            return newMatrix;
        }
    }
}
