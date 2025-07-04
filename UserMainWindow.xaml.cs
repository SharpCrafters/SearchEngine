using System.Windows;

namespace SearchEngine
{
    /// <summary>
    /// Логика взаимодействия для UserMainWindow.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {
        DataBaseService _DataBaseService = new DataBaseService();

        public UserMainWindow()
        {
            InitializeComponent();

            ScannersTable.ItemsSource = _DataBaseService.GetPreparedForGridList();
        }

        private void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            SearchWindow SearchWindow = new SearchWindow();
            SearchWindow.DataUpdated += OnDataUpdated;
            SearchWindow.Show();
        }

        private void OnDataUpdated()
        {
            ScannersTable.ItemsSource = PreparedList.List;
        }

        private void RefreshDataGridClick(object sender, RoutedEventArgs e)
        {
            ScannersTable.ItemsSource = _DataBaseService.GetPreparedForGridList();
        }

    }
}
