using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Forms = System.Windows.Forms;


namespace Task_2
{
    public partial class MainWindow : Window
    {

        private string path;
        List<MatchData> matchDatas = new List<MatchData>();
        DataConverter dataConverter = new DataConverter();
        LineChanger lineChanger = new LineChanger();
        List<string> linesMaskList = new List<string>();
        List<string> filesNameMaskList = new List<string>();

        private readonly List<string> regimes = new List<string>()
        {
            { "Поиск по маске файла"},
            { "Поиск по маске файла и строки"},
            { "Поиск по всем файлам"},
            { "Поиск по выбранным файлам"}
        };

        private readonly List<string> downloadRegimes = new List<string>()
        {
            { "Выгрузить в XML" },
            { "Выгрузить в JSON" },
        };

        private readonly List<string> uploadRegimes = new List<string>()
        {

            { "Загрузить из XML" },
            { "Загрузить из JSON" },
        };

        public MainWindow()
        {
            InitializeComponent();
            regimeBox.ItemsSource = regimes;
            downloadTypeBox.ItemsSource = downloadRegimes;
            uploadTypeBox.ItemsSource = uploadRegimes;
        }

        private void Click(object sender, RoutedEventArgs e)
        {
            try
            {
                resultGrid.ItemsSource = null;
                matchDatas.Clear();

                if (filesNameMaskList.Count != 0 && regimeBox.SelectedItem?.ToString() == "Поиск по выбранным файлам")
                {
                    foreach (string mask in filesNameMaskList)
                    {
                        if (linesMaskList.Count != 0)
                        {
                            foreach (string stringMask in linesMaskList)
                            {
                                matchDatas.AddRange(FilesSeeker.SearchWithRegime(regimeBox.SelectedItem?.ToString(), path, mask, stringMask));
                            }
                            matchDatas.AddRange(FilesSeeker.SearchWithRegime(regimeBox.SelectedItem?.ToString(), path, fileNameBox.Text, lineBox.Text));
                        }
                        else
                        {
                            matchDatas.AddRange(FilesSeeker.SearchWithRegime(regimeBox.SelectedItem?.ToString(), path, mask, lineBox.Text));
                        }
                    }
                }
                else if (filesNameMaskList.Count == 0 && regimeBox.SelectedItem?.ToString() == "Поиск по выбранным файлам")
                {
                    MessageBox.Show("Выберите файлы для поиска");
                }
                else
                {
                    if (linesMaskList.Count != 0)
                    {
                        foreach (string stringMask in linesMaskList)
                        {
                            matchDatas.AddRange(FilesSeeker.SearchWithRegime(regimeBox.SelectedItem?.ToString(), path, fileNameBox.Text, stringMask));
                        }
                        matchDatas.AddRange(FilesSeeker.SearchWithRegime(regimeBox.SelectedItem?.ToString(), path, fileNameBox.Text, lineBox.Text));
                    }
                    else
                    {
                        matchDatas.AddRange(FilesSeeker.SearchWithRegime(regimeBox.SelectedItem?.ToString(), path, fileNameBox.Text, lineBox.Text));
                    }

                }

                if (matchDatas != null && matchDatas.Count > 0)
                {
                    resultGrid.ItemsSource = matchDatas;
                    downloadButton.IsEnabled = true;
                    contextMenu.IsEnabled = true;
                }
                else
                {
                    downloadButton.IsEnabled = false;
                    contextMenu.IsEnabled = false;
                }
            }
            catch (Exception ex)
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
                                dataConverter.DownloadInXML(matchDatas);
                                break;
                            }
                        case "Выгрузить в JSON":
                            {
                                dataConverter.DownloadInJSON(matchDatas);
                                break;
                            }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите режим выгрузки");
                }
            }
            catch (Exception ex)
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
                            matchDatas = dataConverter.UploadFromXML();
                            resultGrid.ItemsSource = matchDatas;
                            if (resultGrid.ItemsSource != null)
                            {
                                downloadButton.IsEnabled = true;
                                contextMenu.IsEnabled = true;
                            }
                            else
                            {
                                downloadButton.IsEnabled = false;
                                contextMenu.IsEnabled = false;
                            }
                            break;
                        }
                    case "Загрузить из JSON":
                        {
                            matchDatas = dataConverter.UploadFromJSON();
                            resultGrid.ItemsSource = matchDatas;
                            if (resultGrid.ItemsSource != null)
                            {
                                downloadButton.IsEnabled = true;
                                contextMenu.IsEnabled = true;
                            }
                            else
                            {
                                downloadButton.IsEnabled = false;
                                contextMenu.IsEnabled = false;
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
                linesMaskList.Add(lineBox.Text.ToString());
                lineMaskBox.ItemsSource = null;
                lineMaskBox.ItemsSource = linesMaskList;
            }
        }

        private void lineMaskBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            linesMaskList.Clear();
            if (lineMaskBox.SelectedItems.Count > 0)
            {
                foreach (var mask in lineMaskBox.SelectedItems)
                {
                    linesMaskList.Add(mask.ToString());
                }
            }
            else
            {
                foreach (var mask in lineMaskBox.Items)
                {
                    linesMaskList.Add(mask.ToString());
                }
            }
        }

        private void fileNameMaskBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filesNameMaskList.Clear();
            if (fileNameMaskBox.SelectedItems.Count > 0)
            {
                foreach (var mask in fileNameMaskBox.SelectedItems)
                {
                    filesNameMaskList.Add(mask.ToString());
                }
            }
            else
            {
                foreach (var mask in fileNameMaskBox.Items)
                {
                    linesMaskList.Add(mask.ToString());
                }
            }
        }

        private void fileNameMaskBox_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            fileNameMaskBox.ItemsSource = null;
            filesNameMaskList.Clear();
        }

        private void fileNameMaskBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Forms.OpenFileDialog fileDialog = new Forms.OpenFileDialog();
            fileDialog.ShowDialog();
            filesNameMaskList.Add(fileDialog.FileName);
            fileNameMaskBox.ItemsSource = null;
            fileNameMaskBox.ItemsSource = filesNameMaskList;
        }

        private void lineMaskBox_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            linesMaskList.Clear();
            lineMaskBox.ItemsSource = null;
        }

        private void MenuItemDelete(object sender, RoutedEventArgs e)
        {
            try
            {
                resultGrid.SelectedItem = lineChanger.ChangeLine((MatchData)resultGrid.SelectedItem, "Удалить строку");
                MessageBox.Show("Строка удалена");
                resultGrid.ItemsSource = null;
                resultGrid.ItemsSource = matchDatas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.TargetSite.Name + "\n");
            }
        }

        private void MenuItemChange(object sender, RoutedEventArgs e)
        { 
            try
            {
                resultGrid.SelectedItem = lineChanger.ChangeLine((MatchData)resultGrid.SelectedItem, "Изменить строку");
                MessageBox.Show("Строка изменена");
                resultGrid.ItemsSource = null;
                resultGrid.ItemsSource = matchDatas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.TargetSite.Name + "\n");
            }
        }

        private void MenuItemBackup(object sender, RoutedEventArgs e)
        {
            try
            {
                MatchData lineToBackup = (MatchData)resultGrid.SelectedItem;
                if (lineToBackup.LineBackup != null)
                {
                    resultGrid.SelectedItem = lineChanger.ChangeLine((MatchData)resultGrid.SelectedItem, "Восстановить строку");
                    MessageBox.Show("Строка восстановлена");
                    resultGrid.ItemsSource = null;
                    resultGrid.ItemsSource = matchDatas;
                }
                else
                {
                    MessageBox.Show("У данной строки нет резевной копии");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.TargetSite.Name + "\n");
            }
        }
    }
}
