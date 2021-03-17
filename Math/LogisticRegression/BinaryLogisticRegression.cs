using System;
using System.Numerics;
using System.Collections.Generic;
using PerleyML_Core.Math.MatrixMath;

namespace PerleyML_Core.Math.LogisticRegression
{
    public class BinaryLogisticRegression
    {
        public string id { get; private set; }
        private double[,] synapticWeights { get; set; }

        public BinaryLogisticRegression(int MatrixRows, int MatrixCols)
        {
            this.id = Guid.NewGuid().ToString();
            synapticWeights = Matrix.BuildPredictionMatrix_Dimmensional_Binary(MatrixRows, MatrixCols);
        }

        public BinaryLogisticRegression(double[,] model, string _id)
        {
            synapticWeights = model;
            this.id = _id;
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
        /*
                weight function: w = error x input x output x ( 1 - output )

                tanh: f(x) = (ex - e-x) / (ex + e-x)

                sigmoid: f(x) = 1.0 / (1.0 + e-x)

                public double Tanh(double x)
        {
            return 2 / (1 + System.Math.Pow(System.Math.E, -(2 * x))) - 1;
        }
        public double DTanh(double x)
        {
            return 1 - System.Math.Pow(Tanh(x), 2);
        }
        public double Sigmoid(double val)
        {
            return (1 / (1 + System.Math.Pow(System.Math.E, (-val))));
        }
        */

        public double[,] tanhFunction(double[,] input, bool deriv)
        {
            if (!deriv)
            {

                List<double[]> ds = new List<double[]>();
                for (int x = 0; x < input.GetUpperBound(0)+1; x++)
                {
                    List<double> items = new List<double>();
                    for (int y = 0; y < input.GetUpperBound(1)+1; y++)
                    {
                        var item = (2*input[x,y]);
                        items.Add(item);
                    }                
                    ds.Add(items.ToArray());
                }

                var convert = Matrix.ToDimmensional(ds.ToArray());

                var exp = Matrix.PointwiseExp(convert);
                
                var add = Matrix.Matrix_Add_Scalar(1, exp);

                var divide = Matrix.Matrix_Divide_Scalar(2, add);

                var subtract = Matrix.Matrix_Subtract_Scalar(1, divide);
                return subtract;
            }
            else
            {
                // todo: implement a method to get a derivative of something. 
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
