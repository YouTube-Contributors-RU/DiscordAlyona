namespace Bot.Models.Settings
{
    public class SettingsModel
    {
        public required string ApplicationName { get; set; }
        public Dictionary<string, string>? DynamicVariables { get; set; }
    }
}
