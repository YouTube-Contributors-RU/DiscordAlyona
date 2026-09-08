using Bot.Models.Logger;
using Bot.Utils.Logger;

namespace Bot
{
    internal static class EntryPoint
    {
        internal static async Task Launch(string[] args)
        {
            Dictionary<int, LoggerApplication> loggerApplicationsModels = [ ]; // Добавить Id для настроек, токенов и jfilemanager

            // Реализовать консольные аргументы (команды) для сохранения токенов.
            
            Console.ReadLine();
            await Task.CompletedTask;
        }
    }
}
