using System;

namespace DloadOrganizer.Interfaces
{
    internal interface IConsole
    {
        void Info(string format, params object[] arguments);
        void Exception(Exception ex);
        void PressKeyAndExit();
    }
}
