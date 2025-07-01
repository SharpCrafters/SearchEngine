using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace SearchEngine
{
    internal class DataBaseService
    {
        private readonly ApplicationContext _dataBase;

        public DataBaseService()
        {
            _dataBase = new ApplicationContext();
            _dataBase.Database.EnsureCreated(); // Создаем таблицу, если ее нет
        }

        public bool IsUserExist(string UserName, string UserPassword)
        {
            try
            {
                // Проверяем, что таблица существует
                if (!_dataBase.Database.CanConnect())
                {
                    MessageBox.Show("Ошибка доступа к базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                string Password = "";
                foreach (var User in _dataBase.Users)
                {
                    if (User.Name == UserName)
                    {
                        Password = User.Password;
                        break;
                    }
                }

                if (Password == UserPassword)
                {
                    MessageBox.Show($"Вход выполнен: {UserName}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }

                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка базы данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}