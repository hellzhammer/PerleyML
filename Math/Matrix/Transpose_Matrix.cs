using System;
using System.Collections.Generic;
namespace PerleyML_Core.Math.Matrix
{
    public class Transpose_Matrix
    {
        public double[][] Transpose_Jagged(double[][] original){
            List<double[]> newMat = new List<double[]>();
            for (int i = 0; i < original[0].Length; i++)
            {
                List<double> newd = new List<double>();
                for (int j = 0; j < original.Length; j++)
                {
                    newd.Add(original[j][i]);
                }
                newMat.Add(newd.ToArray());
            }
            return newMat.ToArray();
        }

        public double[,] Transpose_Dimmensional(double[,] original){
            double[,] newMat = new double[original.GetUpperBound(1) + 1, original.GetUpperBound(0) + 1];

            for (int i = 0; i < original.GetUpperBound(1) + 1; i++)
            {
                for (int j = 0; j < original.GetUpperBound(0) + 1; j++)
                {
                    newMat[i,j] = original[j,i];
                }
            }

            return newMat;
        }
    }
}