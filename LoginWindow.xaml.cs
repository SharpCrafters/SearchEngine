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
            if ((UserNameTextBox.Text != null) && (UserPasswordBox.Password != null) && (UserNameTextBox.Text.ToString().Length > 0) && (UserPasswordBox.Password.ToString().Length > 0))
            {

                if (_DataBaseService.IsUserExist(UserNameTextBox.Text, UserPasswordBox.Password))
                {
                    if (UserNameTextBox.Text == "user")
                    {

                        Window SuccessfulMessage = null;

                        UserMainWindow MainWindow = new UserMainWindow();

                        SuccessfulMessage = new Window
                        {
                            Title = "Успешный вход",
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
                                    Text = $"Вы успешно вошли в систему в качестве пользователя \"{UserNameTextBox.Text}\"!",
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    FontSize = 16,
                                    Margin = new Thickness(0, 0, 0, 20),
                                    Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
                                },
                                new System.Windows.Controls.Button
                                {
                                    Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                                    Content = "Отлично!",
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                                    Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                                    Command = new RelayCommand(() => SuccessfulMessage.Close()),
                                }
                            }
                            }
                        };
                        SuccessfulMessage.Closed += (s, e) => this.Close();

                        SuccessfulMessage.Closed += (s, e) =>
                        {
                            MainWindow.Show();
                        };

                        SuccessfulMessage.ShowDialog();
                    }

                    else if (UserNameTextBox.Text == "admin")
                    {
                        Window SuccessfulMessage = null;

                        AdministratorMainWindow MainWindow = new AdministratorMainWindow();

                        SuccessfulMessage = new Window
                        {
                            Title = "Успешный вход",
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
                                    Text = $"Вы успешно вошли в систему в качестве пользователя \"{UserNameTextBox.Text}\"!",
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    FontSize = 16,
                                    Margin = new Thickness(0, 0, 0, 20),
                                    Foreground = (Brush)new BrushConverter().ConvertFrom("#373737")
                                },
                                new System.Windows.Controls.Button
                                {
                                    Background = (Brush)new BrushConverter().ConvertFrom("#373737"),
                                    Content = "Отлично!",
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    BorderBrush = new SolidColorBrush(Color.FromRgb(55,55,55)),
                                    Foreground = (Brush)new BrushConverter().ConvertFrom("#D9D9D9"),
                                    Command = new RelayCommand(() => SuccessfulMessage.Close()),
                                }
                            }
                            }
                        };

                        SuccessfulMessage.Closed += (s, e) => this.Close();

                        SuccessfulMessage.Closed += (s, e) =>
                        {
                            MainWindow.Show();
                        };

                        SuccessfulMessage.ShowDialog();
                    }
                }

                else
                {
                    Window SuccessfulMessage = null;

                    SuccessfulMessage = new Window
                    {
                        Title = "Неудачный вход",
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
                                    Text = $"Неверное имя пользователя или пароль :(",
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
                                    Command = new RelayCommand(() => SuccessfulMessage.Close())
                                }
                            }
                        }
                    };
                    SuccessfulMessage.Closed += (s, e) => this.UserPasswordBox.Password = "";

                    SuccessfulMessage.ShowDialog();
                }

            }
        }
    }
}

