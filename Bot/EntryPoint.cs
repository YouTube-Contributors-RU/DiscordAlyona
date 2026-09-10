//== Copyright (C) 2026, YouTube Contributors and dmitriykotik. ==
// Released under the MIT License.
// 
// This file is part of YouTube-Contributors-RU/DiscordBot.
// This software is provided "AS IS", without warranty of any kind,
// express or implied, including but not limited to warranties
// of merchantability, fitness for a particular purpose and
// noninfringement.

using Bot.Commands.Internal;
using Bot.Interfaces.Logger;
using Bot.Interfaces.Settings;
using Bot.Internal;
using Bot.Models.Logger;
using Bot.Utils.Logger;
using Bot.Utils.Settings;
using System.Runtime.CompilerServices;

namespace Bot
{
    internal static class EntryPoint
    {
        internal static Dictionary<int, LoggerApplication> LoggerApplicationModels = [];
        internal static ILogger? Logger;
        internal static ISettings? Settings;
        internal static ITokens? Tokens;
        internal static string[] Arguments = [""];


        internal static async Task Launch(string[] args)
        {
            #region Init
            Init(); // Initialization

            #if DEBUG
            #warning This file contains a construct that checks for the presence of DEBUG. When compiled in DEBUG, the code may contain instrumentation necessary for testing, but not required or safe for the RELEASE version. Please remember to change the configuration to RELEASE when compiling the finished project.
            Log(LogLevel.Warn, "Warning! You are in debug mode!");
            #endif

            InitCommands(args); // This... is also initialization.
            #endregion

            Console.ReadLine();
            await Task.CompletedTask;
        }

        private static void Init()
        {
            /* 
             * ----------------
             * I decided there was no point in outputting application settings to a settings file, 
             * since the back-end code depends on it. The settings file also depends on a logger, 
             * and creating a temporary logger just for that purpose doesn't make sense.
             * ---------------- 
             */

            SetAllApplicationModels();
            Logger = new Logger(LoggerApplicationModels);
            Settings = new Settings(ConstVariables.SettingsFile, Tools.GetLoggerApplicationSettings(Logger, ApplicationIds.Settings), Tools.GetLoggerApplicationSettings(Logger, ApplicationIds.JFileManager));
            Tokens = new Tokens(Settings, Tools.GetLoggerApplicationSettings(Logger, ApplicationIds.Tokens));
        }

        private static void InitCommands(string[] args)
        {
            CommandsSettingsModel commandsSettingsModel = new()
            {
                Logger = Logger,
                Settings = Settings,
                Tokens = Tokens
            };
            CommandManager commandManager = new(Arguments, commandsSettingsModel);

            commandManager.ExecuteArgumentProcessing();
        }

        private static void SetAllApplicationModels()
        {
            foreach (ApplicationIds appId in Enum.GetValues(typeof(ApplicationIds)))
                LoggerApplicationModels.Add((int)appId, Tools.GetLoggerApplication(appId));
        }

        private static void Log(LogLevel logLevel, string message, [CallerMemberName] string callerName = "undefined")
        {
            if (Logger == null)
                return;

            Logger.Log((int)ApplicationIds.Core, logLevel, message, LogDestination.Both, callerName);
        }
    }
}
