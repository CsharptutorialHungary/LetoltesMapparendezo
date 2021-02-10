using DloadOrganizer.Interfaces;
using DloadOrganizer.Properties;
using System;

namespace DloadOrganizer
{
    internal class ProgramConsole : IConsole
    {
        public void Exception(Exception ex)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: {0}", ex.Message);
            Console.ForegroundColor = currentColor;
        }

        public void Info(string format, params object[] arguments)
        {
            Console.WriteLine(format, arguments);
        }

        public void PressKeyAndExit()
        {
            Console.WriteLine(Resources.PressKeyToExit);
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
