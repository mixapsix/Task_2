using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace Task_2
{
    public static class FilesSeeker
    {
        public static List<MatchData> GetAllFiles(string path, string fileName, string fileMask)
        {
            List<MatchData> matchDatas = new List<MatchData>();
            List<string> allFiles = new List<string>();
            try
            {               
                OpenDirectory(path, ref allFiles);
                foreach (string filePath in allFiles)
                {
                    matchDatas.AddRange(GetFileData(filePath, fileName, fileMask));
                }
            }
            catch(ArgumentNullException)
            {
                MessageBox.Show("Выберите директорию");
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

        public static List<MatchData> GetFileData(string path, string fileName, string fileMask)
        {
            List<MatchData> matchDatas = new List<MatchData>();
            if (path.Remove(0, path.LastIndexOf('\\')).Contains(fileName))
            {
                int line = 1; //Number of the line in the document
                string matchString = null;
                StreamReader streamReader = new StreamReader(path);
                while (!streamReader.EndOfStream)
                {
                    if ((matchString = streamReader.ReadLine().ToString()).Contains(fileMask))
                    {
                        matchDatas.Add(new MatchData { FileName = path, LineNumber = line, Line = matchString });                           
                    }
                    line++;
                }
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
