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
    public static class FilesSeeker
    {
        public static List<MatchData> GetAllFiles(string path, string fileName, string fileMask)
        {
            List<MatchData> matchDatas = new List<MatchData>();
            List<string> allFilePaths = new List<string>();
            List<string> nameMachedFiles = new List<string>();
            OpenDirectory(path, ref allFilePaths);
            foreach (string allFilePath in allFilePaths)
            {
                nameMachedFiles.AddRange(GetMatchedFiles(allFilePath, fileName));                   
            }
            foreach(string nameMatchedFile in nameMachedFiles)
            {
                matchDatas.AddRange(GetMatchedData(nameMatchedFile, fileMask));
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

        public static List<string> GetMatchedFiles(string path, string fileName)
        {
            List<string> matchedFiles = new List<string>();
            Regex fileMask = new Regex($"\\w*{fileName}\\w*");
            if(fileMask.IsMatch(path.Remove(0, path.LastIndexOf('\\'))))
            {
                matchedFiles.Add(path);
            }
            return matchedFiles;

        }
        public static List<MatchData> GetMatchedData(string path, string fileMask)
        {
            List<MatchData> matchDatas = new List<MatchData>();
            Regex mask = new Regex($"\\w*{fileMask}\\w*");
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

    public class MatchData
    {
        public string FileName { get; set; }
        public int LineNumber { get; set; }
        public string Line { get; set; }
    }
}
