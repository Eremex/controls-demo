using System.Reflection;

using Xunit.Sdk;
using Xunit.v3;

namespace DemoCenter.Wasm.Tests;

/// <summary>
/// The test framework used in the browser.
/// </summary>
/// <remarks>
/// It differs from the standard one in exactly one way: the discoverer and the executor
/// are given a <see cref="WasmTestAssembly"/> instead of an <see cref="XunitTestAssembly"/>
/// - see the comment there about the empty <c>Assembly.Location</c> under wasm.
/// </remarks>
public sealed class WasmTestFramework : XunitTestFramework
{
	protected override ITestFrameworkDiscoverer CreateDiscoverer(Assembly assembly) =>
		new XunitTestFrameworkDiscoverer(new WasmTestAssembly(assembly));

	protected override ITestFrameworkExecutor CreateExecutor(Assembly assembly) =>
		new XunitTestFrameworkExecutor(new WasmTestAssembly(assembly));
}
