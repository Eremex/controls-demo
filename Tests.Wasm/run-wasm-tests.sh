#!/usr/bin/env bash
# Run the DemoCenter test suite inside WebAssembly (Linux/macOS).
#
# Release_WASM is not a stylistic choice: only that configuration excludes the 3D pages
# and their dependencies and defines DEMO_WEB, so it is the one the browser actually
# ships. Tests asserting "3D is not here" are meaningless anywhere else.
set -euo pipefail

CONFIGURATION=Release_WASM
PORT="${WASM_TEST_PORT:-5058}"
TIMEOUT="${WASM_TEST_TIMEOUT:-600}"
RESULTS="${WASM_TEST_RESULTS:-./TestResults}"

ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
HOST_PROJECT="$ROOT/DemoCenter.Wasm.TestHost/DemoCenter.Wasm.TestHost.csproj"
RUNNER_PROJECT="$ROOT/DemoCenter.Wasm.TestRunner/DemoCenter.Wasm.TestRunner.csproj"
WWWROOT="$ROOT/DemoCenter.Wasm.TestHost/bin/$CONFIGURATION/net10.0-browser/publish/wwwroot"

echo "==> Publishing the browser test host"
dotnet publish "$HOST_PROJECT" -c "$CONFIGURATION" -p:NoWarn=NU1900

# Restores the chromium.linux-x64 nuget package too (see the csproj) - that is the
# browser Chromium.Path in Program.cs resolves. No separate Playwright browser
# install is needed anymore, and nothing is downloaded from Playwright's CDN.
echo "==> Restoring nuget packages"
dotnet restore "$RUNNER_PROJECT"

echo "==> Building the runner"
dotnet build "$RUNNER_PROJECT" -p:NoWarn=NU1900 --no-restore

HEADED=""
[ -n "${WASM_TEST_HEADED:-}" ] && HEADED="--headed"

echo "==> Running the tests in the browser"
dotnet run --project "$RUNNER_PROJECT" --no-build -- \
  --wwwroot "$WWWROOT" \
  --port "$PORT" \
  --timeout-seconds "$TIMEOUT" \
  --results "$RESULTS" $HEADED
