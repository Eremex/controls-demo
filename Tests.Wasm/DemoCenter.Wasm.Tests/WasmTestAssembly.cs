using System.Reflection;

using Xunit.Sdk;
using Xunit.v3;

namespace DemoCenter.Wasm.Tests;

/// <summary>
/// A decorator over <see cref="XunitTestAssembly"/> that supplies a synthetic
/// <see cref="AssemblyPath"/>.
/// </summary>
/// <remarks>
/// <para>
/// In browser-wasm <c>Assembly.Location</c> is always empty. xunit derives the name from
/// it: <c>TestAssemblyStarting.AssemblyName = Path.GetFileNameWithoutExtension(AssemblyPath)</c>,
/// and an empty value trips <c>Guard.ArgumentNotNullOrEmpty</c> - every single test fails
/// before it gets to assert anything.
/// </para>
/// <para>
/// The property cannot be replaced by inheritance: it is neither virtual nor settable.
/// The discoverer and the executor do accept the <see cref="IXunitTestAssembly"/>
/// interface, though, so the real instance is wrapped and exactly one property is
/// overridden. A path like <c>/Assembly.Name.dll</c> does not exist on any file system,
/// but xunit never goes there: the value is only used as an identifier in reports.
/// </para>
/// </remarks>
public sealed class WasmTestAssembly(Assembly assembly) : IXunitTestAssembly
{
	private readonly XunitTestAssembly inner = new(assembly);

	public string AssemblyPath { get; } = "/" + (assembly.GetName().Name ?? "UnknownAssembly") + ".dll";

	public Assembly Assembly => inner.Assembly;

	public string AssemblyName => inner.AssemblyName;

	public IReadOnlyCollection<Type> AssemblyFixtureTypes => inner.AssemblyFixtureTypes;

	public IReadOnlyCollection<IBeforeAfterTestAttribute> BeforeAfterTestAttributes => inner.BeforeAfterTestAttributes;

	public ICollectionBehaviorAttribute? CollectionBehavior => inner.CollectionBehavior;

	public IReadOnlyDictionary<string, (Type Type, CollectionDefinitionAttribute Attribute)> CollectionDefinitions =>
		inner.CollectionDefinitions;

	public string? ConfigFilePath => inner.ConfigFilePath;

	public Guid ModuleVersionID => inner.ModuleVersionID;

	public string TargetFramework => inner.TargetFramework;

	public ITestCaseOrderer? TestCaseOrderer => inner.TestCaseOrderer;

	public ITestCollectionOrderer? TestCollectionOrderer => inner.TestCollectionOrderer;

	public IReadOnlyDictionary<string, IReadOnlyCollection<string>> Traits => inner.Traits;

	public string UniqueID => inner.UniqueID;

	public Version Version => inner.Version;
}
