using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;

namespace SearchEngine
{
    /// <summary>
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        DataBaseService _DataBaseService = new DataBaseService();

        public event Action DataUpdated;

        public SearchWindow()
        {
            InitializeComponent();

            DataContext = new SearchWindowViewModel();
        }

        private void SearchScannerButtonClick(object sender, RoutedEventArgs e)
        {
            List<ScannerDataGrid> TempList = new List<ScannerDataGrid>();

            List<ScannerDataGrid> FilterList = _DataBaseService.GetPreparedForGridList();

            if ((CreatorComboBox.SelectedItem != null) && (CreatorComboBox.SelectedItem != "Все"))
            {
                string SelectedCreator = CreatorComboBox.SelectedItem.ToString();

                foreach (var Scanner in FilterList) if (Scanner.creator == SelectedCreator) TempList.Add(Scanner);

                FilterList = TempList;

                TempList.Clear();
            }

            if ((TechnologyComboBox.SelectedItem != null) && (TechnologyComboBox.SelectedItem != "Все"))
            {
                string SelectedTechnology = TechnologyComboBox.SelectedItem.ToString();

                foreach (var Scanner in FilterList) if (Scanner.technology == SelectedTechnology) TempList.Add(Scanner);

                FilterList = TempList;

                TempList.Clear();
            }

            if ((ColorCaptureComboBox.SelectedItem != null) && (ColorCaptureComboBox.SelectedItem != "Все"))
            {
                string SelectedColorCapture = ColorCaptureComboBox.SelectedItem.ToString();

                foreach (var Scanner in FilterList) if (Scanner.colorcapture == SelectedColorCapture) TempList.Add(Scanner);

                FilterList = TempList;

                TempList.Clear();
            }

            if ((ReleaseYearComboBox.SelectedItem != null) && (ReleaseYearComboBox.SelectedItem != "Все"))
            {
                string SelectedReleaseYear = ReleaseYearComboBox.SelectedItem.ToString();

                foreach (var Scanner in FilterList) if (Scanner.release == int.Parse(SelectedReleaseYear)) TempList.Add(Scanner);

                FilterList = TempList;

                TempList.Clear();
            }

            if ((PriceComboBox.SelectedItem != null) && (PriceComboBox.SelectedItem != "Все"))
            {

                if (PriceComboBox.SelectedItem == "Менее 100 000₽")
                {
                    foreach (var ScannerForFirstChecking in FilterList) if (ScannerForFirstChecking.price < 100000) TempList.Add(ScannerForFirstChecking);

                    FilterList = TempList;

                    TempList.Clear();
                }

                else if (PriceComboBox.SelectedItem == "100 000 – 500 000₽")
                {
                    foreach (var ScannerForSecondChecking in FilterList) if ((ScannerForSecondChecking.price >= 100000) && (ScannerForSecondChecking.price < 500000)) TempList.Add(ScannerForSecondChecking);

                    FilterList = TempList;

                    TempList.Clear();
                }

                else if (PriceComboBox.SelectedItem == "500 001 – 1 000 000₽")
                {
                    foreach (var ScannerForThirdChecking in FilterList) if ((ScannerForThirdChecking.price >= 500001) && (ScannerForThirdChecking.price < 1000000)) TempList.Add(ScannerForThirdChecking);

                    FilterList = TempList;

                    TempList.Clear();
                }

                else if (PriceComboBox.SelectedItem == "1 000 001 - 2 000 000₽")
                {
                    foreach (var ScannerForFourthChecking in FilterList) if ((ScannerForFourthChecking.price >= 1000001) && (ScannerForFourthChecking.price < 2000000)) TempList.Add(ScannerForFourthChecking);

                    FilterList = TempList;

                    TempList.Clear();
                }

                else if (PriceComboBox.SelectedItem == "2 000 001 - 3 000 000₽")
                {
                    foreach (var ScannerForFifthChecking in FilterList) if ((ScannerForFifthChecking.price >= 2000001) && (ScannerForFifthChecking.price < 3000000)) TempList.Add(ScannerForFifthChecking);

                    FilterList = TempList;

                    TempList.Clear();
                }

                else if (PriceComboBox.SelectedItem == "3 000 001 - 4 000 000₽")
                {
                    foreach (var ScannerForSixthChecking in FilterList) if ((ScannerForSixthChecking.price >= 3000001) && (ScannerForSixthChecking.price < 4000000)) TempList.Add(ScannerForSixthChecking);

                    FilterList = TempList;

                    TempList.Clear();
                }

                else if (PriceComboBox.SelectedItem == "4 000 001 - 5 000 000₽")
                {
                    foreach (var ScannerForSeventhChecking in FilterList) if ((ScannerForSeventhChecking.price >= 4000001) && (ScannerForSeventhChecking.price < 5000000)) TempList.Add(ScannerForSeventhChecking);

                    FilterList = TempList;

                    TempList.Clear();
                }

                else if (PriceComboBox.SelectedItem == "Более 5 000 000₽")
                {
                    foreach (var ScannerForEighthChecking in FilterList) if (ScannerForEighthChecking.price > 5000000) TempList.Add(ScannerForEighthChecking);

                    FilterList = TempList;

                    TempList.Clear();
                }


            }

            Window Message = null;

            if (FilterList.Count == 0)
            {
                Message = new Window
                {
                    Title = "Неутешительные результаты",
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
                                    Text = $"По заданным фильтрам не было найдено ни одного сканера :(",
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    FontSize = 16,
                                    Margin = new Thickness(0, 0, 0, 20),
                                    Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
                                },
                                new System.Windows.Controls.Button
                                {
                                    Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                                    Content = "Очень жаль!",
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                                    Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                                    Command = new RelayCommand(() => Message.Close())
                                }
                            }
                    }
                };

                Message.Closed += (s, e) =>
                {
                    this.Close();
                };

                Message.ShowDialog();
            }

            if (FilterList.Count > 0)
            {
                Message = new Window
                {
                    Title = "Потрясающие результаты",
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
                                    Text = $"По заданным фильтрам было найдено следующее число записей: {FilterList.Count}.",
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

                Message.Closed += (s, e) =>
                {
                    this.Close();
                };

                Message.ShowDialog();

                PreparedList.List = FilterList;

                DataUpdated?.Invoke();

            }
        }

        private void ComboBoxSelected(object sender, RoutedEventArgs e)
        {
            SearchScannerButton.IsEnabled = true;
        }
    }

    public class NullToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SearchWindowViewModel : INotifyPropertyChanged
    {
        DataBaseService DataBaseService = new DataBaseService();

        public ObservableCollection<string> Technologies { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Prices { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> Creators { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> ReleaseYears { get; } = new ObservableCollection<string>();

        public ObservableCollection<string> ColorCaptureList { get; } = new ObservableCollection<string>();

        public SearchWindowViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            // Загрузка производителей
            Creators.Clear();
            Creators.Add("Все");
            var CreatorsList = DataBaseService.GetCreatorNames();
            foreach (var Creator in CreatorsList)
            {
                Creators.Add(Creator);
            }

            ReleaseYears.Clear();
            ReleaseYears.Add("Все");
            var ReleaseYearsList = DataBaseService.GetReleaseYears();
            foreach (var Year in ReleaseYearsList)
            {
                ReleaseYears.Add(Year.ToString());
            }

            Technologies.Clear();
            Technologies.Add("Все");
            var TechnologiesList = DataBaseService.GetTechnologyNames();
            foreach (var Technology in TechnologiesList)
            {
                Technologies.Add(Technology);
            }

            Prices.Clear();
            Prices.Add("Все");
            Prices.Add("Менее 100 000₽");
            Prices.Add("100 000 – 500 000₽");
            Prices.Add("500 001 – 1 000 000₽");
            Prices.Add("1 000 001 - 2 000 000₽");
            Prices.Add("2 000 001 - 3 000 000₽");
            Prices.Add("3 000 001 - 4 000 000₽");
            Prices.Add("4 000 001 - 5 000 000₽");
            Prices.Add("Более 5 000 000₽");

            ColorCaptureList.Clear();
            ColorCaptureList.Add("Все");
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
