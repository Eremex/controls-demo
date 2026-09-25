// Every test is marshalled onto the single Avalonia UI thread by UiThreadTestRunner, so nothing
// here runs in parallel anyway - but xunit may still interleave two tests at an await, which lets
// one test open a window while another is driving the shared one. macOS is the strictest about
// this: windows belong to the process main thread.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
