using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBATaskCode
{
    public class HeaderCreator
    {
        private string filePath;

        public HeaderCreator(string filePath)
        {
            this.filePath = filePath;
        }

        public List<string> CreateHeader()
        {
            List<string> header = new List<string>();
            GetNextLineFromTheFile nextLine = new GetNextLineFromTheFile(filePath);
            for (int i = 0; i < 5; i++)
            {
                header.Add(nextLine.GetLine());
            }
            nextLine.CloseStream();
            return header;
        }
    }
}
