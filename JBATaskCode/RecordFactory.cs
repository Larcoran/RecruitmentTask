using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBATaskCode
{
    public class RecordFactory
    {
        private string filePath;
        private int datesIncrement = 0;
        private int valuesIncrement = 0;
        public bool EndOfFile = false;

        LineFromFileToValuesConverter Converter = new LineFromFileToValuesConverter();
        GetNextLineFromTheFile getNextLine;

        public RecordFactory(string filePath)
        {
            this.filePath = filePath;
            getNextLine = new GetNextLineFromTheFile(this.filePath);
        }

        public Record GetNextRecord()
        {
            EndOfFile = getNextLine.EndOfFile;
            while (Converter.Values == null)
            {
                Converter.ConvertStringToValues(getNextLine.GetLine());
            }

            Record record = new Record(Converter.Xref, Converter.Yref, Converter.Dates[datesIncrement], Converter.Values[valuesIncrement]);

            if (datesIncrement == 119 && EndOfFile != true)
            {
                Converter.ConvertStringToValues(getNextLine.GetLine());
                EndOfFile = getNextLine.EndOfFile;
                datesIncrement = 0;
            }
            else
                datesIncrement++;
            if (valuesIncrement == 11 && EndOfFile != true)
            {
                Converter.ConvertStringToValues(getNextLine.GetLine());
                EndOfFile = getNextLine.EndOfFile;
                valuesIncrement = 0;
            }
            else
                valuesIncrement++;
            return record;
        }
    }
}
