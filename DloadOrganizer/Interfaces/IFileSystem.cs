namespace DloadOrganizer.Interfaces
{
    internal interface IFileSystem
    {
        void Move(string file, string target);
        string[] GetFiles(string directory);
    }
}
