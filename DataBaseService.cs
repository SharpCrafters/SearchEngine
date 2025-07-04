using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SearchEngine
{
    internal class DataBaseService
    {
        private readonly AccessDbContext _DataBase = new AccessDbContext();

        public bool IsUserExist(string Name, string Password)
        {
            foreach (var User in _DataBase.User) if ((User.name == Name) && (User.password == Password)) return true;

            return false;
        }

        public string GetCreatorNameById(int CreatorId)
        {
            foreach (var Creator in _DataBase.Creator) if (Creator.id == CreatorId) return Creator.name;

            return "";
        }

        public string GetTechnologyNameById(int TechnologyId)
        {
            foreach (var Technology in _DataBase.Technology) if (Technology.id == TechnologyId) return Technology.name;

            return "";
        }

        public List<ScannerDataGrid> GetPreparedForGridList()
        {
            var ScannersList = new List<Scanner>();

                foreach (var Scanner in _DataBase.Scanner) ScannersList.Add(Scanner);

            var PreparedList = new List<ScannerDataGrid>();        

            foreach (var Scanner in ScannersList)
            {
                ScannerDataGrid ScannerForGrid = new ScannerDataGrid();
                ScannerForGrid.id = Scanner.id;
                ScannerForGrid.name = Scanner.name;
                ScannerForGrid.creator = GetCreatorNameById(Scanner.creator);
                ScannerForGrid.technology = GetTechnologyNameById(Scanner.technology);
                ScannerForGrid.accuracy = Scanner.accuracy;
                ScannerForGrid.speed = Scanner.speed;
                ScannerForGrid.colorcapture = Scanner.colorcapture;
                ScannerForGrid.price = Scanner.price;
                ScannerForGrid.release = Scanner.release;
                ScannerForGrid.description = Scanner.description;

                PreparedList.Add(ScannerForGrid);
            }

            return PreparedList;
        }
        
        public List<string> GetCreatorNames()
        {
            List<string> CreatorNames = new List<string>();

            foreach (var Creator in _DataBase.Creator) CreatorNames.Add(Creator.name);

            return CreatorNames;
        }

        public List<int> GetReleaseYears()
        {
            List<int> ReleaseYears = new List<int>();
            
            foreach (var Scanner in _DataBase.Scanner) if (!ReleaseYears.Contains(Scanner.release)) ReleaseYears.Add(Scanner.release);

            return ReleaseYears;
        }

        public List<string> GetTechnologyNames()
        {
            List<string> TechnologyNames = new List<string>();

            foreach (var Technology in _DataBase.Technology) TechnologyNames.Add(Technology.name);

            return TechnologyNames;
        }
    }
}
