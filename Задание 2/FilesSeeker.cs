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
        public string Directory { get; set; }
        public string FileMask { get; set; }
        public string Result { get ; set ; }
        public List<string> AllFiles {  get ; set ; }

        public void FilesInDirectory()
        {
            Result = null;
            try
            {
                AllFiles = System.IO.Directory.GetFiles(Directory).ToList<string>(); ;
            }
            catch(DirectoryNotFoundException)
            {
                MessageBox.Show("Директория не найдена!");
            }

            try
            {
                string temp;
                foreach (string file in AllFiles)
                {
                    temp = file.Remove(0, Directory.Length);
                    if (temp.Contains(FileMask))
                    {
                        Result += temp + "\n";
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
