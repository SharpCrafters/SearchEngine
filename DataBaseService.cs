using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace SearchEngine
{
    internal class DataBaseService
    {
        private readonly ApplicationContext _DataBase = new ApplicationContext();

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
