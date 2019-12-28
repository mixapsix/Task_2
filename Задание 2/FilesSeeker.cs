using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Task_2
{
    /// <summary>
    /// Класс для поиска определенных строк в файлах расположенных по выбранной директории, с поиском в поддпапках
    /// </summary>
    /// 
    public static class FilesSeeker
    {
        /// <summary>
        /// Метод осуществяющий поиск по указнному режиму в параметре regime. Возможен поиск маски строки по всем файлам, по совпадающим маске файла и просто по маске файла.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="fileNameMask"></param>
        /// <param name="lineMask"></param>
        /// <param name="regime"></param>
        /// <returns></returns>
        /// 
        public static List<MatchData> SearchWithRegime(string regime, string directory, string fileNameMask = "", string lineMask = "")
        {
            List<MatchData> matchDatas = new List<MatchData>();

            List<string> allFilePaths = new List<string>();

            List<Task> taskList = new List<Task>();

            GetAllFiles(directory, ref allFilePaths);

            switch (regime)
            {
                case "Поиск по маске файла":
                    {
                        if (fileNameMask == "")
                        {
                           MessageBox.Show("Введите имя файла");
                        }
                        else
                        {
                            foreach (string allFilePath in allFilePaths)
                            {
                                GetMatchedFiles(allFilePath, fileNameMask, ref matchDatas);
                            }
                        }
                        break;
                    }
                case "Поиск по маске файла и строки":
                    {
                        if(fileNameMask == "" && lineMask == "")
                        {
                            MessageBox.Show("Введите имя файла и маску строки");
                        }
                        else if (fileNameMask == "")
                        {
                            MessageBox.Show("Введите имя файла");
                        }
                        else if (lineMask == "")
                        {
                            MessageBox.Show("Введите маску строки");
                        }
                        else
                        {
                            List<MatchData> temp = new List<MatchData>();
                            foreach (string allFilePath in allFilePaths)
                            {
                                GetMatchedFiles(allFilePath, fileNameMask, ref temp);
                            }

                            Parallel.ForEach(temp, fileData => GetMatchedData(fileData.FileName, lineMask, ref matchDatas));

                            taskList.Clear();
                        }
                        break;
                    }
                case "Поиск по всем файлам":
                    {
                        if (lineMask == "")
                        {
                            MessageBox.Show("Введите маску строки");
                        }
                        else
                        {
                            Parallel.ForEach(allFilePaths, filePath => GetMatchedData(filePath, lineMask, ref matchDatas));

                            taskList.Clear();
                        }
                        break;
                    }
            }
            return matchDatas;
        }

        private static void GetAllFiles(string path, ref List<string> allFiles)
        {
            try
            {
                List<string> directories = System.IO.Directory.GetDirectories(path).ToList<string>();

                if (directories != null)
                {
                    foreach (string directory in directories)
                    {
                        GetAllFiles(directory, ref allFiles);
                    }
                }
                allFiles.AddRange(System.IO.Directory.GetFiles(path).ToList<string>());
            }
            catch(UnauthorizedAccessException)
            {
                throw;
            }
        }
        private static void GetMatchedFiles(string path, string fileNameMask, ref List<MatchData> matchDatas)
        {
            List<MatchData> matchedFiles = new List<MatchData>();
            Regex fileMask = new Regex($"\\w*{fileNameMask}\\w*");
            if(fileMask.IsMatch(path.Remove(0, path.LastIndexOf('\\'))))
            {
                matchDatas.Add(new MatchData() { FileName = path });
            }
        }

        private static void GetMatchedData(string path, string lineMask, ref List<MatchData> matchDatas)
        {
            try
            {
                Regex mask = new Regex($"\\w*{lineMask}\\w*");
                int line = 1;
                string matchString = null;
                using (StreamReader streamReader = new StreamReader(path))
                {
                    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

                    while (!streamReader.EndOfStream)
                    {
                        if (mask.IsMatch(matchString = streamReader.ReadLine().ToString()))
                        {
                            matchString = Regex.Replace(matchString , @"[^\w\.@-\\%]", string.Empty);
                            matchDatas.Add(new MatchData { FileName = path, LineNumber = line, Line = matchString });
                        }
                        line++;
                    }
                }
            }
            catch (IOException)
            {
                throw;
            }
        }
    }
}
