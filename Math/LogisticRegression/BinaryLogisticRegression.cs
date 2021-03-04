using System;
using PerleyML_Core.Math.MatrixMath;

namespace PerleyML_Core.Math.LogisticRegression
{
    public class BinaryLogisticRegression
    {
        public string id { get; private set; }
        private double[,] synapticWeights { get; set; }

        public BinaryLogisticRegression(int MatrixRows, int MatrixCols)
        {
            synapticWeights = Matrix.BuildPredictionMatrix_Dimmensional_Binary(MatrixRows, MatrixCols);
        }

        public double[,] sigmoidFunction(double[,] input, bool deriv)
        {
            if (!deriv)
            {
                var exp = Matrix.PointwiseExp(input);
                var add = Matrix.Matrix_Add_Scalar(1, exp);
                var divide = Matrix.Matrix_Divide_Scalar(1, add);
                return divide;
            }
            else
            {
                var subtract = Matrix.Matrix_Subtract_Scalar(1, input);
                var pointwiseMultiply = Matrix.Dotproduct(input, subtract);
                return pointwiseMultiply;
            }
        }

        public void Train(double[,] trainingInput, double[,] trainingOutput, int trainingIterations)
        {
            var mtrainingInput = trainingInput;
            var mtrainingOutput = trainingOutput;

            for (int i = 0; i < trainingIterations; i++)
            {
                var output = Evaluate(trainingInput);
                var error = Matrix.Matrix_Subtract(mtrainingOutput, output);


                var adjustment = dotProduct(Matrix.Transpose_Dimmensional(mtrainingInput), Matrix.PointwiseMultiply(error, sigmoidFunction(output, true)));


                synapticWeights = Matrix.Matrix_Addition(synapticWeights, adjustment);
            }
        }

        public double[,] Evaluate(double[,] inputs)
        {
            return this.sigmoidFunction(this.dotProduct(inputs, synapticWeights), false);
        }

        private double[,] dotProduct(double[,] matrixOne, double[,] matrixTwo)
        {
            return Matrix.Dotproduct(matrixOne, matrixTwo);
        }
    }
}
