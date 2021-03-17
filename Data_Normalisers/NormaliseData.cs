using System.Collections.Generic;
namespace PerleyML_Core.Data_Normalisers
{
    public class NormaliseData
    {
        private Dictionary<string, int> englishAlphabetDict = new Dictionary<string, int> {
            {"aA",1},
            {"bB",2},
            {"cC",3},
            {"dD",4},
            {"eE",5},
            {"fF",6},
            {"gG",7},
            {"hH",8},
            {"iI",9},
            {"jJ",10},
            {"kK",11},
            {"lL",12},
            {"mM",13},
            {"nN",14},
            {"oO",15},
            {"pP",16},
            {"qQ",17},
            {"rR",18},
            {"sS",19},
            {"tT",20},
            {"uU",21},
            {"vV",22},
            {"wW",23},
            {"xX",24},
            {"yY",25},
            {"zZ",26},
        };  

        public double numericAlphaConverter_Single(string character, bool normalize){
            int index = -1;

            foreach (var set in this.englishAlphabetDict)
            {
                for (int i = 0; i < set.Key.Length; i++)
                {
                    if(character == set.Key[i].ToString()){
                        index = set.Value;
                        break;
                    }   
                }
            }

            if(normalize){
                return this.NormaliseNumeric_Decimal(index, 26);
            }
            else{
                return index;
            }
        }

        public double[] numericAlphaConverter_Decimal(){
            List<double> items = new List<double>();

            foreach (var item in this.englishAlphabetDict)
            {
                items.Add(this.NormaliseNumeric_Decimal(item.Value, 26));
            }

            return items.ToArray();
        }

        public double[] numericAlphaConverter_Whole(){
            List<double> items = new List<double>();

            foreach (var item in this.englishAlphabetDict)
            {
                items.Add(double.Parse(item.Value.ToString()));
            }

            return items.ToArray();
        }

        public double NormaliseNumeric_Decimal(int left, int right)
        {
            return left / right;
        }
    }
}