# DiscordBot (YouTube Contributors) | YTCDB.Bot

[English](README.EN.md)

**DiscordBot** (YouTube Contributors RU) — Discord-бот для сообщества [YouTube Contributors RU](https://github.com/YouTube-Contributors-RU). Проект создаётся как основа для автоматизации задач сообщества и развития его Discord-сервера.

> Проект находится на ранней стадии разработки. Функции, взаимодействующие с Discord, ещё могут меняться и дополняться.

## Возможности на текущем этапе

- консольное приложение на .NET 8;
- загрузка и создание файла настроек `settings.json`;
- журналирование в консоль и файл;
- защищённое локальное хранение токенов с помощью ASP.NET Core Data Protection;
- внутренние CLI-команды для сохранения и удаления токенов;
- сценарии сборки, тестирования и публикации для Windows, Linux и macOS;
- автоматическая сборка и публикация релизных артефактов через GitHub Actions.

## Требования

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0);
- Visual Studio Community (Вы можете обойтись и без этой IDE, но мы настоятельно рекомендуем её использовать.);
- Windows, Linux или macOS;
- Bash для использования `build.sh` либо `cmd.exe` для `build.bat`.

## Быстрый старт

```bash
git clone https://github.com/YouTube-Contributors-RU/DiscordBot.git
cd DiscordBot
dotnet run --project Bot/Bot.csproj
```

При первом запуске рядом с исполняемым файлом создаётся `settings.json` с примером настроек. Задайте уникальное имя приложения, прежде чем сохранять токены:

```json
{
  "ApplicationName": "YTCDB.Bot",
  "DynamicVariables": {}
}
```

`ApplicationName` участвует в защите токенов, поэтому не меняйте его после того, как токены уже сохранены.

## Токены

Токены не хранятся в репозитории и не записываются в `settings.json`. Они шифруются средствами Data Protection и сохраняются локально в профиле пользователя: `%AppData%/<ApplicationName>/token.protected` в Windows (или в аналогичном каталоге данных приложения в другой ОС).

Для сохранения токена запустите:

```bash
dotnet run --project Bot/Bot.csproj -- set_token DiscordToken <ваш_токен>
```

Для удаления:

```bash
dotnet run --project Bot/Bot.csproj -- remove_token DiscordToken
```

Имена токенов регистронезависимы. Никогда не передавайте настоящий токен в issue, pull request, логах или командной истории, доступной другим пользователям.

## Сборка и тесты

Стандартные команды .NET:

```bash
dotnet restore
dotnet build YTCDB.sln --configuration Release
dotnet test YTCDB.sln --configuration Release
```

Также в корне доступны интерактивные скрипты:

```bat
build.bat
```

```bash
./build.sh
```

Для неинтерактивного запуска используйте `build.bat -build`, `build.bat -test`, `build.bat -publish` или `./build.sh -build`, `./build.sh -test`, `./build.sh -publish`. Укажите целевую платформу через `-rid`, например `./build.sh -rid linux-x64 -publish`.

## Публикация

Пример самостоятельной публикации для Windows x64:

```bash
dotnet publish Bot/Bot.csproj --configuration Release --runtime win-x64 --self-contained true --output ./publish/win-x64
```

Скрипты сборки поддерживают `win-x64`, `linux-x64`, `osx-x64`, `osx-arm64` и другие совместимые [.NET Runtime Identifier](https://learn.microsoft.com/dotnet/core/rid-catalog).

## Структура проекта

```text
Bot/
├── Commands/        # Внутренние CLI-команды
├── Interfaces/      # Контракты компонентов
├── Internal/        # Внутренние модели и константы
├── Models/          # Модели настроек и журналирования
├── Utils/           # Настройки, JSON и журналирование
├── Native/          # Нативные библиотеки, поставляемые со сборкой
└── Program.cs       # Точка входа
```

## Участие в разработке

Будем рады улучшениям. Создайте ветку, внесите небольшое сфокусированное изменение, выполните сборку и тесты, затем откройте pull request с понятным описанием.

## Лицензия

Код распространяется по лицензии [MIT](LICENSE). Copyright © 2026 YouTube Contributors RU и dmitriykotik.

