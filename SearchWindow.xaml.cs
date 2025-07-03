using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SearchEngine
{
    /// <summary>
    /// Логика взаимодействия для SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();

            DataContext = new SearchWindowViewModel();
        }

        private void SearchScannerButtonClick(object sender, RoutedEventArgs e)
        {

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


        public SearchWindowViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            // Загрузка производителей
            Creators.Clear();
            Creators.Add("Все");
            var CreatorsList = DataBaseService.GetCreators();
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
            var TechnologiesList = DataBaseService.GetTechnologies();
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

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
