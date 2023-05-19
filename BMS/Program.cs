using MySql.Data.MySqlClient;

namespace BMS
{
    internal class Program
    {
        // Details required to connect to the database
        private static string connectionString = "server=localhost;database=bms;uid=root;password=password;";

        private static void Main(string[] args)
        {
            EstablishDatabaseConnection(); // Start the database connection
            Thread.Sleep(1000);
            Console.Clear();
            // Display main menu if the database connection is successful
            Menu.MainMenu();
        }

        // Connecting to the database
        private static void EstablishDatabaseConnection()
        {
            try
            {
                // Create and manage a MySqlConnection object using the connection string (connectionString)
                using MySqlConnection connection = new MySqlConnection(connectionString);
                // Open the database connection
                connection.Open();
                // If the database connection is successful
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    // Display message
                    Console.WriteLine("Server connection successful!");
                }
            }
            // If the database connection is not successful
            catch (MySqlException ex)
            {
                // Display message
                Console.WriteLine(ex.Message);
                // Exit the program
                Environment.Exit(0);
            }
        }
    }
}