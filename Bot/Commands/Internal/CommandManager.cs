using Bot.Internal;
using Bot.Macros.Logger;

namespace Bot.Commands.Internal
{
    #region CLASS | CommandManager
    /// <summary>
    /// Class for handling CLI arguments as internal commands.
    /// </summary>
    internal class CommandManager
    {
        private readonly string[] Arguments;
        private readonly CommandsSettingsModel CommandsSettingsModel;
        private readonly LoggerMacros Logger;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="args">CLI arguments.</param>
        /// <param name="commandsSettingsModel">Model for setting up an internal command manager.</param>
        public CommandManager(string[] args, CommandsSettingsModel commandsSettingsModel)
        {
            Arguments = args;
            CommandsSettingsModel = commandsSettingsModel;
            Logger = new(CommandsSettingsModel.Logger != null
                    ? Tools.GetLoggerApplicationSettings(CommandsSettingsModel.Logger, ApplicationIds.Core)
                    : null);
        }

        /// <summary>
        /// Called to invoke a handler, usually at the beginning of the program after initialization.
        /// </summary>
        public void ExecuteArgumentProcessing()
        {
            if (Arguments.Length <= 0)
                return;

            TokenCommands? tokenCommands = null;
            if (CommandsSettingsModel.Tokens != null)
                tokenCommands = new(Arguments, 
                    CommandsSettingsModel.Logger != null 
                    ? Tools.GetLoggerApplicationSettings(CommandsSettingsModel.Logger, ApplicationIds.Tokens) 
                    : null, 
                    CommandsSettingsModel.Tokens);

            switch (Arguments[0])
            {
                case "set_token":
                    if (tokenCommands == null)
                        return;
                    tokenCommands.SetToken();
                    break;

                case "remove_token":
                    if (tokenCommands == null)
                        return;
                    tokenCommands.RemoveToken();
                    break;

                #if DEBUG
                case "get_token":
                    if (tokenCommands == null)
                        return;
                    tokenCommands.GetToken();
                    break;
                #endif
            }

            Environment.Exit(0);
        }
    }
    #endregion
}
