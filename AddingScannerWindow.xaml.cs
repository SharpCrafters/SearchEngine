using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;

namespace SearchEngine
{
    /// <summary>
    /// Логика взаимодействия для AddingScannerWindow.xaml
    /// </summary>
    public partial class AddingScannerWindow : Window
    {
        DataBaseService _DataBaseService = new DataBaseService();

        public AddingScannerWindow()
        {
            InitializeComponent();

            DataContext = new AddingScannerWindowViewModel();
        }

        private void AddingNewScannerButtonClick(object sender, RoutedEventArgs e)
        {
            bool IsNameGood = !string.IsNullOrWhiteSpace(NameTextBox.Text);
            bool IsCreatorSelected = CreatorComboBox.SelectedItem != null;
            bool IsTechnologySelected = TechnologyComboBox.SelectedItem != null;
            bool IsAccuracyGood = true;
            bool IsSpeedGood = true;
            bool IsColorCaptureSelected = ColorCaptureComboBox.SelectedItem != null;
            bool IsPriceGood = true;
            bool IsReleaseYearGood = true;
            bool IsDescriptionGood = !string.IsNullOrWhiteSpace(DescriptionTextBox.Text);

            int SelectedSpeed = 0, SelectedAccuracy = 0, SelectedPrice = 0, SelectedReleaseYear = 0;

            if (!int.TryParse(SpeedTextBox.Text, out SelectedSpeed))
            {
                IsSpeedGood = false;
            }

            else if (SelectedSpeed <= 0)
            {
                IsSpeedGood = false;
            }

            if (!int.TryParse(AccuracyTextBox.Text, out SelectedAccuracy)) IsAccuracyGood = false;

            else if (SelectedAccuracy <= 0) IsAccuracyGood = false;

            if (!int.TryParse(PriceTextBox.Text, out SelectedPrice)) IsPriceGood = false;

            else if (SelectedPrice <= 0) IsPriceGood = false;

            if (!int.TryParse(ReleaseYearTextBox.Text, out SelectedReleaseYear)) IsReleaseYearGood = false;

            else if ((SelectedReleaseYear < 1970) && (SelectedReleaseYear > 2025)) IsReleaseYearGood = false;

            if (IsNameGood && IsCreatorSelected && IsTechnologySelected && IsAccuracyGood && IsSpeedGood &&
                IsColorCaptureSelected && IsPriceGood && IsReleaseYearGood && IsDescriptionGood)
            {
                var NewScanner = new ScannerDataGrid()
                {
                    name = NameTextBox.Text,
                    creator = CreatorComboBox.SelectedItem.ToString(),
                    technology = TechnologyComboBox.SelectedItem.ToString(),
                    accuracy = SelectedAccuracy,
                    speed = SelectedSpeed,
                    colorcapture = ColorCaptureComboBox.SelectedItem.ToString(),
                    price = SelectedPrice,
                    release = SelectedReleaseYear,
                    description = DescriptionTextBox.Text,
                };

                _DataBaseService.SaveScannerByDataGrid(NewScanner);

                Window SuccessfulMessage = null;

                SuccessfulMessage = new Window
                {
                    Title = "Успешное добавление",
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
                Text = $"Данные были успешно добавлены!",
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
                            SuccessfulMessage.Close();
                                })
                            },


                        }
                    }
                };

                SuccessfulMessage.Closed += (s, e) =>
                {
                    this.Close();
                };

                SuccessfulMessage.ShowDialog();
            }

            else
            {
                string NameError = "";
                string CreatorError = "";
                string TechnologyError = "";
                string AccuracyError = "";
                string SpeedError = "";
                string ColorCaptureError = "";
                string PriceError = "";
                string ReleaseError = "";
                string DescriptionError = "";

                if (!IsNameGood) NameError = $"Имя: \"{NameTextBox.Text}\"\n";
                if (!IsCreatorSelected) CreatorError = $"Не выбран производитель\n";
                if (!IsTechnologySelected) TechnologyError = $"Не выбрана технология\n";
                if (!IsAccuracyGood) AccuracyError = $"Точность: {AccuracyTextBox.Text}\n";
                if (!IsSpeedGood) SpeedError = $"Скорость: {SpeedTextBox.Text}\n";
                if (!IsColorCaptureSelected) ColorCaptureError = $"Не выбран захват цвета\n";
                if (!IsPriceGood) PriceError = $"Стоимость: {PriceTextBox.Text}\n";
                if (!IsReleaseYearGood) ReleaseError = $"Год выпуска: {ReleaseYearTextBox.Text}\n";
                if (!IsDescriptionGood) DescriptionError = $"Описание: \"{DescriptionTextBox.Text}\"\n";

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
                Text = $"Не удалось добавить новый сканер, поскольку:\n" +
                       $"{NameError}{CreatorError}{TechnologyError}{AccuracyError}{SpeedError}{ColorCaptureError}{PriceError}{ReleaseError}{DescriptionError}",
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 20),
                Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
            },

                    new System.Windows.Controls.Button
                    {
                        Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                        Content = "Сейчас исправим!",
                        Margin = new Thickness(0, 0, 10, 0),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                        Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                        Command = new RelayCommand(() =>
                        {
                            ErrorMessage.Close();
                                })
                            },


                        }
                    }
                };

                ErrorMessage.ShowDialog();
            }
        }
    }

    public class AddingScannerWindowViewModel : INotifyPropertyChanged
    {
        DataBaseService _DataBaseService = new DataBaseService();

        public ObservableCollection<string> Technologies { get; } = new ObservableCollection<string>();

        public ObservableCollection<string> Creators { get; } = new ObservableCollection<string>();

        public ObservableCollection<string> ColorCaptureList { get; } = new ObservableCollection<string>();

        public AddingScannerWindowViewModel()
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
