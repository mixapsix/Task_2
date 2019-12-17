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
        public static string FileMask { get; set; }
        public static string FileName { get; set; }

        public static List<MatchData> GetAllFiles(string path)
        {
            List<MatchData> matchDatas = new List<MatchData>();
            List<string> allFiles = new List<string>();
            try
            {
                OpenDirectory(path, ref allFiles);
                foreach (string filePath in allFiles)
                {
                    matchDatas.Add(GetFileData(filePath, FileName, FileMask));
                }
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Директория не найдена!");
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
            List<string> files = System.IO.Directory.GetFiles(path).ToList<string>();
            foreach(string file in files)
            {
                allFiles.Add(file);
            }
        }

        public static List<MatchData> GetFileData(string path, string fileName, string fileMask)
        {
        //    try
        //    {
        //        List<MatchData> matchDatas = new List<MatchData>();
        //        if (path.Remove(0, path.Length).Contains(fileName))
        //        {
        //            int line = 1; //Number of the line in the document
        //            bool name = false; //Print or not file name
        //            string matchString = null; 
        //            StreamReader streamReader = new StreamReader(path);
        //            while (!streamReader.EndOfStream)
        //            {
        //                if ((matchString = streamReader.ReadLine().ToString()).Contains(FileMask))
        //                {
        //                    if (name != true)
        //                    {
        //                        matchData.FileName += path.Remove(0, path.Length + 1) + "\n"; // Print file name
        //                        name = true;
        //                    }
        //                    matchData += "[" + line + "]" + matchString + "\n"; //Number of the line
        //                }
        //                line++;
        //            }
        //        }

        //        return Result;
        //    }
        //    catch (NullReferenceException)
        //    {
        //        MessageBox.Show("Files not found");
        //    }
        }
    }

    public class MatchData
    {
        public string FileName { get; set; }
        public int LineNumber { get; set; }
        public string Line { get; set; }
    }
}
