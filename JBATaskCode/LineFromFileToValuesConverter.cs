using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBATaskCode
{
    public class LineFromFileToValuesConverter
    {
        public int Xref;
        public int Yref;
        public List<DateTime> Dates = new List<DateTime>();
        public List<int> Values;
        public bool EndOfFile = false;

        public void ConvertStringToValues(string line)
        {
            if (line != null)
            {
                if (line.Contains("Grid-ref="))
                {
                    List<int> GridRefs = ExtractIntsListFromString(line, ',', "Grid-ref=");
                    Xref = GridRefs[0];
                    Yref = GridRefs[1];
                }
                if (line.Contains("[Years="))
                {
                    List<int> tempYears = ExtractIntsListFromString(line, '-', "[Years=");
                    List<int> Years = new List<int>();
                    Years.AddRange(Enumerable.Range(tempYears[0], tempYears[1] - tempYears[0] + 1));
                    foreach (int year in Years)
                    {
                        for (int month = 1; month <= 12; month++)
                        {
                            Dates.Add(new DateTime(year, month, 1));
                        }
                    }
                }
                if (CheckDoesLineContainsOnlyNumbers(line))
                {
                    List<int> valuesInts = new List<int>();
                    string[] values = line.Split(' ');
                    foreach (string value in values)
                    {
                        if (value.Length > 5)
                        {
                            string temp = value.Insert(value.Length - 5, " ");
                            string[] longValueSplit = temp.Split(' ');
                            valuesInts.Add(int.Parse(longValueSplit[0]));
                            valuesInts.Add(int.Parse(longValueSplit[1]));
                        }
                        else if (value != "")
                            valuesInts.Add(int.Parse(value));
                    }
                    Values = valuesInts;
                }
            }
            else
                EndOfFile = true;
        }

        private List<int> ExtractIntsListFromString(string line, char splitChar, string subStringIndexChar)
        {
            string sub;
            sub = line.Substring(line.IndexOf(subStringIndexChar));
            sub = sub.Substring(sub.IndexOf(subStringIndexChar.Last()) + 1);
            if (sub.Contains(']') == true)
            {
                sub = sub.Remove(sub.IndexOf(']'));
            }
            string[] subsArray = sub.Split(splitChar);
            List<int> intRefs = new List<int>();
            for (int i = 0; i < subsArray.Length; i++)
            {
                int number;
                bool result;
                result = int.TryParse(subsArray[i], out number);
                if (result)
                {
                    intRefs.Add(number);
                }
            }
            return intRefs;
        }

        public bool CheckDoesLineContainsOnlyNumbers(string line)
        {
            char[] numbers = new char[11] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ' };
            foreach (char c in line)
            {
                if (!numbers.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
