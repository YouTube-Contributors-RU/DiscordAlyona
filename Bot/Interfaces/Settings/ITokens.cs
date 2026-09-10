//== Copyright (C) 2026, YouTube Contributors and dmitriykotik. ==
// Released under the MIT License.
// 
// This file is part of DiscordAlyona.
// This software is provided "AS IS", without warranty of any kind,
// express or implied, including but not limited to warranties
// of merchantability, fitness for a particular purpose and
// noninfringement.

namespace Bot.Interfaces.Settings
{
    #region INTERFACE | ITokens
    /// <summary>
    /// Interface for the token class.
    /// </summary>
    public interface ITokens
    {
        /// <summary>
        /// Receives a token.
        /// </summary>
        /// <param name="key">A variable containing the required token.</param>
        /// <returns>Token.</returns>
        public string? GetToken(string key);

        /// <summary>
        /// Sets the token to a variable.
        /// </summary>
        /// <param name="key">The variable into which the token must be entered.</param>
        /// <param name="token">Token.</param>
        public bool SetToken(string key, string token);

        /// <summary>
        /// Removes the token.
        /// </summary>
        /// <param name="key">A variable containing a token.</param>
        /// <returns>true - if the token deletion was successful. Otherwise, else.</returns>
        public bool RemoveToken(string key);
    }
    #endregion
}
