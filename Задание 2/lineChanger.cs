using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace Task_2
{
    public class LineChanger
    {
        public MatchData ChangeLine(MatchData lineData, string regime)
        {
            try
            {
                int symbols = lineData.countSymbols();
                int offset = (symbols + lineData.Line.Length + 1);
                int i = 0;
                MatchData matchData = lineData;
                switch (regime)
                {
                    case "1":
                        {
                            using (FileStream fileStream = new FileStream(lineData.FileName, FileMode.Open))
                            {
                                byte[] bytes = new byte[fileStream.Length - offset];
                                fileStream.Seek(offset, SeekOrigin.Begin);
                                while (fileStream.Position != fileStream.Length)
                                {
                                    bytes[i] = (byte)fileStream.ReadByte();
                                    i++;
                                }
                                fileStream.Seek(symbols, SeekOrigin.Begin);
                                // Запрос ввода.
                                //fileStream.Write(Encoding.UTF8.GetBytes("Hello\n"), 0, "Hello\n".Length);
                                fileStream.Write(bytes, 0, offset);
                                break;

                            }
                        }                 
                    case "back":
                        {
                            matchData.Line = lineData.LineBackup; 
                            matchData.LineBackup = null;
                            using (FileStream fileStream = new FileStream(lineData.FileName, FileMode.Open))
                            {
                                byte[] bytes = new byte[fileStream.Length - offset];
                                fileStream.Seek(offset, SeekOrigin.Begin);
                                while (fileStream.Position != fileStream.Length)
                                {
                                    bytes[i] = (byte)fileStream.ReadByte();
                                    i++;
                                }
                                fileStream.Seek(symbols, SeekOrigin.Begin);
                                fileStream.Write(Encoding.UTF8.GetBytes(lineData.LineBackup), 0, lineData.LineBackup.Length);
                                fileStream.Write(bytes, 0, (int)fileStream.Length - offset);
                                break;
                            }
                        }
                    case "del":
                        {
                            matchData.LineBackup = lineData.Line;
                            matchData.Line = null;
                            using (FileStream fileStream = new FileStream(lineData.FileName, FileMode.Open))
                            {
                                byte[] bytes = new byte[fileStream.Length - offset];
                                fileStream.Seek(offset, SeekOrigin.Begin);
                                while (fileStream.Position != fileStream.Length)
                                {
                                    bytes[i] = (byte)fileStream.ReadByte();
                                    i++;
                                }
                                fileStream.Seek(symbols, SeekOrigin.Begin);
                                fileStream.Write(bytes, 0, (int)fileStream.Length - offset);
                                break;
                            }
                        }          
                }
                return matchData;
            }
            catch
            {
                throw;
            }
        }
    }
}
