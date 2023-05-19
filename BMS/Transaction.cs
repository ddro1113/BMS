using MySql.Data.MySqlClient;

namespace BMS
{
    internal class Transaction
    {
        // Details required to connect to the database
        private static string connectionString = "server=localhost;database=bms;uid=root;password=password;";

        public static void Deposit()
        {
            try
            {
                Console.Write("Account number: ");
                string accountNumber = Console.ReadLine();

                Console.Write("Password: ");
                string password = Read.Input();

                Console.Write("Enter the amount to deposit: ");
                decimal depositAmount = decimal.Parse(Console.ReadLine());

                // Create and manage a MySqlConnection object using the connection string(connectionString)
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Open the database connection
                    connection.Open();
                    // SQL query to retrieve the count of rows from the "user" table where the account number and password match the provided values
                    string selectQuery = "SELECT COUNT(*) FROM user WHERE AccountNumber = @AccountNumber AND Password = @Password";
                    // Create a MySqlCommand object to execute the selectQuery using the provided connection 
                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        // Binding the account number and password parameters to the selectCommand
                        selectCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);
                        selectCommand.Parameters.AddWithValue("@Password", password);
                        // Executing the query and retrieving the count of rows using ExecuteScalar
                        int count = Convert.ToInt32(selectCommand.ExecuteScalar());
                        // Checking if the count of rows is greater than 0 to verify if there is a user with the given account number and password in the "user" table
                        if (count > 0)
                        {
                            // SQL query to update the "Balance" column in the "user" table by adding the deposit amount for the user with the specified account number
                            string updateQuery = "UPDATE user SET Balance = Balance + @DepositAmount WHERE AccountNumber = @AccountNumber";
                            // Create a MySqlCommand object to execute the selectQuery using the provided connection 
                            using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                            {
                                // Binding the account number and password parameters to the updateCommand
                                updateCommand.Parameters.AddWithValue("@DepositAmount", depositAmount);
                                updateCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);
                                int rowsAffected = updateCommand.ExecuteNonQuery();
                                // Checking if the count of rows is greater than 0 to verify if there is a user with the given account number and password in the "user" table
                                if (rowsAffected > 0)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Deposit successful!");

                                    // SQL query to check the "Balance" column in the "user" table by after adding the deposit amount for the user with the specified account number
                                    string balanceQuery = "SELECT Balance FROM user WHERE AccountNumber = @AccountNumber";
                                    // Create a MySqlCommand object to execute the selectQuery using the provided connection 
                                    using (MySqlCommand balanceCommand = new MySqlCommand(balanceQuery, connection))
                                    {
                                        // Binding the account number and password parameters to the balanceCommand
                                        balanceCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);
                                        // Executing the balanceCommand and retrieving the balance value from the database
                                        using (MySqlDataReader reader = balanceCommand.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {
                                                // Retrieving the balance value from the reader and storing it in the balance variable
                                                decimal balance = reader.GetDecimal(0);
                                                // Displaying the account number and balance
                                                Console.WriteLine("Account number: " + accountNumber);
                                                Console.WriteLine("Balance: $" + balance);
                                                Console.WriteLine("***********************************");
                                            }
                                        }
                                    }
                                }
                                else
                                // If the account number and/or the password does not match with any user on the database
                                {
                                    Console.WriteLine("Failed to deposit! Account not found or incorrect password.");
                                    Console.WriteLine("***********************************");
                                }
                            }
                        }
                        else
                        {
                            // If the account number and/or the password does not match with any user on the database
                            Console.WriteLine("Failed to deposit! Account not found or incorrect password.");
                            Console.WriteLine("***********************************");
                        }
                    }
                }
            }
            // If the connection to the database failed
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
            }
        }

        public static void Withdraw()
        {
            try
            {
                Console.Write("Account number: ");
                string accountNumber = Console.ReadLine();

                Console.Write("Password: ");
                string password = Read.Input();

                Console.Write("Enter the amount to withdraw: ");
                decimal withdrawAmount = decimal.Parse(Console.ReadLine());

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT COUNT(*) FROM user WHERE AccountNumber = @AccountNumber AND Password = @Password";

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);
                        selectCommand.Parameters.AddWithValue("@Password", password);
                        int count = Convert.ToInt32(selectCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            string updateQuery = "UPDATE user SET Balance = Balance - @WithdrawAmount WHERE AccountNumber = @AccountNumber";

                            using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@WithdrawAmount", withdrawAmount);
                                updateCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);
                                int rowsAffected = updateCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Withdraw successful!");

                                    string balanceQuery = "SELECT Balance FROM user WHERE AccountNumber = @AccountNumber";

                                    using (MySqlCommand balanceCommand = new MySqlCommand(balanceQuery, connection))
                                    {
                                        balanceCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);

                                        using (MySqlDataReader reader = balanceCommand.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {
                                                decimal balance = reader.GetDecimal(0);
                                                Console.WriteLine("Account number: " + accountNumber);
                                                Console.WriteLine("Balance: $" + balance);
                                                Console.WriteLine("***********************************");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Failed to withdraw! Account not found or incorrect password.");
                                    Console.WriteLine("***********************************");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Failed to withdraw! Account not found or incorrect password.");
                            Console.WriteLine("***********************************");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
            }
        }

        public static void Transfer()
        {
            try
            {
                Console.Write("Account number: ");
                string accountNumber = Console.ReadLine();

                Console.Write("Password: ");
                string password = Read.Input();

                Console.Write("Enter the transfer amount: $");
                decimal transferAmount = decimal.Parse(Console.ReadLine());

                Console.Write("Transfer to: ");
                decimal transferTo = decimal.Parse(Console.ReadLine());

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT COUNT(*) FROM user WHERE AccountNumber = @AccountNumber AND Password = @Password";

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);
                        selectCommand.Parameters.AddWithValue("@Password", password);
                        int count = Convert.ToInt32(selectCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            // SQL query to update the "Balance" column in the "user" table by deducting the transfer amount for the user with the specified account number
                            string transferFromQuery = "UPDATE user SET Balance = Balance - @TransferFrom WHERE AccountNumber = @AccountNumber";

                            using (MySqlCommand transferFromCommand = new MySqlCommand(transferFromQuery, connection))
                            {
                                transferFromCommand.Parameters.AddWithValue("@TransferFrom", transferAmount);
                                transferFromCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);
                                int rowsAffected = transferFromCommand.ExecuteNonQuery();

                                // Check if the transfer from command updated any rows
                                if (rowsAffected > 0)
                                {
                                    // SQL query to update the "Balance" column in the "user" table by adding the transfer amount for the user with the specified transfer to (account number)
                                    string transferToQuery = "UPDATE user SET Balance = Balance + @TransferTo WHERE AccountNumber = @TransferToAccount";

                                    using (MySqlCommand transferToCommand = new MySqlCommand(transferToQuery, connection))
                                    {
                                        transferToCommand.Parameters.AddWithValue("@TransferTo", transferAmount);
                                        transferToCommand.Parameters.AddWithValue("@TransferToAccount", transferTo);

                                        int rowsAffectedTransferTo = transferToCommand.ExecuteNonQuery();

                                        // Check if both the transfer from and transfer to commands updated rows
                                        if (rowsAffectedTransferTo > 0)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Transfer of ${0} to {1} successful!", transferAmount, transferTo);

                                            string balanceQuery = "SELECT Balance FROM user WHERE AccountNumber = @AccountNumber";

                                            using (MySqlCommand balanceCommand = new MySqlCommand(balanceQuery, connection))
                                            {
                                                balanceCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);

                                                using (MySqlDataReader reader = balanceCommand.ExecuteReader())
                                                {
                                                    if (reader.Read())
                                                    {
                                                        decimal balance = reader.GetDecimal(0);
                                                        Console.WriteLine("Account number: " + accountNumber);
                                                        Console.WriteLine("Balance: $" + balance);
                                                        Console.WriteLine("***********************************");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Failed to transfer! Account not found or incorrect password.");
                                            Console.WriteLine("***********************************");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Failed to transfer! Account not found or incorrect password.");
                                    Console.WriteLine("***********************************");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Failed to transfer! Account not found or incorrect password.");
                            Console.WriteLine("***********************************");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
            }
        }

        public static void Balance()
        {
            try
            {
                Console.Write("Account number: ");
                string accountNumber = Console.ReadLine();

                Console.Write("Password: ");
                string password = Read.Input();

                using MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                // SQL query to display the "Balance" column in the "user" table with the specified account number
                string query = "SELECT Balance FROM user WHERE AccountNumber = @AccountNumber AND Password = @Password";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountNumber", accountNumber);
                command.Parameters.AddWithValue("@Password", password);

                using MySqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Console.Clear();
                    decimal balance = reader.GetDecimal(0);
                    Console.WriteLine("Account number: " + accountNumber);
                    Console.WriteLine("Balance: $" + balance);
                    Console.WriteLine("***********************************");
                }
                else
                {
                    Console.WriteLine("Invalid account number or password! Please try again...");
                    Console.WriteLine("***********************************");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************************");
            }
        }
    }
}
