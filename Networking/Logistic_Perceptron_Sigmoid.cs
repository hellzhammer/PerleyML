using System;
using PerleyML_Core.Math;
using PerleyML_Core.Math.Matrix;
using PerleyML_Core.Networking.Models;

namespace PerleyML_Core.Networking
{
    public class Logistic_Perceptron_Sigmoid : Activation, INetwork
    {
        public string NetworkID { get; set; }
        public double[,] weights { get; set; }
        public double MaxThreshold { get;  set; }
        public double MinThreshold { get; set; }
        public double Bias { get; set; }

        public Logistic_Perceptron_Sigmoid(int inputRows, int outputRows, double minThresh = 0, double maxThresh = 1, double bias = 1){
            this.MaxThreshold = maxThresh;
            this.MinThreshold = minThresh;
            this.Bias = bias;
            this.NetworkID = Guid.NewGuid().ToString();
            BuildPrediction_Matrix bpm = new BuildPrediction_Matrix();
            weights = bpm.BuildPredictionMatrix_Dimmensional_AllValues(inputRows, outputRows, maxThresh, minThresh);
        }

        public Logistic_Perceptron_Sigmoid(double[,] savedMatrix, string networkID, double minThresh = 0, double maxThresh = 1, double bias = 1)
        {
            this.MaxThreshold = maxThresh;
            this.MinThreshold = minThresh;
            this.weights = savedMatrix;
            this.NetworkID = networkID;
            this.Bias = bias;
        }

        public double[,] Evaluate(double[,] inputs){
            Matrix_Math math = new Matrix_Math();
            var dot = math.Dotproduct(inputs, weights);
            var rtnval = Sigmoid(dot, false);
            return rtnval;
        }

        public void Test(double[,] InputData, double[,] OutputData)
        {
            Console.WriteLine("\n LPS: ");
            Matrix_Math math = new Matrix_Math();
            Transpose_Matrix trans = new Transpose_Matrix();
            //here we determine the main output and the main error
            var output = Evaluate(InputData);
            Matrix_Console_Writer.ToDimmensionalString_Console(output);
            Console.WriteLine("\n");
            var error = math.Matrix_Subtract(OutputData, output);//mOutput - output;
            Matrix_Console_Writer.ToDimmensionalString_Console(error);
            Console.WriteLine("\n");
            //here we calculate the adjustment
            var sig = Sigmoid(output, true);
            Matrix_Console_Writer.ToDimmensionalString_Console(sig);
            Console.WriteLine("\n");
            var errorPointwise = math.PointwiseMultiply(error, sig);
            Matrix_Console_Writer.ToDimmensionalString_Console(errorPointwise);
            Console.WriteLine("\n");
            var inputTran = trans.Transpose_Dimmensional(InputData);
            Matrix_Console_Writer.ToDimmensionalString_Console(inputTran);
            Console.WriteLine("\n");
            var adjustment = math.Dotproduct(inputTran, errorPointwise);
            Console.WriteLine("\n");
            Matrix_Console_Writer.ToDimmensionalString_Console(adjustment);
            weights = math.Matrix_Addition(adjustment, weights);
            Console.WriteLine("\n OUT:");
            Matrix_Console_Writer.ToDimmensionalString_Console(weights);
        }

        public void Train(double[,] InputData, double[,] OutputData, int IterationCount){
            Matrix_Math math = new Matrix_Math();
            Transpose_Matrix trans = new Transpose_Matrix();
            for (int i = 0; i < IterationCount; i++)
            {
                //here we determine the main output and the main error
                var output = Evaluate(InputData);
                var error =  math.Matrix_Subtract(OutputData, output);//mOutput - output;

                //here we calculate the adjustment
                var inputTran = trans.Transpose_Dimmensional(InputData);
                var sig = Sigmoid(output, true);

                var errorPointwise = math.PointwiseMultiply(error, sig);

                var adjustment = math.Dotproduct(inputTran, errorPointwise);

                weights = math.Matrix_Addition(adjustment, weights);
            }
            Console.WriteLine("\n");
            Matrix_Console_Writer.ToDimmensionalString_Console(weights);
        }
    }
}