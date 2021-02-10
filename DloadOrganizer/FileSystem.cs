using DloadOrganizer.Interfaces;
using System.IO;

namespace DloadOrganizer
{
    internal class FileSystem : IFileSystem
    {
        public string[] GetFiles(string directory)
        {
            return Directory.GetFiles(directory);
        }

        public void Move(string file, string target)
        {
            string? targetDir = Path.GetDirectoryName(target);

            if (targetDir != null 
                && !Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            File.Move(file, target);
        }
    }
}
