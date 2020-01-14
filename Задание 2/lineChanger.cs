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
                int i = 0;
                MatchData matchData = lineData;
                switch (regime)
                {
                    case "Изменить строку":
                        {
                            using (FileStream fileStream = new FileStream(lineData.FileName, FileMode.Open))
                            {
                                offset = symbols + lineData.Line.Length + 1; 
                                byte[] bytes = new byte[fileStream.Length - offset];
                                fileStream.Seek(offset, SeekOrigin.Begin);
                                while (fileStream.Position != fileStream.Length)
                                {
                                    bytes[i] = (byte)fileStream.ReadByte();
                                    i++;
                                }
                                fileStream.Seek(symbols, SeekOrigin.Begin);

                                StringInputDialog stringInputDialog = new StringInputDialog();
                                stringInputDialog.ShowDialog();
                                string line = stringInputDialog.changeLineBox.Text.ToString();
                                
                                fileStream.Write(Encoding.Default.GetBytes(line + "\n"), 0, line.Length + 1);
                                fileStream.Write(bytes, 0, bytes.Length);

                                matchData.LineBackup = matchData.Line;
                                matchData.Line = line;
                                break;
                            }
                        }                 
                    case "Восстановить строку":
                        {                          
                            using (FileStream fileStream = new FileStream(lineData.FileName, FileMode.Open))
                            {
                                if (lineData.Line == null)
                                {
                                    byte[] bytes = new byte[fileStream.Length - symbols];
                                    fileStream.Seek(symbols, SeekOrigin.Begin);
                                    while (fileStream.Position != fileStream.Length)
                                    {
                                        bytes[i] = (byte)fileStream.ReadByte();
                                        i++;
                                    }
                                    fileStream.Seek(symbols, SeekOrigin.Begin);
                                    fileStream.Write(Encoding.Default.GetBytes(lineData.LineBackup + "\n"), 0, lineData.LineBackup.Length + 1);
                                    fileStream.Write(bytes, 0, bytes.Length);

                                    matchData.Line = lineData.LineBackup;
                                    matchData.LineBackup = null;
                                    break;
                                }
                                else
                                {
                                    offset = symbols + lineData.Line.Length + 1;
                                    byte[] bytes = new byte[fileStream.Length - offset];
                                    fileStream.Seek(offset, SeekOrigin.Begin);
                                    while (fileStream.Position != fileStream.Length)
                                    {
                                        bytes[i] = (byte)fileStream.ReadByte();
                                        i++;
                                    }
                                    fileStream.Seek(symbols, SeekOrigin.Begin);
                                    fileStream.Write(Encoding.Default.GetBytes(lineData.LineBackup + "\n"), 0, lineData.LineBackup.Length + 1);
                                    fileStream.Write(bytes, 0, bytes.Length);

                                    string temp = matchData.Line;
                                    matchData.Line = matchData.LineBackup;
                                    matchData.LineBackup = temp;
                                    break;
                                }
                            } 
                        }
                    case "Удалить строку":
                        {
                            offset = symbols + lineData.Line.Length + 1;
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
                                fileStream.Write(bytes, 0, bytes.Length);

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
