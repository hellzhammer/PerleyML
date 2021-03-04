/*
using System;
using System.Collections.Generic;
using System.IO;
using PerleyML_Core.ML_IO.Interfaces;
namespace PerleyML_Core.ML_IO
{
    /// <summary>
    /// This class handles IO. 
    /// So far just loads Tsv and Csv Files.
    /// </summary>
    public class ML_IO : IML_IO
    {
        /// <summary>
        /// Loads a file from given path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public double[,] LoadTsv(string filePath){
            List<double[]> lines = new List<double[]>(); 
            string[] json = File.ReadAllLines(filePath);
            for (int i = 0; i < json.Length; i++)
            {
                List<double> newlines = new List<double>();
                if(!string.IsNullOrEmpty(json[i])){
                    var items = json[i].Split('\t');
                    for (int j = 0; j < items.Length; j++)
                    {
                        newlines.Add(double.Parse(items[j]));
                    }
                }
                lines.Add(newlines.ToArray());
            }

            double[,] matrix = new double[lines.Count, lines[0].Length];
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    matrix[i,j] = lines[i][j];
                }
            }
            return matrix;
        }

        /// <summary>
        /// Loads a file from given path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public double[,] LoadCsv(string filePath){
            List<double[]> lines = new List<double[]>(); 
            string[] json = File.ReadAllLines(filePath);
            for (int i = 0; i < json.Length; i++)
            {
                List<double> newlines = new List<double>();
                if(!string.IsNullOrEmpty(json[i])){
                    var items = json[i].Split(',');
                    for (int j = 0; j < items.Length; j++)
                    {
                        newlines.Add(double.Parse(items[j]));
                    }
                }
                lines.Add(newlines.ToArray());
            }

            double[,] matrix = new double[lines.Count, lines[0].Length];
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    matrix[i,j] = lines[i][j];
                }
            }
            return matrix;
        }

        /// <summary>
        /// reads a json file and then converts data into network and returns.
        /// </summary>
        /// <returns>The json model.</returns>
        /// <param name="filePath">File path.</param>
        public string readJsonModel(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            return null;
        }

        /// <summary>
        /// reads data and deserializes data model for use at runtime.
        /// </summary>
        /// <returns>The binary model.</returns>
        /// <param name="fileName">File name.</param>
        public string readBinaryModel(string fileName)
        {
            string s = string.Empty;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader r = new BinaryReader(fs))
                {
                    s = r.ReadString();
                    r.Close();
                    r.Dispose();
                }
                fs.Close();
                fs.Dispose();
            }
            return s;
        }

        /// <summary>
        /// creates a binary file from the network data model
        /// </summary>
        /// <returns><c>true</c>, if binary model was created, <c>false</c> otherwise.</returns>
        /// <param name="fileName">File name.</param>
        public bool createBinaryModel(string fileName, string data)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.CreateNew))
                {
                    using (BinaryWriter w = new BinaryWriter(fs))
                    {
                        w.Write(data);
                        w.Close();
                        w.Dispose();
                    }
                    fs.Close();
                    fs.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// creates json file and stores the model in the specified directory.
        /// </summary>
        /// <returns><c>true</c>, if json model was created, <c>false</c> otherwise.</returns>
        /// <param name="filePath">File path.</param>
        public bool createJsonModel(string filePath, string data)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    sw.Write(data);
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return false;
            }
            return true;
        }
    }
}
*/