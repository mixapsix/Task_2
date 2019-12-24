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
using System.Windows.Forms;
using System.ComponentModel;

namespace Task_2
{
    public partial class MainWindow : Window
    {
        private string path;
        public MainWindow()
        {
            InitializeComponent();
            regimeBox.ItemsSource = RegimeTranformer.regime.Values.ToList();
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            if(path == null || path == "")
            {
                System.Windows.MessageBox.Show("Выберите директорию");
            }
            else
            {              
                resultGrid.ItemsSource = FilesSeeker.SearchWithRegime(RegimeTranformer.TransformToInt((string)regimeBox.SelectionBoxItem), path, fileNameBox.Text, stringMaskBox.Text);
            }
        }

        private void OpenExplorer(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            path = folderBrowserDialog.SelectedPath;
            directoryLabel.Content = "Директория для поиска: " + folderBrowserDialog.SelectedPath;
        }
    }
}
