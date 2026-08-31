using Bot.Models;
using Bot.Utils.Logger;

namespace Bot
{
    internal static class EntryPoint
    {
        internal static async Task Launch(string[] args)
        {
            Console.WriteLine("Point");
            Dictionary<int, LoggerApplication> model = [ ];
            model.Add(0, new("Main", "main.log", "That's desc"));
            Logger logger = new Logger(model);
            logger.Log(0, LogLevel.Info, "Hi!");

            Console.ReadLine();
            await Task.CompletedTask;
        }
    }
}
