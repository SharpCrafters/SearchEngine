using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;

namespace SearchEngine
{
    /// <summary>
    /// Логика взаимодействия для AdministratorMainWindow.xaml
    /// </summary>
    public partial class AdministratorMainWindow : Window
    {
        DataBaseService _DataBaseService = new DataBaseService();
        public AdministratorMainWindow()
        {

            InitializeComponent();

            ScannersTable.ItemsSource = _DataBaseService.GetPreparedForGridList();

            _DataBaseService.GetCreatorTableData(CreatorsTable);

            _DataBaseService.GetTechnologyTableData(TechologiesTable);

            DataContext = new AdministratorWindowViewModel();

            ScannersTable.AddHandler(System.Windows.Controls.DataGrid.KeyDownEvent,
            new KeyEventHandler(ScannersTableKeyDown),
            handledEventsToo: true);

            CreatorsTable.AddHandler(System.Windows.Controls.DataGrid.KeyDownEvent,
            new KeyEventHandler(CreatorsTableKeyDown),
            handledEventsToo: true);

            TechologiesTable.AddHandler(System.Windows.Controls.DataGrid.KeyDownEvent,
            new KeyEventHandler(TechologiesTableKeyDown),
            handledEventsToo: true);
        }

        private void RefreshScannerDataGridClick(object sender, RoutedEventArgs e)
        {
            ScannersTable.ItemsSource = _DataBaseService.GetPreparedForGridList();
        }

        private void RefreshCreatorDataClick(object sender, RoutedEventArgs e)
        {
            _DataBaseService.GetCreatorTableData(CreatorsTable);
        }

        private void RefreshTechnologyDataClick(object sender, RoutedEventArgs e)
        {
            _DataBaseService.GetTechnologyTableData(TechologiesTable);
        }

