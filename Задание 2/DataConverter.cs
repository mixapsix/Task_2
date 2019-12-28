using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json;
using System.IO;
using System.Windows.Forms;

namespace Task_2
{
    class DataConverter
    {
        public void ConvertToXML(List<MatchData> data)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<MatchData>));
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            using (FileStream fileStream = new FileStream(folderBrowserDialog.SelectedPath + "Result.xml", FileMode.OpenOrCreate)) 
            {
                    formatter.Serialize(fileStream, data);
            }
        }


        public void ConvertToJSON(List<MatchData> data)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            using (FileStream fileStream = new FileStream(folderBrowserDialog.SelectedPath + "Result.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.SerializeAsync<List<MatchData>>(fileStream, data, options);
            }
        }
    }
}
