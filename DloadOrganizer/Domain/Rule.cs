using System;
using System.Collections.Generic;

namespace DloadOrganizer.Domain
{
    public class Rule : IEquatable<Rule>
    {
        public string[] Extensions { get; set; }
        public string TargetFolder { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Rule);
        }

        public bool Equals(Rule other)
        {
            return other != null &&
                   EqualityComparer<string[]>.Default.Equals(Extensions, other.Extensions) &&
                   TargetFolder == other.TargetFolder;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Extensions, TargetFolder);
        }

        public static bool operator ==(Rule left, Rule right)
        {
            return EqualityComparer<Rule>.Default.Equals(left, right);
        }

        public static bool operator !=(Rule left, Rule right)
        {
            return !(left == right);
        }
    }
}
