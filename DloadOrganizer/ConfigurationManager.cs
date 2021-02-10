using DloadOrganizer.Configuration;
using DloadOrganizer.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace DloadOrganizer
{
    internal sealed class ConfigurationManager: IConfigurationManager
    {
        public string AppDir { get; }

        private const string ConfigFile = "DloadOrganizerConfig.json";

        public ConfigurationManager()
        {
            AppDir = AppContext.BaseDirectory;
            //AppDir = Path.GetDirectoryName(Process.GetCurrentProcess().StartInfo.FileName) ?? Environment.CurrentDirectory;
        }

        public bool IsConfigExisting
        {
            get => File.Exists(Path.Combine(AppDir, ConfigFile));
        }

        public Config ReadConfigurationFile()
        {
            using (StreamReader file = File.OpenText(Path.Combine(AppDir, ConfigFile)))
            {
                string json = file.ReadToEnd();
                return JsonSerializer.Deserialize<Config>(json);
            }
        }

        public void WriteExampleConfig()
        {
            Config cfg = new Config
            {
                SourceDirectory = "Path to downloads...",
                Rules = new Rule[]
                {
                    new Rule
                    {
                        Patterns = new string[] { ".pdf", ".docx" },
                        TargetDirectory = "Docomuents"
                    }
                }
            };

            using (StreamWriter file = File.CreateText(Path.Combine(AppDir, ConfigFile)))
            {
                string json = JsonSerializer.Serialize(cfg, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                file.Write(json);
            }
        }
    }
}
