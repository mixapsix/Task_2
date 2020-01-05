using System;
using System.Text;
using System.IO;

namespace Task_2
{
    /// <summary>
    /// Класс который содержит данные о файле и строке с совпадающей маской.
    /// </summary>
    /// 
    [Serializable]
    public class MatchData
    {
        public string FileName { get; set; }
        public int LineNumber { get; set; }
        public string Line { get; set; }

        public byte[] ConvertToByte()
        {
            return Encoding.UTF8.GetBytes(Line);
        }

        public int countSymbols()
        {
            using (StreamReader streamReader = new StreamReader(FileName))
            {
                int lineCount = 0;
                int symbols = 0;
                while (lineCount < LineNumber)
                {
                    
                    if(streamReader.Read() != 13)
                    {
                        symbols++;
                    }
                    else
                    {
                        symbols++;
                        lineCount++;
                    }
                }
                return symbols;
            }
        }
    }
}
