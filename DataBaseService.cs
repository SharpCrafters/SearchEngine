using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace SearchEngine
{

    internal class DataBaseService
    {
        private readonly ApplicationContext _DataBase = new ApplicationContext();

        

        public void GetScannersTableData(DataGrid ScannersTable)
        {
            ScannersTable.ItemsSource = _DataBase.Scanners.ToList();
        }

        public bool IsUserExist(string UserName, string UserPassword)
        {
            string Password = "";
            foreach (var User in _DataBase.Users)
            {
                if (User.Name == UserName)
                {
                    Password = User.Password;
                    break;
                }
            }

            if (Password == UserPassword) return true;

            return false;
        }
    }
}
