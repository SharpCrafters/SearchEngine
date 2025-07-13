using System.Windows.Controls;

namespace SearchEngine
{
    internal class DataBaseService
    {
        private readonly ApplicationContext _DataBase = new ApplicationContext();

        public void SetNewPasswordHashToUser()
        {
            string NewHash = PasswordHasher.HashPassword("user");

            foreach (var User in _DataBase.User)

                if (User.Name == "user")
                {
                    User.PasswordHash = NewHash;

                    _DataBase.Update(User);

                    _DataBase.SaveChanges();
                }
        }

        public void SetNewPasswordHashToAdministrator()
        {
            string NewHash = PasswordHasher.HashPassword("admin");

            foreach (var User in _DataBase.User)

                if (User.Name == "admin")
                {
                    User.PasswordHash = NewHash;

                    _DataBase.Update(User);

                    _DataBase.SaveChanges();
                }
        }

        public bool IsScannerNameRepeated(Scanner Scanner)
        {
            if (!string.IsNullOrWhiteSpace(Scanner.Name)) return false;

            foreach (var _Scanner in _DataBase.Scanner) if ((_Scanner.Name == Scanner.Name) && (_Scanner.id != Scanner.id)) return true;

            return false;
        }

        public bool IsCreatorNameRepeated(Creator Creator)
        {
            if (string.IsNullOrWhiteSpace(Creator.Name)) return false;

            foreach (var _Creator in _DataBase.Creator) if ((_Creator.Name == Creator.Name) && (_Creator.id != Creator.id)) return true;

            return false;
        }

        public bool IsTechnologyNameRepeated(Technology Technology)
        {
            if (string.IsNullOrWhiteSpace(Technology.Name)) return false;

            foreach (var _Technology in _DataBase.Technology) if ((_Technology.Name == Technology.Name) && (_Technology.id != Technology.id)) return true;

            return false;
        }

        public bool IsScannerNameExist(string ScannerName)
        {
            foreach (var Scanner in _DataBase.Scanner) if (ScannerName == Scanner.Name) return true;

            return false;
        }

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

            var string1 = PasswordHasher.HashPassword("admin");

            var string2 = PasswordHasher.HashPassword("user");

            foreach (var User in _DataBase.User) if ((User.Name == Name) && (User.VerifyPassword(Password, User.PasswordHash))) return true;

            return false;
        }

        public string GetCreatorNameById(int CreatorId)
        {
            foreach (var Creator in _DataBase.Creator) if (Creator.id == CreatorId) return Creator.Name;

            return "";
        }

        public int GetCreatorIdByName(string CreatorName)
        {
            foreach (var Creator in _DataBase.Creator) if (Creator.Name == CreatorName) return Creator.id;

            return 0;
        }

        public string GetTechnologyNameById(int TechnologyId)
        {
            foreach (var Technology in _DataBase.Technology) if (Technology.id == TechnologyId) return Technology.Name;

            return "";
        }

        public int GetTechnologyIdByName(string TechnologyName)
        {
            foreach (var Technology in _DataBase.Technology) if (Technology.Name == TechnologyName) return Technology.id;

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
                ScannerForGrid.Name = Scanner.Name;
                ScannerForGrid.Creator = GetCreatorNameById(Scanner.Creator);
                ScannerForGrid.Technology = GetTechnologyNameById(Scanner.Technology);
                ScannerForGrid.Accuracy = Scanner.Accuracy;
                ScannerForGrid.Speed = Scanner.Speed;
                ScannerForGrid.ColorCapture = Scanner.ColorCapture;
                ScannerForGrid.Price = Scanner.Price;
                ScannerForGrid.Release = Scanner.Release;
                ScannerForGrid.Description = Scanner.Description;

                PreparedList.Add(ScannerForGrid);
            }

            return PreparedList;
        }

        public List<string> GetCreatorNames()
        {
            List<string> CreatorNames = new List<string>();

            foreach (var Creator in _DataBase.Creator) CreatorNames.Add(Creator.Name);

            return CreatorNames;
        }

        public List<int> GetReleaseYears()
        {
            List<int> ReleaseYears = new List<int>();

            foreach (var Scanner in _DataBase.Scanner) if (!ReleaseYears.Contains(Scanner.Release)) ReleaseYears.Add(Scanner.Release);

            return ReleaseYears;
        }

        public List<string> GetTechnologyNames()
        {
            List<string> TechnologyNames = new List<string>();

            foreach (var Technology in _DataBase.Technology) TechnologyNames.Add(Technology.Name);

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

            foreach (Scanner Scanner in _DataBase.Scanner) if (Scanner.Creator == CreatorId) RequiredScanners.Add(Scanner);

            return RequiredScanners;
        }

        public List<Scanner> GetScannersByTechnology(int TechnologyId)
        {
            List<Scanner> RequiredScanners = new List<Scanner>();

            foreach (Scanner Scanner in _DataBase.Scanner) if (Scanner.Technology == TechnologyId) RequiredScanners.Add(Scanner);

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

            _Scanner.Name = SelectedScanner.Name;
            _Scanner.Creator = GetCreatorIdByName(SelectedScanner.Creator);
            _Scanner.Technology = GetTechnologyIdByName(SelectedScanner.Technology);
            _Scanner.Accuracy = SelectedScanner.Accuracy;
            _Scanner.Speed = SelectedScanner.Speed;
            _Scanner.ColorCapture = SelectedScanner.ColorCapture;
            _Scanner.Price = SelectedScanner.Price;
            _Scanner.Release = SelectedScanner.Release;
            _Scanner.Description = SelectedScanner.Description;

            _DataBase.Update(_Scanner);
            _DataBase.SaveChanges();
        }
    }
}
