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
            // Получаем данные один раз
            var FilterList = _DataBaseService.GetPreparedForGridList();

            // Применяем фильтры последовательно
            FilterList = ApplyComboBoxFilter(FilterList, CreatorComboBox, Scanner => Scanner.Creator);
            FilterList = ApplyComboBoxFilter(FilterList, TechnologyComboBox, Scanner => Scanner.Technology);
            FilterList = ApplyComboBoxFilter(FilterList, ColorCaptureComboBox, Scanner => Scanner.ColorCapture);
            FilterList = ApplyReleaseYearFilter(FilterList, ReleaseYearComboBox);
            FilterList = ApplyPriceFilter(FilterList, PriceComboBox);

            // Показываем результат
            ShowResultMessage(FilterList);
        }

        // Общий метод для фильтрации по ComboBox (кроме цены и года)
        private List<ScannerDataGrid> ApplyComboBoxFilter(List<ScannerDataGrid> Source, ComboBox ComboBox, Func<ScannerDataGrid, string> PropertySelector)
        {
            if (ComboBox.SelectedItem == null || ComboBox.SelectedItem.ToString() == "Все")
                return Source;

            var SelectedValue = ComboBox.SelectedItem.ToString();
            return Source.Where(Item => PropertySelector(Item) == SelectedValue).ToList();
        }

        // Фильтр для года выпуска
        private List<ScannerDataGrid> ApplyReleaseYearFilter(List<ScannerDataGrid> Source, ComboBox ComboBox)
        {
            if (ComboBox.SelectedItem == null || ComboBox.SelectedItem.ToString() == "Все")
                return Source;

            if (int.TryParse(ComboBox.SelectedItem.ToString(), out int Year))
            {
                return Source.Where(Item => Item.Release == Year).ToList();
            }
            return Source;
        }

        // Фильтр для цены
        private List<ScannerDataGrid> ApplyPriceFilter(List<ScannerDataGrid> Source, ComboBox ComboBox)
        {
            if (ComboBox.SelectedItem == null || ComboBox.SelectedItem.ToString() == "Все")
                return Source;

            var SelectedPrice = ComboBox.SelectedItem.ToString();
            return SelectedPrice switch
            {
                "Менее 100 000₽" => Source.Where(x => x.Price < 100000).ToList(),
                "100 000 – 500 000₽" => Source.Where(x => x.Price >= 100000 && x.Price <= 500000).ToList(),
                "500 001 – 1 000 000₽" => Source.Where(x => x.Price >= 500001 && x.Price <= 1000000).ToList(),
                "1 000 001 - 2 000 000₽" => Source.Where(x => x.Price >= 1000001 && x.Price <= 2000000).ToList(),
                "2 000 001 - 3 000 000₽" => Source.Where(x => x.Price >= 2000001 && x.Price <= 3000000).ToList(),
                "3 000 001 - 4 000 000₽" => Source.Where(x => x.Price >= 3000001 && x.Price <= 4000000).ToList(),
                "4 000 001 - 5 000 000₽" => Source.Where(x => x.Price >= 4000001 && x.Price <= 5000000).ToList(),
                "Более 5 000 000₽" => Source.Where(x => x.Price > 5000000).ToList(),
                _ => Source
            };
        }

        // Показ результата
        private void ShowResultMessage(List<ScannerDataGrid> FilterList)
        {
            var IsSuccess = FilterList.Count > 0;
            var Title = IsSuccess ? "Потрясающие результаты" : "Неутешительные результаты";
            var Message = IsSuccess
                ? $"По заданным фильтрам было найдено следующее число записей: {FilterList.Count}."
                : "По заданным фильтрам не было найдено ни одного сканера :(";
            var buttonText = IsSuccess ? "Замечательно!" : "Очень жаль!";

            Window MessageWindow = null;

            MessageWindow = new Window
            {
                Title = Title,
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
                    Text = Message,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 16,
                    Margin = new Thickness(0, 0, 0, 20),
                    Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
                },
                new Button
                {
                    Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                    Content = buttonText,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                    Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                    Command = new RelayCommand(() => MessageWindow.Close())
                }
            }
                }
            };

            MessageWindow.Closed += (s, e) => this.Close();
            MessageWindow.ShowDialog();

            if (IsSuccess)
            {
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
        DataBaseService _DataBaseService = new DataBaseService();

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
            var CreatorsList = _DataBaseService.GetCreatorNames();
            foreach (var Creator in CreatorsList)
            {
                Creators.Add(Creator);
            }

            ReleaseYears.Clear();
            ReleaseYears.Add("Все");
            var ReleaseYearsList = _DataBaseService.GetReleaseYears();
            foreach (var Year in ReleaseYearsList)
            {
                ReleaseYears.Add(Year.ToString());
            }

            Technologies.Clear();
            Technologies.Add("Все");
            var TechnologiesList = _DataBaseService.GetTechnologyNames();
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