        private void ScannersTableKeyDown(object sender, KeyEventArgs e)
        {
            var SelectedScannerTableItem = ScannersTable.SelectedItem as ScannerDataGrid;

            var SelectedScanner = _DataBaseService.GetScannerById(SelectedScannerTableItem.id);

            Window Message = null;

            if (e.Key == Key.Delete)
            {
                // Создаем переменную для хранения результата
                bool? DialogResult = null;

                Message = new Window
                {
                    Title = "Подтверждение удаления",
                    SizeToContent = SizeToContent.WidthAndHeight,
                    ResizeMode = ResizeMode.NoResize,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Icon = Application.Current.MainWindow.Icon,
                    Background = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                    Content = new StackPanel
                    {
                        Margin = new Thickness(20),
                        Children =
        {
            new TextBlock
            {
                Text = $"Вы действительно хотите удалить сканер №{SelectedScanner.id}?",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
                Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
            },
            new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Children =
                {
                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Да",
                        Width = 100,
                        Margin = new Thickness(0, 0, 10, 0),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = true;
                            Message.Close();
                        })
                    },
                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Нет",
                        Width = 100,
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = false;
                            Message.Close();
                        })
                    }
                }
            }
        }
                    }
                };

                Message.ShowDialog();

                if (DialogResult == true)
                {
                    if (SelectedScanner != null)
                    {
                        int SelectedRowNumber = SelectedScanner.id;

                        var ScannerToDelete = _DataBaseService.GetScannerToDelete(SelectedRowNumber);

                        if (ScannerToDelete != null)
                        {
                            _DataBaseService.DeleteScanner(ScannerToDelete);

                            Message = new Window
                            {
                                Title = "Информация",
                                SizeToContent = SizeToContent.WidthAndHeight,
                                ResizeMode = ResizeMode.NoResize,
                                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                                Icon = Application.Current.MainWindow.Icon,
                                Background = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                                Content = new StackPanel
                                {
                                    Margin = new Thickness(20),
                                    Children =
                            {
                                new TextBlock
                                {
                                    Text = $"Информация о сканере была удалена.",
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    FontSize = 16,
                                    Margin = new Thickness(0, 0, 0, 20),
                                    Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
                                },
                                new System.Windows.Controls.Button
                                {
                                    Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                                    Content = "Замечательно!",
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                                    Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                                    Command = new RelayCommand(() => Message.Close())
                                }
                            }
                                }
                            };

                            Message.ShowDialog();

                        }

                        ScannersTable.ItemsSource = _DataBaseService.GetPreparedForGridList();
                    }

                }
            }

            if (e.Key == Key.Home)
            {
                // Создаем переменную для хранения результата
                bool? DialogResult = null;

                Message = new Window
                {
                    Title = "Подтверждение сохранения",
                    SizeToContent = SizeToContent.WidthAndHeight,
                    ResizeMode = ResizeMode.NoResize,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Icon = Application.Current.MainWindow.Icon,
                    Background = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                    Content = new StackPanel
                    {
                        Margin = new Thickness(20),
                        Children =
        {
            new TextBlock
            {
                Text = $"Вы действительно хотите сохранить информацию о сканере №{SelectedScannerTableItem.id}?",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
                Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
            },
            new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Children =
                {
                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Да",
                        Width = 100,
                        Margin = new Thickness(0, 0, 10, 0),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = true;
                            Message.Close();
                        })
                    },
                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Нет",
                        Width = 100,
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = false;
                            Message.Close();
                        })
                    }
                }
            }
        }
                    }
                };

                Message.ShowDialog();

                if (DialogResult == true)
                {
                    bool IsNameGood = true;
                    bool IsAccuracyGood = true;
                    bool IsSpeedGood = true;
                    bool IsPriceGood = true;
                    bool IsYearGood = true;
                    bool IsDescriptionGood = true;

                    if (string.IsNullOrWhiteSpace(SelectedScannerTableItem.name))
                    {
                        IsNameGood = false;
                    }

                    if (SelectedScannerTableItem.accuracy <= 0)
                    {
                        IsAccuracyGood = false;
                    }

                    if (SelectedScannerTableItem.speed <= 0)
                    {
                        IsSpeedGood = false;
                    }

                    if (SelectedScannerTableItem.price <= 0)
                    {
                        IsPriceGood = false;
                    }

                    if ((SelectedScannerTableItem.release < 1970) && (SelectedScannerTableItem.release > 2025))
                    {
                        IsYearGood = false;
                    }

                    if (string.IsNullOrWhiteSpace(SelectedScannerTableItem.description))
                    {
                        IsDescriptionGood = false;
                    }

                    if (IsNameGood && IsAccuracyGood && IsSpeedGood && IsPriceGood && IsYearGood && IsDescriptionGood)
                    {
                        _DataBaseService.SaveScannerByDataGrid(SelectedScannerTableItem);

                        Window SuccessfulMessage = null;

                        SuccessfulMessage = new Window
                        {
                            Title = "Успешное сохранение",
                            SizeToContent = SizeToContent.WidthAndHeight,
                            ResizeMode = ResizeMode.NoResize,
                            WindowStartupLocation = WindowStartupLocation.CenterScreen,
                            Icon = Application.Current.MainWindow.Icon,
                            Background = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                            Content = new StackPanel
                            {
                                Margin = new Thickness(20),
                                Children =
        {
            new TextBlock
            {
                Text = $"Данные были успешно сохранены!",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
                Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
            },

                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Замечательно!",
                        Margin = new Thickness(0, 0, 10, 0),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = true;
                            SuccessfulMessage.Close();
                                })
                            },


                        }
                            }
                        };

                        SuccessfulMessage.ShowDialog();
                    }
                    else
                    {
                        string NameError = "";
                        string AccuracyError = "";
                        string SpeedError = "";
                        string PriceError = "";
                        string ReleaseError = "";
                        string DescriptionError = "";

                        if (!IsNameGood) NameError = $"Имя: \"{SelectedScannerTableItem.name}\"\n";
                        if (!IsAccuracyGood) AccuracyError = $"Точность: {SelectedScannerTableItem.accuracy}\n";
                        if (!IsSpeedGood) SpeedError = $"Скорость: {SelectedScannerTableItem.speed}\n";
                        if (!IsPriceGood) PriceError = $"Стоимость: {SelectedScannerTableItem.price}\n";
                        if (!IsYearGood) ReleaseError = $"Год выпуска: {SelectedScannerTableItem.release}\n";
                        if (!IsDescriptionGood) DescriptionError = $"Описание: \"{SelectedScannerTableItem.description}\"\n";

                        Window ErrorMessage = null;

                        ErrorMessage = new Window
                        {
                            Title = "Ошибка сохранения",
                            SizeToContent = SizeToContent.WidthAndHeight,
                            ResizeMode = ResizeMode.NoResize,
                            WindowStartupLocation = WindowStartupLocation.CenterScreen,
                            Icon = Application.Current.MainWindow.Icon,
                            Background = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                            Content = new StackPanel
                            {
                                Margin = new Thickness(20),
                                Children =
        {
            new TextBlock
            {
                Text = $"Не удалось сохранить информацию о сканере, поскольку:\n" +
                       $"{NameError}{AccuracyError}{SpeedError}{PriceError}{ReleaseError}{DescriptionError}",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
                Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
            },

                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Очень жаль!",
                        Margin = new Thickness(0, 0, 10, 0),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = true;
                            ErrorMessage.Close();
                                })
                            },


                        }
                            }
                        };

                        ErrorMessage.ShowDialog();
                    }
                    ;
                }
            }
        }

        private void CreatorsTableKeyDown(object sender, KeyEventArgs e)
        {
            var SelectedCreator = CreatorsTable.SelectedItem as Creator;

            Window Message = null;

            if (e.Key == Key.Delete)
            {
                // Создаем переменную для хранения результата
                bool? DialogResult = null;

                Message = new Window
                {
                    Title = "Подтверждение удаления",
                    SizeToContent = SizeToContent.WidthAndHeight,
                    ResizeMode = ResizeMode.NoResize,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Icon = Application.Current.MainWindow.Icon,
                    Background = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                    Content = new StackPanel
                    {
                        Margin = new Thickness(20),
                        Children =
        {
            new TextBlock
            {
                Text = $"Вы действительно хотите удалить информацию о производителе №{SelectedCreator.id}? \n" +
                       $"При удалении информации о производителе будут удалены сканеры этого производителя.",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
                Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
            },
            new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Children =
                {
                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Да",
                        Width = 100,
                        Margin = new Thickness(0, 0, 10, 0),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = true;
                            Message.Close();
                        })
                    },
                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Нет",
                        Width = 100,
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = false;
                            Message.Close();
                        })
                    }
                }
            }
        }
                    }
                };

                Message.ShowDialog();

                if (DialogResult == true)
                {
                    if (SelectedCreator != null)
                    {
                        int SelectedRowNumber = SelectedCreator.id;

                        var CreatorToDelete = _DataBaseService.GetCreatorToDelete(SelectedRowNumber);

                        if (CreatorToDelete != null)
                        {
                            List<Scanner> ScannersByCreator = _DataBaseService.GetScannersByCreator(SelectedCreator.id);

                            if (ScannersByCreator.Count != 0)
                            {

                                foreach (var Scanner in ScannersByCreator) _DataBaseService.DeleteScanner(Scanner);
                            }

                            _DataBaseService.DeleteCreator(CreatorToDelete);

                            Message = new Window
                            {
                                Title = "Информация",
                                SizeToContent = SizeToContent.WidthAndHeight,
                                ResizeMode = ResizeMode.NoResize,
                                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                                Icon = Application.Current.MainWindow.Icon,
                                Background = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                                Content = new StackPanel
                                {
                                    Margin = new Thickness(20),
                                    Children =
                            {
                                new TextBlock
                                {
                                    Text = $"Информация о производителе и сканерах этого производителя были удалены.",
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    FontSize = 16,
                                    Margin = new Thickness(0, 0, 0, 20),
                                    Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
                                },
                                new System.Windows.Controls.Button
                                {
                                    Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                                    Content = "Замечательно!",
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                                    Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                                    Command = new RelayCommand(() => Message.Close())
                                }
                            }
                                }
                            };

                            Message.ShowDialog();

                        }

                        _DataBaseService.GetCreatorTableData(CreatorsTable);

                        ScannersTable.ItemsSource = _DataBaseService.GetPreparedForGridList();
                    }

                }
            }

            if (e.Key == Key.Home)
            {
                // Создаем переменную для хранения результата
                bool? DialogResult = null;

                Message = new Window
                {
                    Title = "Подтверждение сохранения",
                    SizeToContent = SizeToContent.WidthAndHeight,
                    ResizeMode = ResizeMode.NoResize,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Icon = Application.Current.MainWindow.Icon,
                    Background = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                    Content = new StackPanel
                    {
                        Margin = new Thickness(20),
                        Children =
        {
            new TextBlock
            {
                Text = $"Вы действительно хотите сохранить информацию о производителе №{SelectedCreator.id}?",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
                Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
            },
            new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Children =
                {
                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Да",
                        Width = 100,
                        Margin = new Thickness(0, 0, 10, 0),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = true;
                            Message.Close();
                        })
                    },
                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Нет",
                        Width = 100,
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = false;
                            Message.Close();
                        })
                    }
                }
            }
        }
                    }
                };

                Message.ShowDialog();

                if (DialogResult == true)
                {
                    bool IsNameGood = true;

                    if (string.IsNullOrWhiteSpace(SelectedCreator.name))
                    {
                        IsNameGood = false;
                    }

                    if (IsNameGood)
                    {
                        _DataBaseService.SaveCreator(SelectedCreator);

                        Window SuccessfulMessage = null;

                        SuccessfulMessage = new Window
                        {
                            Title = "Успешное сохранение",
                            SizeToContent = SizeToContent.WidthAndHeight,
                            ResizeMode = ResizeMode.NoResize,
                            WindowStartupLocation = WindowStartupLocation.CenterScreen,
                            Icon = Application.Current.MainWindow.Icon,
                            Background = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                            Content = new StackPanel
                            {
                                Margin = new Thickness(20),
                                Children =
        {
            new TextBlock
            {
                Text = $"Данные были успешно сохранены!",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
                Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
            },

                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Замечательно!",
                        Margin = new Thickness(0, 0, 10, 0),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = true;
                            SuccessfulMessage.Close();
                                })
                            },


                        }
                            }
                        };

                        SuccessfulMessage.ShowDialog();
                    }
                    else
                    {
                        string NameError = "";

                        if (!IsNameGood) NameError = $"Имя: \"{SelectedCreator.name}\"\n";

                        Window ErrorMessage = null;

                        ErrorMessage = new Window
                        {
                            Title = "Ошибка сохранения",
                            SizeToContent = SizeToContent.WidthAndHeight,
                            ResizeMode = ResizeMode.NoResize,
                            WindowStartupLocation = WindowStartupLocation.CenterScreen,
                            Icon = Application.Current.MainWindow.Icon,
                            Background = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                            Content = new StackPanel
                            {
                                Margin = new Thickness(20),
                                Children =
        {
            new TextBlock
            {
                Text = $"Не удалось сохранить информацию о производителе, поскольку:\n" +
                       $"{NameError}",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
                Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
            },

                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Очень жаль!",
                        Margin = new Thickness(0, 0, 10, 0),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = true;
                            ErrorMessage.Close();
                                })
                            },


                        }
                            }
                        };

                        ErrorMessage.ShowDialog();
                    }
                    ;
                }
            }
        }

        private void TechologiesTableKeyDown(object sender, KeyEventArgs e)
        {
            var SelectedTechnology = TechologiesTable.SelectedItem as Technology;

            Window Message = null;

            if (e.Key == Key.Delete)
            {
                // Создаем переменную для хранения результата
                bool? DialogResult = null;

                Message = new Window
                {
                    Title = "Подтверждение удаления",
                    SizeToContent = SizeToContent.WidthAndHeight,
                    ResizeMode = ResizeMode.NoResize,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Icon = Application.Current.MainWindow.Icon,
                    Background = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                    Content = new StackPanel
                    {
                        Margin = new Thickness(20),
                        Children =
        {
            new TextBlock
            {
                Text = $"Вы действительно хотите удалить информацию о технологии №{SelectedTechnology.id}? \n" +
                       $"При удалении информации о технологии будут удалены сканеры этой технологии.",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
                Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
            },
            new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Children =
                {
                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Да",
                        Width = 100,
                        Margin = new Thickness(0, 0, 10, 0),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = true;
                            Message.Close();
                        })
                    },
                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Нет",
                        Width = 100,
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = false;
                            Message.Close();
                        })
                    }
                }
            }
        }
                    }
                };

                Message.ShowDialog();

                if (DialogResult == true)
                {
                    if (SelectedTechnology != null)
                    {
                        int SelectedRowNumber = SelectedTechnology.id;

                        var TechnologyToDelete = _DataBaseService.GetTechnologyToDelete(SelectedRowNumber);

                        if (TechnologyToDelete != null)
                        {
                            List<Scanner> ScannersByTechnology = _DataBaseService.GetScannersByTechnology(SelectedTechnology.id);

                            if (ScannersByTechnology.Count != 0)
                            {

                                foreach (var Scanner in ScannersByTechnology) _DataBaseService.DeleteScanner(Scanner);
                            }

                            _DataBaseService.DeleteTechnology(TechnologyToDelete);

                            Message = new Window
                            {
                                Title = "Информация",
                                SizeToContent = SizeToContent.WidthAndHeight,
                                ResizeMode = ResizeMode.NoResize,
                                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                                Icon = Application.Current.MainWindow.Icon,
                                Background = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                                Content = new StackPanel
                                {
                                    Margin = new Thickness(20),
                                    Children =
                            {
                                new TextBlock
                                {
                                    Text = $"Информация о технологии и сканерах этой технологии были удалены.",
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    FontSize = 16,
                                    Margin = new Thickness(0, 0, 0, 20),
                                    Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
                                },
                                new System.Windows.Controls.Button
                                {
                                    Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                                    Content = "Замечательно!",
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                                    Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                                    Command = new RelayCommand(() => Message.Close())
                                }
                            }
                                }
                            };

                            Message.ShowDialog();

                        }

                        _DataBaseService.GetTechnologyTableData(TechologiesTable);

                        ScannersTable.ItemsSource = _DataBaseService.GetPreparedForGridList();
                    }

                }
            }

            if (e.Key == Key.Home)
            {
                // Создаем переменную для хранения результата
                bool? DialogResult = null;

                Message = new Window
                {
                    Title = "Подтверждение сохранения",
                    SizeToContent = SizeToContent.WidthAndHeight,
                    ResizeMode = ResizeMode.NoResize,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Icon = Application.Current.MainWindow.Icon,
                    Background = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                    Content = new StackPanel
                    {
                        Margin = new Thickness(20),
                        Children =
        {
            new TextBlock
            {
                Text = $"Вы действительно хотите сохранить информацию о технологии №{SelectedTechnology.id}?",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
                Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
            },
            new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                Children =
                {
                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Да",
                        Width = 100,
                        Margin = new Thickness(0, 0, 10, 0),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = true;
                            Message.Close();
                        })
                    },
                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Нет",
                        Width = 100,
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = false;
                            Message.Close();
                        })
                    }
                }
            }
        }
                    }
                };

                Message.ShowDialog();

                if (DialogResult == true)
                {
                    bool IsNameGood = true;

                    if (string.IsNullOrWhiteSpace(SelectedTechnology.name))
                    {
                        IsNameGood = false;
                    }

                    if (IsNameGood)
                    {
                        _DataBaseService.SaveTechnology(SelectedTechnology);

                        Window SuccessfulMessage = null;

                        SuccessfulMessage = new Window
                        {
                            Title = "Успешное сохранение",
                            SizeToContent = SizeToContent.WidthAndHeight,
                            ResizeMode = ResizeMode.NoResize,
                            WindowStartupLocation = WindowStartupLocation.CenterScreen,
                            Icon = Application.Current.MainWindow.Icon,
                            Background = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                            Content = new StackPanel
                            {
                                Margin = new Thickness(20),
                                Children =
        {
            new TextBlock
            {
                Text = $"Данные были успешно сохранены!",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
                Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
            },

                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Замечательно!",
                        Margin = new Thickness(0, 0, 10, 0),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = true;
                            SuccessfulMessage.Close();
                                })
                            },


                        }
                            }
                        };

                        SuccessfulMessage.ShowDialog();
                    }
                    else
                    {
                        string NameError = "";

                        if (!IsNameGood) NameError = $"Имя: \"{SelectedTechnology.name}\"\n";

                        Window ErrorMessage = null;

                        ErrorMessage = new Window
                        {
                            Title = "Ошибка сохранения",
                            SizeToContent = SizeToContent.WidthAndHeight,
                            ResizeMode = ResizeMode.NoResize,
                            WindowStartupLocation = WindowStartupLocation.CenterScreen,
                            Icon = Application.Current.MainWindow.Icon,
                            Background = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                            Content = new StackPanel
                            {
                                Margin = new Thickness(20),
                                Children =
        {
            new TextBlock
            {
                Text = $"Не удалось сохранить информацию о технологии, поскольку:\n" +
                       $"{NameError}",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
                Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
            },

                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Очень жаль!",
                        Margin = new Thickness(0, 0, 10, 0),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            DialogResult = true;
                            ErrorMessage.Close();
                                })
                            },


                        }
                            }
                        };

                        ErrorMessage.ShowDialog();
                    }
                    ;
                }
            }
        }

        private void AddingTechnologyButtonClick(object sender, RoutedEventArgs e)
        {
            var NewTechnology = new Technology();

            _DataBaseService.AddTechnology(NewTechnology);

            _DataBaseService.GetTechnologyTableData(TechologiesTable);

            TechologiesTable.Items.Refresh();
        }

        private void AddingCreatorButtonClick(object sender, RoutedEventArgs e)
        {
            var NewCreator = new Creator();

            _DataBaseService.AddCreator(NewCreator);

            _DataBaseService.GetCreatorTableData(CreatorsTable);

            CreatorsTable.Items.Refresh();
        }
        private void AddingScannerButtonClick(object sender, RoutedEventArgs e)
        {

        }

    }
    public class AdministratorWindowViewModel : INotifyPropertyChanged
    {
        DataBaseService _DataBaseService = new DataBaseService();

        public ObservableCollection<string> Technologies { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Creators { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> ColorCaptureList { get; } = new ObservableCollection<string>();

        public AdministratorWindowViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            // Загрузка производителей
            Creators.Clear();
            var CreatorsList = _DataBaseService.GetCreatorNames();
            foreach (var Creator in CreatorsList)
            {
                Creators.Add(Creator);
            }

            Technologies.Clear();
            var TechnologiesList = _DataBaseService.GetTechnologyNames();
            foreach (var Technology in TechnologiesList)
            {
                Technologies.Add(Technology);
            }

            ColorCaptureList.Clear();
            ColorCaptureList.Add("Да");
            ColorCaptureList.Add("Нет");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }



}
