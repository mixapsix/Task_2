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
    public static class FilesSeeker
    {
        delegate List<string> fileTask(string x,string y);
        delegate List<MatchData> stringTask(string x, string y);

        /// <summary>
        /// Метод осуществляет поиск по директории path и поддиректориям, файлов содержащих маску fileNameMask. В данных файлах происходит поиск номеров строк по маске lineMask.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="fileNameMask"></param>
        /// <param name="lineMask"></param>
        /// <returns></returns>
        public static List<MatchData> GetAllFiles(string directory, string fileNameMask, string lineMask)
        {
            List<MatchData> matchDatas = new List<MatchData>();
            List<string> allFilePaths = new List<string>();
            List<string> nameMachedFiles = new List<string>();
            Queue<Task> foundFileQueue = new Queue<Task>();
            Queue<Task> foundStringQueue = new Queue<Task>();

            OpenDirectory(directory, ref allFilePaths);

            foreach (string allFilePath in allFilePaths)
            {
                
                nameMachedFiles.AddRange(GetMatchedFiles(allFilePath, fileNameMask));                   
            }
            foreach(string nameMatchedFile in nameMachedFiles)
            {
                matchDatas.AddRange(GetMatchedData(nameMatchedFile, lineMask));
            }
            return matchDatas;
        }

        private static void OpenDirectory(string path, ref List<string> allFiles)
        {
            List<string> directories = System.IO.Directory.GetDirectories(path).ToList<string>();
            if(directories!=null)
            {
                foreach (string directory in directories)
                {
                    OpenDirectory(directory, ref allFiles);
                }
            }
            allFiles.AddRange(System.IO.Directory.GetFiles(path).ToList<string>());
        }

        private static List<string> GetMatchedFiles(string path, string fileNameMask)
        {
            List<string> matchedFiles = new List<string>();
            Regex fileMask = new Regex($"\\w*{fileNameMask}\\w*");
            if(fileMask.IsMatch(path.Remove(0, path.LastIndexOf('\\'))))
            {
                matchedFiles.Add(path);
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
