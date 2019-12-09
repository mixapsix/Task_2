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

namespace Задание_2
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
            TextBox textBox = (TextBox)sender;
            fileSeeker.Directory = textBox.Text;
        }

        private void FileMask(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            fileSeeker.FileMask = textBox.Text;
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = new TextBlock();
            fileSeeker.FilesInDirectory();
            MessageBox.Show(fileSeeker.Result);
        }
    }
}
