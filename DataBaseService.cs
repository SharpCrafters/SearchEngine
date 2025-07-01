using System.Windows;
using System.Windows.Controls;

namespace SearchEngine
{
    internal class DataBaseService
    {
        private static readonly ApplicationContext _dataBase = new ApplicationContext();

        public void GetData(DataGrid DataGrid)
        {
            DataGrid.ItemsSource = _dataBase.Scanners;
        }

        public bool IsUserExist(string UserName, string UserPassword)
        {
            string Password = "";

            foreach (var User in _dataBase.Users) if (User.Name == UserName) Password = User.Password;

            if (Password == UserPassword)
            {
                MessageBox.Show($"Вход выполнен в качестве пользователя \"{UserName}\".", "Успешный вход", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }

            MessageBox.Show($"Пользователь с именем \"{UserName}\" и паролем \"{UserPassword}\" не найден.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;
        }
    }
}