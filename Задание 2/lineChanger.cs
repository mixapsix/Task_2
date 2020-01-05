using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task_2
{
    class LineChanger
    {
        public void ChangeLine(MatchData lineData)
        {
            int symbols = lineData.countSymbols();
            using (FileStream fileStream = new FileStream(lineData.FileName, FileMode.Open))
            {                
                fileStream.Seek(symbols, SeekOrigin.Begin);
                fileStream.Write(Encoding.UTF8.GetBytes("Hello"), 0 , Encoding.UTF8.GetBytes("Hello").Length);

            }

        }
    }
}
