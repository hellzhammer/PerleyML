namespace PerleyML_Core.Networking.MathNet
{
    public class NetworkBuilder
    {
        /// <summary>
        /// returns a simple binary classifier
        /// </summary>
        /// <returns>The binary classifier.</returns>
        /// <param name="inputRowCount">Input row count.</param>
        /// <param name="outputMax">Output max.</param>
        /// <param name="outputMin">Output minimum.</param>
        public static Network_Perceptron BuildBinaryClassifier(int inputRowCount, int outputMax = 1, int outputMin = 0)
        {
            return new Network_Perceptron(inputRowCount, 2, outputMax, outputMin);
        }

        /// <summary>
        /// returns a simple multiclass classifier
        /// </summary>
        /// <returns>The multi class classifier.</returns>
        /// <param name="inputRowCount">Input row count.</param>
        /// <param name="labelCount">Label count.</param>
        /// <param name="outputMax">Output max.</param>
        /// <param name="outputMin">Output minimum.</param>
        public static Network_Perceptron BuildMultiClassClassifier(int inputRowCount, int labelCount, int outputMax = 1, int outputMin = 0)
        {
            return new Network_Perceptron(inputRowCount, labelCount, outputMax, outputMin);
        }
    }
}