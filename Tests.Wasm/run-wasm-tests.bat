@echo off
rem Run the DemoCenter test suite inside WebAssembly (Windows).
rem
rem Publishes the browser test host, serves it locally, drives headless Chromium and
rem writes a JUnit report into TestResults\.
rem
rem Release_WASM is not a stylistic choice: only that configuration excludes the 3D
rem pages and their dependencies and defines DEMO_WEB, so it is the one the browser
rem actually ships. Tests asserting "3D is not here" are meaningless anywhere else.
rem
rem Usage:
rem   run-wasm-tests.bat
rem
rem Environment variables:
rem   WASM_TEST_PORT                 local static server port (default 5058)
rem   WASM_TEST_TIMEOUT              run timeout in seconds (default 600)
rem   WASM_TEST_RESULTS              report output directory (default .\TestResults)
rem   WASM_TEST_HEADED               non-empty - show the browser window

setlocal

set "CONFIGURATION=Release_WASM"

if "%WASM_TEST_PORT%"=="" set "WASM_TEST_PORT=5058"
if "%WASM_TEST_TIMEOUT%"=="" set "WASM_TEST_TIMEOUT=600"
if "%WASM_TEST_RESULTS%"=="" set "WASM_TEST_RESULTS=.\TestResults"

set "ROOT=%~dp0"
set "HOST_PROJECT=%ROOT%DemoCenter.Wasm.TestHost\DemoCenter.Wasm.TestHost.csproj"
set "RUNNER_PROJECT=%ROOT%DemoCenter.Wasm.TestRunner\DemoCenter.Wasm.TestRunner.csproj"
set "WWWROOT=%ROOT%DemoCenter.Wasm.TestHost\bin\%CONFIGURATION%\net10.0-browser\publish\wwwroot"

echo ==^> Publishing the browser test host
dotnet publish "%HOST_PROJECT%" -c %CONFIGURATION% -p:NoWarn=NU1900
if errorlevel 1 exit /b %errorlevel%

rem Restores the chromium.win-x64 nuget package too (see the csproj) - that is the
rem browser Chromium.Path in Program.cs resolves. No separate Playwright browser
rem install is needed anymore.
echo ==^> Restoring nuget packages
dotnet restore "%RUNNER_PROJECT%"
if errorlevel 1 exit /b %errorlevel%

echo ==^> Building the runner
dotnet build "%RUNNER_PROJECT%" -p:NoWarn=NU1900 --no-restore
if errorlevel 1 exit /b %errorlevel%

set "HEADED="
if not "%WASM_TEST_HEADED%"=="" set "HEADED=--headed"

echo ==^> Running the tests in the browser
dotnet run --project "%RUNNER_PROJECT%" --no-build -- ^
  --wwwroot "%WWWROOT%" ^
  --port %WASM_TEST_PORT% ^
  --timeout-seconds %WASM_TEST_TIMEOUT% ^
  --results "%WASM_TEST_RESULTS%" %HEADED%

rem Runner exit code is the number of failed tests, capped at 252. Special outcomes
rem take the top of the range so they cannot be mistaken for a failure count:
rem 2 - nothing published, 253 - timeout, 254 - the .NET runtime died,
rem 255 - the host itself crashed.
exit /b %errorlevel%
