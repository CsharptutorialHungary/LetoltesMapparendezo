using DloadOrganizer.Configuration;
using System.Collections.Generic;

namespace DloadOrganizer.Interfaces
{
    internal interface IConfigurationValidator
    {
        IEnumerable<string> Errors { get; }
        bool IsValid(Config config);
    }
}
