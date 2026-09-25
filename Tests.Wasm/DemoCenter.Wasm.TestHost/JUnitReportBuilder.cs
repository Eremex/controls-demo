using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using Xunit.Sdk;

namespace DemoCenter.Wasm.TestHost;

/// <summary>
/// An xunit message sink that prints run progress to <see cref="Console"/> (which is
/// devtools in the browser, and the page console event for the external driver) while
/// accumulating the results into JUnit-compatible XML.
/// </summary>
/// <remarks>
/// The format deliberately matches what <c>JunitXml.TestLogger</c> produces on desktop
/// runs: GitLab picks it up with the very same <c>./**/*test-result.xml</c> glob from the
/// existing <c>reports: junit:</c>.
/// </remarks>
public sealed class JUnitReportBuilder : IMessageSink
{
	private readonly string assemblyName;
	private readonly Dictionary<string, string> classNamesByUniqueID = [];
	private readonly Dictionary<string, TestInfo> testsByUniqueID = [];
	private readonly List<TestOutcome> outcomes = [];

	/// <summary>How many finished tests between progress marks.</summary>
	private const int ProgressStep = 250;

	public JUnitReportBuilder(string assemblyName) => this.assemblyName = assemblyName;

	public int Passed { get; private set; }

	public int Failed { get; private set; }

	public int Skipped { get; private set; }

	public decimal TotalTime { get; private set; }

	public bool OnMessage(IMessageSinkMessage message)
	{
		switch (message)
		{
			case ITestClassStarting testClassStarting when testClassStarting.TestClassUniqueID is { } classUniqueID:
				classNamesByUniqueID[classUniqueID] = testClassStarting.TestClassName;
				break;

			case ITestStarting testStarting:
				testsByUniqueID[testStarting.TestUniqueID] = new TestInfo(
					testStarting.TestDisplayName,
					ResolveClassName(testStarting.TestClassUniqueID));
				break;

			case ITestPassed testPassed:
				Passed++;
				Record(testPassed.TestUniqueID, testPassed.ExecutionTime, TestStatus.Passed, null, null);
				break;

			case ITestFailed testFailed:
				Failed++;
				Record(
					testFailed.TestUniqueID,
					testFailed.ExecutionTime,
					TestStatus.Failed,
					FormatExceptionMessage(testFailed),
					string.Join(Environment.NewLine, testFailed.StackTraces.Where(x => x is not null)));
				break;

			case ITestSkipped testSkipped:
				Skipped++;
				Record(testSkipped.TestUniqueID, testSkipped.ExecutionTime, TestStatus.Skipped, testSkipped.Reason, null);
				break;

			case IErrorMessage errorMessage:
				// A catastrophic runner failure, not a failing test. Printed with the stack:
				// without it there is nothing to diagnose from in the browser.
				Console.WriteLine("[emx][runner-error] " + string.Join(" | ", errorMessage.Messages));
				foreach (var stackTrace in errorMessage.StackTraces)
				{
					Console.WriteLine(stackTrace);
				}

				break;

			case ITestNotRun testNotRun:
				Skipped++;
				Record(testNotRun.TestUniqueID, testNotRun.ExecutionTime, TestStatus.Skipped, "Test was not run (explicit)", null);
				break;
		}

		return true;
	}

	public string ToJUnitXml() => Combine([this]);

	/// <summary>Merges the reports of several assemblies into one <c>testsuites</c> document.</summary>
	public static string Combine(IReadOnlyCollection<JUnitReportBuilder> reports)
	{
		var document = new XDocument(new XElement("testsuites", reports.Select(x => x.ToTestSuiteElement())));

		var builder = new StringBuilder();

		// An XmlWriter over a StringBuilder always declares utf-16; the string leaves through
		// JS interop and is saved by the driver as UTF-8, so the declaration is written by
		// hand.
		builder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
		using (var writer = XmlWriter.Create(builder, new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true }))
		{
			document.Save(writer);
		}

