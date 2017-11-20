using System;
using System.Data.SqlClient;

namespace WpfApp1
{
    public class HDBConnectionEstablisher
    {
        public static SqlConnection EstablishConnection(string usrName, string userPasswd, string dbHostName, string sqlDbName)
        {
            var dbConnection = new SqlConnection("user id=" + usrName +
                              ";password=" + userPasswd + ";address=" + dbHostName +
                              ";database=" + sqlDbName + "; connection timeout=30;");
            //Attempt to open the connection, if successful continue
            try
            {
                dbConnection.Open();
                Console.WriteLine("Connection opened to database.");
                return dbConnection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine("I'm dying...  hit any key and program will end");
                Console.ReadKey();
                throw e;
            }

        }
    }
}
