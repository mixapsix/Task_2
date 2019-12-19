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

namespace Task_2
{
    public partial class MainWindow : Window
    {
        private string path;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            if (fileNameBox.Text == "")
            {
                System.Windows.MessageBox.Show("Введите имя файла");
            }
            else if (stringMaskBox.Text == "")
            {
                System.Windows.MessageBox.Show("Введите маску строки");
            }
            else if(path == null || path == "")
            {
                System.Windows.MessageBox.Show("Выберите директорию");
            }
            else
            {
                resultGrid.ItemsSource = FilesSeeker.GetAllFiles(path, fileNameBox.Text, stringMaskBox.Text);
            }
        }

        private void OpenExplorer(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            path = folderBrowserDialog.SelectedPath;
            directoryLabel.Content = "Директория для поиска: " + folderBrowserDialog.SelectedPath;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
