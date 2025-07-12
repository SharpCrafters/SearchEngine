using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;

namespace SearchEngine
{

    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        DataBaseService _DataBaseService = new DataBaseService();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UserNameTextBox.Text) || string.IsNullOrEmpty(UserPasswordBox.Password))
            {
                return;
            }

            if (!_DataBaseService.IsUserExist(UserNameTextBox.Text, UserPasswordBox.Password))
            {
                ShowErrorMessage();
                return;
            }

            switch (UserNameTextBox.Text)
            {
                case "user":
                    ShowSuccessMessage("user", new UserMainWindow());
                    break;
                case "admin":
                    ShowSuccessMessage("admin", new AdministratorMainWindow());
                    break;
            }
        }

        private void ShowErrorMessage()
        {
            var Message = CreateMessageWindow("Неверное имя пользователя или пароль :(", "Неудачный вход", true);
            Message.Closed += (s, e) => UserPasswordBox.Password = "";
            Message.ShowDialog();
        }

        private void ShowSuccessMessage(string RoleName, Window MainWindow)
        {
            var Message = CreateMessageWindow($"Вы успешно вошли в систему в качестве пользователя \"{RoleName}\"!", "Успешный вход");

            Message.Closed += (s, e) => this.Close();
            Message.Closed += (s, e) => MainWindow.Show();
            Message.ShowDialog();
        }

        private Window Window;

        private Window CreateMessageWindow(string MessageText, string Title, bool IsError = false)
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
                    Text = MessageText,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 16,
                    Margin = new Thickness(0, 0, 0, 20),
                    Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
                },
                new Button
                {
                    Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                    Content = IsError ? "Очень жаль!" : "Отлично!",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                    Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                    Command = new RelayCommand(() => Window.Close()),
                }
            }
                }
            };

            return Window;
        }
    }
}

