using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SearchEngine
{

    internal class DataBaseService
    {
        private readonly ApplicationContext _DataBase = new ApplicationContext();

        public List<string> GetCreators()
        {
            List<string> CreatorsList = new List<string>();

            foreach (var Scanner in _DataBase.Scanners) 
            {
                if (!CreatorsList.Contains(Scanner.Creator))
                {
                    CreatorsList.Add(Scanner.Creator);
                }
            }

            CreatorsList.Sort();

            return CreatorsList;
        }

        public List<int> GetReleaseYears()
        {
            List<int> ReleaseYearsList = new List<int>();

            foreach (var Scanner in _DataBase.Scanners)
            {
                if (!ReleaseYearsList.Contains(Scanner.ReleaseYear))
                {
                    ReleaseYearsList.Add(Scanner.ReleaseYear);
                }
            }

            ReleaseYearsList.Sort();

            ReleaseYearsList.Reverse();

            return ReleaseYearsList;
        }

        public List<string> GetNamesByCreator(string Creator)
        {
            List<string> NamesByCreator = new List<string>();

            foreach (var Scanner in _DataBase.Scanners)
            {
                if (Scanner.Creator == Creator) NamesByCreator.Add(Scanner.Name);
            }

            NamesByCreator.Sort();

            return NamesByCreator;
        }

        public List<string> GetTechnologies()
        {
            List<string> TechnologiesList = new List<string>();

            foreach (var Scanner in _DataBase.Scanners)
            {
                if (!TechnologiesList.Contains(Scanner.Technology))
                {
                    TechnologiesList.Add(Scanner.Technology);
                }
            }

            TechnologiesList.Sort();

            return TechnologiesList;
        }

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
