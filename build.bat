REM //== Copyright (C) 2026, YouTube Contributors and dmitriykotik. ==
REM // Released under the MIT License.
REM // 
REM // This file is part of DiscordAlyona.
REM // This software is provided "AS IS", without warranty of any kind,
REM // express or implied, including but not limited to warranties
REM // of merchantability, fitness for a particular purpose and
REM // noninfringement.

@echo off
chcp 65001 > nul
setlocal enabledelayedexpansion

set "CONFIG=Release"
set "RID=win-x64"
set "SINGLE_FILE=true"
set "SELF_CONTAINED=true"
set "READY_TO_RUN=false"

if /i "%~1"=="-build" goto CLI_BUILD
if /i "%~1"=="-publish" goto CLI_PUBLISH
if /i "%~1"=="-test" goto CLI_TEST
if /i "%~1"=="-rid" (
    if not "%~2"=="" (
        set "RID=%~2"
        shift
        shift
        if /i "%~1"=="-build" goto CLI_BUILD
        if /i "%~1"=="-publish" goto CLI_PUBLISH
        if /i "%~1"=="-test" goto CLI_TEST
    )
)

:MENU
cls
echo ===================================================
echo    YouTube Contributors Discord Bot - Build Tool
echo ===================================================
echo  Current Build Settings:
echo    [1] Configuration   : !CONFIG!
echo    [2] Target RID      : !RID!
echo    [3] Single File     : !SINGLE_FILE!
echo    [4] Self-Contained  : !SELF_CONTAINED!
echo    [5] ReadyToRun      : !READY_TO_RUN!
echo ---------------------------------------------------
echo  Available Target RIDs:
echo    [W] win-x64   [L] linux-x64   [M] osx-x64   [A] osx-arm64
echo ---------------------------------------------------
echo  Actions:
echo    [B] Build solution
echo    [T] Run tests
echo    [P] Publish project
echo    [X] Exit
echo ===================================================
set /p CHOICE="Select an option: "

if /i "%CHOICE%"=="1" ( if "!CONFIG!"=="Release" (set "CONFIG=Debug") else (set "CONFIG=Release") & goto MENU )
if /i "%CHOICE%"=="2" goto SELECT_RID
if /i "%CHOICE%"=="3" ( if "!SINGLE_FILE!"=="true" (set "SINGLE_FILE=false") else (set "SINGLE_FILE=true") & goto MENU )
if /i "%CHOICE%"=="4" ( if "!SELF_CONTAINED!"=="true" (set "SELF_CONTAINED=false") else (set "SELF_CONTAINED=true") & goto MENU )
if /i "%CHOICE%"=="5" ( if "!READY_TO_RUN!"=="true" (set "READY_TO_RUN=false") else (set "READY_TO_RUN=true") & goto MENU )

if /i "%CHOICE%"=="W" set "RID=win-x64" & goto MENU
if /i "%CHOICE%"=="L" set "RID=linux-x64" & goto MENU
if /i "%CHOICE%"=="M" set "RID=osx-x64" & goto MENU
if /i "%CHOICE%"=="A" set "RID=osx-arm64" & goto MENU

if /i "%CHOICE%"=="B" goto ACTION_BUILD
if /i "%CHOICE%"=="T" goto ACTION_TEST
if /i "%CHOICE%"=="P" goto ACTION_PUBLISH
if /i "%CHOICE%"=="X" exit /b 0

goto MENU

:SELECT_RID
echo.
echo Enter target RID (e.g. win-x64, linux-x64, osx-x64, osx-arm64, linux-arm64):
set /p USER_RID="RID: "
if not "!USER_RID!"=="" set "RID=!USER_RID!"
goto MENU

:ACTION_BUILD
cls
call :DO_BUILD
pause
goto MENU

:ACTION_TEST
cls
call :DO_TEST
pause
goto MENU

:ACTION_PUBLISH
cls
call :DO_PUBLISH
pause
goto MENU

:CLI_BUILD
call :DO_BUILD
exit /b %errorlevel%

:CLI_PUBLISH
call :DO_PUBLISH
exit /b %errorlevel%

:CLI_TEST
call :DO_TEST
exit /b %errorlevel%

:DO_BUILD
echo Building solution (dotnet build)...
dotnet build YTCDB.sln -c !CONFIG!
exit /b %errorlevel%

:DO_TEST
echo Running tests (dotnet test)...
dotnet test YTCDB.sln -c !CONFIG!
exit /b %errorlevel%

:DO_PUBLISH
echo Publishing application for !RID!...
set "CMD=dotnet publish Bot/Bot.csproj -c !CONFIG! -r !RID! --self-contained !SELF_CONTAINED! -o ./publish/!RID!"
if "!SINGLE_FILE!"=="true" set "CMD=!CMD! /p:PublishSingleFile=true"
if "!READY_TO_RUN!"=="true" set "CMD=!CMD! /p:PublishReadyToRun=true"

echo Executing: !CMD!
echo.
!CMD!
exit /b %errorlevel%