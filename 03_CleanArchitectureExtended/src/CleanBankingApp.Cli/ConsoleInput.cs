using System;

namespace CleanBankingApp.Cli
{
    public class ConsoleInput
    {
        public static string ReadString(string prompt)
        {
            Console.Write(prompt + ": ");
            return Console.ReadLine();
        }

        public static int ReadInteger(string prompt)
        {
            string numberInput = ReadString(prompt);
            while (!(int.TryParse(numberInput, out _)))
            {
                Console.WriteLine("Please enter a whole number");
                numberInput = ReadString(prompt);
            }
            return Convert.ToInt32(numberInput);
        }

        public static int ReadInteger(string prompt, int minimum, int maximum)
        {
            int number = ReadInteger(prompt);
            while (number < minimum || number > maximum)
            {
                Console.WriteLine("Please enter a whole number from " +
                                  minimum + " to " + maximum);
                number = ReadInteger(prompt);
            }
            return number;
        }

        public static decimal ReadDecimal(string prompt)
        {
            string numberInput = ReadString(prompt);
            while (!(decimal.TryParse(numberInput, out decimal number)) || number < 0)
            {
                Console.WriteLine("Please enter a decimal number, $0.00 or greater");
                numberInput = ReadString(prompt);
            }
            return Convert.ToDecimal(numberInput);
        }
    }
}