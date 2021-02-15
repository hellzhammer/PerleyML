using System;
namespace PerleyML_Core.Math.Matrix
{
    public class BuildPrediction_Matrix
    {
        public double[,] BuildPredictionMatrix_Dimmensional_SingleValue(int Rows, int Columns, double MaxOutput, double MinOutput){
            Random r = new Random();
            var rand = r.NextDouble();
            double[,] m = new double[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    double Scores = rand * (MaxOutput - MinOutput) + MinOutput;
                    m[i, j] = Scores;
                }
            }
            return m;
        }

        public double[,] BuildPredictionMatrix_Dimmensional_AllValues(int Rows, int Columns, double MaxOutput, double MinOutput){
            Random r = new Random();
            double[,] m = new double[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    double Scores = r.NextDouble() * (MaxOutput - MinOutput) + MinOutput;
                    m[i, j] = Scores;
                }
            }
            return m;
        }
    }
}