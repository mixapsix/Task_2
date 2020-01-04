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
        List<MatchData> matchData = new List<MatchData>();
        DataConverter dataConverter = new DataConverter();
        List<string> lineMaskList = new List<string>();
        List<string> fileNameMaskList = new List<string>();

        private readonly List<string> regime = new List<string>()
        {
            { "Поиск по маске файла"},
            { "Поиск по маске файла и строки"},
            { "Поиск по всем файлам"},
            { "Поиск по выбранным файлам"}
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
                resultGrid.ItemsSource = null;
                matchData.Clear();

                if(fileNameMaskList.Count != 0 && regimeBox.SelectedItem?.ToString() == "Поиск по выбранным файлам")
                {
                    foreach (string mask in fileNameMaskList)
                    {
                        if (lineMaskList.Count == 0)
                        {
                            matchData.AddRange(FilesSeeker.SearchWithRegime(regimeBox.SelectedItem?.ToString(), path, mask, lineBox.Text));
                        }
                        else 
                        {
                            foreach (string stringMask in lineMaskList)
                            {
                                matchData.AddRange(FilesSeeker.SearchWithRegime(regimeBox.SelectedItem?.ToString(), path, mask, stringMask));
                            }
                        }                           
                    }
                }
                else if(regimeBox.SelectedItem?.ToString() == "Поиск по выбранным файлам")
                {
                    MessageBox.Show("Выберите файлы для поиска");
                }
                else 
                { 
                    if (lineMaskList.Count == 0 || regimeBox.SelectedItem?.ToString() == "Поиск по маске файла")
                    {
                        matchData.AddRange(FilesSeeker.SearchWithRegime(regimeBox.SelectedItem?.ToString(), path, fileNameBox.Text, lineBox.Text));
                    }
                    else
                    {
                        foreach (string stringMask in lineMaskList)
                        {
                            matchData.AddRange(FilesSeeker.SearchWithRegime(regimeBox.SelectedItem?.ToString(), path, fileNameBox.Text, stringMask));
                        }
                    }

                }
                
                if (matchData != null)
                {
                    resultGrid.ItemsSource = matchData;
                    downloadButton.IsEnabled = true;
                }
                else
                {
                    downloadButton.IsEnabled = false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.TargetSite.Name);
            }
        }

        private void OpenExplorer(object sender, RoutedEventArgs e)
        {
            Forms.FolderBrowserDialog folderBrowserDialog = new Forms.FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            if (folderBrowserDialog.SelectedPath != "")
            {
                path = folderBrowserDialog.SelectedPath;
                directoryLabel.Content = "Директория для поиска: " + folderBrowserDialog.SelectedPath;
            }
        }

        private void Download(object sender, RoutedEventArgs e)
        {
            try
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.TargetSite.Name);
            }
        }

        private void Upload(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (uploadTypeBox.SelectedItem)
                {
                    case "Загрузить из XML":
                        {
                            matchData = dataConverter.UploadFromXML();
                            resultGrid.ItemsSource = matchData;
                            if (resultGrid.ItemsSource != null)
                            {
                                downloadButton.IsEnabled = true;
                            }
                            break;
                        }
                    case "Загрузить из JSON":
                        {
                            matchData = dataConverter.UploadFromJSON();
                            resultGrid.ItemsSource = matchData;
                            if (resultGrid.ItemsSource != null)
                            {
                                downloadButton.IsEnabled = true;
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.TargetSite.Name);
            }
            
        }

        private void lineBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                lineMaskList.Add(lineBox.Text.ToString());
                lineMaskBox.ItemsSource = null;
                lineMaskBox.ItemsSource = lineMaskList;
            }          
        }

        private void lineMaskBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lineMaskList.Clear();
            if (lineMaskBox.SelectedItems.Count > 0)
            {
                foreach (var mask in lineMaskBox.SelectedItems)
                {
                    lineMaskList.Add(mask.ToString());
                }
            }
            else
            {
                foreach (var mask in lineMaskBox.Items)
                {
                    lineMaskList.Add(mask.ToString());
                }
            }
        }

        private void fileNameMaskBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fileNameMaskList.Clear();
            if (fileNameMaskBox.SelectedItems.Count > 0)
            {
                foreach (var mask in fileNameMaskBox.SelectedItems)
                {
                    fileNameMaskList.Add(mask.ToString());
                }
            }
            else
            {
                foreach (var mask in fileNameMaskBox.Items)
                {
                    lineMaskList.Add(mask.ToString());
                }
            }
        }

        private void fileNameMaskBox_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            fileNameMaskBox.ItemsSource = null;
            fileNameMaskList.Clear();
        }

        private void fileNameMaskBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Forms.OpenFileDialog fileDialog = new Forms.OpenFileDialog();
            fileDialog.ShowDialog();
            fileNameMaskList.Add(fileDialog.FileName);
            fileNameMaskBox.ItemsSource = null;
            fileNameMaskBox.ItemsSource = fileNameMaskList;
        }

        private void lineMaskBox_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            lineMaskList.Clear();
            lineMaskBox.ItemsSource = null;
        }

    }
}
