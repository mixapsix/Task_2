using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
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
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter streamWriter = new StreamWriter(folderBrowserDialog.SelectedPath + "Result.json"))
            {
                using (JsonWriter writer = new JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(writer, data);
                }
            }
        }

        public List<MatchData> UploadFromJSON()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();

            if (Regex.IsMatch(fileDialog.FileName, "\\w*.json"))
            {
                using (StreamReader streamReader = new StreamReader(fileDialog.FileName))
                {
                    return JsonConvert.DeserializeObject<List<MatchData>>(streamReader.ReadToEnd());
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
