using DloadOrganizer.Configuration;

namespace DloadOrganizer.Interfaces
{
    internal interface IConfigurationManager
    {
        bool IsConfigExisting { get; }
        Config ReadConfigurationFile();
        void WriteExampleConfig();
    }
}
