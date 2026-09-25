"""Local static server for DemoCenter.Wasm.TestHost.

Needed only because the browser requires the application/wasm MIME type for wasm, and the
standard http.server does not know it. Usage:

    python Tests.Wasm/serve.py [port] [directory]
"""

import functools
import http.server
import os
import socketserver
import sys

DEFAULT_PORT = 5058
DEFAULT_ROOT = os.path.join(
    os.path.dirname(os.path.abspath(__file__)),
    "DemoCenter.Wasm.TestHost", "bin", "Release_WASM", "net10.0-browser", "publish", "wwwroot",
)


class Handler(http.server.SimpleHTTPRequestHandler):
    extensions_map = {
        **http.server.SimpleHTTPRequestHandler.extensions_map,
        ".wasm": "application/wasm",
        ".js": "text/javascript",
        ".mjs": "text/javascript",
        ".json": "application/json",
        ".dat": "application/octet-stream",
        ".blat": "application/octet-stream",
        ".pdb": "application/octet-stream",
        ".dll": "application/octet-stream",
    }

    def end_headers(self):
        # Headers for multi-threaded wasm: needed if WasmEnableThreads=true is ever
        # required. Harmless for a single-threaded build.
        self.send_header("Cross-Origin-Opener-Policy", "same-origin")
        self.send_header("Cross-Origin-Embedder-Policy", "require-corp")
        self.send_header("Cache-Control", "no-store")
        super().end_headers()

    def log_message(self, format, *args):
        pass


def main():
    port = int(sys.argv[1]) if len(sys.argv) > 1 else DEFAULT_PORT
    root = sys.argv[2] if len(sys.argv) > 2 else DEFAULT_ROOT
    if not os.path.isdir(root):
        raise SystemExit(f"Directory not found: {root}\nRun dotnet publish first.")

    socketserver.TCPServer.allow_reuse_address = True
    with socketserver.TCPServer(("127.0.0.1", port), functools.partial(Handler, directory=root)) as httpd:
        print(f"http://127.0.0.1:{port}/  ->  {root}", flush=True)
        httpd.serve_forever()


if __name__ == "__main__":
    main()
