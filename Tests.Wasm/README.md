# Demo tests under WebAssembly

Runs DemoCenter in a real browser runtime: opens every demo module in turn and checks that
none of them fails, then checks that the Resources links navigate. Nothing under
`DemoCenter/` is modified — the tests reference it as an ordinary project.

## Running

```
run-wasm-tests.bat
```

or, on Linux/macOS:

```
./run-wasm-tests.sh
```

The browser itself is not downloaded or installed as a separate step: `DemoCenter.Wasm.TestRunner`
references the `chromium` / `chromium.linux-x64` / `chromium.win-x64` nuget packages
([devlooped/chromium](https://github.com/devlooped/chromium)), an ordinary `dotnet restore`
puts the native binary in the NuGet cache, and `Chromium.Path` in `Program.cs` finds it there
for the current platform at startup. Playwright drives that binary through `ExecutablePath`
instead of its own cache under `~/.cache/ms-playwright` / `%LOCALAPPDATA%\ms-playwright`.

If the matching `chromium.<rid>` package is not referenced or does not support the current
platform, `Chromium.Path` returns `null`, the runner prints a warning and launches Playwright
without `ExecutablePath` — then the browser has to be installed by hand beforehand via
`playwright install chromium`. System libraries that Chromium needs on slim Linux images
(libnss3 and the like) are not installed by these scripts — that is the CI image's job, not
the test run's.

The JUnit report is written to `TestResults/DemoCenter.Wasm-test-result.xml`. The exit code
is the number of failed tests (capped at 252). The special outcomes take the top of the
range so they cannot be mistaken for a failure count: `2` — nothing to run (the host is not
published), `253` — timeout, `254` — the .NET runtime died, `255` — the host itself crashed.

## Why Release_WASM rather than Debug

Only that configuration builds the demo the way the browser sees it: `DEMO_WEB` is defined,
and the 3D pages and their dependencies (Controls3D, Silk.NET, Assimp, SharpDX.Direct3D11)
are excluded. In `Debug` the 3D pages are present, and the "there is no 3D on the web" test
would be checking something other than what ships. That is why the run scripts do not ask
for a configuration.

Trimming is on here too, again to match what ships: the trimmer removes code that static
analysis could not reach, and that only breaks at run time — exactly the class of breakage
this run exists to catch.

## What it is made of

| Project | What it does |
| --- | --- |
| `DemoCenter.Wasm.Tests` | The tests themselves. `net10.0-browser`, xunit v3. |
| `DemoCenter.Wasm.TestHost` | The browser application: starts DemoCenter and runs the tests inside wasm. |
| `DemoCenter.Wasm.TestRunner` | The desktop driver: serves the static files, drives headless Chromium, collects the report. |

## What had to be done differently

**Our own `Main` instead of the stock runner.** Microsoft.Testing.Platform does not work in
browser-wasm: it looks for a JSON config on the file system next to the assembly
([testfx#2196](https://github.com/microsoft/testfx/issues/2196)). The stock
`ProjectAssemblyRunner.Run` is no good either — internally it spins up an
`InProcessFrontController`, which derives the assembly name from `Assembly.Location`, empty
in the browser. So the run goes straight through `ITestFramework`, discoverer and executor.

**`WasmTestAssembly`.** For the same reason — an empty `Assembly.Location` — every test used
to fail on `TestAssemblyStarting.AssemblyName`. A decorator over `XunitTestAssembly` supplies
a non-empty path and delegates everything else.

**`SynchronousMessageReporting`.** Without it xunit starts a separate thread to deliver
messages (`MessageBus..ctor` → `Thread.Start`), and in single-threaded wasm that is a
`PlatformNotSupportedException` before the first test.

**xunit itself is a trimmer root.** It creates its extensions through
`Activator.CreateInstance`, and the trimmer cannot see those constructors: without the root
the publish succeeds and the run fails with a `MissingMethodException` in
`CollectionPerAssemblyTestCollectionFactory`.

**Layout is computed explicitly rather than by waiting for a frame.** Avalonia's rendering in
the browser is driven by `requestAnimationFrame`, which does not fire in a background tab. A
test that waits for painting would simply hang in a headless run; `UpdateLayout()` builds the
visual tree deterministically. Where the thing under test *is* produced by rendering — the
`OverlayLayer` used for popups, for instance — `DemoApp.SettleFrames` waits for real frames
instead, and the mount point in the host page is kept visible.

**xunit v3, not v2 as in `Tests/DemoCenter.Desktop.UI.Tests`.** Not a matter of age: v2 has
no in-process runner that can be started in a browser. The desktop
`WindowsTests.ShowAllModules` was also not portable because it opens a `MainWindow`, and
there are no windows at all in the browser — the application lives in
`ISingleViewApplicationLifetime.MainView`.

## Known log noise

```
Failed to create render target for mode 3 : HTMLCanvasElement.getContext returned null.
Failed to create render target for mode 2 : HTMLCanvasElement.getContext returned null.
```

Avalonia probing graphics backends in a headless browser. Skia falls back to software
rendering and draws fine; this has no effect on the run.
