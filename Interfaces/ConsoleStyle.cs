using System;

namespace Interfaces
{

    class ConsoleStyle
    {

        public ConsoleStyle()
        {

        }

        public void MainTitle(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\n\t{ message }\n");
            Console.ResetColor();
        }
        public void ServiceTitle(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"> { message }");
            Console.ResetColor();
        }
        public void ValidMessage(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"> { message }\n");
            Console.ResetColor();
        }

        public void ErrorMessage(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"> { message }\n");
            Console.ResetColor();
        }

        public void Imput()
        {
            Console.Write("\n> ");
        }
    }
}