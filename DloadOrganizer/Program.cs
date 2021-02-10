using DloadOrganizer.Interfaces;
using System;

namespace DloadOrganizer
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            IConsole console = new ProgramConsole();
            IConfigurationManager configurationManager = new ConfigurationManager();
            IConfigurationValidator validator = new ConfigurationValidator();
            IFileSystem fileSystem = new FileSystem();

            var organizer = new Organizer(console, configurationManager, validator, fileSystem);

            try
            {
                organizer.LoadConfig();
                organizer.Run();
            }
            catch (Exception ex)
            {
                console.Exception(ex);
                console.PressKeyAndExit();
            }

        }
    }
}
