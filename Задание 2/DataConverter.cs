using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
            using (FileStream fileStream = new FileStream(folderBrowserDialog.SelectedPath + "Result.xml", FileMode.Create)) 
            {
                    formatter.Serialize(fileStream, data);
            }
        }
    }
}
