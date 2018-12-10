using System;
using System.Collections.Generic;
using System.Text;

namespace JBATaskCode
{
    public class Record
    {
        public int Xref { get; set; }
        public int Yref { get; set; }
        public DateTime Date { get; set; }
        public int Value { get; set; }

        public Record()
        {

        }

        public Record(int xref, int yref, DateTime date, int value)
        {
            Xref = xref;
            Yref = yref;
            Date = date;
            Value = value;
        }
    }
}
