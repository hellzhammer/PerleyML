using System;
namespace PerleyML_Core.Networking.Models
{
    public interface INetwork
    {
        string NetworkID { get; set; }
        double[,] weights { get; set; }
        double MaxThreshold { get; set; }
        double MinThreshold { get; set; }
        double Bias { get; set; }

        double[,] Evaluate(double[,] inputs);
        void Train(double[,] InputData, double[,] OutputData, int IterationCount);
    }
}
