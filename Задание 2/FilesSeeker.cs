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

            GetAllFiles(directory, ref allFilePaths);

            switch (regime)
            {
                case regime.AllFileSearch:
                    {
                        foreach (string allFilePath in allFilePaths)
                        {
                            matchDatas.AddRange(GetMatchedData(allFilePath, lineMask));
                        }

                        break;
                    }
                case regime.FileMaskSearch:
                    {
                        foreach (string allFilePath in allFilePaths)
                        {
                            matchDatas.AddRange(GetMatchedFiles(allFilePath, fileNameMask));
                        }

                        break;
                    }
                case regime.MatchFileSearch:
                    {
                        List<MatchData> temp = new List<MatchData>();
                        foreach (string allFilePath in allFilePaths)
                        {

                            temp.AddRange(GetMatchedFiles(allFilePath, fileNameMask));
                        }

                        foreach (MatchData data in temp)
                        {
                            matchDatas.AddRange(GetMatchedData(data.FileName, lineMask));
                        }

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
        private static List<MatchData> GetMatchedFiles(string path, string fileNameMask)
        {
            List<MatchData> matchedFiles = new List<MatchData>();
            Regex fileMask = new Regex($"\\w*{fileNameMask}\\w*");
            if(fileMask.IsMatch(path.Remove(0, path.LastIndexOf('\\'))))
            {
                matchedFiles.Add(new MatchData() { FileName = path });
            }
            return matchedFiles;

        }

        private static List<MatchData> GetMatchedData(string path, string lineMask)
        {
            List<MatchData> matchDatas = new List<MatchData>();
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
            return matchDatas;
        }
    }
}
