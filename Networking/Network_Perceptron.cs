using MathNet.Numerics.LinearAlgebra;
using System;
using Newtonsoft.Json;
using PerleyML_Core.Networking.Models;
namespace PerleyML_Core.Networking.MathNet
{
    /// <summary>
    /// This class creates a single layer perceptron.
    /// Allows you to train and use network to evaluate problems.
    /// </summary>
    public class Network_Perceptron : NetworkModel
    {
        /// <summary>
        /// This constructor creates a brand new network from raw values.
        /// </summary>
        /// <param name="inputRows">Input rows.</param>
        /// <param name="outputRows">Output rows.</param>
        /// <param name="_MaxOutput">Max output.</param>
        /// <param name="_MinOutput">Minimum output.</param>
        public Network_Perceptron(int inputRows, int outputRows, double _MaxOutput = 1, double _MinOutput = 0, double bias = 1)
        {
            NetworkID = Guid.NewGuid().ToString();

            MaxThreshold = _MaxOutput;
            MinThreshold = _MinOutput;
            Bias = bias;
            weights = MatrixManager.BuildPredictionMatrix(inputRows, outputRows, _MaxOutput, _MinOutput);
        }

        /// <summary>
        /// This contructor is called when recreating a network
        /// </summary>
        /// <param name="netID">Net identifier.</param>
        /// <param name="_weights">Weights.</param>
        /// <param name="maxOutput">Max output.</param>
        /// <param name="minOutput">Minimum output.</param>
        public Network_Perceptron(string netID, double[,] _weights, double maxOutput, double minOutput)
        {
            this.NetworkID = netID;
            this.weights = MatrixManager.MatrixArray(_weights);
            this.MaxThreshold = maxOutput;
            this.MinThreshold = minOutput;
        }

        /// <summary>
        /// Trains the network with the supplied expected inputs and expected outputs.
        /// Iteration count is how many times the network will attempt to learn from this data.
        /// </summary>
        /// <param name="InputData"></param>
        /// <param name="OutputData"></param>
        /// <param name="IterationCount"></param>
        public void Train_Network(double[,] InputData, double[,] OutputData, int IterationCount)
        {
            var mInput = MatrixManager.MatrixArray(InputData);
            var mOutput = MatrixManager.MatrixArray(OutputData);

            for (int i = 0; i < IterationCount; i++)
            {
                var output = Evaluate(InputData);

                var error = mOutput - output;
                var mTrans = mInput.Transpose();
                var sig = MatrixManager.SigmoidMethod(output, true);
                var er = error.PointwiseMultiply(sig);
                var adjustment = MatrixManager.dotProduct(mTrans, er);

                weights += adjustment;
                Console.WriteLine("\n");
                Console.WriteLine(weights.ToMatrixString());
            }
        }

        public void Test(double[,] InputData, double[,] OutputData)
        {
            Console.WriteLine("\n Math.Net: ");
            var mInput = MatrixManager.MatrixArray(InputData);
            var mOutput = MatrixManager.MatrixArray(OutputData);

            var output = Evaluate(InputData);
            Console.WriteLine(output.ToMatrixString());

            var error = mOutput - output;
            Console.WriteLine(error.ToMatrixString());

            var mTrans = mInput.Transpose();
            var sig = MatrixManager.SigmoidMethod(output, true);
            Console.WriteLine(sig.ToMatrixString());
            var er = error.PointwiseMultiply(sig);
            Console.WriteLine(er.ToMatrixString());
            var adjustment = MatrixManager.dotProduct(mTrans, er);
            Console.WriteLine(adjustment.ToMatrixString());

            weights += adjustment;
            Console.WriteLine("\n OUT:");
            Console.WriteLine(weights.ToMatrixString());
        }

        /// <summary>
        /// Evaluates data and outputs a prediction.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public Matrix<double> Evaluate(double[,] inputs)
        {
            var minputs = MatrixManager.MatrixArray(inputs);
            var dot = MatrixManager.dotProduct(minputs, weights);
            //Console.WriteLine(dot.ToMatrixString());
            Matrix<double> rtnval = MatrixManager.SigmoidMethod(dot, false);
            //Console.WriteLine(rtnval.ToMatrixString());
            return rtnval;
        }

        /// <summary>
        /// Do not use currently. Untested.
        /// </summary>
        /// <returns></returns>
        public string ModelToJSON()
        {
            string rtnval = string.Empty;
            try
            {
                NetworkModel nm = new NetworkModel()
                {
                    NetworkID = this.NetworkID,
                    weights = this.weights,
                    MaxThreshold = this.MaxThreshold,
                    MinThreshold = this.MinThreshold
                };

                return JsonConvert.SerializeObject(nm);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return null;
            }
        }
    }
}