using System.Collections.Generic;
namespace PerleyML_Core.Math.LogisticRegression
{
    public class MultinomialLogisticRegression
    {
        public List<BinaryLogisticRegression> logits { get; private set; }
        public MultinomialLogisticRegression()
        {
            logits = new List<BinaryLogisticRegression>();
        }

        /// <summary>
        /// initializes a new array of binary logits
        /// </summary>
        /// <param name="MatrixRows">Matrix rows.</param>
        /// <param name="MatrixCols">Matrix cols.</param>
        /// <param name="maxOuput">Max ouput.</param>
        public void Init(int MatrixRows, int MatrixCols, int maxOuput)
        {
            for (int i = 0; i < maxOuput; i++)
            {
                var b = new BinaryLogisticRegression(MatrixRows, MatrixCols);
                this.logits.Add(b);
            }
        }

        /// <summary>
        /// for manually adding new logits
        /// if you chose not to use init
        /// </summary>
        /// <param name="logit">Individual Logit.</param>
        public void ManualInit(BinaryLogisticRegression logit)
        {
            this.logits.Add(logit);
        }

        /// <summary>
        /// for manually adding new logits
        /// if you chose not to use init
        /// </summary>
        /// <param name="_logits">List of logits.</param>
        public void ManualInit(List<BinaryLogisticRegression> _logits)
        {
            this.logits = _logits;
        }

        /// <summary>
        /// calculates the weights for each logit
        /// </summary>
        /// <param name="inputs">Inputs.</param>
        /// <param name="outputs">Outputs.</param>
        /// <param name="trainingIterations">Training iterations.</param>
        public void TrainLogits(double[,] inputs, double[,] outputs, int trainingIterations)
        {
            for (int i = 0; i < this.logits.Count; i++)
            {
                var thisOutput = this.ConvertOuput(i, outputs);
                logits[i].Train(inputs, outputs, 25000);
            }
        }

        /// <summary>
        /// Converts the original ouput into an 
        /// output that this single network can use.
        /// </summary>
        /// <returns>The ouput.</returns>
        /// <param name="expectedOutput">Expected output.</param>
        /// <param name="outputs">Outputs.</param>
        private double[,] ConvertOuput(double expectedOutput, double[,] outputs)
        {
            double[,] rtndata = new double[outputs.GetUpperBound(0) + 1, outputs.GetUpperBound(1) + 1];
            for (int x = 0; x < outputs.GetUpperBound(0) + 1; x++)
            {
                for (int y = 0; y < outputs.GetUpperBound(1) + 1; y++)
                {
                    if (outputs[x,y].ToString() != expectedOutput.ToString()) 
                    {
                        rtndata[x, y] = 0;
                    }

                    else if (outputs[x, y].ToString() == expectedOutput.ToString())
                    {
                        rtndata[x, y] = 1;
                    }
                }
            }

            return rtndata;
        }

        /// <summary>
        /// evaluates an expression and 
        /// returns the list of outputs
        /// </summary>
        /// <returns>The logit outputs.</returns>
        /// <param name="input">Main inputs.</param>
        public List<double[,]> Evaluate(double[,] input)
        {
            List<double[,]> outputs = new List<double[,]>();
            for (int i = 0; i < this.logits.Count; i++)
            {
                var output = this.logits[i].Evaluate(input);
                outputs.Add(output);
            }
            return outputs;
        }
    }
}
