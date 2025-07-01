using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void UserNameTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Имя пользователя")
            {
                textBox.Text = "";
            }
        }

        private void UserNameTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Имя пользователя";
            }
        }

        private void PasswordTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Пароль")
            {
                textBox.Text = "";
            }
        }

        private void PasswordTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Пароль";
            }
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            string UserName = UserNameTextBox.Text;
            string UserPassword = PasswordTextBox.Text;

            if ((UserName != null) && (UserPassword != null) && (UserName != "Имя пользователя") && (UserPassword != "Пароль"))
            {
                bool IsUserExist = _DataBaseService.IsUserExist(UserName, UserPassword);

                if (IsUserExist) this.Close();
            } 
        }
    }
}