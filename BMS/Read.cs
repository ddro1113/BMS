using System.Text;

namespace BMS
{
    internal class Read
    {
        // Converting password input to "*"
        public static string Input()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b");
                    password.Length--;
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    password.Append(keyInfo.KeyChar);
                }
            }
            while (keyInfo.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password.ToString();
        }

        // Converting password output to "*"
        public static string Output(string password)
        {
            return new string('*', password.Length);
        }
    }
}
