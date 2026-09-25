import { dotnet } from './_framework/dotnet.js'

const is_browser = typeof window != "undefined";
if (!is_browser) throw new Error(`Expected to be running in a browser`);

const log = document.getElementById('log');

function appendLine(text) {
    if (!log) return;
    log.textContent += text + '\n';
}

// Which assembly to run arrives in the query string: ?assembly=Assembly.Name
// The special value --list makes the host print the list of assemblies and run nothing.
// Each assembly is loaded by its own page - one runtime does not have enough wasm heap for
// all of them, and Mono aborts in gmem.c.
// skip/take select a slice of the tests inside an assembly; it is what makes bisecting for
// whatever kills the runtime possible.
const params = new URLSearchParams(location.search);
const requested = params.get('assembly');
const slice = requested ? [params.get('skip') ?? '0', params.get('take') ?? '-1'] : [];

// The external driver waits for the DONE: prefix in document.title, then reads
// globalThis.__emxTestResults (JUnit XML) and globalThis.__emxTestFailed.
function publish(xml, failed, names, hostError) {
    globalThis.__emxTestResults = xml;
    globalThis.__emxTestFailed = failed;
    globalThis.__emxAssemblies = names;
    if (hostError) globalThis.__emxHostError = hostError;
    document.title = 'DONE:' + failed;
    appendLine('');
    appendLine('=== DONE, failed=' + failed + ' ===');
}

// The runtime culture can ONLY be set before startup, through applicationCulture, and the
// same setting picks the ICU shard. The ?culture= parameter makes it possible to run the
// tests under a different culture and watch the set of failures change (confirmed: under
// ja-JP the Japanese tests go green and the Russian ones go red).
const probeCulture = params.get('culture');
let builder = dotnet.withDiagnosticTracing(false);
if (probeCulture) builder = builder.withConfig({ applicationCulture: probeCulture });
const dotnetRuntime = await builder.create();

const config = dotnetRuntime.getConfig();

try {
    await dotnetRuntime.runMain(config.mainAssemblyName, requested ? [requested, ...slice] : []);

    // The result is read after Main returns, through the assembly exports: this avoids
    // JSHost.ImportAsync, which resolves an ES module path relative to _framework/.
    const exports = await dotnetRuntime.getAssemblyExports(config.mainAssemblyName);
    const program = exports.DemoCenter.Wasm.TestHost.Program;
    publish(program.GetResultsXml(), program.GetFailedCount(), program.GetAssemblyNames());
} catch (error) {
    // A failure of the host itself must be distinguishable from red tests: failed = -1.
    publish('', -1, '', String(error && error.stack ? error.stack : error));
    appendLine('=== HOST ERROR ===');
    appendLine(globalThis.__emxHostError);
    throw error;
}
