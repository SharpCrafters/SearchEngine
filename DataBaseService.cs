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


    }
}
