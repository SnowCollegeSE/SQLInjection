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
                fRunVulnQuery.IsEnabled = true;
                fProtectedQuery.IsEnabled = true;

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

        private void fQuery_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            fVulnQueryText.Content = "Select * from Customer where id=" + fVulnCustID.Text;
        }

        private void fProtectedCustID_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            fProtectedQueryText.Content = "Select * from Customer where id= (?)";
        }

        private void fRunVulnQuery_Click(object sender, RoutedEventArgs e)
        {   SqlDataAdapter da = new SqlDataAdapter(fVulnQueryText.Content.ToString(), sqlConnection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            fVulnDataGrid.ItemsSource = dt.DefaultView;
            da.Update(dt);
        }

        private void fProtectedQuery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand(null, sqlConnection);
                command.CommandText = "Select * from customer where id = @ID";
                SqlParameter idParameter = new SqlParameter("@ID", SqlDbType.Int, 0);
                idParameter.Value = fProtectedCustID.Text;
                command.Parameters.Add(idParameter);
                command.Prepare();
                var dataAdapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                fProtectedDataGrid.ItemsSource = dt.DefaultView;
                dataAdapter.Update(dt);
            } 
            catch 
            { //I ate the exception //BAD CODING PRACTICE
            }
        }
    }
}