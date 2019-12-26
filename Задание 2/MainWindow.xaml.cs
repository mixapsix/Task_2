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

        private Dictionary<string, int> regime = new Dictionary<string, int>()
        {
            { "Поиск по маске файла" , 0},
            { "Поиск по маске файла и строки", 1},
            { "Поиск по всем файлам" , 2 }
        };

        public MainWindow()
        {
            InitializeComponent();
            regimeBox.ItemsSource = regime.Keys.ToList();
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
                    int selectedRegime;
                    regime.TryGetValue((string)regimeBox.SelectionBoxItem, out selectedRegime);
                    matchData = FilesSeeker.SearchWithRegime(selectedRegime, path, fileNameBox.Text, stringMaskBox.Text);
                    if(matchData != null)
                    {
                        resultGrid.ItemsSource = matchData;
                        DownloadButton.IsEnabled = true;
                    }
                    else
                    {
                        DownloadButton.IsEnabled = false;
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

        }
    }
}
