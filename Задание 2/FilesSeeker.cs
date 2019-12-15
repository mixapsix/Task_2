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
                foreach (string file in AllFiles)
                {
                    string matchString; //String that math the mask
                    if (file.Remove(0, Directory.Length).Contains(FileMask))
                    {
                        int line = 1; //Number of the line in the document
                        bool fileName = false; //Print or not file name
                        StreamReader streamReader = new StreamReader(file);
                        while (!streamReader.EndOfStream)
                        {

                            if ((matchString = streamReader.ReadLine().ToString()).Contains(FileMask))
                            {
                                if (fileName != true)
                                {
                                    Result += file.Remove(0, Directory.Length + 1) + "\n"; // Print file name
                                    fileName = true;                                  
                                }
                                Result += "["+ line + "]" + matchString + "\n"; //Number of the line
                            }
                            line++;
                        }
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
