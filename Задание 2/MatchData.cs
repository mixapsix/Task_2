using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    /// <summary>
    /// Класс который содержит данные о файле и строке с совпадающей маской.
    /// </summary>
    public class MatchData
    {
        public string FileName { get; set; }
        public int LineNumber { get; set; }
        public string Line { get; set; }
    }
}
