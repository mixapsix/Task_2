using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using Forms = System.Windows.Forms;
using System.ComponentModel;

namespace Task_2
{
    public partial class MainWindow : Window
    {

        private string path;
        List<MatchData> matchData;
        DataConverter dataConverter = new DataConverter();

        private readonly List<string> regime = new List<string>()
        {
            { "Поиск по маске файла"},
            { "Поиск по маске файла и строки"},
            { "Поиск по всем файлам"}
        };

        private readonly List<string> downloadRegime = new List<string>()
        {
            { "Выгрузить в XML" },
            { "Выгрузить в JSON" },
        };

        private readonly List<string> uploadRegime = new List<string>()
        {

            { "Загрузить из XML" },
            { "Загрузить из JSON" },
        };


        public MainWindow()
        {
            InitializeComponent();
            regimeBox.ItemsSource = regime;
            downloadTypeBox.ItemsSource = downloadRegime;
            uploadTypeBox.ItemsSource = uploadRegime;
        }
       
        private void Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (path == null || path == "")
                {
                   MessageBox.Show("Выберите директорию");
                }
                else
                {
                    matchData = FilesSeeker.SearchWithRegime(regimeBox.SelectedItem?.ToString(), path, fileNameBox.Text, stringMaskBox.Text);
                    if(matchData != null)
                    {
                        resultGrid.ItemsSource = matchData;
                        downloadButton.IsEnabled = true;
                    }
                    else
                    {
                        downloadButton.IsEnabled = false;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OpenExplorer(object sender, RoutedEventArgs e)
        {
            Forms.FolderBrowserDialog folderBrowserDialog = new Forms.FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            path = folderBrowserDialog.SelectedPath;
            directoryLabel.Content = "Директория для поиска: " + folderBrowserDialog.SelectedPath;
        }

        private void Download(object sender, RoutedEventArgs e)
        {
            if (downloadTypeBox.SelectedItem != null)
            {
                switch (downloadTypeBox.SelectedItem)
                {
                    case "Выгрузить в XML":
                        {
                            dataConverter.DownloadInXML(matchData);
                            break;
                        }
                    case "Выгрузить в JSON":
                        {
                            dataConverter.DownloadInJSON(matchData);
                            break;
                        }
                }
            }
            else
            {
                MessageBox.Show("Выберите режим выгрузки");
            }
        }

        private void Upload(object sender, RoutedEventArgs e)
        {
            switch(uploadTypeBox.SelectedItem)
            {
                case "Загрузить из XML":
                    {
                        resultGrid.ItemsSource = dataConverter.UploadFromXML();
                        break;
                    }
                case "Загрузить из JSON":
                    {
                        resultGrid.ItemsSource = dataConverter.UploadFromJSON();
                        break;
                    }
            }
        }
    }
}
