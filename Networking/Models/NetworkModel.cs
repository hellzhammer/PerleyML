using MathNet.Numerics.LinearAlgebra;
namespace PerleyML_Core.Networking.Models
{
    public class NetworkModel
    {
        public string NetworkID { get; set; }
        public Matrix<double> weights { get; set; }
        public double MaxThreshold { get; set; }
        public double MinThreshold { get; set; }
        public double Bias { get; set; }
    }
}