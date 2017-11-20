//Experimenting with c# and connecting to databases with it. Hope to eventually extend this to work for web pages.
//Program is of extremely bad design in one big nasty main and made with little testing because the unit testing wouldn't let me ref this and was i was getting furious so i went without
//Program also has almost no error handling so be perfect :))))
//Program by Ammon Riley last edited 11/16/2017 around 10:40 pm
using System;
using System.Data.SqlClient;



namespace data2Web
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection myConnection;
            myConnection =  HDBConnectionEstablisher.EstablishConnection("classdemo", "classdemo", "snow-se-1", "fall2017");
            /*
            //Test to see if we can run some sql commands mang
            SqlCommand makeTable = new SqlCommand("CREATE TABLE testSchema.testTable (PersonID int identity(1,1) not null, firstName varchar(255));", newConnection);
            int i = makeTable.ExecuteNonQuery();
            Console.Write(i);

            TEST SUCCESS
            */

            //Loop for commands user wants to do
            while (true)
            {
                //Get the command or exit prompt
                Console.WriteLine("Input SQL command (in one line and perfectly) or input EXIT to close\nNote that only insert/create/alter statements supported no queries");
                String userWant = Console.ReadLine();

                //If prompt is EXIT then exit the loop and close program
                if (userWant == "EXIT")
                    break;

                //Make the command
                SqlCommand userCommand = new SqlCommand(userWant, myConnection);
                userCommand.ExecuteNonQuery();

                //FOR WHEN I'M MOTIVATED ENOUGH TO SUPPORT QUERIES AYY :)))))
                //SqlDataReader userReader = null;
                //userReader = userCommand.ExecuteReader();

            }

            //Attempt to close connection when done
            try
            {
                myConnection.Close();
                Console.WriteLine("Connection closed to database.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            //Wait for a keypress to exit the console
            Console.Write("\n");
            Console.WriteLine("Press any character to exit the program.");
            Console.ReadKey();
        }

    }
}
