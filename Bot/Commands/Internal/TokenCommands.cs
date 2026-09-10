using Bot.Interfaces.Settings;
using Bot.Macros.Logger;
using Bot.Models.Logger;
using Bot.Utils.Logger;

namespace Bot.Commands.Internal
{
    #region CLASS | TokenCommands
    /// <summary>
    /// An internal class that defines command methods for working with tokens.
    /// </summary>
    internal class TokenCommands
    {
        private readonly string[] Arguments;
        private readonly LoggerApplicationSettings? LoggerApplicationSettings;
        private readonly LoggerMacros Logger;
        private readonly ITokens Tokens;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="args">CLI arguments.</param>
        /// <param name="loggerApplicationSettings">Logger settings for the application.</param>
        /// <param name="tokens">Tokens. (<see cref="ITokens" />)</param>
        public TokenCommands(string[] args, LoggerApplicationSettings? loggerApplicationSettings, ITokens tokens)
        {
            Arguments = args;
            LoggerApplicationSettings = loggerApplicationSettings;
            Logger = new(LoggerApplicationSettings);
            Tokens = tokens;
        }

        /// <summary>
        /// Sets the token.
        /// </summary>
        /// <returns>True if the operation was successful. Otherwise, False.</returns>
        public bool SetToken()
        {
            int requiredNumberOfArguments = 2;

            if (Arguments.Length != requiredNumberOfArguments + 1)
            {
                Logger.Log(LogLevel.Error, "This command requires two arguments: a variable and a value. Example: `set_token DogToken SuperSecret_Token`");
                return false;
            }

            try
            {
                Tokens.SetToken(Arguments[1], Arguments[2]);
                Logger.Log(LogLevel.Info, "Token saved successfully!");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"An unexpected error occurred while saving the token. More details: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Removes the token.
        /// </summary>
        /// <returns>True if the operation was successful. Otherwise, False.</returns>
        public bool RemoveToken()
        {
            int requiredNumberOfArguments = 1;

            if (Arguments.Length != requiredNumberOfArguments + 1)
            {
                Logger.Log(LogLevel.Error, "This command requires one argument: a variable. Example: `remove_token DogToken`");
                return false;
            }

            try
            {
                Tokens.RemoveToken(Arguments[1]);
                Logger.Log(LogLevel.Info, "Token deleted successfully!");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"An unexpected error occurred while deleting the token. More details: {ex.Message}");
                return false;
            }
        }

        #if DEBUG
        #warning This file contains a construct that checks for the presence of DEBUG. When compiled in DEBUG, the code may contain instrumentation necessary for testing, but not required or safe for the RELEASE version. Please remember to change the configuration to RELEASE when compiling the finished project.
        /// <summary>
        /// (ONLY DEBUG MODE) Receives a token.
        /// </summary>
        /// <returns>Decrypted token.</returns>
        public bool GetToken()
        {
            int requiredNumberOfArguments = 1;

            if (Arguments.Length != requiredNumberOfArguments + 1)
            {
                Logger.Log(LogLevel.Error, "This command requires one argument: a variable. Example: `get_token DogToken`");
                return false;
            }

            try
            {
                string? token = Tokens.GetToken(Arguments[1]);
                Logger.Log(LogLevel.Warn, "This command is only available in `Debug` mode. Please use the `Release` version!");
                Logger.Log(LogLevel.Info, $"`{Arguments[1]}`: {token}");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, $"An unexpected error occurred while deleting the token. More details: {ex.Message}");
                return false;
            }
        }
        #endif
    }
    #endregion
}
