using System;
using System.Data.SqlClient;

namespace data2Web
{
    public class HDBConnectionEstablisher
    {
        public static SqlConnection EstablishConnection()
        {
            //Nothing passed in, so I'm going to ask user for variables
            Console.WriteLine("Input User Id: ");
            String userID = Console.ReadLine();
            Console.Write("\n");
            //Get user password and make it invisible
            String userPass = null;
            Console.WriteLine("Input User Password: ");
            bool passLoop = true;
            while (passLoop == true)
            {
                var key = System.Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    passLoop = false;
                userPass += key.KeyChar;
            }
            Console.Write("\n");
            //Get name/ip of database server
            Console.WriteLine("Input Server Name: ");
            String serverName = Console.ReadLine();
            Console.Write("\n");
            //Get database you want to connect to
            Console.WriteLine("Input Database Name: ");
            String databaseName = Console.ReadLine();
            Console.Write("\n");

            return EstablishConnection(userID, userPass, serverName, databaseName);
        }
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
