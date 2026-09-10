# DiscordBot (YouTube Contributors) | YTCDB.Bot

[Русский](README.RU.md)

**DiscordBot** (YouTube Contributors RU) is a Discord bot for the [YouTube Contributors RU](https://github.com/YouTube-Contributors-RU) community. The project is being built as a foundation for community automation and for developing its Discord server.

> The project is at an early stage of development. Discord-facing functionality may still change and expand.

## Current capabilities

- .NET 8 console application;
- loading and creating the `settings.json` configuration file;
- console and file logging;
- protected local token storage using ASP.NET Core Data Protection;
- internal CLI commands for saving and removing tokens;
- build, test, and publish scripts for Windows, Linux, and macOS;
- GitHub Actions workflows for continuous builds and release artifacts.

## Requirements

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0);
- Visual Studio Community (You can do without this IDE, but we highly recommend it);
- Windows, Linux, or macOS;
- Bash to use `build.sh`, or `cmd.exe` to use `build.bat`.

## Quick start

```bash
git clone https://github.com/YouTube-Contributors-RU/DiscordBot.git
cd DiscordBot
dotnet run --project Bot/Bot.csproj
```

On its first start, the application creates a sample `settings.json` in the current working directory. Set a unique application name before saving tokens:

```json
{
  "ApplicationName": "YTCDB.Bot",
  "DynamicVariables": {}
}
```

`ApplicationName` is part of the token-protection setup, so do not change it after tokens have been saved.

## Tokens

Tokens are neither stored in the repository nor written to `settings.json`. They are encrypted through Data Protection and kept locally in the user profile: `%AppData%/<ApplicationName>/token.protected` on Windows (or the equivalent application-data directory on another OS).

Save a token with:

```bash
dotnet run --project Bot/Bot.csproj -- set_token DiscordToken <your_token>
```

Remove a token with:

```bash
dotnet run --project Bot/Bot.csproj -- remove_token DiscordToken
```

Token names are case-insensitive. Never share a real token in an issue, pull request, log, or command history available to other people.

## Build and test

Use the standard .NET commands:

```bash
dotnet restore
dotnet build YTCDB.sln --configuration Release
dotnet test YTCDB.sln --configuration Release
```

Interactive scripts are also available in the repository root:

```bat
build.bat
```

```bash
./build.sh
```

For non-interactive use, run `build.bat -build`, `build.bat -test`, `build.bat -publish`, or `./build.sh -build`, `./build.sh -test`, `./build.sh -publish`. Select a target platform with `-rid`, for example: `./build.sh -rid linux-x64 -publish`.

## Publishing

Example self-contained publish for Windows x64:

```bash
dotnet publish Bot/Bot.csproj --configuration Release --runtime win-x64 --self-contained true --output ./publish/win-x64
```

The build scripts support `win-x64`, `linux-x64`, `osx-x64`, `osx-arm64`, and other compatible [.NET Runtime Identifiers](https://learn.microsoft.com/dotnet/core/rid-catalog).

## Project structure

```text
Bot/
├── Commands/        # Internal CLI commands
├── Interfaces/      # Component contracts
├── Internal/        # Internal models and constants
├── Models/          # Settings and logging models
├── Utils/           # Settings, JSON, and logging utilities
├── Native/          # Native libraries shipped with the build
└── Program.cs       # Entry point
```

## Contributing

Contributions are welcome. Create a branch, make a small focused change, run the build and tests, then open a pull request with a clear description.

## License

This project is released under the [MIT License](LICENSE). Copyright © 2026 YouTube Contributors RU and dmitriykotik.

