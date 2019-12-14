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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FilesSeeker fileSeeker = new FilesSeeker();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DirectoryInput(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = (System.Windows.Controls.TextBox)sender;
            fileSeeker.Directory = textBox.Text;
        }

        private void FileMask(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = (System.Windows.Controls.TextBox)sender;
            fileSeeker.FileMask = textBox.Text;
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            fileSeeker.FilesInDirectory();
            ResultBox.Text = fileSeeker.Result;
        }

        private void OpenExplorer(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            fileSeeker.Directory = folderBrowserDialog.SelectedPath;
            directoryLabel.Content = "Директория для поиска: " + fileSeeker.Directory;
        }
    }
}
