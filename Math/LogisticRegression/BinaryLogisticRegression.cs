using System;

namespace PerleyML_Core.Math.LogisticRegression
{
    public class BinaryLogisticRegression
    {
        private double[,] synapticWeights { get; set; }

        public BinaryLogisticRegression(int MatrixRows, int MatrixCols)
        {
            synapticWeights = MatrixMath.Matrix.BuildPredictionMatrix_Dimmensional(MatrixRows, MatrixCols);
        }

        public double[,] sigmoidFunction(double[,] input, bool deriv)
        {
            if (!deriv)
            {
                var exp = MatrixMath.Matrix.PointwiseExp(input);
                var add = MatrixMath.Matrix.Matrix_Add_Scalar(1, exp);
                var divide = MatrixMath.Matrix.Matrix_Divide_Scalar(1, add);
                return divide;
            }
            else
            {
                var subtract = MatrixMath.Matrix.Matrix_Subtract_Scalar(1, input);
                var pointwiseMultiply = MatrixMath.Matrix.Dotproduct(input, subtract);
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
                var error = MatrixMath.Matrix.Matrix_Subtract(mtrainingOutput, output);


                var adjustment = dotProduct(MatrixMath.Matrix.Transpose_Dimmensional(mtrainingInput), MatrixMath.Matrix.PointwiseMultiply(error, sigmoidFunction(output, true)));


                synapticWeights = MatrixMath.Matrix.Matrix_Addition(synapticWeights, adjustment);
            }
        }

        public double[,] Evaluate(double[,] inputs)
        {
            return this.sigmoidFunction(this.dotProduct(inputs, synapticWeights), false);
        }

        private double[,] dotProduct(double[,] matrixOne, double[,] matrixTwo)
        {
            return MatrixMath.Matrix.Dotproduct(matrixOne, matrixTwo);
        }
    }
}
