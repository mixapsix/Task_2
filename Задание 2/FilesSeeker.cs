using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace Task_2
{
    class FilesSeeker
    {
        private string directory;
        private string fileMask;
        private string result;
        private List<string> allFiles= new List<string>();

        public string Directory { get => directory; set => directory = value; }
        public string FileMask { get => fileMask; set => fileMask = value; }
        public string Result { get => result; set => result = value; }

        public void FilesInDirectory()
        {
            try
            {
                string[] allFiles = System.IO.Directory.GetFiles(Directory);
                for(int  i = 0; i < allFiles.Length ;i++)
                {
                   this.allFiles.Add(allFiles[i]);
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
                    if (file.Contains(FileMask))
                    {
                        Result += file + "\n";
                    }
                }

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Files not found");
            }
        }
    }
}
