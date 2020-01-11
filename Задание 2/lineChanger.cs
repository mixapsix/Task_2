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
                int offset;
                if (lineData.Line != null)
                {
                    offset = symbols + lineData.Line.Length + 1;
                }
                else
                {
                    offset = symbols + lineData.LineBackup.Length + 1;
                }
                int i = 0;
                MatchData matchData = lineData;
                switch (regime)
                {
                    case "Изменить строку":
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
                    case "Восстановить строку":
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
                                fileStream.Write(Encoding.UTF8.GetBytes(lineData.LineBackup), 0, lineData.LineBackup.Length);
                                fileStream.Write(bytes, 0, (int)fileStream.Length - offset);

                                matchData.Line = lineData.LineBackup;
                                matchData.LineBackup = null;
                                break;
                            }
                        }
                    case "Удалить строку":
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
                                fileStream.Write(bytes, 0, (int)fileStream.Length - offset);

                                matchData.LineBackup = lineData.Line;
                                matchData.Line = null;
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
