namespace Bot.Models
{
    public class LoggerApplication
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }

        public LoggerApplication(string name, string fileName, string description)
        {
            Name = name;
            FileName = fileName;
            Description = description;
        }
    }
}
