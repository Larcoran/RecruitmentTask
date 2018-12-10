using System;
using System.IO;

namespace JBATaskCode
{
    public class GetNextLineFromTheFile
    {
        StreamReader reader;
        private string currentLine;

        public bool EndOfFile = false;

        public GetNextLineFromTheFile(string filePath)
        {
            reader = new StreamReader(filePath, true);
        }

        public string GetLine()
        {
            if (!reader.EndOfStream)
            {
                currentLine = reader.ReadLine();
                return currentLine;
            }
            if (reader.EndOfStream)
            {
                EndOfFile = true;
                reader.Close();
            }
            return null;
        }

        public void CloseStream()
        {
            reader.Close();
        }

        public override string ToString()
        {
            return currentLine;
        }
    }
}
