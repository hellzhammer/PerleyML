using System;
namespace PerleyML_Core.ML_IO
{
    public class Matrix_Console_Writer
    {
        public static void ToJaggedString_Console(double[][] jagTran){
            for (int i = 0; i < jagTran.Length; i++)
            {
                string line = "";
                for (int j = 0; j < jagTran[i].Length; j++)
                {
                    if(j == 0){
                        line += jagTran[i][j].ToString();
                    }
                    else{
                        line += ", " + jagTran[i][j].ToString();
                    }
                }
                Console.WriteLine(line);
            }
        }
        
        public static void ToDimmensionalString_Console(double[,] dimTran){
            for (int i = 0; i < dimTran.GetUpperBound(0) + 1; i++)
            {
                string line = "";
                for (int j = 0; j < dimTran.GetUpperBound(1) + 1; j++)
                {
                    if(j == 0){
                        line += dimTran[i,j].ToString();
                    }
                    else{
                        line += ", " + dimTran[i,j].ToString();
                    }
                }
                Console.WriteLine(line);
            }
        }
    }
}