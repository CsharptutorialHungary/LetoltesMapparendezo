using DloadOrganizer.Configuration;
using DloadOrganizer.Interfaces;
using DloadOrganizer.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DloadOrganizer
{
    internal class Organizer
    {
        private readonly IConsole _console;
        private readonly IConfigurationManager _configurationManager;
        private readonly IConfigurationValidator _validator;
        private readonly IFileSystem _fileSystem;

        private Config? _configuration;

        public Organizer(IConsole console,
                         IConfigurationManager configurationManager,
                         IConfigurationValidator validator,
                         IFileSystem fileSystem)
        {
            _console = console;
            _configurationManager = configurationManager;
            _validator = validator;
            _fileSystem = fileSystem;
        }

        public void LoadConfig()
        {
            if (_configurationManager.IsConfigExisting)
            {
                _configuration = _configurationManager.ReadConfigurationFile();
                if (!_validator.IsValid(_configuration))
                {
                    _console.Info(Resources.ConfigFileErrors);
                    _console.Info(string.Join("\r\n", _validator.Errors));
                    _console.PressKeyAndExit();
                }
            }
            else
            {
                _configurationManager.WriteExampleConfig();
                _console.Info(Resources.ConfigFileCreated);
                _console.PressKeyAndExit();
            }
        }

        public void Run()
        {
            if (_configuration == null)
                throw new InvalidOperationException(Resources.ConfigNotLoaded);

            string[] files = _fileSystem.GetFiles(_configuration.SourceDirectory);
            foreach (var rule in _configuration.Rules)
            {
                IEnumerable<string> filesForRule = GetFilesThatMatchExtension(files, rule.Patterns);
                CopyFiles(filesForRule, rule.TargetDirectory);
            }
        }

        private void CopyFiles(IEnumerable<string> filesForRule, string targetDirectory)
        {
            foreach (var sourceFile in filesForRule)
            {
                string fileName = Path.GetFileName(sourceFile);
                string target = Path.Combine(targetDirectory, fileName);
                _fileSystem.Move(sourceFile, target);
            }
        }

        private IEnumerable<string> GetFilesThatMatchExtension(string[] files, string[] extensions)
        {
            foreach (var file in files)
            {
                string extension = Path.GetExtension(file);
                if (extensions.Contains(extension))
                {
                    yield return file;
                }
            }
        }
    }
}
