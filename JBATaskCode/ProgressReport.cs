using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBATaskCode
{
    public class ProgressReport
    {
        public int RecordsPercentageComplete { get; set; } = 0;
        public bool Finished { get; set; }
    }
}
