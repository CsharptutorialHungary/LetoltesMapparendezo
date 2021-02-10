using DloadOrganizer.Configuration;
using DloadOrganizer.Interfaces;
using DloadOrganizer.Properties;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DloadOrganizer
{
    internal sealed class ConfigurationValidator: IConfigurationValidator
    {
        private List<string> _errors;

        public IEnumerable<string> Errors => _errors;

        public ConfigurationValidator()
        {
            _errors = new List<string>();
        }

        public bool IsValid(Config config)
        {
            _errors.Clear();

            if (string.IsNullOrEmpty(config.SourceDirectory))
            {
                _errors.Add(Resources.ValidationNoSourceDir);
            }
            else if (!Directory.Exists(config.SourceDirectory))
            {
                _errors.Add(Resources.ValidationSourcetDirNotExist);
            }

            if (config.Rules == null || config.Rules.Length < 1)
            {
                _errors.Add(Resources.ValidationNoRules);
                return false;
            }
            else if (config.Rules.Any(r => string.IsNullOrEmpty(r.TargetDirectory)))
            {
                _errors.Add(Resources.ValidationRuleNoTarget);
            }
            else if (config.Rules.Any(r => !Directory.Exists(r.TargetDirectory)))
            {
                _errors.Add(Resources.ValidationRuleTargetNotExist);
            }
            else if (config.Rules.Any(r => r.Patterns == null || r.Patterns.Length == 0))
            {
                _errors.Add(Resources.ValidationRuleNoExtensions);
            }

            return _errors.Count == 0;
        }
    }
}
