using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Text.RegularExpressions;

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
        public static List<MatchData> SearchWithRegime(string directory, string fileNameMask, string lineMask, regime regime)
        {
            List<MatchData> matchDatas = new List<MatchData>();

            List<string> allFilePaths = new List<string>();

            List<Task> taskList = new List<Task>();

            GetAllFiles(directory, ref allFilePaths);

            switch (regime)
            {
                case regime.AllFileSearch:
                    {
                        foreach (string allFilePath in allFilePaths)
                        {
                            taskList.Add(new Task(() => 
                            {
                                GetMatchedData(allFilePath, lineMask, ref matchDatas);
                            }));
                        }

                        foreach(Task task in taskList)
                        {
                            task.Start();
                        }

                        Task.WaitAll(taskList.ToArray());
                        taskList.Clear();

                        break;
                    }
                case regime.FileMaskSearch:
                    {
                        foreach (string allFilePath in allFilePaths)
                        {
                            taskList.Add(new Task(() =>
                            {
                                GetMatchedFiles(allFilePath, fileNameMask, ref matchDatas);
                            }));

                        }

                        foreach (Task task in taskList)
                        {
                            task.Start();
                        }

                        Task.WaitAll(taskList.ToArray());
                        taskList.Clear();

                        break;
                    }
                case regime.MatchFileSearch:
                    {
                        List<MatchData> temp = new List<MatchData>();
                        foreach (string allFilePath in allFilePaths)
                        {
                            taskList.Add(new Task(() =>
                            {
                                GetMatchedFiles(allFilePath, fileNameMask, ref temp);
                            }));
                        }

                        foreach (Task task in taskList)
                        {
                            task.Start();
                        }

                        Task.WaitAll(taskList.ToArray());
                        taskList.Clear();

                        foreach (MatchData data in temp)
                        {
                            taskList.Add(new Task(() =>
                            {
                                GetMatchedData(data.FileName, lineMask, ref matchDatas);
                            }));
                        }

                        foreach (Task task in taskList)
                        {
                            task.Start();
                        }

                        Task.WaitAll(taskList.ToArray());
                        taskList.Clear();

                        break;
                    }
            }
            return matchDatas;
        }

        private static void GetAllFiles(string path, ref List<string> allFiles)
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
            Regex mask = new Regex($"\\w*{lineMask}\\w*");
            int line = 1; //Number of the line in the document
            string matchString = null;
            StreamReader streamReader = new StreamReader(path);
            while (!streamReader.EndOfStream)
            {
                if (mask.IsMatch(matchString = streamReader.ReadLine().ToString()))
                {
                    matchDatas.Add(new MatchData { FileName = path, LineNumber = line, Line = matchString });                           
                }
                line++;
            }
        }
    }
}
