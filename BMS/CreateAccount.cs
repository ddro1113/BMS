using MySql.Data.MySqlClient;

namespace BMS
{
    internal class CreateAccount
    {
        // Details required to connect to the database
        private static string connectionString = "server=localhost;database=bms;uid=root;password=password;";
        public static void AccountCreated(string createAccountFirstName, string createAccountLastName, string createAccountDOB, string createAccountGender, string createAccountAddress, string createAccountPhoneNumber, string createAccountEmail, string createAccountIDNumber, string createAccountPassword, string accountNumber)
        {
            try
            {
                // Create and manage a MySqlConnection object using the connection string (connectionString)
                using MySqlConnection connection = new MySqlConnection(connectionString);
                // Open the database connection
                connection.Open();

                // The SQL query to insert user data into the 'user' table with parameterized values
                string query = "INSERT INTO user (FirstName, LastName, DOB, Gender, Address, PhoneNumber, Email, IdNumber, Password, AccountNumber) VALUES (@FirstName, @LastName, @DOB, @Gender, @Address, @PhoneNumber, @Email, @IdNumber, @Password, @AccountNumber)";

                // Generate an account number for the user
                GenerateAccountNumber();

                // Create a MySqlCommand object with the query and connection
                // Set parameter values for the query using user input
                // Execute the query to insert the user data into the database
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", createAccountFirstName);
                command.Parameters.AddWithValue("@LastName", createAccountLastName);
                command.Parameters.AddWithValue("@DOB", createAccountDOB);
                command.Parameters.AddWithValue("@Gender", createAccountGender);
                command.Parameters.AddWithValue("@Address", createAccountAddress);
                command.Parameters.AddWithValue("@phoneNumber", createAccountPhoneNumber);
                command.Parameters.AddWithValue("@Email", createAccountEmail);
                command.Parameters.AddWithValue("@IdNumber", createAccountIDNumber);
                command.Parameters.AddWithValue("@Password", createAccountPassword);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                command.ExecuteNonQuery();

            }
            // If the database connection is not successful
            catch (Exception ex)
            {
                // Display message
                Console.WriteLine(ex.Message);
            }
        }

        // Generate a random number for the account number
        public static string GenerateAccountNumber()
        {
            Random random = new Random();
            int accountNumber = random.Next(1230000000, 1239999999);
            return accountNumber.ToString();
        }

