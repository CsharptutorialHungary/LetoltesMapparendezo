namespace DloadOrganizer.Configuration
{
    internal sealed class Config
    {
        public string SourceDirectory { get; set; }

        public Rule[] Rules { get; set; }

        public Config()
        {
            SourceDirectory = string.Empty;
            Rules = new Rule[0];
        }
    }
}
