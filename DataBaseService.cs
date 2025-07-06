using System.Windows.Controls;

namespace SearchEngine
{
    internal class DataBaseService
    {
        private readonly AccessDbContext _DataBase = new AccessDbContext();

        public void GetCreatorTableData(DataGrid CreatorsTable)
        {
            CreatorsTable.ItemsSource = _DataBase.Creator.ToList();
        }

        public void GetTechnologyTableData(DataGrid TechnologiesTable)
        {
            TechnologiesTable.ItemsSource = _DataBase.Technology.ToList();
        }

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

        public int GetCreatorIdByName(string CreatorName)
        {
            foreach (var Creator in _DataBase.Creator) if (Creator.name == CreatorName) return Creator.id;

            return 0;
        }
        public string GetTechnologyNameById(int TechnologyId)
        {
            foreach (var Technology in _DataBase.Technology) if (Technology.id == TechnologyId) return Technology.name;

            return "";
        }

        public int GetTechnologyIdByName(string TechnologyName)
        {
            foreach (var Technology in _DataBase.Technology) if (Technology.name == TechnologyName) return Technology.id;

            return 0;
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

        public Scanner GetScannerById(int ScannerId)
        {
            foreach (var Scanner in _DataBase.Scanner) if (Scanner.id == ScannerId) return Scanner;

            return new Scanner();
        }

        public Scanner? GetScannerToDelete(int SelectedRowNumber) => _DataBase.Scanner.FirstOrDefault(s => s.id == SelectedRowNumber);

        public Creator? GetCreatorToDelete(int SelectedRowNumber) => _DataBase.Creator.FirstOrDefault(s => s.id == SelectedRowNumber);

        public Technology? GetTechnologyToDelete(int SelectedRowNumber) => _DataBase.Technology.FirstOrDefault(s => s.id == SelectedRowNumber);

        public void DeleteScanner(Scanner Scanner)
        {
            _DataBase.Scanner.Remove(Scanner);
            _DataBase.SaveChanges();
        }

        public void DeleteCreator(Creator Creator)
        {
            _DataBase.Creator.Remove(Creator);
            _DataBase.SaveChanges();
        }

        public void DeleteTechnology(Technology Technology)
        {
            _DataBase.Technology.Remove(Technology);
            _DataBase.SaveChanges();
        }

        public void AddCreator(Creator Creator)
        {
            _DataBase.Creator.Add(Creator);
            _DataBase.SaveChanges();
        }

        public void AddTechnology(Technology Technology)
        {
            _DataBase.Technology.Add(Technology);
            _DataBase.SaveChanges();
        }

        public List<Scanner> GetScannersByCreator(int CreatorId)
        {
            List<Scanner> RequiredScanners = new List<Scanner>();

            foreach (Scanner Scanner in _DataBase.Scanner) if (Scanner.creator == CreatorId) RequiredScanners.Add(Scanner);

            return RequiredScanners;
        }

        public List<Scanner> GetScannersByTechnology(int TechnologyId)
        {
            List<Scanner> RequiredScanners = new List<Scanner>();

            foreach (Scanner Scanner in _DataBase.Scanner) if (Scanner.technology == TechnologyId) RequiredScanners.Add(Scanner);

            return RequiredScanners;
        }

        public void SaveTechnology(Technology Technology)
        {
            _DataBase.Update(Technology);
            _DataBase.SaveChanges();
        }
        public void SaveCreator(Creator Creator)
        {
            _DataBase.Update(Creator);
            _DataBase.SaveChanges();
        }
        public void SaveScannerByDataGrid(ScannerDataGrid SelectedScanner)
        {
            var _Scanner = GetScannerById(SelectedScanner.id);

            _Scanner.name = SelectedScanner.name;
            _Scanner.creator = GetCreatorIdByName(SelectedScanner.creator);
            _Scanner.technology = GetTechnologyIdByName(SelectedScanner.technology);
            _Scanner.accuracy = SelectedScanner.accuracy;
            _Scanner.speed = SelectedScanner.speed;
            _Scanner.colorcapture = SelectedScanner.colorcapture;
            _Scanner.price = SelectedScanner.price;
            _Scanner.release = SelectedScanner.release;
            _Scanner.description = SelectedScanner.description;

            _DataBase.Update(_Scanner);
            _DataBase.SaveChanges();
        }
    }
}