        public static void Details()
        {
            // Set the values to empty for looping purpose
            string createAccountFirstName = String.Empty;
            string createAccountLastName = String.Empty;
            string createAccountDOB = String.Empty;
            string createAccountGender = String.Empty;
            string createAccountAddress = String.Empty;
            string createAccountPhoneNumber = String.Empty;
            string createAccountEmail = String.Empty;
            string createAccountIDNumber = String.Empty;
            string createAccountPassword = String.Empty;

            // If the value is empty which is the default,
            while (String.IsNullOrEmpty(createAccountFirstName))
            {
                // Ask for the user input
                Console.Write("First name: ");
                createAccountFirstName = Console.ReadLine();
                // If the user input is empty
                if (String.IsNullOrEmpty(createAccountFirstName))
                {
                    Console.Clear();
                    Console.WriteLine("First name cannot be empty! Please try again..."); // Display message and ask the user again
                }
            }

            while (String.IsNullOrEmpty(createAccountLastName))
            {
                Console.Write("Last name: ");
                createAccountLastName = Console.ReadLine();
                if (String.IsNullOrEmpty(createAccountLastName))
                {
                    Console.Clear();
                    Console.WriteLine("Last name cannot be empty! Please try again...");
                }
            }

            while (String.IsNullOrEmpty(createAccountDOB))
            {
                Console.Write("DOB (MM/DD/YYYY): ");
                createAccountDOB = Console.ReadLine();
                if (String.IsNullOrEmpty(createAccountDOB))
                {
                    Console.Clear();
                    Console.WriteLine("DOB cannot be empty! Please try again...");
                }
            }

            while (String.IsNullOrEmpty(createAccountGender))
            {
                Console.Write("Gender: (M/F): ");
                createAccountGender = Console.ReadLine();
                if (String.IsNullOrEmpty(createAccountGender))
                {
                    Console.Clear();
                    Console.WriteLine("Gender cannot be empty! Please try again...");
                }
            }

            while (String.IsNullOrEmpty(createAccountAddress))
            {
                Console.Write("Address: ");
                createAccountAddress = Console.ReadLine();
                if (String.IsNullOrEmpty(createAccountAddress))
                {
                    Console.Clear();
                    Console.WriteLine("Address cannot be empty! Please try again...");
                }
            }

            while (String.IsNullOrEmpty(createAccountPhoneNumber))
            {
                Console.Write("Phone number: ");
                createAccountPhoneNumber = Console.ReadLine();
                if (String.IsNullOrEmpty(createAccountPhoneNumber))
                {
                    Console.Clear();
                    Console.WriteLine("Phone number cannot be empty! Please try again...");
                }
            }

            while (String.IsNullOrEmpty(createAccountEmail))
            {
                Console.Write("Email: ");
                createAccountEmail = Console.ReadLine();
                if (String.IsNullOrEmpty(createAccountEmail))
                {
                    Console.Clear();
                    Console.WriteLine("Email cannot be empty! Please try again...");
                }
            }

            while (String.IsNullOrEmpty(createAccountIDNumber))
            {
                Console.Write("ID Number: ");
                createAccountIDNumber = Console.ReadLine();
                if (String.IsNullOrEmpty(createAccountIDNumber))
                {
                    Console.Clear();
                    Console.WriteLine("ID number cannot be empty! Please try again...");
                }
            }

            string createAccountPasswordConfirm = String.Empty;
            while (String.IsNullOrEmpty(createAccountPassword) || createAccountPassword != createAccountPasswordConfirm)
            {
                Console.Write("Password: ");
                createAccountPassword = Read.Input();
                if (String.IsNullOrEmpty(createAccountPassword))
                {
                    Console.Clear();
                    Console.WriteLine("Password cannot be empty! Please try again...");
                    continue;
                }

                Console.Write("Confirm password: ");
                createAccountPasswordConfirm = Read.Input();
                if (String.IsNullOrEmpty(createAccountPasswordConfirm))
                {
                    Console.Clear();
                    Console.WriteLine("Password confirmation cannot be empty! Please try again...");
                    continue;
                }

                if (createAccountPasswordConfirm != createAccountPassword)
                {
                    Console.Clear();
                    Console.WriteLine("Password does not match! Please try again...");
                }
            }
            string accountNumber = BMS.CreateAccount.GenerateAccountNumber(); // Generate the user account number
            Console.Clear(); // Clear the console
            Console.WriteLine("Account created successfully!"); // Display message
            Thread.Sleep(500); // Pause execution for 500 milliseconds
            Console.Clear();
            BMS.CreateAccount.AccountCreated(createAccountFirstName, createAccountLastName, createAccountDOB, createAccountGender, createAccountAddress, createAccountPhoneNumber, createAccountEmail, createAccountIDNumber, createAccountPassword, accountNumber); // Call the AccountCreated method from the BMS.CreateAccount class
            DisplayAccountDetails(createAccountFirstName, createAccountLastName, createAccountDOB, createAccountGender, createAccountAddress, createAccountPhoneNumber, createAccountEmail, createAccountIDNumber, createAccountPassword, accountNumber); // Display the created account deatils
        }

        // Display the created account deatils
        public static void DisplayAccountDetails(string createAccountFirstName, string createAccountLastName, string createAccountDOB, string createAccountGender, string createAccountAddress, string createAccountPhoneNumber, string createAccountEmail, string createAccountIDNumber, string createAccountPassword, string accountNumber)
        {
            Console.WriteLine("""
                First name: {0}
                Last name: {1}
                DOB: {2}
                Gender: {3}
                Address: {4}
                Phone number: {5}
                Email: {6}
                ID Number: {7}
                Password: {8}
                Account number: {9}
                ***********************************
                """,
                createAccountFirstName,
                createAccountLastName,
                createAccountDOB,
                createAccountGender,
                createAccountAddress,
                createAccountPhoneNumber,
                createAccountEmail,
                createAccountIDNumber,
                Read.Output(createAccountPassword),
                accountNumber
                ); ;
        }
    }
}
