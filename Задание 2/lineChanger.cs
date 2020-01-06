using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace Task_2
{
    class LineChanger
    {
        public void ChangeLine(MatchData lineData)
        {
            try 
            { 
                int symbols = lineData.countSymbols();
                using (FileStream fileStream = new FileStream(lineData.FileName, FileMode.Open))
                {
                    byte[] bytes = new byte[fileStream.Length - (symbols + lineData.Line.Length + 1)];
                    fileStream.Seek((symbols + lineData.Line.Length + 1), SeekOrigin.Begin);
                    int i = 0;
                    while (fileStream.Position != fileStream.Length)
                    {
                        bytes[i] = (byte)fileStream.ReadByte();
                        i++;
                    }
                    fileStream.Seek(symbols, SeekOrigin.Begin);
                    fileStream.Write(Encoding.UTF8.GetBytes("Hello\n"), 0, "Hello\n".Length);
                    fileStream.Write(bytes, 0, (int)fileStream.Length - (symbols + lineData.Line.Length + Environment.NewLine.Length));
                }               
            }
            catch
            {
                throw;
            }

        }
    }
}
