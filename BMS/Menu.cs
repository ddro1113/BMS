namespace BMS
{
    internal class Menu
    {
        private static string accountNumber = string.Empty; // Add a class-level variable to store the account number

        public static void MainMenu()
        {
            bool exitProgram = false;
            while (!exitProgram)
            {
                // Main menu
                Console.WriteLine("""
                    1. Create account
                    2. Deposit
                    3. Withdraw
                    4. Transfer Funds
                    5. Check Balance
                    6. Clear
                    7. Exit
                    """);
                Console.Write("> ");
                string mainMenuInput = Console.ReadLine();
                if (int.TryParse(mainMenuInput, out int mainMenuOutput))
                {
                    switch (mainMenuOutput)
                    {
                        case 1:
                            Console.Clear();
                            CreateAccount.Details();
                            break;

                        case 2:
                            Console.Clear();
                            Transaction.Deposit();
                            break;

                        case 3:
                            Console.Clear();
                            Transaction.Withdraw();
                            break;

                        case 4:
                            Console.Clear();
                            Transaction.Transfer();
                            break;

                        case 5:
                            Console.Clear();
                            Transaction.Balance();
                            break;

                        case 6:
                            Console.Clear();
                            break;

                        case 7:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid input! Please try again...");
                            Console.WriteLine("***********************************");
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input! Please try again...");
                    Console.WriteLine("***********************************");
                }
            }
        }
    }
}