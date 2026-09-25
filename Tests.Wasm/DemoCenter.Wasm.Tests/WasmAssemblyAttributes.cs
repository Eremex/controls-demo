using DemoCenter.Wasm.Tests;

// Single-threaded wasm: parallel execution is neither present nor possible.
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = true, MaxParallelThreads = 1)]

// WasmTestFramework supplies a non-empty assembly path, without which every test fails in
// the browser: Assembly.Location is empty under wasm, see WasmTestAssembly.
[assembly: global::Xunit.TestFramework(typeof(WasmTestFramework))]
