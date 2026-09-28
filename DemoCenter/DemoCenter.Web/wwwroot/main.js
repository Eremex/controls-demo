import { dotnet } from './_framework/dotnet.js'

const is_browser = typeof window != "undefined";
if (!is_browser) throw new Error(`Expected to be running in a browser`);

// Loading progress.
//
// The splash screen used to show only "Loading ...", and the application takes a while to
// start: the browser downloads several hundred runtime files, with no way to tell whether
// anything was happening at all.
//
// Bytes are counted rather than files: file sizes differ by two orders of magnitude, so
// their count says nothing about how much is left. The total is not known up front, so it
// accumulates from Content-Length as responses arrive; until the first ones land, only the
// downloaded amount is shown.
const progressBar = document.getElementById('loading-progress-bar');
const progressText = document.getElementById('loading-progress-text');

let loadedBytes = 0;
let expectedBytes = 0;
let filesDone = 0;

const formatMB = bytes => (bytes / 1048576).toFixed(1) + ' MB';

function renderProgress() {
    if (!progressText) return;

    if (expectedBytes > 0) {
        const ratio = Math.min(loadedBytes / expectedBytes, 1);
        if (progressBar) progressBar.style.width = (ratio * 100).toFixed(1) + '%';
        progressText.textContent = `${formatMB(loadedBytes)} of ${formatMB(expectedBytes)} · files: ${filesDone}`;
    } else {
        progressText.textContent = `${formatMB(loadedBytes)} · files: ${filesDone}`;
    }
}

// fetch is wrapped before the runtime starts: it exposes no progress event of its own,
// and every request to _framework has to be intercepted.
const originalFetch = globalThis.fetch;
globalThis.fetch = async function (input, init) {
    const url = typeof input === 'string' ? input : (input && input.url) || '';
    const isRuntimeAsset = url.includes('/_framework/');

    const response = await originalFetch(input, init);
    if (!isRuntimeAsset || !response.ok) return response;

    expectedBytes += Number(response.headers.get('content-length')) || 0;

    // Count on a clone so the stream handed to the runtime is left untouched.
    const counted = response.clone();
    (async () => {
        try {
            const buffer = await counted.arrayBuffer();
            loadedBytes += buffer.byteLength;
            filesDone++;
            renderProgress();
        } catch { }
    })();

    return response;
};

renderProgress();

const dotnetRuntime = await dotnet
    .withDiagnosticTracing(false)
    .withApplicationArgumentsFromQuery()
    .create();

// The runtime is up: nothing left to download, but the window will not appear instantly -
// report the current stage so the pause does not look like a hang.
globalThis.fetch = originalFetch;
if (progressBar) progressBar.style.width = '100%';
if (progressText) progressText.textContent = 'starting the application…';

const config = dotnetRuntime.getConfig();

await dotnetRuntime.runMain(config.mainAssemblyName, [window.location.search]);
