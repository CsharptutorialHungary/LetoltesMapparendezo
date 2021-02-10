namespace DloadOrganizer.Configuration
{
    internal sealed class Rule
    {
        public string[] Patterns { get; set; }
        public string TargetDirectory { get; set; }

        public Rule()
        {
            Patterns = new string[0];
            TargetDirectory = string.Empty;
        }
    }
}
