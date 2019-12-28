using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Task_2
{
    class DataConverter
    {
        public void DownloadInXML(List<MatchData> data)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<MatchData>));
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            using (FileStream fileStream = new FileStream(folderBrowserDialog.SelectedPath + "Result.xml", FileMode.OpenOrCreate)) 
            {
                    formatter.Serialize(fileStream, data);
            }
        }

        public List<MatchData> UploadFromXML()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<MatchData>));
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            if (Regex.IsMatch(fileDialog.FileName, "\\w*.xml"))
            {
                using (FileStream fileStream = new FileStream(fileDialog.FileName, FileMode.Open))
                {

                    return (List<MatchData>)formatter.Deserialize(fileStream);
                }
            }
            else
            {
                MessageBox.Show("Файл имеет не верный формат");
                return null;
            }
        }


        public void DownloadInJSON(List<MatchData> data)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            using (FileStream fileStream = new FileStream(folderBrowserDialog.SelectedPath + "Result.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.SerializeAsync<List<MatchData>>(fileStream, data);
            }
        }

        public List<MatchData> UploadFromJSON()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            if (Regex.IsMatch(fileDialog.FileName, "\\w*.json"))
            {
                using (StreamReader streamReader = new StreamReader(fileDialog.FileName))
                {
                    List<MatchData> matchDatas = new List<MatchData>();
                    while (!streamReader.EndOfStream)
                    {
                        matchDatas.AddRange(JsonSerializer.Deserialize<List<MatchData>>(streamReader.ReadLine(), options));
                    }
                    return matchDatas;
                }
            }
            else
            {
                MessageBox.Show("Файл имеет не верный формат");
                return null;
            }
        }
    }
}