		return builder.ToString();
	}

	private XElement ToTestSuiteElement()
	{
		var testSuite = new XElement(
			"testsuite",
			new XAttribute("name", assemblyName),
			new XAttribute("tests", outcomes.Count),
			new XAttribute("failures", Failed),
			new XAttribute("skipped", Skipped),
			new XAttribute("errors", 0),
			new XAttribute("time", TotalTime.ToString(CultureInfo.InvariantCulture)),
			new XAttribute("hostname", "browser-wasm"));

		foreach (var outcome in outcomes)
		{
			// xunit includes the class name in TestCaseDisplayName; JUnit already carries it
			// in classname, so name keeps only the method with its arguments.
			var shortName = outcome.DisplayName.StartsWith(outcome.ClassName + ".", StringComparison.Ordinal)
				? outcome.DisplayName[(outcome.ClassName.Length + 1)..]
				: outcome.DisplayName;

			var testCase = new XElement(
				"testcase",
				new XAttribute("name", shortName),
				new XAttribute("classname", outcome.ClassName),
				new XAttribute("time", outcome.ExecutionTime.ToString(CultureInfo.InvariantCulture)));

			switch (outcome.Status)
			{
				case TestStatus.Failed:
					testCase.Add(new XElement(
						"failure",
						new XAttribute("message", Sanitize(outcome.Message ?? "Test failed")),
						new XCData(Sanitize(outcome.StackTrace ?? string.Empty))));
					break;

				case TestStatus.Skipped:
					testCase.Add(new XElement("skipped", new XAttribute("message", Sanitize(outcome.Message ?? string.Empty))));
					break;
			}

			testSuite.Add(testCase);
		}

		return testSuite;
	}

	private void Record(string testUniqueID, decimal executionTime, TestStatus status, string? message, string? stackTrace)
	{
		var info = testsByUniqueID.TryGetValue(testUniqueID, out var known)
			? known
			: new TestInfo(testUniqueID, assemblyName);

		TotalTime += executionTime;
		outcomes.Add(new TestOutcome(info.DisplayName, info.ClassName, executionTime, status, message, stackTrace));

		// Only failures go to the console: with more than two thousand tests, a line per
		// green one is several thousand lines in which the failures get lost. The full list
		// of everything that ran stays in the JUnit report.
		if (status == TestStatus.Failed)
		{
			// The xunit DisplayName already contains the class name; it is not repeated here.
			Console.WriteLine($"[FAIL] {info.DisplayName}");
			Console.WriteLine($"       {message}");
			if (!string.IsNullOrWhiteSpace(stackTrace))
			{
				Console.WriteLine(stackTrace);
			}

			return;
		}

		// A sparse progress mark: without it a fifteen-minute run looks hung.
		//
		// There is deliberately no total here. Discovery yields test CASES, while what is
		// counted are tests: every data row of a [Theory] is a separate test inside one
		// case, so the final number is always larger, and a denominator taken from discovery
		// would only mislead (in the Controls.Tests run that was 2354 tests against 1509
		// cases).
		if (outcomes.Count % ProgressStep == 0)
		{
			Console.WriteLine($"[emx] tests run: {outcomes.Count}, failed: {Failed}");
		}
	}

	private string ResolveClassName(string? testClassUniqueID) =>
		testClassUniqueID is not null && classNamesByUniqueID.TryGetValue(testClassUniqueID, out var className)
			? className
			: assemblyName;

	private static string FormatExceptionMessage(ITestFailed testFailed)
	{
		var types = testFailed.ExceptionTypes;
		var messages = testFailed.Messages;
		var parts = new List<string>(messages.Length);
		for (var i = 0; i < messages.Length; i++)
		{
			var type = i < types.Length ? types[i] : null;
			parts.Add(type is null ? messages[i] : $"{type}: {messages[i]}");
		}

		return string.Join(Environment.NewLine + "---- ", parts);
	}

	/// <summary>Strips characters invalid in XML 1.0; otherwise GitLab rejects the whole report.</summary>
	private static string Sanitize(string value)
	{
		var builder = new StringBuilder(value.Length);
		foreach (var symbol in value)
		{
			if (XmlConvert.IsXmlChar(symbol))
			{
				builder.Append(symbol);
			}
		}

		return builder.ToString();
	}

	private enum TestStatus
	{
		Passed,
		Failed,
		Skipped,
	}

	private sealed record TestInfo(string DisplayName, string ClassName);

	private sealed record TestOutcome(
		string DisplayName,
		string ClassName,
		decimal ExecutionTime,
		TestStatus Status,
		string? Message,
		string? StackTrace);
}
