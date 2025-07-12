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
            var ValidationResult = ValidateInputs();

            if (ValidationResult.IsValid)
            {
                var NewScanner = CreateScannerFromInputs();
                _DataBaseService.SaveScannerByDataGrid(NewScanner);
                ShowMessage("Успешное добавление", "Данные были успешно добавлены!", "Замечательно!");
            }
            else
            {
                ShowErrorMessage(ValidationResult);
            }
        }

        private (bool IsValid, Dictionary<string, string> Errors) ValidateInputs()
        {
            var Errors = new Dictionary<string, string>();

            // Проверка имени
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                Errors["Name"] = $"Имя: \"{NameTextBox.Text}\"\n";
            }
            else if (_DataBaseService.IsScannerNameExist(NameTextBox.Text))
            {
                Errors["NameExist"] = "(сканер с таким именем уже существует)\n";
            }

            // Проверка выпадающих списков
            if (CreatorComboBox.SelectedItem == null)
                Errors["Creator"] = "Не выбран производитель\n";

            if (TechnologyComboBox.SelectedItem == null)
                Errors["Technology"] = "Не выбрана технология\n";

            if (ColorCaptureComboBox.SelectedItem == null)
                Errors["ColorCapture"] = "Не выбран захват цвета\n";

            // Проверка числовых полей
            ValidateNumberField(SpeedTextBox.Text, "Speed", 1, int.MaxValue, "Скорость", Errors);
            ValidateNumberField(AccuracyTextBox.Text, "Accuracy", 1, int.MaxValue, "Точность", Errors);
            ValidateNumberField(PriceTextBox.Text, "Price", 1, double.MaxValue, "Стоимость", Errors);
            ValidateNumberField(ReleaseYearTextBox.Text, "ReleaseYear", 1970, 2025, "Год выпуска", Errors);

            // Проверка описания
            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                Errors["Description"] = $"Описание: \"{DescriptionTextBox.Text}\"\n";
            }

            return (Errors.Count == 0, Errors);
        }

        private void ValidateNumberField(string Input, string FieldName, int MinValue, int MaxValue, string DisplayName, Dictionary<string, string> Errors)
        {
            if (!int.TryParse(Input, out int Value))
            {
                Errors[FieldName] = $"{DisplayName}: {Input}\n";
                return;
            }

            if (Value < MinValue || Value > MaxValue)
            {
                Errors[FieldName] = $"{DisplayName}: {Input} (должно быть между {MinValue} и {MaxValue})\n";
            }
        }

        private void ValidateNumberField(string Input, string FieldName, double MinValue, double MaxValue, string DisplayName, Dictionary<string, string> Errors)
        {
            if (!double.TryParse(Input, out double Value))
            {
                Errors[FieldName] = $"{DisplayName}: {Input}\n";
                return;
            }

            if (Value < MinValue || Value > MaxValue)
            {
                Errors[FieldName] = $"{DisplayName}: {Input} (должно быть между {MinValue} и {MaxValue})\n";
            }
        }

        private ScannerDataGrid CreateScannerFromInputs()
        {
            return new ScannerDataGrid()
            {
                Name = NameTextBox.Text,
                Creator = CreatorComboBox.SelectedItem.ToString(),
                Technology = TechnologyComboBox.SelectedItem.ToString(),
                Accuracy = int.Parse(AccuracyTextBox.Text),
                Speed = int.Parse(SpeedTextBox.Text),
                ColorCapture = ColorCaptureComboBox.SelectedItem.ToString(),
                Price = double.Parse(PriceTextBox.Text),
                Release = int.Parse(ReleaseYearTextBox.Text),
                Description = DescriptionTextBox.Text,
            };
        }

        private void ShowMessage(string Title, string Message, string ButtonText)
        {
            var MessageWindow = CreateMessageWindow(Title, Message, ButtonText);
            MessageWindow.Closed += (s, e) => this.Close();
            MessageWindow.ShowDialog();
        }

        private void ShowErrorMessage((bool IsValid, Dictionary<string, string> Errors) ValidationResult)
        {
            var ErrorMessage = "Не удалось добавить новый сканер, поскольку:\n" +
                              string.Join("", ValidationResult.Errors.Values);

            var ErrorWindow = CreateMessageWindow("Ошибка сохранения", ErrorMessage, "Сейчас исправим!");
            ErrorWindow.ShowDialog();
        }

        private Window Window;

        private Window CreateMessageWindow(string Title, string Message, string ButtonText)
        {

            Window = new Window
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
                    Content = ButtonText,
                    Margin = new Thickness(0, 0, 10, 0),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                    Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                    Command = new RelayCommand(() => Window.Close())
                }
            }
                }
            };

            return Window;
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
