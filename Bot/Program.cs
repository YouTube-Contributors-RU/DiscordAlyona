//== Copyright (C) 2026, YouTube Contributors and dmitriykotik. ==
// Released under the MIT License.
// 
// This file is part of YouTube-Contributors-RU/DiscordBot.
// This software is provided "AS IS", without warranty of any kind,
// express or implied, including but not limited to warranties
// of merchantability, fitness for a particular purpose and
// noninfringement.

namespace Bot
{
    internal class Program
    {
        // This is the entry point to the program.
        static void Main(string[] args) => EntryPoint.Launch(args).GetAwaiter().GetResult();
    }
}
