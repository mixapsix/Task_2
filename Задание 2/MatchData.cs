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
        public string LineBackup { get; set; }

        public byte[] ConvertToByte()
        {
            return Encoding.UTF8.GetBytes(Line);
        }

        public int countSymbols()
        {
            try
            {
                int lineCount = 0;
                int symbols = 0;
                int temp;
                if (Line != null)
                {
                    using (StreamReader streamReader = new StreamReader(FileName))
                    {

                        while (lineCount < LineNumber - 1)
                        {
                            temp = streamReader.Read();
                            if (temp != 13 && temp != 10)
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
                else
                {
                    using (StreamReader streamReader = new StreamReader(FileName))
                    {

                        while (lineCount < LineNumber - 1)
                        {
                            temp = streamReader.Read();
                            if (temp != 13 && temp != 10)
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
            catch
            {
                throw;
            }
        }
    }
}
