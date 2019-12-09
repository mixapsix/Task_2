using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace Задание_2
{
    class FilesSeeker
    {
        private string directory;
        private string fileMask;
        private string result;

        public string Directory { get => directory; set => directory = value; }
        public string FileMask { get => fileMask; set => fileMask = value; }
        public string Result { get => result; set => result = value; }

        public void FilesInDirectory()
        {
            string[] allFiles = null;
            string filesDirectory = @"C:\" + Directory;
            string matchFiles = "";
            try
            {
                allFiles = System.IO.Directory.GetFiles(filesDirectory);
                for(int  i = 0; i < allFiles.Length ;i++)
                {
                    allFiles[i] = allFiles[i].Remove(0, filesDirectory.Length);
                }
            }
            catch(DirectoryNotFoundException)
            {
                MessageBox.Show("Директория не найдена!");
            }

            try
            {
                foreach (string file in allFiles)
                {
                    if (file.Contains(fileMask))
                    {
                        matchFiles += file + "\n";
                    }
                }
                result = matchFiles;

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Files not found");
            }
        }
    }
}
