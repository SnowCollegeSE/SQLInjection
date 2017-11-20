using System.Windows;
using System.Data.SqlClient;
using System.Windows.Media;
using System;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SqlConnection sqlConnection;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                    sqlConnection = null;
                }
                sqlConnection = HDBConnectionEstablisher.EstablishConnection(fUsername.Text, fUserpass.Password, fDBHost.Text, fDBName.Text);
                fStatus.Background = Brushes.Green;
                fStatus.Content = "Connected!";
                fRunQueryButton.IsEnabled = true;
            }
            catch(Exception except)
            {
                fStatus.Background = Brushes.Red;
                fStatus.Content = "Failed!";
                fRunQueryButton.IsEnabled = false;

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dataAdapter = new SqlDataAdapter(fQuery.Text, sqlConnection);

            
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            fDataGrid.ItemsSource = dt.DefaultView;
            dataAdapter.Update(dt);





        }
    }
}