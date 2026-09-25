//! Licensed to the .NET Foundation under one or more agreements.
//! The .NET Foundation licenses this file to you under the MIT license.

var e=!1;const t=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,4,1,96,0,0,3,2,1,0,10,8,1,6,0,6,64,25,11,11])),o=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,15,1,13,0,65,1,253,15,65,2,253,15,253,128,2,11])),n=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,10,1,8,0,65,0,253,15,253,98,11])),r=Symbol.for("wasm promise_control");function i(e,t){let o=null;const n=new Promise((function(n,r){o={isDone:!1,promise:null,resolve:t=>{o.isDone||(o.isDone=!0,n(t),e&&e())},reject:e=>{o.isDone||(o.isDone=!0,r(e),t&&t())}}}));o.promise=n;const i=n;return i[r]=o,{promise:i,promise_control:o}}function s(e){return e[r]}function a(e){e&&function(e){return void 0!==e[r]}(e)||Be(!1,"Promise is not controllable")}const l="__mono_message__",c=["debug","log","trace","warn","info","error"],d="MONO_WASM: ";let u,f,m,g,p,h;function w(e){g=e}function b(e){if(Pe.diagnosticTracing){const t="function"==typeof e?e():e;console.debug(d+t)}}function y(e,...t){console.info(d+e,...t)}function v(e,...t){console.info(e,...t)}function E(e,...t){console.warn(d+e,...t)}function _(e,...t){if(t&&t.length>0&&t[0]&&"object"==typeof t[0]){if(t[0].silent)return;if(t[0].toString)return void console.error(d+e,t[0].toString())}console.error(d+e,...t)}function x(e,t,o){return function(...n){try{let r=n[0];if(void 0===r)r="undefined";else if(null===r)r="null";else if("function"==typeof r)r=r.toString();else if("string"!=typeof r)try{r=JSON.stringify(r)}catch(e){r=r.toString()}t(o?JSON.stringify({method:e,payload:r,arguments:n.slice(1)}):[e+r,...n.slice(1)])}catch(e){m.error(`proxyConsole failed: ${e}`)}}}function j(e,t,o){f=t,g=e,m={...t};const n=`${o}/console`.replace("https://","wss://").replace("http://","ws://");u=new WebSocket(n),u.addEventListener("error",A),u.addEventListener("close",S),function(){for(const e of c)f[e]=x(`console.${e}`,T,!0)}()}function R(e){let t=30;const o=()=>{u?0==u.bufferedAmount||0==t?(e&&v(e),function(){for(const e of c)f[e]=x(`console.${e}`,m.log,!1)}(),u.removeEventListener("error",A),u.removeEventListener("close",S),u.close(1e3,e),u=void 0):(t--,globalThis.setTimeout(o,100)):e&&m&&m.log(e)};o()}function T(e){u&&u.readyState===WebSocket.OPEN?u.send(e):m.log(e)}function A(e){m.error(`[${g}] proxy console websocket error: ${e}`,e)}function S(e){m.debug(`[${g}] proxy console websocket closed: ${e}`,e)}function D(){Pe.preferredIcuAsset=O(Pe.config);let e="invariant"==Pe.config.globalizationMode;if(!e)if(Pe.preferredIcuAsset)Pe.diagnosticTracing&&b("ICU data archive(s) available, disabling invariant mode");else{if("custom"===Pe.config.globalizationMode||"all"===Pe.config.globalizationMode||"sharded"===Pe.config.globalizationMode){const e="invariant globalization mode is inactive and no ICU data archives are available";throw _(`ERROR: ${e}`),new Error(e)}Pe.diagnosticTracing&&b("ICU data archive(s) not available, using invariant globalization mode"),e=!0,Pe.preferredIcuAsset=null}const t="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT",o=Pe.config.environmentVariables;if(void 0===o[t]&&e&&(o[t]="1"),void 0===o.TZ)try{const e=Intl.DateTimeFormat().resolvedOptions().timeZone||null;e&&(o.TZ=e)}catch(e){y("failed to detect timezone, will fallback to UTC")}}function O(e){var t;if((null===(t=e.resources)||void 0===t?void 0:t.icu)&&"invariant"!=e.globalizationMode){const t=e.applicationCulture||(ke?globalThis.navigator&&globalThis.navigator.languages&&globalThis.navigator.languages[0]:Intl.DateTimeFormat().resolvedOptions().locale),o=e.resources.icu;let n=null;if("custom"===e.globalizationMode){if(o.length>=1)return o[0].name}else t&&"all"!==e.globalizationMode?"sharded"===e.globalizationMode&&(n=function(e){const t=e.split("-")[0];return"en"===t||["fr","fr-FR","it","it-IT","de","de-DE","es","es-ES"].includes(e)?"icudt_EFIGS.dat":["zh","ko","ja"].includes(t)?"icudt_CJK.dat":"icudt_no_CJK.dat"}(t)):n="icudt.dat";if(n)for(let e=0;e<o.length;e++){const t=o[e];if(t.virtualPath===n)return t.name}}return e.globalizationMode="invariant",null}(new Date).valueOf();const C=class{constructor(e){this.url=e}toString(){return this.url}};async function k(e,t){try{const o="function"==typeof globalThis.fetch;if(Se){const n=e.startsWith("file://");if(!n&&o)return globalThis.fetch(e,t||{credentials:"same-origin"});p||(h=Ne.require("url"),p=Ne.require("fs")),n&&(e=h.fileURLToPath(e));const r=await p.promises.readFile(e);return{ok:!0,headers:{length:0,get:()=>null},url:e,arrayBuffer:()=>r,json:()=>JSON.parse(r),text:()=>{throw new Error("NotImplementedException")}}}if(o)return globalThis.fetch(e,t||{credentials:"same-origin"});if("function"==typeof read)return{ok:!0,url:e,headers:{length:0,get:()=>null},arrayBuffer:()=>new Uint8Array(read(e,"binary")),json:()=>JSON.parse(read(e,"utf8")),text:()=>read(e,"utf8")}}catch(t){return{ok:!1,url:e,status:500,headers:{length:0,get:()=>null},statusText:"ERR28: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t},text:()=>{throw t}}}throw new Error("No fetch implementation available")}function I(e){return"string"!=typeof e&&Be(!1,"url must be a string"),!M(e)&&0!==e.indexOf("./")&&0!==e.indexOf("../")&&globalThis.URL&&globalThis.document&&globalThis.document.baseURI&&(e=new URL(e,globalThis.document.baseURI).toString()),e}const U=/^[a-zA-Z][a-zA-Z\d+\-.]*?:\/\//,P=/[a-zA-Z]:[\\/]/;function M(e){return Se||Ie?e.startsWith("/")||e.startsWith("\\")||-1!==e.indexOf("///")||P.test(e):U.test(e)}let L,N=0;const $=[],z=[],W=new Map,F={"js-module-threads":!0,"js-module-runtime":!0,"js-module-dotnet":!0,"js-module-native":!0,"js-module-diagnostics":!0},B={...F,"js-module-library-initializer":!0},V={...F,dotnetwasm:!0,heap:!0,manifest:!0},q={...B,manifest:!0},H={...B,dotnetwasm:!0},J={dotnetwasm:!0,symbols:!0},Z={...B,dotnetwasm:!0,symbols:!0},Q={symbols:!0};function G(e){return!("icu"==e.behavior&&e.name!=Pe.preferredIcuAsset)}function K(e,t,o){null!=t||(t=[]),Be(1==t.length,`Expect to have one ${o} asset in resources`);const n=t[0];return n.behavior=o,X(n),e.push(n),n}function X(e){V[e.behavior]&&W.set(e.behavior,e)}function Y(e){Be(V[e],`Unknown single asset behavior ${e}`);const t=W.get(e);if(t&&!t.resolvedUrl)if(t.resolvedUrl=Pe.locateFile(t.name),F[t.behavior]){const e=ge(t);e?("string"!=typeof e&&Be(!1,"loadBootResource response for 'dotnetjs' type should be a URL string"),t.resolvedUrl=e):t.resolvedUrl=ce(t.resolvedUrl,t.behavior)}else if("dotnetwasm"!==t.behavior)throw new Error(`Unknown single asset behavior ${e}`);return t}function ee(e){const t=Y(e);return Be(t,`Single asset for ${e} not found`),t}let te=!1;async function oe(){if(!te){te=!0,Pe.diagnosticTracing&&b("mono_download_assets");try{const e=[],t=[],o=(e,t)=>{!Z[e.behavior]&&G(e)&&Pe.expected_instantiated_assets_count++,!H[e.behavior]&&G(e)&&(Pe.expected_downloaded_assets_count++,t.push(se(e)))};for(const t of $)o(t,e);for(const e of z)o(e,t);Pe.allDownloadsQueued.promise_control.resolve(),Promise.all([...e,...t]).then((()=>{Pe.allDownloadsFinished.promise_control.resolve()})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e})),await Pe.runtimeModuleLoaded.promise;const n=async e=>{const t=await e;if(t.buffer){if(!Z[t.behavior]){t.buffer&&"object"==typeof t.buffer||Be(!1,"asset buffer must be array-like or buffer-like or promise of these"),"string"!=typeof t.resolvedUrl&&Be(!1,"resolvedUrl must be string");const e=t.resolvedUrl,o=await t.buffer,n=new Uint8Array(o);pe(t),await Ue.beforeOnRuntimeInitialized.promise,Ue.instantiate_asset(t,e,n)}}else J[t.behavior]?("symbols"===t.behavior&&(await Ue.instantiate_symbols_asset(t),pe(t)),J[t.behavior]&&++Pe.actual_downloaded_assets_count):(t.isOptional||Be(!1,"Expected asset to have the downloaded buffer"),!H[t.behavior]&&G(t)&&Pe.expected_downloaded_assets_count--,!Z[t.behavior]&&G(t)&&Pe.expected_instantiated_assets_count--)},r=[],i=[];for(const t of e)r.push(n(t));for(const e of t)i.push(n(e));Promise.all(r).then((()=>{Ce||Ue.coreAssetsInMemory.promise_control.resolve()})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e})),Promise.all(i).then((async()=>{Ce||(await Ue.coreAssetsInMemory.promise,Ue.allAssetsInMemory.promise_control.resolve())})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e}))}catch(e){throw Pe.err("Error in mono_download_assets: "+e),e}}}let ne=!1;function re(){if(ne)return;ne=!0;const e=Pe.config,t=[];if(e.assets)for(const t of e.assets)"object"!=typeof t&&Be(!1,`asset must be object, it was ${typeof t} : ${t}`),"string"!=typeof t.behavior&&Be(!1,"asset behavior must be known string"),"string"!=typeof t.name&&Be(!1,"asset name must be string"),t.resolvedUrl&&"string"!=typeof t.resolvedUrl&&Be(!1,"asset resolvedUrl could be string"),t.hash&&"string"!=typeof t.hash&&Be(!1,"asset resolvedUrl could be string"),t.pendingDownload&&"object"!=typeof t.pendingDownload&&Be(!1,"asset pendingDownload could be object"),t.isCore?$.push(t):z.push(t),X(t);else if(e.resources){const o=e.resources;o.wasmNative||Be(!1,"resources.wasmNative must be defined"),o.jsModuleNative||Be(!1,"resources.jsModuleNative must be defined"),o.jsModuleRuntime||Be(!1,"resources.jsModuleRuntime must be defined"),K(z,o.wasmNative,"dotnetwasm"),K(t,o.jsModuleNative,"js-module-native"),K(t,o.jsModuleRuntime,"js-module-runtime"),o.jsModuleDiagnostics&&K(t,o.jsModuleDiagnostics,"js-module-diagnostics");const n=(e,t,o)=>{const n=e;n.behavior=t,o?(n.isCore=!0,$.push(n)):z.push(n)};if(o.coreAssembly)for(let e=0;e<o.coreAssembly.length;e++)n(o.coreAssembly[e],"assembly",!0);if(o.assembly)for(let e=0;e<o.assembly.length;e++)n(o.assembly[e],"assembly",!o.coreAssembly);if(0!=e.debugLevel&&Pe.isDebuggingSupported()){if(o.corePdb)for(let e=0;e<o.corePdb.length;e++)n(o.corePdb[e],"pdb",!0);if(o.pdb)for(let e=0;e<o.pdb.length;e++)n(o.pdb[e],"pdb",!o.corePdb)}if(e.loadAllSatelliteResources&&o.satelliteResources)for(const e in o.satelliteResources)for(let t=0;t<o.satelliteResources[e].length;t++){const r=o.satelliteResources[e][t];r.culture=e,n(r,"resource",!o.coreAssembly)}if(o.coreVfs)for(let e=0;e<o.coreVfs.length;e++)n(o.coreVfs[e],"vfs",!0);if(o.vfs)for(let e=0;e<o.vfs.length;e++)n(o.vfs[e],"vfs",!o.coreVfs);const r=O(e);if(r&&o.icu)for(let e=0;e<o.icu.length;e++){const t=o.icu[e];t.name===r&&n(t,"icu",!1)}if(o.wasmSymbols)for(let e=0;e<o.wasmSymbols.length;e++)n(o.wasmSymbols[e],"symbols",!1)}if(e.appsettings)for(let t=0;t<e.appsettings.length;t++){const o=e.appsettings[t],n=he(o);"appsettings.json"!==n&&n!==`appsettings.${e.applicationEnvironment}.json`||z.push({name:o,behavior:"vfs",cache:"no-cache",useCredentials:!0})}e.assets=[...$,...z,...t]}async function ie(e){const t=await se(e);return await t.pendingDownloadInternal.response,t.buffer}async function se(e){try{return await ae(e)}catch(t){if(!Pe.enableDownloadRetry)throw t;if(Ie||Se)throw t;if(e.pendingDownload&&e.pendingDownloadInternal==e.pendingDownload)throw t;if(e.resolvedUrl&&-1!=e.resolvedUrl.indexOf("file://"))throw t;if(t&&404==t.status)throw t;e.pendingDownloadInternal=void 0,await Pe.allDownloadsQueued.promise;try{return Pe.diagnosticTracing&&b(`Retrying download '${e.name}'`),await ae(e)}catch(t){return e.pendingDownloadInternal=void 0,await new Promise((e=>globalThis.setTimeout(e,100))),Pe.diagnosticTracing&&b(`Retrying download (2) '${e.name}' after delay`),await ae(e)}}}async function ae(e){for(;L;)await L.promise;try{++N,N==Pe.maxParallelDownloads&&(Pe.diagnosticTracing&&b("Throttling further parallel downloads"),L=i());const t=await async function(e){if(e.pendingDownload&&(e.pendingDownloadInternal=e.pendingDownload),e.pendingDownloadInternal&&e.pendingDownloadInternal.response)return e.pendingDownloadInternal.response;if(e.buffer){const t=await e.buffer;return e.resolvedUrl||(e.resolvedUrl="undefined://"+e.name),e.pendingDownloadInternal={url:e.resolvedUrl,name:e.name,response:Promise.resolve({ok:!0,arrayBuffer:()=>t,json:()=>JSON.parse(new TextDecoder("utf-8").decode(t)),text:()=>{throw new Error("NotImplementedException")},headers:{get:()=>{}}})},e.pendingDownloadInternal.response}const t=e.loadRemote&&Pe.config.remoteSources?Pe.config.remoteSources:[""];let o;for(let n of t){n=n.trim(),"./"===n&&(n="");const t=le(e,n);e.name===t?Pe.diagnosticTracing&&b(`Attempting to download '${t}'`):Pe.diagnosticTracing&&b(`Attempting to download '${t}' for ${e.name}`);try{e.resolvedUrl=t;const n=fe(e);if(e.pendingDownloadInternal=n,o=await n.response,!o||!o.ok)continue;return o}catch(e){o||(o={ok:!1,url:t,status:0,statusText:""+e});continue}}const n=e.isOptional||e.name.match(/\.pdb$/)&&Pe.config.ignorePdbLoadErrors;if(o||Be(!1,`Response undefined ${e.name}`),!n){const t=new Error(`download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`);throw t.status=o.status,t}y(`optional download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`)}(e);return t?(J[e.behavior]||(e.buffer=await t.arrayBuffer(),++Pe.actual_downloaded_assets_count),e):e}finally{if(--N,L&&N==Pe.maxParallelDownloads-1){Pe.diagnosticTracing&&b("Resuming more parallel downloads");const e=L;L=void 0,e.promise_control.resolve()}}}function le(e,t){let o;return null==t&&Be(!1,`sourcePrefix must be provided for ${e.name}`),e.resolvedUrl?o=e.resolvedUrl:(o=""===t?"assembly"===e.behavior||"pdb"===e.behavior?e.name:"resource"===e.behavior&&e.culture&&""!==e.culture?`${e.culture}/${e.name}`:e.name:t+e.name,o=ce(Pe.locateFile(o),e.behavior)),o&&"string"==typeof o||Be(!1,"attemptUrl need to be path or url string"),o}function ce(e,t){return Pe.modulesUniqueQuery&&q[t]&&(e+=Pe.modulesUniqueQuery),e}let de=0;const ue=new Set;function fe(e){try{e.resolvedUrl||Be(!1,"Request's resolvedUrl must be set");const t=function(e){let t=e.resolvedUrl;if(Pe.loadBootResource){const o=ge(e);if(o instanceof Promise)return o;"string"==typeof o&&(t=o)}const o={};return e.cache?o.cache=e.cache:Pe.config.disableNoCacheFetch||(o.cache="no-cache"),e.useCredentials?o.credentials="include":!Pe.config.disableIntegrityCheck&&e.hash&&(o.integrity=e.hash),Pe.fetch_like(t,o)}(e),o={name:e.name,url:e.resolvedUrl,response:t};return ue.add(e.name),o.response.then((()=>{"assembly"==e.behavior&&Pe.loadedAssemblies.push(e.name),de++,Pe.onDownloadResourceProgress&&Pe.onDownloadResourceProgress(de,ue.size)})),o}catch(t){const o={ok:!1,url:e.resolvedUrl,status:500,statusText:"ERR29: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t}};return{name:e.name,url:e.resolvedUrl,response:Promise.resolve(o)}}}const me={resource:"assembly",assembly:"assembly",pdb:"pdb",icu:"globalization",vfs:"configuration",manifest:"manifest",dotnetwasm:"dotnetwasm","js-module-dotnet":"dotnetjs","js-module-native":"dotnetjs","js-module-runtime":"dotnetjs","js-module-threads":"dotnetjs"};function ge(e){var t;if(Pe.loadBootResource){const o=null!==(t=e.hash)&&void 0!==t?t:"",n=e.resolvedUrl,r=me[e.behavior];if(r){const t=Pe.loadBootResource(r,e.name,n,o,e.behavior);return"string"==typeof t?I(t):t}}}function pe(e){e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null}function he(e){let t=e.lastIndexOf("/");return t>=0&&t++,e.substring(t)}async function we(e){e&&await Promise.all((null!=e?e:[]).map((e=>async function(e){try{const t=e.name;if(!e.moduleExports){const o=ce(Pe.locateFile(t),"js-module-library-initializer");Pe.diagnosticTracing&&b(`Attempting to import '${o}' for ${e}`),e.moduleExports=await import(/*! webpackIgnore: true */o)}Pe.libraryInitializers.push({scriptName:t,exports:e.moduleExports})}catch(t){E(`Failed to import library initializer '${e}': ${t}`)}}(e))))}async function be(e,t){if(!Pe.libraryInitializers)return;const o=[];for(let n=0;n<Pe.libraryInitializers.length;n++){const r=Pe.libraryInitializers[n];r.exports[e]&&o.push(ye(r.scriptName,e,(()=>r.exports[e](...t))))}await Promise.all(o)}async function ye(e,t,o){try{await o()}catch(o){throw E(`Failed to invoke '${t}' on library initializer '${e}': ${o}`),Xe(1,o),o}}function ve(e,t){if(e===t)return e;const o={...t};return void 0!==o.assets&&o.assets!==e.assets&&(o.assets=[...e.assets||[],...o.assets||[]]),void 0!==o.resources&&(o.resources=_e(e.resources||{assembly:[],jsModuleNative:[],jsModuleRuntime:[],wasmNative:[]},o.resources)),void 0!==o.environmentVariables&&(o.environmentVariables={...e.environmentVariables||{},...o.environmentVariables||{}}),void 0!==o.runtimeOptions&&o.runtimeOptions!==e.runtimeOptions&&(o.runtimeOptions=[...e.runtimeOptions||[],...o.runtimeOptions||[]]),Object.assign(e,o)}function Ee(e,t){if(e===t)return e;const o={...t};return o.config&&(e.config||(e.config={}),o.config=ve(e.config,o.config)),Object.assign(e,o)}function _e(e,t){if(e===t)return e;const o={...t};return void 0!==o.coreAssembly&&(o.coreAssembly=[...e.coreAssembly||[],...o.coreAssembly||[]]),void 0!==o.assembly&&(o.assembly=[...e.assembly||[],...o.assembly||[]]),void 0!==o.lazyAssembly&&(o.lazyAssembly=[...e.lazyAssembly||[],...o.lazyAssembly||[]]),void 0!==o.corePdb&&(o.corePdb=[...e.corePdb||[],...o.corePdb||[]]),void 0!==o.pdb&&(o.pdb=[...e.pdb||[],...o.pdb||[]]),void 0!==o.jsModuleWorker&&(o.jsModuleWorker=[...e.jsModuleWorker||[],...o.jsModuleWorker||[]]),void 0!==o.jsModuleNative&&(o.jsModuleNative=[...e.jsModuleNative||[],...o.jsModuleNative||[]]),void 0!==o.jsModuleDiagnostics&&(o.jsModuleDiagnostics=[...e.jsModuleDiagnostics||[],...o.jsModuleDiagnostics||[]]),void 0!==o.jsModuleRuntime&&(o.jsModuleRuntime=[...e.jsModuleRuntime||[],...o.jsModuleRuntime||[]]),void 0!==o.wasmSymbols&&(o.wasmSymbols=[...e.wasmSymbols||[],...o.wasmSymbols||[]]),void 0!==o.wasmNative&&(o.wasmNative=[...e.wasmNative||[],...o.wasmNative||[]]),void 0!==o.icu&&(o.icu=[...e.icu||[],...o.icu||[]]),void 0!==o.satelliteResources&&(o.satelliteResources=function(e,t){if(e===t)return e;for(const o in t)e[o]=[...e[o]||[],...t[o]||[]];return e}(e.satelliteResources||{},o.satelliteResources||{})),void 0!==o.modulesAfterConfigLoaded&&(o.modulesAfterConfigLoaded=[...e.modulesAfterConfigLoaded||[],...o.modulesAfterConfigLoaded||[]]),void 0!==o.modulesAfterRuntimeReady&&(o.modulesAfterRuntimeReady=[...e.modulesAfterRuntimeReady||[],...o.modulesAfterRuntimeReady||[]]),void 0!==o.extensions&&(o.extensions={...e.extensions||{},...o.extensions||{}}),void 0!==o.vfs&&(o.vfs=[...e.vfs||[],...o.vfs||[]]),Object.assign(e,o)}function xe(){const e=Pe.config;if(e.environmentVariables=e.environmentVariables||{},e.runtimeOptions=e.runtimeOptions||[],e.resources=e.resources||{assembly:[],jsModuleNative:[],jsModuleWorker:[],jsModuleRuntime:[],wasmNative:[],vfs:[],satelliteResources:{}},e.assets){Pe.diagnosticTracing&&b("config.assets is deprecated, use config.resources instead");for(const t of e.assets){const o={};switch(t.behavior){case"assembly":o.assembly=[t];break;case"pdb":o.pdb=[t];break;case"resource":o.satelliteResources={},o.satelliteResources[t.culture]=[t];break;case"icu":o.icu=[t];break;case"symbols":o.wasmSymbols=[t];break;case"vfs":o.vfs=[t];break;case"dotnetwasm":o.wasmNative=[t];break;case"js-module-threads":o.jsModuleWorker=[t];break;case"js-module-runtime":o.jsModuleRuntime=[t];break;case"js-module-native":o.jsModuleNative=[t];break;case"js-module-diagnostics":o.jsModuleDiagnostics=[t];break;case"js-module-dotnet":break;default:throw new Error(`Unexpected behavior ${t.behavior} of asset ${t.name}`)}_e(e.resources,o)}}e.debugLevel,e.applicationEnvironment||(e.applicationEnvironment="Production"),e.applicationCulture&&(e.environmentVariables.LANG=`${e.applicationCulture}.UTF-8`),Ue.diagnosticTracing=Pe.diagnosticTracing=!!e.diagnosticTracing,Ue.waitForDebugger=e.waitForDebugger,Pe.maxParallelDownloads=e.maxParallelDownloads||Pe.maxParallelDownloads,Pe.enableDownloadRetry=void 0!==e.enableDownloadRetry?e.enableDownloadRetry:Pe.enableDownloadRetry}let je=!1;async function Re(e){var t;if(je)return void await Pe.afterConfigLoaded.promise;let o;try{if(e.configSrc||Pe.config&&0!==Object.keys(Pe.config).length&&(Pe.config.assets||Pe.config.resources)||(e.configSrc="dotnet.boot.js"),o=e.configSrc,je=!0,o&&(Pe.diagnosticTracing&&b("mono_wasm_load_config"),await async function(e){const t=e.configSrc,o=Pe.locateFile(t);let n=null;void 0!==Pe.loadBootResource&&(n=Pe.loadBootResource("manifest",t,o,"","manifest"));let r,i=null;if(n)if("string"==typeof n)n.includes(".json")?(i=await s(I(n)),r=await Ae(i)):r=(await import(I(n))).config;else{const e=await n;"function"==typeof e.json?(i=e,r=await Ae(i)):r=e.config}else o.includes(".json")?(i=await s(ce(o,"manifest")),r=await Ae(i)):r=(await import(ce(o,"manifest"))).config;function s(e){return Pe.fetch_like(e,{method:"GET",credentials:"include",cache:"no-cache"})}Pe.config.applicationEnvironment&&(r.applicationEnvironment=Pe.config.applicationEnvironment),ve(Pe.config,r)}(e)),xe(),await we(null===(t=Pe.config.resources)||void 0===t?void 0:t.modulesAfterConfigLoaded),await be("onRuntimeConfigLoaded",[Pe.config]),e.onConfigLoaded)try{await e.onConfigLoaded(Pe.config,Le),xe()}catch(e){throw _("onConfigLoaded() failed",e),e}xe(),Pe.afterConfigLoaded.promise_control.resolve(Pe.config)}catch(t){const n=`Failed to load config file ${o} ${t} ${null==t?void 0:t.stack}`;throw Pe.config=e.config=Object.assign(Pe.config,{message:n,error:t,isError:!0}),Xe(1,new Error(n)),t}}function Te(){return!!globalThis.navigator&&(Pe.isChromium||Pe.isFirefox)}async function Ae(e){const t=Pe.config,o=await e.json();t.applicationEnvironment||o.applicationEnvironment||(o.applicationEnvironment=e.headers.get("Blazor-Environment")||e.headers.get("DotNet-Environment")||void 0),o.environmentVariables||(o.environmentVariables={});const n=e.headers.get("DOTNET-MODIFIABLE-ASSEMBLIES");n&&(o.environmentVariables.DOTNET_MODIFIABLE_ASSEMBLIES=n);const r=e.headers.get("ASPNETCORE-BROWSER-TOOLS");return r&&(o.environmentVariables.__ASPNETCORE_BROWSER_TOOLS=r),o}"function"!=typeof importScripts||globalThis.onmessage||(globalThis.dotnetSidecar=!0);const Se="object"==typeof process&&"object"==typeof process.versions&&"string"==typeof process.versions.node,De="function"==typeof importScripts,Oe=De&&"undefined"!=typeof dotnetSidecar,Ce=De&&!Oe,ke="object"==typeof window||De&&!Se,Ie=!ke&&!Se;let Ue={},Pe={},Me={},Le={},Ne={},$e=!1;const ze={},We={config:ze},Fe={mono:{},binding:{},internal:Ne,module:We,loaderHelpers:Pe,runtimeHelpers:Ue,diagnosticHelpers:Me,api:Le};function Be(e,t){if(e)return;const o="Assert failed: "+("function"==typeof t?t():t),n=new Error(o);_(o,n),Ue.nativeAbort(n)}function Ve(){return void 0!==Pe.exitCode}function qe(){return Ue.runtimeReady&&!Ve()}function He(){Ve()&&Be(!1,`.NET runtime already exited with ${Pe.exitCode} ${Pe.exitReason}. You can use runtime.runMain() which doesn't exit the runtime.`),Ue.runtimeReady||Be(!1,".NET runtime didn't start yet. Please call dotnet.create() first.")}function Je(){ke&&(globalThis.addEventListener("unhandledrejection",et),globalThis.addEventListener("error",tt))}let Ze,Qe;function Ge(e){Qe&&Qe(e),Xe(e,Pe.exitReason)}function Ke(e){Ze&&Ze(e||Pe.exitReason),Xe(1,e||Pe.exitReason)}function Xe(t,o){var n,r;const i=o&&"object"==typeof o;t=i&&"number"==typeof o.status?o.status:void 0===t?-1:t;const s=i&&"string"==typeof o.message?o.message:""+o;(o=i?o:Ue.ExitStatus?function(e,t){const o=new Ue.ExitStatus(e);return o.message=t,o.toString=()=>t,o}(t,s):new Error("Exit with code "+t+" "+s)).status=t,o.message||(o.message=s);const a=""+(o.stack||(new Error).stack);try{Object.defineProperty(o,"stack",{get:()=>a})}catch(e){}const l=!!o.silent;if(o.silent=!0,Ve())Pe.diagnosticTracing&&b("mono_exit called after exit");else{try{We.onAbort==Ke&&(We.onAbort=Ze),We.onExit==Ge&&(We.onExit=Qe),ke&&(globalThis.removeEventListener("unhandledrejection",et),globalThis.removeEventListener("error",tt)),Ue.runtimeReady?(Ue.jiterpreter_dump_stats&&Ue.jiterpreter_dump_stats(!1),0===t&&(null===(n=Pe.config)||void 0===n?void 0:n.interopCleanupOnExit)&&Ue.forceDisposeProxies(!0,!0),e&&0!==t&&(null===(r=Pe.config)||void 0===r||r.dumpThreadsOnNonZeroExit)):(Pe.diagnosticTracing&&b(`abort_startup, reason: ${o}`),function(e){Pe.allDownloadsQueued.promise_control.reject(e),Pe.allDownloadsFinished.promise_control.reject(e),Pe.afterConfigLoaded.promise_control.reject(e),Pe.wasmCompilePromise.promise_control.reject(e),Pe.runtimeModuleLoaded.promise_control.reject(e),Ue.dotnetReady&&(Ue.dotnetReady.promise_control.reject(e),Ue.afterInstantiateWasm.promise_control.reject(e),Ue.beforePreInit.promise_control.reject(e),Ue.afterPreInit.promise_control.reject(e),Ue.afterPreRun.promise_control.reject(e),Ue.beforeOnRuntimeInitialized.promise_control.reject(e),Ue.afterOnRuntimeInitialized.promise_control.reject(e),Ue.afterPostRun.promise_control.reject(e))}(o))}catch(e){E("mono_exit A failed",e)}try{l||(function(e,t){if(0!==e&&t){const e=Ue.ExitStatus&&t instanceof Ue.ExitStatus?b:_;"string"==typeof t?e(t):(void 0===t.stack&&(t.stack=(new Error).stack+""),t.message?e(Ue.stringify_as_error_with_stack?Ue.stringify_as_error_with_stack(t.message+"\n"+t.stack):t.message+"\n"+t.stack):e(JSON.stringify(t)))}!Ce&&Pe.config&&(Pe.config.logExitCode?Pe.config.forwardConsoleLogsToWS?R("WASM EXIT "+e):v("WASM EXIT "+e):Pe.config.forwardConsoleLogsToWS&&R())}(t,o),function(e){if(ke&&!Ce&&Pe.config&&Pe.config.appendElementOnExit&&document){const t=document.createElement("label");t.id="tests_done",0!==e&&(t.style.background="red"),t.innerHTML=""+e,document.body.appendChild(t)}}(t))}catch(e){E("mono_exit B failed",e)}Pe.exitCode=t,Pe.exitReason||(Pe.exitReason=o),!Ce&&Ue.runtimeReady&&We.runtimeKeepalivePop()}if(Pe.config&&Pe.config.asyncFlushOnExit&&0===t)throw(async()=>{try{await async function(){try{const e=await import(/*! webpackIgnore: true */"process"),t=e=>new Promise(((t,o)=>{e.on("error",o),e.end("","utf8",t)})),o=t(e.stderr),n=t(e.stdout);let r;const i=new Promise((e=>{r=setTimeout((()=>e("timeout")),1e3)}));await Promise.race([Promise.all([n,o]),i]),clearTimeout(r)}catch(e){_(`flushing std* streams failed: ${e}`)}}()}finally{Ye(t,o)}})(),o;Ye(t,o)}function Ye(e,t){if(Ue.runtimeReady&&Ue.nativeExit)try{Ue.nativeExit(e)}catch(e){!Ue.ExitStatus||e instanceof Ue.ExitStatus||E("set_exit_code_and_quit_now failed: "+e.toString())}if(0!==e||!ke)throw Se&&Ne.process?Ne.process.exit(e):Ue.quit&&Ue.quit(e,t),t}function et(e){ot(e,e.reason,"rejection")}function tt(e){ot(e,e.error,"error")}function ot(e,t,o){e.preventDefault();try{t||(t=new Error("Unhandled "+o)),void 0===t.stack&&(t.stack=(new Error).stack),t.stack=t.stack+"",t.silent||(_("Unhandled error:",t),Xe(1,t))}catch(e){}}!function(e){if($e)throw new Error("Loader module already loaded");$e=!0,Ue=e.runtimeHelpers,Pe=e.loaderHelpers,Me=e.diagnosticHelpers,Le=e.api,Ne=e.internal,Object.assign(Le,{INTERNAL:Ne,invokeLibraryInitializers:be}),Object.assign(e.module,{config:ve(ze,{environmentVariables:{}})});const r={mono_wasm_bindings_is_ready:!1,config:e.module.config,diagnosticTracing:!1,nativeAbort:e=>{throw e||new Error("abort")},nativeExit:e=>{throw new Error("exit:"+e)}},l={gitHash:"95017c711e6afc1085133d440e42b4bd78155701",config:e.module.config,diagnosticTracing:!1,maxParallelDownloads:16,enableDownloadRetry:!0,_loaded_files:[],loadedFiles:[],loadedAssemblies:[],libraryInitializers:[],workerNextNumber:1,actual_downloaded_assets_count:0,actual_instantiated_assets_count:0,expected_downloaded_assets_count:0,expected_instantiated_assets_count:0,afterConfigLoaded:i(),allDownloadsQueued:i(),allDownloadsFinished:i(),wasmCompilePromise:i(),runtimeModuleLoaded:i(),loadingWorkers:i(),is_exited:Ve,is_runtime_running:qe,assert_runtime_running:He,mono_exit:Xe,createPromiseController:i,getPromiseController:s,assertIsControllablePromise:a,mono_download_assets:oe,resolve_single_asset_path:ee,setup_proxy_console:j,set_thread_prefix:w,installUnhandledErrorHandler:Je,retrieve_asset_download:ie,invokeLibraryInitializers:be,isDebuggingSupported:Te,exceptions:t,simd:n,relaxedSimd:o};Object.assign(Ue,r),Object.assign(Pe,l)}(Fe);let nt,rt,it,st=!1,at=!1;async function lt(e){if(!at){if(at=!0,ke&&Pe.config.forwardConsoleLogsToWS&&void 0!==globalThis.WebSocket&&j("main",globalThis.console,globalThis.location.origin),We||Be(!1,"Null moduleConfig"),Pe.config||Be(!1,"Null moduleConfig.config"),"function"==typeof e){const t=e(Fe.api);if(t.ready)throw new Error("Module.ready couldn't be redefined.");Object.assign(We,t),Ee(We,t)}else{if("object"!=typeof e)throw new Error("Can't use moduleFactory callback of createDotnetRuntime function.");Ee(We,e)}await async function(e){if(Se){const e=await import(/*! webpackIgnore: true */"process"),t=14;if(e.versions.node.split(".")[0]<t)throw new Error(`NodeJS at '${e.execPath}' has too low version '${e.versions.node}', please use at least ${t}. See also https://aka.ms/dotnet-wasm-features`)}const t=/*! webpackIgnore: true */import.meta.url,o=t.indexOf("?");var n;if(o>0&&(Pe.modulesUniqueQuery=t.substring(o)),Pe.scriptUrl=t.replace(/\\/g,"/").replace(/[?#].*/,""),Pe.scriptDirectory=(n=Pe.scriptUrl).slice(0,n.lastIndexOf("/"))+"/",Pe.locateFile=e=>"URL"in globalThis&&globalThis.URL!==C?new URL(e,Pe.scriptDirectory).toString():M(e)?e:Pe.scriptDirectory+e,Pe.fetch_like=k,Pe.out=console.log,Pe.err=console.error,Pe.onDownloadResourceProgress=e.onDownloadResourceProgress,ke&&globalThis.navigator){const e=globalThis.navigator,t=e.userAgentData&&e.userAgentData.brands;t&&t.length>0?Pe.isChromium=t.some((e=>"Google Chrome"===e.brand||"Microsoft Edge"===e.brand||"Chromium"===e.brand)):e.userAgent&&(Pe.isChromium=e.userAgent.includes("Chrome"),Pe.isFirefox=e.userAgent.includes("Firefox"))}Ne.require=Se?await import(/*! webpackIgnore: true */"module").then((e=>e.createRequire(/*! webpackIgnore: true */import.meta.url))):Promise.resolve((()=>{throw new Error("require not supported")})),void 0===globalThis.URL&&(globalThis.URL=C)}(We)}}async function ct(e){return await lt(e),Ze=We.onAbort,Qe=We.onExit,We.onAbort=Ke,We.onExit=Ge,We.ENVIRONMENT_IS_PTHREAD?async function(){(function(){const e=new MessageChannel,t=e.port1,o=e.port2;t.addEventListener("message",(e=>{var n,r;n=JSON.parse(e.data.config),r=JSON.parse(e.data.monoThreadInfo),st?Pe.diagnosticTracing&&b("mono config already received"):(ve(Pe.config,n),Ue.monoThreadInfo=r,xe(),Pe.diagnosticTracing&&b("mono config received"),st=!0,Pe.afterConfigLoaded.promise_control.resolve(Pe.config),ke&&n.forwardConsoleLogsToWS&&void 0!==globalThis.WebSocket&&Pe.setup_proxy_console("worker-idle",console,globalThis.location.origin)),t.close(),o.close()}),{once:!0}),t.start(),self.postMessage({[l]:{monoCmd:"preload",port:o}},[o])})(),await Pe.afterConfigLoaded.promise,function(){const e=Pe.config;e.assets||Be(!1,"config.assets must be defined");for(const t of e.assets)X(t),Q[t.behavior]&&z.push(t)}(),setTimeout((async()=>{try{await oe()}catch(e){Xe(1,e)}}),0);const e=dt(),t=await Promise.all(e);return await ut(t),We}():async function(){var e;await Re(We),re();const t=dt();(async function(){try{const e=ee("dotnetwasm");await se(e),e&&e.pendingDownloadInternal&&e.pendingDownloadInternal.response||Be(!1,"Can't load dotnet.native.wasm");const t=await e.pendingDownloadInternal.response,o=t.headers&&t.headers.get?t.headers.get("Content-Type"):void 0;let n;if("function"==typeof WebAssembly.compileStreaming&&"application/wasm"===o)n=await WebAssembly.compileStreaming(t);else{ke&&"application/wasm"!==o&&E('WebAssembly resource does not have the expected content type "application/wasm", so falling back to slower ArrayBuffer instantiation.');const e=await t.arrayBuffer();Pe.diagnosticTracing&&b("instantiate_wasm_module buffered"),n=Ie?await Promise.resolve(new WebAssembly.Module(e)):await WebAssembly.compile(e)}e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null,Pe.wasmCompilePromise.promise_control.resolve(n)}catch(e){Pe.wasmCompilePromise.promise_control.reject(e)}})(),setTimeout((async()=>{try{D(),await oe()}catch(e){Xe(1,e)}}),0);const o=await Promise.all(t);return await ut(o),await Ue.dotnetReady.promise,await we(null===(e=Pe.config.resources)||void 0===e?void 0:e.modulesAfterRuntimeReady),await be("onRuntimeReady",[Fe.api]),Le}()}function dt(){const e=ee("js-module-runtime"),t=ee("js-module-native");if(nt&&rt)return[nt,rt,it];"object"==typeof e.moduleExports?nt=e.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${e.resolvedUrl}' for ${e.name}`),nt=import(/*! webpackIgnore: true */e.resolvedUrl)),"object"==typeof t.moduleExports?rt=t.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${t.resolvedUrl}' for ${t.name}`),rt=import(/*! webpackIgnore: true */t.resolvedUrl));const o=Y("js-module-diagnostics");return o&&("object"==typeof o.moduleExports?it=o.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${o.resolvedUrl}' for ${o.name}`),it=import(/*! webpackIgnore: true */o.resolvedUrl))),[nt,rt,it]}async function ut(e){const{initializeExports:t,initializeReplacements:o,configureRuntimeStartup:n,configureEmscriptenStartup:r,configureWorkerStartup:i,setRuntimeGlobals:s,passEmscriptenInternals:a}=e[0],{default:l}=e[1],c=e[2];s(Fe),t(Fe),c&&c.setRuntimeGlobals(Fe),await n(We),Pe.runtimeModuleLoaded.promise_control.resolve(),l((e=>(Object.assign(We,{ready:e.ready,__dotnet_runtime:{initializeReplacements:o,configureEmscriptenStartup:r,configureWorkerStartup:i,passEmscriptenInternals:a}}),We))).catch((e=>{if(e.message&&e.message.toLowerCase().includes("out of memory"))throw new Error(".NET runtime has failed to start, because too much memory was requested. Please decrease the memory by adjusting EmccMaximumHeapSize. See also https://aka.ms/dotnet-wasm-features");throw e}))}const ft=new class{withModuleConfig(e){try{return Ee(We,e),this}catch(e){throw Xe(1,e),e}}withOnConfigLoaded(e){try{return Ee(We,{onConfigLoaded:e}),this}catch(e){throw Xe(1,e),e}}withConsoleForwarding(){try{return ve(ze,{forwardConsoleLogsToWS:!0}),this}catch(e){throw Xe(1,e),e}}withExitOnUnhandledError(){try{return ve(ze,{exitOnUnhandledError:!0}),Je(),this}catch(e){throw Xe(1,e),e}}withAsyncFlushOnExit(){try{return ve(ze,{asyncFlushOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withExitCodeLogging(){try{return ve(ze,{logExitCode:!0}),this}catch(e){throw Xe(1,e),e}}withElementOnExit(){try{return ve(ze,{appendElementOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withInteropCleanupOnExit(){try{return ve(ze,{interopCleanupOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withDumpThreadsOnNonZeroExit(){try{return ve(ze,{dumpThreadsOnNonZeroExit:!0}),this}catch(e){throw Xe(1,e),e}}withWaitingForDebugger(e){try{return ve(ze,{waitForDebugger:e}),this}catch(e){throw Xe(1,e),e}}withInterpreterPgo(e,t){try{return ve(ze,{interpreterPgo:e,interpreterPgoSaveDelay:t}),ze.runtimeOptions?ze.runtimeOptions.push("--interp-pgo-recording"):ze.runtimeOptions=["--interp-pgo-recording"],this}catch(e){throw Xe(1,e),e}}withConfig(e){try{return ve(ze,e),this}catch(e){throw Xe(1,e),e}}withConfigSrc(e){try{return e&&"string"==typeof e||Be(!1,"must be file path or URL"),Ee(We,{configSrc:e}),this}catch(e){throw Xe(1,e),e}}withVirtualWorkingDirectory(e){try{return e&&"string"==typeof e||Be(!1,"must be directory path"),ve(ze,{virtualWorkingDirectory:e}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariable(e,t){try{const o={};return o[e]=t,ve(ze,{environmentVariables:o}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariables(e){try{return e&&"object"==typeof e||Be(!1,"must be dictionary object"),ve(ze,{environmentVariables:e}),this}catch(e){throw Xe(1,e),e}}withDiagnosticTracing(e){try{return"boolean"!=typeof e&&Be(!1,"must be boolean"),ve(ze,{diagnosticTracing:e}),this}catch(e){throw Xe(1,e),e}}withDebugging(e){try{return null!=e&&"number"==typeof e||Be(!1,"must be number"),ve(ze,{debugLevel:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArguments(...e){try{return e&&Array.isArray(e)||Be(!1,"must be array of strings"),ve(ze,{applicationArguments:e}),this}catch(e){throw Xe(1,e),e}}withRuntimeOptions(e){try{return e&&Array.isArray(e)||Be(!1,"must be array of strings"),ze.runtimeOptions?ze.runtimeOptions.push(...e):ze.runtimeOptions=e,this}catch(e){throw Xe(1,e),e}}withMainAssembly(e){try{return ve(ze,{mainAssemblyName:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArgumentsFromQuery(){try{if(!globalThis.window)throw new Error("Missing window to the query parameters from");if(void 0===globalThis.URLSearchParams)throw new Error("URLSearchParams is supported");const e=new URLSearchParams(globalThis.window.location.search).getAll("arg");return this.withApplicationArguments(...e)}catch(e){throw Xe(1,e),e}}withApplicationEnvironment(e){try{return ve(ze,{applicationEnvironment:e}),this}catch(e){throw Xe(1,e),e}}withApplicationCulture(e){try{return ve(ze,{applicationCulture:e}),this}catch(e){throw Xe(1,e),e}}withResourceLoader(e){try{return Pe.loadBootResource=e,this}catch(e){throw Xe(1,e),e}}async download(){try{await async function(){lt(We),await Re(We),re(),D(),oe(),await Pe.allDownloadsFinished.promise}()}catch(e){throw Xe(1,e),e}}async create(){try{return this.instance||(this.instance=await async function(){return await ct(We),Fe.api}()),this.instance}catch(e){throw Xe(1,e),e}}async run(){try{return We.config||Be(!1,"Null moduleConfig.config"),this.instance||await this.create(),this.instance.runMainAndExit()}catch(e){throw Xe(1,e),e}}},mt=Xe,gt=ct;Ie||"function"==typeof globalThis.URL||Be(!1,"This browser/engine doesn't support URL API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),"function"!=typeof globalThis.BigInt64Array&&Be(!1,"This browser/engine doesn't support BigInt64Array API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),ft.withConfig(/*json-start*/{
  "mainAssemblyName": "DemoCenter.Web",
  "resources": {
    "hash": "sha256-9rOHXfy4ZuGIo/l+9vgSf53Zh8JqBuSTIT69eaDRMxE=",
    "jsModuleNative": [
      {
        "name": "dotnet.native.mswd13wxr1.js"
      }
    ],
    "jsModuleRuntime": [
      {
        "name": "dotnet.runtime.v06hirbjsv.js"
      }
    ],
    "wasmNative": [
      {
        "name": "dotnet.native.8tiqd8ffyh.wasm",
        "hash": "sha256-RCF9ERN9Hg24TJTPrRMb8Hhgf3U22Ak3xxu4ft9SXUQ=",
        "cache": "force-cache"
      }
    ],
    "icu": [
      {
        "virtualPath": "icudt_CJK.dat",
        "name": "icudt_CJK.tjcz0u77k5.dat",
        "hash": "sha256-SZLtQnRc0JkwqHab0VUVP7T3uBPSeYzxzDnpxPpUnHk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "icudt_EFIGS.dat",
        "name": "icudt_EFIGS.tptq2av103.dat",
        "hash": "sha256-8fItetYY8kQ0ww6oxwTLiT3oXlBwHKumbeP2pRF4yTc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "icudt_no_CJK.dat",
        "name": "icudt_no_CJK.lfu7j35m59.dat",
        "hash": "sha256-L7sV7NEYP37/Qr2FPCePo5cJqRgTXRwGHuwF5Q+0Nfs=",
        "cache": "force-cache"
      }
    ],
    "coreAssembly": [
      {
        "virtualPath": "System.Private.CoreLib.wasm",
        "name": "System.Private.CoreLib.1vy46s12u6.wasm",
        "hash": "sha256-gzDngaKE1fzrenrnFvi+7MHiHJEUQn0brQD1g3Q+hFI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.InteropServices.JavaScript.wasm",
        "name": "System.Runtime.InteropServices.JavaScript.9kq1vofond.wasm",
        "hash": "sha256-uQsOh1GEq2ZX0sXBxXnyoYzNMf4yeXUeLKUgb1q0fKo=",
        "cache": "force-cache"
      }
    ],
    "assembly": [
      {
        "virtualPath": "Avalonia.Base.wasm",
        "name": "Avalonia.Base.1wwzy2kcx1.wasm",
        "hash": "sha256-v4S8S6jhb0DuEJwFvHyvc3qRNunbZ4Cerr4G2f+2xCQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Browser.wasm",
        "name": "Avalonia.Browser.npyo138c0v.wasm",
        "hash": "sha256-RS0FAKdv0gHD0INnx3L/iFismW7XSnPr/xVonWveHiY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Controls.ColorPicker.wasm",
        "name": "Avalonia.Controls.ColorPicker.k3p3e34b7y.wasm",
        "hash": "sha256-0FvROj2fRCYZggPzSzN6vfFiKx4/ApMM8h1vZ/KJ1YA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Controls.DataGrid.wasm",
        "name": "Avalonia.Controls.DataGrid.pcqulqvthq.wasm",
        "hash": "sha256-6e3NPd1ZN9x1exwh6gA8EhDNgvBfAYg9jKfajHa5FjE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Controls.wasm",
        "name": "Avalonia.Controls.xy0mkxadpj.wasm",
        "hash": "sha256-l7lLi6CCZGCWztfQv6YADppNyy+w5YLaerj80iWoKXk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.DesignerSupport.wasm",
        "name": "Avalonia.DesignerSupport.l3ixncwh27.wasm",
        "hash": "sha256-H7/YoyxbVIy7hv21aXhZiTZed57EPDA8LOB79EGurq4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Dialogs.wasm",
        "name": "Avalonia.Dialogs.nlxf575rlr.wasm",
        "hash": "sha256-2wiDbTn2f09Autwtd188+YxiV3FdULK58EpmBdZuY0k=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Fonts.Inter.wasm",
        "name": "Avalonia.Fonts.Inter.qfgnjk30eg.wasm",
        "hash": "sha256-ClpoNnU65h35OyCJe8sNJ8+d4s1FG+nll3aHEV4DxKg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.HarfBuzz.wasm",
        "name": "Avalonia.HarfBuzz.ysvha5qh5b.wasm",
        "hash": "sha256-Qhn9KOIcj34S9so8NQ/4o1FCgDkJ+7QSgdYS8UCa4k0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Markup.Xaml.wasm",
        "name": "Avalonia.Markup.Xaml.thf0h2x2ka.wasm",
        "hash": "sha256-cdfyImxo2whUZ+qpCbfU5+3Z10vyc3ipsjru4UZo4NM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Markup.wasm",
        "name": "Avalonia.Markup.pl1zfsdinb.wasm",
        "hash": "sha256-3aG6jC5dk8pcqCGXT/Gj0k1/b/qPHHUpXJp1Q/J57KM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Metal.wasm",
        "name": "Avalonia.Metal.6m4q6spwfj.wasm",
        "hash": "sha256-IiAucO1ub9uokrzDMlhKyT4hjeZgfuREgHmQ5dX/xoA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.MicroCom.wasm",
        "name": "Avalonia.MicroCom.jaqh4ajcm2.wasm",
        "hash": "sha256-W7tEtNF4g8hdxNEi0bC2oHVJbAmkCky07WKkotXjWmA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.OpenGL.wasm",
        "name": "Avalonia.OpenGL.kazovu5t7v.wasm",
        "hash": "sha256-VPAoufYOAcux9SSjhodnL+LBtXGww3ulOj6ZopQq88c=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Remote.Protocol.wasm",
        "name": "Avalonia.Remote.Protocol.vqfrepas9f.wasm",
        "hash": "sha256-d/oevvq3QoB1lezKSyj2kqJVa//xjOJmIheeQ92IB78=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Skia.wasm",
        "name": "Avalonia.Skia.m9ivmezgiu.wasm",
        "hash": "sha256-xpND7PY3b3f5IvKdXjsbWNwem4n+g3fCKfVVyi5BBC4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Themes.Fluent.wasm",
        "name": "Avalonia.Themes.Fluent.6n8h39sf75.wasm",
        "hash": "sha256-Am1vEpgk3+sOAT250Fl/jaBBrqldmoJHhj3yjvfrseQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Vulkan.wasm",
        "name": "Avalonia.Vulkan.l1ylp6pmaz.wasm",
        "hash": "sha256-mUAIJN187+1MVOmtFu++Ht9gNzqpPqSndDKtzF+W0tM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.wasm",
        "name": "Avalonia.npkq25rpvv.wasm",
        "hash": "sha256-W1FheViUxHVcifbKgKtZkGE316FI44J84SRFai2baSY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "AvaloniaEdit.TextMate.wasm",
        "name": "AvaloniaEdit.TextMate.2zzyvq7hmd.wasm",
        "hash": "sha256-nsGd8GIWZVF0ompubimsRlVlonEeOZMsGMliOMah2NM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "AvaloniaEdit.wasm",
        "name": "AvaloniaEdit.tf6utuj9c0.wasm",
        "hash": "sha256-555JupLWyo/70z8i9UYpQNOx8vHDZsRWSuGp0rMS3Zk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "BouncyCastle.Cryptography.wasm",
        "name": "BouncyCastle.Cryptography.wt571kmjes.wasm",
        "hash": "sha256-RKpr9b3wIMIUe21a7UznNzKYNUkZ0zXZteGpUBd3O28=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "CommunityToolkit.Mvvm.wasm",
        "name": "CommunityToolkit.Mvvm.7gmxmsktu4.wasm",
        "hash": "sha256-5zW+GBaJU3Yx7rmRTIROwNk92oqs1+nRVbHJ1xleJRQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DemoCenter.wasm",
        "name": "DemoCenter.2wmg6hayc7.wasm",
        "hash": "sha256-4qFtJmUUse1wNJUo3oYLqUmq+yWi73ovrxz0gOxTjKM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DemoCenter.Web.wasm",
        "name": "DemoCenter.Web.6868vzsu13.wasm",
        "hash": "sha256-hvyCTvGrSrtKhql3gKAQmH5ldPcD2hcANUvr5XFGHs4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DynamicData.wasm",
        "name": "DynamicData.7nmvv7bgy7.wasm",
        "hash": "sha256-ndA8BK9kRtuNqgKySbwvjY6TMhnMtJC6KoXurkYXbCg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Avalonia.Charts.wasm",
        "name": "Eremex.Avalonia.Charts.07hag2i45x.wasm",
        "hash": "sha256-dBIY90DsQ2GA+LkoKWQSXb5afYwLl9HGVaGgm1nrcXI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Avalonia.Controls.wasm",
        "name": "Eremex.Avalonia.Controls.555ong3fng.wasm",
        "hash": "sha256-9yjdw5zo2ePhWOWRdc/BpmkFTmrmB7SLxIFTJ22Wpyw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Avalonia.Controls.ApplicationServices.wasm",
        "name": "Eremex.Avalonia.Controls.ApplicationServices.oz8nslilay.wasm",
        "hash": "sha256-RP2JRo3IUiRaI4Tmy2GF/lB1RYWuaHqO3O6nPcc27ng=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Avalonia.Icons.wasm",
        "name": "Eremex.Avalonia.Icons.cjudhrfb80.wasm",
        "hash": "sha256-KHc8y2SfL9qZtsqO82rkRoQuv3yUtd1zDWVxAW52lNI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Avalonia.Themes.DeltaDesign.wasm",
        "name": "Eremex.Avalonia.Themes.DeltaDesign.qgrf291m3h.wasm",
        "hash": "sha256-ys7trFiHMD9+yls/OpZzuybaY5Czb/d1Gf2AeRX+ISk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Common.Contracts.wasm",
        "name": "Eremex.Common.Contracts.pqt0bmmtzv.wasm",
        "hash": "sha256-kTlseKqTyHRvOo+hc3CRTV8RHBR5jgzyoM8MQxhjdpk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.DocumentProcessing.wasm",
        "name": "Eremex.DocumentProcessing.fe24ibwlng.wasm",
        "hash": "sha256-J4N5RIy6eLlsr3xxK4C5a+1kk06efmyTxlDU/M/rUxk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Drawing.Skia.wasm",
        "name": "Eremex.Drawing.Skia.8g261wa03b.wasm",
        "hash": "sha256-6BLon543UwIyDM1/e3mkpoYFyHiAufknJPsKfJ3M29E=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Drawing.wasm",
        "name": "Eremex.Drawing.khj2yf37zu.wasm",
        "hash": "sha256-KL2k9sk/g6hCJPESA0MYf63yvHlsiF0h19MhN3Tf6m4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Pdf.wasm",
        "name": "Eremex.Pdf.piu4ezp4n1.wasm",
        "hash": "sha256-gT1LYLa3ZTgt/s/a6/ZorwR/dp9yZvS6QvHyP6/xWu4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "ExCSS.wasm",
        "name": "ExCSS.jbixd2k5bc.wasm",
        "hash": "sha256-ATAW0isJpz7mjLGpEjkTzbHywbCFEJIXm3eAaOFzF7g=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "HarfBuzzSharp.wasm",
        "name": "HarfBuzzSharp.skj2rjd285.wasm",
        "hash": "sha256-ZLlju1KLIdabrxDX12d4wrYNpCKTakVe2knm60pqduY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "MicroCom.Runtime.wasm",
        "name": "MicroCom.Runtime.viw2o55mwq.wasm",
        "hash": "sha256-Jy3LZVB8MBkdFkecGnunWSDdyD2xZAIqfIRAd29LhYI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.CSharp.wasm",
        "name": "Microsoft.CSharp.mp8lhr6rez.wasm",
        "hash": "sha256-psMoxmxraX+UqMuR6yuCJ2c/xLT3w20OO1JzVzNJ8qQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.CodeAnalysis.wasm",
        "name": "Microsoft.CodeAnalysis.32jmmtpt80.wasm",
        "hash": "sha256-+0NGSntDfIQc6hdyaivxtxAI08mHI7AUh4IvgNXqOE0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.CodeAnalysis.CSharp.Scripting.wasm",
        "name": "Microsoft.CodeAnalysis.CSharp.Scripting.20iv3sqjqg.wasm",
        "hash": "sha256-HmACyPLAX02y0jNRFix3k5lse71qhh+JHGG3wXkDmP0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.CodeAnalysis.CSharp.wasm",
        "name": "Microsoft.CodeAnalysis.CSharp.nx7496rbnr.wasm",
        "hash": "sha256-K/IvZVmj9uUbv0ZgcseSW7kVr5sbqlfl1jFCdfaqvfU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.CodeAnalysis.Scripting.wasm",
        "name": "Microsoft.CodeAnalysis.Scripting.vmkyli7bir.wasm",
        "hash": "sha256-94mGKPfg5QFlv12yFY99nGRA3tXVP4nOWeKTmgctMGM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.wasm",
        "name": "Microsoft.Extensions.Configuration.3cfwexxe27.wasm",
        "hash": "sha256-o5ezesHCjl4655ks49HD2a1FPfLaSJo/n6qvlh++Jfc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Abstractions.wasm",
        "name": "Microsoft.Extensions.Configuration.Abstractions.78ikuo4yzn.wasm",
        "hash": "sha256-R5f+UCInswsfRL3Kk+rRKfXRofqUOTWd3/kjRvgkZo8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Configuration.Binder.wasm",
        "name": "Microsoft.Extensions.Configuration.Binder.cl71x0iwsw.wasm",
        "hash": "sha256-UELeZZZwzg/JZA1UqPuIJFXws1xfntMuDC9Uwfx7SF8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.DependencyInjection.Abstractions.wasm",
        "name": "Microsoft.Extensions.DependencyInjection.Abstractions.zz54p87jze.wasm",
        "hash": "sha256-XwCs1lQ9uzNurl1sOToQvfQxl4SqdjwFO9Ch34oMUAQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.DependencyInjection.wasm",
        "name": "Microsoft.Extensions.DependencyInjection.pfab3mc78m.wasm",
        "hash": "sha256-lYSnf7qQE14JH+dffJ/AvrBo/h5IliigqIRZtwrKuCs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Logging.Abstractions.wasm",
        "name": "Microsoft.Extensions.Logging.Abstractions.k4lvsffzcu.wasm",
        "hash": "sha256-tGS8ZaLaHmBJwYP/0cStbSZ0a32DZJmFmfFDvjM9kfY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Logging.Configuration.wasm",
        "name": "Microsoft.Extensions.Logging.Configuration.0p2z2uteml.wasm",
        "hash": "sha256-nD0OV9TPBZvNX6IBhn477wTZa0QcvPjR0OVHCD2TOBk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Logging.Console.wasm",
        "name": "Microsoft.Extensions.Logging.Console.twh8urgi6i.wasm",
        "hash": "sha256-U0nK+5aQsCKefrDuVA9weT4OtgChHSrbqoCBlIXIsOY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Logging.wasm",
        "name": "Microsoft.Extensions.Logging.oouy1cp12o.wasm",
        "hash": "sha256-2PD6mv0oN7crfvv7AVwN8Ca7+y+th6dlWJxAjZkWd3s=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Options.wasm",
        "name": "Microsoft.Extensions.Options.8dp7doq4zy.wasm",
        "hash": "sha256-1FaSGBwUAZPHmD1nJG6vCF4lfbBqqhVk9PLP8GkylV8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Options.ConfigurationExtensions.wasm",
        "name": "Microsoft.Extensions.Options.ConfigurationExtensions.nrj5vraneo.wasm",
        "hash": "sha256-reH1ytQBS8+rToYmSn7y4cSrDWGJTWI5gw1tXnLSS4U=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Extensions.Primitives.wasm",
        "name": "Microsoft.Extensions.Primitives.vbo14pe7i6.wasm",
        "hash": "sha256-a1kiNp4o9lw2QUJ7yUEBv9QZsyllrykBlkQaCWzVWMc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.VisualBasic.wasm",
        "name": "Microsoft.VisualBasic.0e5fchkecn.wasm",
        "hash": "sha256-+AZA2Jpi7YedyPg/Z7OAvV4jcPpueAU4ylSeRvJDt80=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.VisualBasic.Core.wasm",
        "name": "Microsoft.VisualBasic.Core.17wn27qal2.wasm",
        "hash": "sha256-veBhGyorrzdMHfiRDt/VovNG125rLLrL7enX5fvvfnE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Win32.Primitives.wasm",
        "name": "Microsoft.Win32.Primitives.9d566sle9e.wasm",
        "hash": "sha256-UxlTjVzD23T3TLHwBAoQkPNd2pcDDBI/aV05iU6Dxwo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Win32.Registry.wasm",
        "name": "Microsoft.Win32.Registry.52dy96yk8i.wasm",
        "hash": "sha256-Y+jj+a+2gTHHiYlyKc94wEH41wg3Vjrdd9KSP0crV6g=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Win32.SystemEvents.wasm",
        "name": "Microsoft.Win32.SystemEvents.asbyu30enw.wasm",
        "hash": "sha256-1kUoFxSGzlbndQM4zal3G/YBdouUTf444lDjqhuWALU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Newtonsoft.Json.wasm",
        "name": "Newtonsoft.Json.qkbufwhni2.wasm",
        "hash": "sha256-GlXMWKvDs45M2pACoR3Y4Qh8mcrOZGljqmvJY+6JZ5s=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Onigwrap.wasm",
        "name": "Onigwrap.wabw7w2fpc.wasm",
        "hash": "sha256-yDd0dEOwD+z+0eLlunXj62GypEMaW4k4LowPgZVFpDI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "PdfSharp.Charting.wasm",
        "name": "PdfSharp.Charting.6g5sgdb583.wasm",
        "hash": "sha256-aqGW88HIjp/nC4W+ZVETkex3A+YR2JqP/qr0ytKDKxY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "PdfSharp.Quality.wasm",
        "name": "PdfSharp.Quality.v64pv2qdvv.wasm",
        "hash": "sha256-d4g9zXaMARpTD7EMS1TE/9lLOkTx1VR1Xy4T60USO8w=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "PdfSharp.Snippets.wasm",
        "name": "PdfSharp.Snippets.4t7is8dpeh.wasm",
        "hash": "sha256-CMQZIb5bfs4UeB8/BUO86eHhMWoZBaJ/L2VYvf19Dog=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "PdfSharp.System.wasm",
        "name": "PdfSharp.System.3euzaydwhw.wasm",
        "hash": "sha256-ZZnvrXLQhe3mAcFNifg1GTcJ/F03Pcm/bd1eSrpN63M=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "PdfSharp.WPFonts.wasm",
        "name": "PdfSharp.WPFonts.um5zexeuvv.wasm",
        "hash": "sha256-RlnYAdd9+eNKGpdJZHgcNn9cM0Uz3fDwVEAHR1mJxPk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "PdfSharp.wasm",
        "name": "PdfSharp.m11gmbyyl0.wasm",
        "hash": "sha256-thQiHbthhtvTGRIPHls7qN4sdVWg8G28sLwVTwqXuPQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "ReactiveUI.wasm",
        "name": "ReactiveUI.95l7pt3n8d.wasm",
        "hash": "sha256-M1z0BeGKL6KNZqYsIpozT8l80TLzFsaOOFNlrn6Zc94=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "ReactiveUI.Avalonia.wasm",
        "name": "ReactiveUI.Avalonia.cfzea3mcnd.wasm",
        "hash": "sha256-KL8T1Rb0hiLf/15I6HrN8KnHuoNgREjLwwciO/xLHwI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SharpDX.DXGI.wasm",
        "name": "SharpDX.DXGI.bfya588il9.wasm",
        "hash": "sha256-Oe6A34L7aoDZNIMR2PBtysYPxLpguQn1L3dO9ycI6yA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SharpDX.Direct2D1.wasm",
        "name": "SharpDX.Direct2D1.urqccnr5tc.wasm",
        "hash": "sha256-ikocUXQs//1JTXNmSw3MIYbEU5c/OtAazXj4iGeu0Ew=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SharpDX.wasm",
        "name": "SharpDX.qn3ez2iiz5.wasm",
        "hash": "sha256-+gp5Yu2+P9OdyF3pADH7Umfc8BPbxOjSrN14YHqPn+A=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "ShimSkiaSharp.wasm",
        "name": "ShimSkiaSharp.uc14cgt2v4.wasm",
        "hash": "sha256-3vvQefudGuQtfAgiUuI5fYtb7EobApd+CrbYmBlCRo8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "SkiaSharp.wasm",
        "name": "SkiaSharp.jplvzyrjai.wasm",
        "hash": "sha256-WUX3pIObMgqBmG3FmMAe+brTVISSmE6jzR95+QNTq3Y=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Splat.Builder.wasm",
        "name": "Splat.Builder.m2lrel3npt.wasm",
        "hash": "sha256-btUb1F+zt0t6YghFAUI4Fbw07ad1PVEtZ86Jrr/13vQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Splat.Core.wasm",
        "name": "Splat.Core.6zbj9wglw9.wasm",
        "hash": "sha256-ntOmXKzsJ42R7kLTomRmxiL+sbAyk8IDI6AKOO3bVmM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Splat.Logging.wasm",
        "name": "Splat.Logging.qwwuw0tpfx.wasm",
        "hash": "sha256-5s0JYbPRm3HFiHvc7aKeRJAjswnfnM2vLo8fKN5yyiY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Splat.wasm",
        "name": "Splat.h69ae4xiai.wasm",
        "hash": "sha256-+Tpsh3Romx9jZ7CpKlnovpHnCVawsyK7Pv/YHwCINd8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Svg.Animation.wasm",
        "name": "Svg.Animation.ydbmjt30rc.wasm",
        "hash": "sha256-Jpqzx9OE/fIqs8RlvZh3qusgsobt7z+8Ip8GJpDuGks=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Svg.Controls.Skia.Avalonia.wasm",
        "name": "Svg.Controls.Skia.Avalonia.krydlauo99.wasm",
        "hash": "sha256-QG6VgCB+XIFUMa4Jm//XsDHRfDgWxqDd3lJbt3xwKLQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Svg.Custom.wasm",
        "name": "Svg.Custom.fvfm4r0wm0.wasm",
        "hash": "sha256-JTY8TyrD89erHueTV/J80HtE7jmHJmtuhvoO2OQGS8M=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Svg.Model.wasm",
        "name": "Svg.Model.5yj75ri1sp.wasm",
        "hash": "sha256-Q86NUTfpWMXU1JYfcPP/eXGRqrv+TLBhLzRKOnE49Wc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Svg.SceneGraph.wasm",
        "name": "Svg.SceneGraph.lpwvi9ok6q.wasm",
        "hash": "sha256-WU2oIBiq5xog4aEYOd8pYnPegLDyU5d2urClPZphEe0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Svg.Skia.wasm",
        "name": "Svg.Skia.p8eda75aly.wasm",
        "hash": "sha256-p+DUE2PWMhuzlykwMbfOBorV1HqHGNVu71xtyKQk4vg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.AppContext.wasm",
        "name": "System.AppContext.jwujeh069z.wasm",
        "hash": "sha256-M1WENJWA+3wr2NvHhLXh44JWWE4VVZag928GiVWbXG0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Buffers.wasm",
        "name": "System.Buffers.y0isk1x97h.wasm",
        "hash": "sha256-QtI7D2XiD7NyCRDNW7Q5jPY5RrljqkJWOofJWLBpLOo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Concurrent.wasm",
        "name": "System.Collections.Concurrent.x5koi7bzem.wasm",
        "hash": "sha256-60VYOK8iVcWICnieMcztr96fSxmXaDwHYglhHVkfcWI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Immutable.wasm",
        "name": "System.Collections.Immutable.diwg7advch.wasm",
        "hash": "sha256-lWe3YUN/Fc5uruvGWP/m8qaunD50OIQZlIigCznydZc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.NonGeneric.wasm",
        "name": "System.Collections.NonGeneric.4089xs440e.wasm",
        "hash": "sha256-PH4kaa45WLnMxiRfMTzKCQ9u42dAwwdy99kptJLonnA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Specialized.wasm",
        "name": "System.Collections.Specialized.hj81u7zblb.wasm",
        "hash": "sha256-g9RG+K+iaKmrK6vAd+/MRgz68IbHHpSlRyOFIY27sUI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.wasm",
        "name": "System.Collections.nvrdeoknr1.wasm",
        "hash": "sha256-nQNshHg3oKyRQXw5zLw1fEm4B4SQKVyeH2F66wBEe3o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.Annotations.wasm",
        "name": "System.ComponentModel.Annotations.yatcnfjeuk.wasm",
        "hash": "sha256-WuI5nXRLzegBvcDXd12p9roPYpi8jofCeEJ6v8s63+E=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.DataAnnotations.wasm",
        "name": "System.ComponentModel.DataAnnotations.led9l5awu3.wasm",
        "hash": "sha256-40rhAIB8j+yJ1YvnDmQ2rPIVWKNJO6UHLv/U4oSxlBI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.EventBasedAsync.wasm",
        "name": "System.ComponentModel.EventBasedAsync.hpvp6cf4de.wasm",
        "hash": "sha256-Oy0URE8zV5ayhb45ITWZbEnl6sGM9LatkYgbeAURRKM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.Primitives.wasm",
        "name": "System.ComponentModel.Primitives.v7cajwpaqo.wasm",
        "hash": "sha256-W9/zGt2iJxheWHJRApI7G7Z0ckE5RfuWUz4Nnwxgbq4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.TypeConverter.wasm",
        "name": "System.ComponentModel.TypeConverter.xk1hnyt762.wasm",
        "hash": "sha256-QfHpIsM6pT5gYELaVBtPojDrl0GNwck46q4Mu933WCo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.wasm",
        "name": "System.ComponentModel.rrnda1lucx.wasm",
        "hash": "sha256-aYhTsxu5l5DdQMIprD4ShbtwjTiJBH78I6wMQzBRnys=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Configuration.ConfigurationManager.wasm",
        "name": "System.Configuration.ConfigurationManager.3cipyddygh.wasm",
        "hash": "sha256-Ey9QseSg8lIw8pRXkXaGaDOuLW5qJ/o7jUjkeomqUmk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Configuration.wasm",
        "name": "System.Configuration.bn9vghjcq8.wasm",
        "hash": "sha256-l2Yo9KFaXkgXpXguTZdnuTCytOYuPLjF8cZT0HHWwlg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Console.wasm",
        "name": "System.Console.09ajpiafhz.wasm",
        "hash": "sha256-XKTYAvVqGguN3HteeYxjDwZqlFcFmIfpLOjNVjwXSNE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Core.wasm",
        "name": "System.Core.wc649o5mw9.wasm",
        "hash": "sha256-0C1e8tZ9+pmcU1MPfcf8D7NNcmR2pH8LdxsDMYrVHG0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Data.Common.wasm",
        "name": "System.Data.Common.s4cowzd9a9.wasm",
        "hash": "sha256-bO/LPpxha6hSooSI6P+lazCSxTKXav6d+SSGI5xKoF8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Data.DataSetExtensions.wasm",
        "name": "System.Data.DataSetExtensions.5fm360ikxl.wasm",
        "hash": "sha256-QXrN9ZMMpWZeFTBu6yZlaw5lYx1+tUxEjhfaNEdHG4o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Data.wasm",
        "name": "System.Data.fsu4j4t4jw.wasm",
        "hash": "sha256-ITg6VN5fCPuyFKC9ug/0vqXRoagnjGYhWj10y5heiTU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Contracts.wasm",
        "name": "System.Diagnostics.Contracts.p74re5oxzn.wasm",
        "hash": "sha256-6+8hQYc91I+/Kqc5U6zCuNw2ConQevD1fmEvhSZFfoc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Debug.wasm",
        "name": "System.Diagnostics.Debug.622sixg49n.wasm",
        "hash": "sha256-kpDQTiMcfbO9nP5Mja6gzL4XTOp/9ixE1CD739WQlMo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.DiagnosticSource.wasm",
        "name": "System.Diagnostics.DiagnosticSource.2j35bc2g2b.wasm",
        "hash": "sha256-HlxtCU+2k1YAqiabFqu5jOoUbMyV2ZEjNNlUEs/57NM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.FileVersionInfo.wasm",
        "name": "System.Diagnostics.FileVersionInfo.7n0wdvltaa.wasm",
        "hash": "sha256-vZsGhIqd6ATctrY/LP4iZy0cDa+gN+Vzk8mA9ecl4as=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Process.wasm",
        "name": "System.Diagnostics.Process.8iffh49mew.wasm",
        "hash": "sha256-QE2oJ8DjLG05OdC+fLmt+YhFiUEOSwyowwQ25oZCvTw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.StackTrace.wasm",
        "name": "System.Diagnostics.StackTrace.1yz88vico8.wasm",
        "hash": "sha256-+Wsf/lB7CDI+7OUXetsTyE+lo4TYc/SAS1aMv1yniNM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.TextWriterTraceListener.wasm",
        "name": "System.Diagnostics.TextWriterTraceListener.ua8iuh2vhj.wasm",
        "hash": "sha256-emPIv5g8xFmDr7oIZhZM/DTWGrb2Lggg7c+fjA99i5c=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Tools.wasm",
        "name": "System.Diagnostics.Tools.roy5445ugj.wasm",
        "hash": "sha256-2yLIVwYrPkQWk0Vxh+qS+KxNElAj+Z4UTmjJNv8uYNk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.TraceSource.wasm",
        "name": "System.Diagnostics.TraceSource.6qx1ldzz7g.wasm",
        "hash": "sha256-AiYhBtmkwpOLWooLU6+KmJ0ySg6RY/ihjlCDgwQAT8E=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Tracing.wasm",
        "name": "System.Diagnostics.Tracing.b4799zpaas.wasm",
        "hash": "sha256-S7GiSac0VcsB7GLrukNOyOx3UkyChvLMWC5lQW1nj0Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Drawing.wasm",
        "name": "System.Drawing.8gcjjqj0pu.wasm",
        "hash": "sha256-OG93onlStdjfWzcLq3GSd7p2posHrpoBAptujpzu/ks=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Drawing.Common.wasm",
        "name": "System.Drawing.Common.3y29mvytmn.wasm",
        "hash": "sha256-K4kz7R8w/w1hXO31g3GdLNqPgu4KDENnDuMEnMfDDnc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Drawing.Primitives.wasm",
        "name": "System.Drawing.Primitives.y2uveru2yk.wasm",
        "hash": "sha256-CiSvrtqNxb5mKdwQ7obhnTRpcvSS1e31SMHq2s6nrUc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Dynamic.Runtime.wasm",
        "name": "System.Dynamic.Runtime.023zrvzh9x.wasm",
        "hash": "sha256-yL5qlZRpb8jHMNsapIzA7qNxs9DZvvAJx3/hCobHnpM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Formats.Asn1.wasm",
        "name": "System.Formats.Asn1.znpklkbgqv.wasm",
        "hash": "sha256-EYyyGznhWECmsc5FFdH6k26PlZIvsOFYMbRyQHlFCas=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Formats.Tar.wasm",
        "name": "System.Formats.Tar.5yybjepfi2.wasm",
        "hash": "sha256-rcCcYOlxhVRG07eBm/7U2xhPK/cimbMV9phmtLI+B3o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Globalization.Calendars.wasm",
        "name": "System.Globalization.Calendars.10g6hyjy3k.wasm",
        "hash": "sha256-gb8ndHQ5edkgeIuuwJAZmpSvDSqmmSPYJbBgTzel/ps=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Globalization.Extensions.wasm",
        "name": "System.Globalization.Extensions.hrinfrismg.wasm",
        "hash": "sha256-vZkkaKr0ImZEgmnHPzo/msMMeEJQCqxSo2mTbfOzRzw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Globalization.wasm",
        "name": "System.Globalization.uvs4rah8fk.wasm",
        "hash": "sha256-xiEtuDrnECbsdmpAPAp2VzmtL1v4IfFyExR96Fud89Y=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.wasm",
        "name": "System.IO.4j645gw40d.wasm",
        "hash": "sha256-8KAD71Rcb1edG/x+Cx4wUA8/0cmGUGSciPma6jeXLXo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.wasm",
        "name": "System.IO.Compression.2z7vdt6tfb.wasm",
        "hash": "sha256-Hpsq0zZpxCksmebhGlgCLpBfZlw6fRJn/pZ/QIvF8YQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.Brotli.wasm",
        "name": "System.IO.Compression.Brotli.fv80xs79j3.wasm",
        "hash": "sha256-643QZL7ILdpDBnNsv23Zi0Cyzz5SoTjI9wGguHqusmM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.FileSystem.wasm",
        "name": "System.IO.Compression.FileSystem.88jdddz3sw.wasm",
        "hash": "sha256-aOwoN3Ibisa2gVhxzBpktC2L4SEDgvRphJChLSaTA+Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.ZipFile.wasm",
        "name": "System.IO.Compression.ZipFile.jkn9avr4vo.wasm",
        "hash": "sha256-k7XPfNshrfMVIgaiuvKd377GtI7UYYnjDqJZyUkj+jc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.FileSystem.AccessControl.wasm",
        "name": "System.IO.FileSystem.AccessControl.yme0uhbu37.wasm",
        "hash": "sha256-GjwWqllU9odjGWVpAjmupGx8/yiy5lsN2Not1a1soGg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.FileSystem.DriveInfo.wasm",
        "name": "System.IO.FileSystem.DriveInfo.qas7wf73rs.wasm",
        "hash": "sha256-/ojn2l2CNWusI2atVrRd8dbi7k4BARzLTJjRH6YgotM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.FileSystem.Primitives.wasm",
        "name": "System.IO.FileSystem.Primitives.d1jmhkzu0e.wasm",
        "hash": "sha256-H8W0uho5WxWoH8hgJZEmNbdPpPYUgD8xkWHRSKx7MMc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.FileSystem.Watcher.wasm",
        "name": "System.IO.FileSystem.Watcher.z570derp0d.wasm",
        "hash": "sha256-ucYunghHF7vLfVmQGmCY6aJsB2Kx9YKcaLYiWtXTNlY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.FileSystem.wasm",
        "name": "System.IO.FileSystem.o6jtz51fup.wasm",
        "hash": "sha256-YA3yQ7lL5H0WBYDoElU5ZeVKxPgfltC+tweV2Ybbn/A=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.IsolatedStorage.wasm",
        "name": "System.IO.IsolatedStorage.hqyeyb5ipe.wasm",
        "hash": "sha256-pxoMRl5qWZqgJSQnJ8mOHEcxOyZYWmM2Mr5oYUy+Glo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.MemoryMappedFiles.wasm",
        "name": "System.IO.MemoryMappedFiles.zhamjnusxd.wasm",
        "hash": "sha256-WZLsRujfD/qa85HQq+jH/mNvqaSC8EfcorhN7V+E+I8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Pipelines.wasm",
        "name": "System.IO.Pipelines.wrmw5vws31.wasm",
        "hash": "sha256-TDz5qGlXb4ZW8yA27qUZMYc8j1MMcr9och2a7PScXg4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Pipes.AccessControl.wasm",
        "name": "System.IO.Pipes.AccessControl.qcancj8gj2.wasm",
        "hash": "sha256-KsyFANWIAfdArrjBnJnX3LWpJAMzXAJi29tm3Kb5mgw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Pipes.wasm",
        "name": "System.IO.Pipes.zhcyguvwkr.wasm",
        "hash": "sha256-OVQT1MG8JbyAXuZTrGXlrR+u0SL2S/0PT6accLkQu54=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.UnmanagedMemoryStream.wasm",
        "name": "System.IO.UnmanagedMemoryStream.o8cpjnlard.wasm",
        "hash": "sha256-yHbYTenL8GXOuqqmxkdwREWeOUezLPi2eCSzn979C+k=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.AsyncEnumerable.wasm",
        "name": "System.Linq.AsyncEnumerable.hyc0b7okjw.wasm",
        "hash": "sha256-mSgezbBarV5SBEY+t7LObP5nQ3XgEVF6R5qAhQY90so=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Expressions.wasm",
        "name": "System.Linq.Expressions.ldibvm0efj.wasm",
        "hash": "sha256-yUg/PQ8henAChE7w0Fuf2GVEpweY2df9f20ChCg2GME=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Parallel.wasm",
        "name": "System.Linq.Parallel.2buevezvch.wasm",
        "hash": "sha256-Eggt4fURp1Dc3UevhKbp4Lj9fjdjlc8/jGqM2ifdzIs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Queryable.wasm",
        "name": "System.Linq.Queryable.cvq43g1awj.wasm",
        "hash": "sha256-+OubGWG+/zfH+Fs0lXunuGpq8wzuc4YXIfJU2fHdzDA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.wasm",
        "name": "System.Linq.lz6mvi0hm2.wasm",
        "hash": "sha256-4ROqweguD0hFn3XZ2XC+JKzncsxZNBiX2yeKPuQs9bM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Memory.wasm",
        "name": "System.Memory.3jekerw0ga.wasm",
        "hash": "sha256-cV8BYmP+pMPC5iDRi3x21fsgc2dUObz+vFs7usZY2js=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Http.Json.wasm",
        "name": "System.Net.Http.Json.8k237kkwec.wasm",
        "hash": "sha256-SFmib3glSdfRTQZAtBg0AFgrNS9J3dUgpsDmt9X9vJE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Http.wasm",
        "name": "System.Net.Http.cityx2bvcl.wasm",
        "hash": "sha256-5EKrlM9SabAClzp0n57qTFE2eaLB+jP0/z1nRz/iwQk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.HttpListener.wasm",
        "name": "System.Net.HttpListener.s1jreclykx.wasm",
        "hash": "sha256-MOeibcHl0sx5SEqNQHQhRZOR598ACQUcNnX/FVecteo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Mail.wasm",
        "name": "System.Net.Mail.cmlj4c0gto.wasm",
        "hash": "sha256-zNeqttiCsELKrGrpy/T5NQKxD09jocn8hXJv3BcfKo4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.NameResolution.wasm",
        "name": "System.Net.NameResolution.q3gf18q13l.wasm",
        "hash": "sha256-ul/5prZbC0vGrIt6UESE+4lpJo/wXYgWcu+1+gAgc5k=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.NetworkInformation.wasm",
        "name": "System.Net.NetworkInformation.xaz5tjw7d0.wasm",
        "hash": "sha256-+WqoHrz7stgVcQGbtpi7jQqel4rJ2hx2SF0HJokwHa8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Ping.wasm",
        "name": "System.Net.Ping.a40atim1h0.wasm",
        "hash": "sha256-5nvH8j0VIm/1nMN1+lVTI54Ri8D5ntm30A2fhB0hc/Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Primitives.wasm",
        "name": "System.Net.Primitives.i88i20r8sr.wasm",
        "hash": "sha256-zhN/oiyikQbwjunDNbYt4XbUn3w/DgR7jOA8zD1phRA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Quic.wasm",
        "name": "System.Net.Quic.hx06eo5xkj.wasm",
        "hash": "sha256-NQhcYyf0Hr4tfihcptLj6kjDKjbD3nIU6f7VUfP+jXk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Requests.wasm",
        "name": "System.Net.Requests.hkjxlfe5f5.wasm",
        "hash": "sha256-UdJOWmu1xm90dNiv+WiZLNoDnNaUDC8SByJPZAY/fSM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Security.wasm",
        "name": "System.Net.Security.ch34yya69o.wasm",
        "hash": "sha256-YMvWX7YyMp6BsYKuNwDVWeeq0qr6S82HoFuZJ08E3DY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.ServerSentEvents.wasm",
        "name": "System.Net.ServerSentEvents.0u7o0jk3p1.wasm",
        "hash": "sha256-qGmAP22IUTQuqxOQHHoMv2/ULrRmNDq/8/wmKKhaui0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.ServicePoint.wasm",
        "name": "System.Net.ServicePoint.xbhxnytx4l.wasm",
        "hash": "sha256-/STSB/pmoEd7eBP1ewThAYMeJGveTDOrKftoV06TyiI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Sockets.wasm",
        "name": "System.Net.Sockets.hzgzlvkc9l.wasm",
        "hash": "sha256-vZEy2LgIvPJwM0p6XiZJMoT5fvBlV6yhrIf0ZiUU2Ok=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.WebClient.wasm",
        "name": "System.Net.WebClient.84i8nwzbgu.wasm",
        "hash": "sha256-SAuUMWKRzK2wStVZ46JGxoZNUEPdH9QiUK2Psd4LYcc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.WebHeaderCollection.wasm",
        "name": "System.Net.WebHeaderCollection.326nl7s9g3.wasm",
        "hash": "sha256-ay8HWX3V52s3fo/zr8I3p76ZOdAuY0ecre1U2TCwOVI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.WebProxy.wasm",
        "name": "System.Net.WebProxy.oq3mgkkqef.wasm",
        "hash": "sha256-kKO3TH8HsBXfC92Sy938oIV1HdKMS0uVpztIaUDDeU4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.WebSockets.wasm",
        "name": "System.Net.WebSockets.2cx1v74vg8.wasm",
        "hash": "sha256-/qUjzqNF74n5FiEQpOww2wGmAOT57HohJWFc6N/CtJc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.WebSockets.Client.wasm",
        "name": "System.Net.WebSockets.Client.rafzkktw8j.wasm",
        "hash": "sha256-M2xvJviBA3wTLi4RJU+payoXVPiEryPgwyydiLJ04WA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.wasm",
        "name": "System.Net.w1hpj3bo6r.wasm",
        "hash": "sha256-bE5JhulvXs+VFtgIW1IMKhELHc5gUDe0EPzNiDtoZpE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Numerics.Vectors.wasm",
        "name": "System.Numerics.Vectors.oyv7biw9ux.wasm",
        "hash": "sha256-cK8cVJbRJLyKnySzrxM3O/YJXg8JlRarWMJAYxG2QcU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Numerics.wasm",
        "name": "System.Numerics.lyu36wqn19.wasm",
        "hash": "sha256-g5p6oqfQl2OUj/Or3JJiTCnkMEiv5Yl6r3Aki08Xdvc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ObjectModel.wasm",
        "name": "System.ObjectModel.kxu80demuu.wasm",
        "hash": "sha256-aP1cHdnCvYP1drrICHt/oMYUqSOQVVVlW5fxAzhNYuE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.DataContractSerialization.wasm",
        "name": "System.Private.DataContractSerialization.ao8k9iu77a.wasm",
        "hash": "sha256-6jmrkfhbtGkAs8F0pAMzP0ymubYGNI7n7a1J2xY4y98=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Uri.wasm",
        "name": "System.Private.Uri.itn0n2zjd0.wasm",
        "hash": "sha256-atgB1gY60K6/ntpQXNN8jsGpnw185Y8zBq6WNNt5LKg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Xml.wasm",
        "name": "System.Private.Xml.19nzpxrx8w.wasm",
        "hash": "sha256-i9KhUrIpGVyhMpgL4GV/JczYp4NdDf+o18PilPlm48E=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Xml.Linq.wasm",
        "name": "System.Private.Xml.Linq.tw5eq1hjvo.wasm",
        "hash": "sha256-beNcumb5VoNQYWiR9bxGCuUPTD15NSD6EqARdh9HU/Y=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reactive.wasm",
        "name": "System.Reactive.drqaelofxl.wasm",
        "hash": "sha256-+ScSClhQuiJQu2gf7sGnB/ZKQFNojmrCzv/fAzn3KPw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.DispatchProxy.wasm",
        "name": "System.Reflection.DispatchProxy.7lajnj8s8c.wasm",
        "hash": "sha256-W9+0UBxtz0EvZ9xgkmng+tHOhXWW2GBKdjcZAVbc6YE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Emit.wasm",
        "name": "System.Reflection.Emit.7liz9zln06.wasm",
        "hash": "sha256-JZK/huZ9doWYIPv0yzev1wYuhUXAIQrxo2NCyu7A1OA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Emit.ILGeneration.wasm",
        "name": "System.Reflection.Emit.ILGeneration.1ut3qgacw8.wasm",
        "hash": "sha256-yfOooDyPto0Lng17CwgJ55NXqhWz+t9SfhiGJeT79HY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Emit.Lightweight.wasm",
        "name": "System.Reflection.Emit.Lightweight.d0gvllb1jj.wasm",
        "hash": "sha256-MwR9j/suPuezXSGdbNPU23VHe5GIBf6R9Bs1p9BtP3k=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Extensions.wasm",
        "name": "System.Reflection.Extensions.zr5bhwfczd.wasm",
        "hash": "sha256-/xKkpAsqwNQ8soTda97aUed6al1D9dGTdTA2lRvrLxM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Metadata.wasm",
        "name": "System.Reflection.Metadata.ff6np6mz2j.wasm",
        "hash": "sha256-tc7STzUxi0O+S8ivukRXeP7SVKVDwsqSYPtr8b0nhqQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Primitives.wasm",
        "name": "System.Reflection.Primitives.igpmvwv78c.wasm",
        "hash": "sha256-niYygMlK1HnbEugbNiTeBc7EkQIXXPnnZkgZUqq3qLs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.TypeExtensions.wasm",
        "name": "System.Reflection.TypeExtensions.qouojzlnp9.wasm",
        "hash": "sha256-pt8RKbO93gvHhs2DfKf4ctYNpnH6Ec2j6l+M0SgdAS4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.wasm",
        "name": "System.Reflection.mk3jyxvh13.wasm",
        "hash": "sha256-1k6WUVW1mJwUKSKAwvKj5wtG/WTYf3scCmFyc4uRtWs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Resources.Extensions.wasm",
        "name": "System.Resources.Extensions.h420yd526h.wasm",
        "hash": "sha256-wZDDhuYAwClwePytlLJ1r4Aow96/XkyYITmaQjxB5V4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Resources.Reader.wasm",
        "name": "System.Resources.Reader.41ppfyz2vc.wasm",
        "hash": "sha256-+DE3yDPtRF5JM8T52ycQt1qSAJ0vbjX5nu4Zoj+SuZ4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Resources.ResourceManager.wasm",
        "name": "System.Resources.ResourceManager.sfwvfmw1ea.wasm",
        "hash": "sha256-CIM4gUMmot/+RliOJhpf94ZlIsAr4YiEfWvOAXbovMU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Resources.Writer.wasm",
        "name": "System.Resources.Writer.3uvs9tp38z.wasm",
        "hash": "sha256-Kx23sKGyz/kEgJVd1ZRRDUI93A95CFpkYIzCejkCxtQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.CompilerServices.Unsafe.wasm",
        "name": "System.Runtime.CompilerServices.Unsafe.puz1xm53oc.wasm",
        "hash": "sha256-P6tIzozdCJqHiy0zGjuPtLVfOYb5yb0QPwFMwMUn4AY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.CompilerServices.VisualC.wasm",
        "name": "System.Runtime.CompilerServices.VisualC.9qdau315py.wasm",
        "hash": "sha256-/4HwDzaQ5a7S2V+Qzr94Nz3zi91/aZ30tRhta0XKqYE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Extensions.wasm",
        "name": "System.Runtime.Extensions.i7gntui96b.wasm",
        "hash": "sha256-Mnk3n1eK4eqTEZkm/ZfT7LQmGdwGAoF2anLqrZbDPbM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Handles.wasm",
        "name": "System.Runtime.Handles.00oxj4o6ov.wasm",
        "hash": "sha256-QIVT4ckpz17JUAEsvaaKYYH35Ou8V9ErIKuwNI05CZU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.InteropServices.RuntimeInformation.wasm",
        "name": "System.Runtime.InteropServices.RuntimeInformation.my7qeqbhfh.wasm",
        "hash": "sha256-zhC5CCW9nCkqGXdOfIfwawjFHi5JjlQNnD8pGkdfWoE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.InteropServices.wasm",
        "name": "System.Runtime.InteropServices.vqp80tnwc7.wasm",
        "hash": "sha256-Vzg1KlSYjHMKB8KW97OKaZsRei8Q593KoPgnQe1n5Uo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Intrinsics.wasm",
        "name": "System.Runtime.Intrinsics.u0samjybfo.wasm",
        "hash": "sha256-Xo6xM56FQTZ30/6Vggp3fYR+4FxV/KmMooNBNpt+w6I=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Loader.wasm",
        "name": "System.Runtime.Loader.n6zxw1b98a.wasm",
        "hash": "sha256-X+RMnAfTVC4+TDBQoS/h7LhyPBwJjorpoELFBfV7248=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Numerics.wasm",
        "name": "System.Runtime.Numerics.o40sjhz59x.wasm",
        "hash": "sha256-qDYDPlthKrlj2dnaBsNoMK5X/nxKhTIdUiQ9ugVTuW0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Formatters.wasm",
        "name": "System.Runtime.Serialization.Formatters.1esvva4te3.wasm",
        "hash": "sha256-h/dAFonk9eigobeeUZLSOdbPf0Gz2On4fYCbW4hGwmk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Json.wasm",
        "name": "System.Runtime.Serialization.Json.4n7u09advh.wasm",
        "hash": "sha256-MCbIAsHQBugo8qpJp51Z0UHPviJI013X8cqoUXXfa7M=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Primitives.wasm",
        "name": "System.Runtime.Serialization.Primitives.xs2bo91wvk.wasm",
        "hash": "sha256-DzErfw7r3LKDKFr89PBxgF+SxxMEJcb6lGnpmdvn3A8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Xml.wasm",
        "name": "System.Runtime.Serialization.Xml.ny21jmziti.wasm",
        "hash": "sha256-P4CWph0hEmcsfiwKYkRp4me/5o8puJqWxBfj0AcLfWk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.wasm",
        "name": "System.Runtime.Serialization.qcxaur5wqs.wasm",
        "hash": "sha256-pr4aZh6q8+LiDRjtVtJkkLeCZuVgRF9iOhru3cXTfUY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.wasm",
        "name": "System.Runtime.odq454d6j9.wasm",
        "hash": "sha256-dJxxG1+xZ1brKjnFZcEsO69maKgVzhnw94U5gUor6Gs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.AccessControl.wasm",
        "name": "System.Security.AccessControl.8yfnzmqzaq.wasm",
        "hash": "sha256-gCsoRrVikUlzNxsrNbZ+6QVGT2+XUPKJWx95hBKEl/w=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Claims.wasm",
        "name": "System.Security.Claims.8ogy0860de.wasm",
        "hash": "sha256-GPcSbHwWVLH1fca0stI2MqZE+xsVLrA/kskuiQycJ4Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.Algorithms.wasm",
        "name": "System.Security.Cryptography.Algorithms.8zijp1svfw.wasm",
        "hash": "sha256-LDzYiGnR0brxYcFNo0ocBo59dYZZb9MBe3ild2znJSc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.Cng.wasm",
        "name": "System.Security.Cryptography.Cng.yxagtt77no.wasm",
        "hash": "sha256-ZpyrNDtC+3ti6m3d6mhH1as/m+ZFqLtc2bZBFNtuVH8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.Csp.wasm",
        "name": "System.Security.Cryptography.Csp.uhfbvvau84.wasm",
        "hash": "sha256-Mp7WemXwikMFbyEZOblQA8l8wqAZ5jhmh6QboTii32o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.Encoding.wasm",
        "name": "System.Security.Cryptography.Encoding.2ywrusvy8q.wasm",
        "hash": "sha256-duIpVcBN7g31lDD77yYuAG3xM3U5GZtePHznUiVz0m4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.OpenSsl.wasm",
        "name": "System.Security.Cryptography.OpenSsl.oa268u7i92.wasm",
        "hash": "sha256-4H6EYIOcQKX7spTR+G74jmTVJg6+MgAtY9g8b22KcFg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.Primitives.wasm",
        "name": "System.Security.Cryptography.Primitives.5uhvqe8puk.wasm",
        "hash": "sha256-DX5w2C46UrZY0ubTaEn3lxmlfYnJFHE1TN8wI4qsMgc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.ProtectedData.wasm",
        "name": "System.Security.Cryptography.ProtectedData.15bha6zbjy.wasm",
        "hash": "sha256-W5tMSDmYEB2xQ9h2R6YF/fHX/B7hkusJMm/i/cqRTV4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.X509Certificates.wasm",
        "name": "System.Security.Cryptography.X509Certificates.6i7pxilopt.wasm",
        "hash": "sha256-wnRBilXju2GhWZsScBp6pBZ8dNSD81FuW+DXXZxUu1I=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.wasm",
        "name": "System.Security.Cryptography.d0quqk1iyr.wasm",
        "hash": "sha256-LZkStq41MX59Tptph5/JGQRSG8B5+a1ldrb0QpNVB6I=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Permissions.wasm",
        "name": "System.Security.Permissions.m9hbxwov85.wasm",
        "hash": "sha256-Kq3ZZSKQkbZ2H5kMG7Mqq4KycqKa35CdKT9M9DM6w4U=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Principal.Windows.wasm",
        "name": "System.Security.Principal.Windows.gpqua4rz29.wasm",
        "hash": "sha256-TNOfYwVGiv+d/Xk6LXgrOyiDkxGzv9NyH2IGyuVIMR4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Principal.wasm",
        "name": "System.Security.Principal.i20kfklf3g.wasm",
        "hash": "sha256-Wow4n7EbYXc1HfQqHQu1Jhl7f8vCrcVNkFAXLZvyvTg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.SecureString.wasm",
        "name": "System.Security.SecureString.x2y8tzci3c.wasm",
        "hash": "sha256-d7gVbpst3NnK88ZEcpaJoH5w3zfmsRXDJQvvqbL+RVY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.wasm",
        "name": "System.Security.e83vvh6sl2.wasm",
        "hash": "sha256-HuX7LBO46UViQb2u/0vvs+XLxgVQE+KVlOs6UH+Gw0g=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ServiceModel.Web.wasm",
        "name": "System.ServiceModel.Web.poglliv2no.wasm",
        "hash": "sha256-R4tw5kXVUZ+Qsq6/4Xkq7Qedv5pzouTdXVacoeG0V/0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ServiceProcess.wasm",
        "name": "System.ServiceProcess.yro5uj2opo.wasm",
        "hash": "sha256-Usmz3M5o5WSM1Sc2dU3njqXU1MeSMvgc8oaxTK8gBGo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encoding.CodePages.wasm",
        "name": "System.Text.Encoding.CodePages.2wchx40i7z.wasm",
        "hash": "sha256-giy48Parl41Ox46pfccHLTXlypNv3dXKu+yRKiWcJC0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encoding.Extensions.wasm",
        "name": "System.Text.Encoding.Extensions.8utaw61kn5.wasm",
        "hash": "sha256-kL2CE+V74498vfb1kuYNsti2ylJDViK5eEnTmKJv+54=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encoding.wasm",
        "name": "System.Text.Encoding.cxdyfepnpj.wasm",
        "hash": "sha256-AM6c0tx+lLrDZ6OZN3oLhe6nC21LZ6j1sEKHYC0gX9Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encodings.Web.wasm",
        "name": "System.Text.Encodings.Web.37ehbi7cye.wasm",
        "hash": "sha256-H0VkIeM0rlJX/Veq/zPhEyH7piskV3JOKngzP/djJE8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Json.wasm",
        "name": "System.Text.Json.ixiaa8hd3h.wasm",
        "hash": "sha256-qsdCabiszf3qHMsvYiRuxa71WNCjNekpqBRHs8469vQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.RegularExpressions.wasm",
        "name": "System.Text.RegularExpressions.2fhfmkk6vf.wasm",
        "hash": "sha256-7msxYtUuVfZjmYO75wrZGO6rapEVdjT7RCgt6Y9EyGc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.wasm",
        "name": "System.Threading.6pry74a050.wasm",
        "hash": "sha256-ulcimTPEQYo+vzvzjcj97+VdF4TYl1acGxv2F4064sc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.AccessControl.wasm",
        "name": "System.Threading.AccessControl.76zfdfy8go.wasm",
        "hash": "sha256-z0hatN5fwMJgHdz4QHMG8+e9nDMcL+jWvoEY/4+50K8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Channels.wasm",
        "name": "System.Threading.Channels.gpxnr01lmp.wasm",
        "hash": "sha256-ZFVZEgZJ4fAvrAa84Zoj5j1OLX+7VhF5jq0XJoGdPX0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Overlapped.wasm",
        "name": "System.Threading.Overlapped.aqpl8tzdyc.wasm",
        "hash": "sha256-goELF2wzUusbMI1lATDF4myHde5GT8E3X4qmtHFNOeg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.Dataflow.wasm",
        "name": "System.Threading.Tasks.Dataflow.ytwttt7fzh.wasm",
        "hash": "sha256-dkzneK8QtxQQ6flGCimi/ofDvV+VowH1LcxXReD935E=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.Extensions.wasm",
        "name": "System.Threading.Tasks.Extensions.0deavcb00v.wasm",
        "hash": "sha256-NEYDYaYBpgF3So553VgamGq3KZUL32UDvps2Y/dLdis=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.Parallel.wasm",
        "name": "System.Threading.Tasks.Parallel.tjwtxugso0.wasm",
        "hash": "sha256-97xFkzLi4/C0otWqBmpjcxGrMAFBRhu4b29HpMXaTZw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.wasm",
        "name": "System.Threading.Tasks.fu4qd5h3qy.wasm",
        "hash": "sha256-+TOeSAsKIGT1UJBmjHRX61gRkGIQ+0vEtBQr8k+7HlI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Thread.wasm",
        "name": "System.Threading.Thread.klaua2by9m.wasm",
        "hash": "sha256-KFOGstVXEAMQHwhfWTIZj3A5HwiPl+KlgZetTOfe0cY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.ThreadPool.wasm",
        "name": "System.Threading.ThreadPool.mkzm212njz.wasm",
        "hash": "sha256-atxs3p14Jj6AnnJM+AEdbVR3Le/oST9CtB8bz0mcM5U=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Timer.wasm",
        "name": "System.Threading.Timer.yp49llu5h2.wasm",
        "hash": "sha256-2rbLSkDUh/CdKMUXN12xK590P5PdtQ6+D/HUF1Y2zNY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Transactions.Local.wasm",
        "name": "System.Transactions.Local.vz6cgeh4w7.wasm",
        "hash": "sha256-lTkTSlfTqj7jJYcWurf1fHboUKCwCLKHNyJpgQFQy1E=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Transactions.wasm",
        "name": "System.Transactions.ipwm3rsy66.wasm",
        "hash": "sha256-6kE7mivQoxfhMTVAkmYi5VlsQhqXLYcImAuc9D7Skjo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ValueTuple.wasm",
        "name": "System.ValueTuple.7ol6ljuhx8.wasm",
        "hash": "sha256-yctvPmRbtoHiPpM5pKnajABrjRQR9a3b+HPWHyga2kY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Web.HttpUtility.wasm",
        "name": "System.Web.HttpUtility.itlvncjd96.wasm",
        "hash": "sha256-Nhc8L23J1+APB7ju97KZlDcnoEa6XvC6p4LluoAjVkY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Web.wasm",
        "name": "System.Web.np8uzzakvl.wasm",
        "hash": "sha256-m5aBz++zzF8tNzlN4+R5pPqGhS882Pxw4KwoakadVrI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Windows.wasm",
        "name": "System.Windows.18nzio44t3.wasm",
        "hash": "sha256-r62brvRKMGvdcIg3SIbWWi7k8/NqCfpn+LPqnwitvKE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Windows.Extensions.wasm",
        "name": "System.Windows.Extensions.quwz1fxlaw.wasm",
        "hash": "sha256-Eh4ZyIvqo34A19igrKJBmrt1iW+vrM/Cjy/zUQHTPe8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.wasm",
        "name": "System.Xml.45jprtg1pa.wasm",
        "hash": "sha256-KPyqHJl+ZMo3c6PCl2vPaQGBdD8P4KUBxsUkvR90/hk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.Linq.wasm",
        "name": "System.Xml.Linq.v9v8udncm1.wasm",
        "hash": "sha256-rQ0vSy0Z16GnVFmS/wCs02+prtLem1HSrbHdfWIzNK4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.ReaderWriter.wasm",
        "name": "System.Xml.ReaderWriter.a9mr08uzci.wasm",
        "hash": "sha256-bmkzRCDnwMgFnyZvQKnjJh5rdfZJtZ4nirjaJufIrqk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.Serialization.wasm",
        "name": "System.Xml.Serialization.03juoguwgc.wasm",
        "hash": "sha256-HPQyo7cnsrAul9FfHP0JLVYXonc/DYwM87t6IXA4jPc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.XDocument.wasm",
        "name": "System.Xml.XDocument.c9dhjnzqjb.wasm",
        "hash": "sha256-YLwIjMK8F1xq/FIgzG6+jiJStkbL+5sLQLdzD2sgpa8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.XPath.XDocument.wasm",
        "name": "System.Xml.XPath.XDocument.138b12i1pm.wasm",
        "hash": "sha256-rUb1sC2EPqYnLys9V/C0H/313aLByFrqm0qT6zJpUcE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.XPath.wasm",
        "name": "System.Xml.XPath.pvxm9341ce.wasm",
        "hash": "sha256-+5VqXw8HmAjDvyi2+y7K95eMarW7XY4wPW0bh7x8L/I=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.XmlDocument.wasm",
        "name": "System.Xml.XmlDocument.450wsyxvl0.wasm",
        "hash": "sha256-uPwda+H2hEsj0Jbt2bXGnSVbjkdbRsFU3TjifwMLQi4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.XmlSerializer.wasm",
        "name": "System.Xml.XmlSerializer.y3t2n0tce7.wasm",
        "hash": "sha256-IrnG91lN1tfKsabToUYpusckhRr9wXb3Jxm6CqsOTJE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.wasm",
        "name": "System.gjriafnqmz.wasm",
        "hash": "sha256-7DGslki9AfExPpvTTP5Lou+UeVMPtxC7TMoTwvo5sKY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "TextMateSharp.wasm",
        "name": "TextMateSharp.3olxz8ckyu.wasm",
        "hash": "sha256-DTHW/5xjq6HmQ3YFCgn8dIPYF72lJo2iCRO1LsvRSY4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "TextMateSharp.Grammars.wasm",
        "name": "TextMateSharp.Grammars.t0mspxa7kp.wasm",
        "hash": "sha256-fZt2Ipu6LgEThlfY6KRjc6oDnbLFgFvhLQMPgaT9JwI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "WindowsBase.wasm",
        "name": "WindowsBase.8zsce9qxfb.wasm",
        "hash": "sha256-tI94+TOjFqR3Z880lf+2Ye9R7uo6OxX3t73SZC3Wodc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "mscorlib.wasm",
        "name": "mscorlib.8ptuc08eli.wasm",
        "hash": "sha256-3H2G5ngT7eIaYsTDs+CoGgdvfTchxty175b/4H9+qxI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "netstandard.wasm",
        "name": "netstandard.eymp85mooh.wasm",
        "hash": "sha256-dtyh2m2c4Ak4ASCotVspn+EmOiuXkI5zqS2q90qHb+s=",
        "cache": "force-cache"
      }
    ],
    "satelliteResources": {
      "cs": [
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.5eeka80b2k.wasm",
          "hash": "sha256-3a+ij+iseYpgBvxJ7vnUKoWvdl+mZ0TwKXTraobUpis=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.resources.rmqmof73hs.wasm",
          "hash": "sha256-UxRMt9B1Hmgrh5DAepookxeNMuMO2LdHrsFaUG6rRDM=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.Scripting.resources.hxd99z9rfi.wasm",
          "hash": "sha256-xRMB8gWGAfVhzhVVgamWokmSW2yk005q9NFpTIix3BA=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.resources.wasm",
          "name": "Microsoft.CodeAnalysis.resources.xh2h9jttco.wasm",
          "hash": "sha256-5QsL5qZBucoLvm3HqFueiqftQ6TGC0gJUZgd+tLsuvk=",
          "cache": "force-cache"
        }
      ],
      "de": [
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.g2ad9uhyj7.wasm",
          "hash": "sha256-OOWCOAQCqJRgChUKEGsf3hmHagIYl4liwnI6ARAbFsw=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.resources.0sjl6dozm1.wasm",
          "hash": "sha256-YLE5mCSsAkMq15cHuuNukDQHQki/wACkjjluJhPIf1A=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.Scripting.resources.wflgvl89v3.wasm",
          "hash": "sha256-jIPpdT5VAwBcT+jUg46fJDSS/toUEem2ZadRv+JmVeE=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.resources.wasm",
          "name": "Microsoft.CodeAnalysis.resources.w1is3e68l8.wasm",
          "hash": "sha256-ZIXn6Rrk56QVMbr9B+IbqDL19dogtJSq47mlRUr4R9I=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "PdfSharp.Charting.resources.wasm",
          "name": "PdfSharp.Charting.resources.0pzesx4pvg.wasm",
          "hash": "sha256-zBOpRbINF37lbdQAj2Zzzwcj8FFW994ZQlptIfhcNgA=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "PdfSharp.resources.wasm",
          "name": "PdfSharp.resources.8aymcuz4wc.wasm",
          "hash": "sha256-EEtbueezwzRJ89tL+xT0gOoype7kdhS+nnp4Gvqj36k=",
          "cache": "force-cache"
        }
      ],
      "es": [
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.b3innhb2em.wasm",
          "hash": "sha256-V0bbAENtrEAA1xDI6UsSXb4G9rGQStG3yWqJSTYKE2s=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.resources.l0slkn8tzt.wasm",
          "hash": "sha256-626euG12bRLuseBrvBQfRHfA3jeTFzz3NmsJQc/jSTc=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.Scripting.resources.5cwqwzf0os.wasm",
          "hash": "sha256-NaLF92hdC6d11/P0o90zymCqFPmyryHQS89yPzD5coo=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.resources.wasm",
          "name": "Microsoft.CodeAnalysis.resources.capy0fg3mn.wasm",
          "hash": "sha256-fO+ZGbGREznBU88K9VAYvvV1XH2ZXkfdCSqThYp6xXo=",
          "cache": "force-cache"
        }
      ],
      "fr": [
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.gay7i1276a.wasm",
          "hash": "sha256-WH9d29D9y/8KvvSQVXIzzy5qC9sGt225RfChADCvdPw=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.resources.p120v92ljy.wasm",
          "hash": "sha256-Xa2eHsV8UU99GG6Y/SlJcrBwsJkc8ompVKMIr3IIj5w=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.Scripting.resources.pmrabhjlvz.wasm",
          "hash": "sha256-YelKorPOt3ECvzETpdI+1NTax35LkWsCKF0ONHx+vJA=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.resources.wasm",
          "name": "Microsoft.CodeAnalysis.resources.nmqnxddoau.wasm",
          "hash": "sha256-cTRr7RJWW7CdlbxXNK2p3f9OvU3mb7p9QTAboao9VhY=",
          "cache": "force-cache"
        }
      ],
      "it": [
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.3fwpgx35rn.wasm",
          "hash": "sha256-oetI+sp6sPn0EABVQKCZV7o5P6ZmWRIdM92c2fcy+7U=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.resources.9j17t7bxbo.wasm",
          "hash": "sha256-hawtFRR5ONFdmfFJ385bbuLS6Ewu6RbPgxFIxDBBRlU=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.Scripting.resources.x96r8n6833.wasm",
          "hash": "sha256-e4NfcZE44fjUsiHKFtxGii0/V5gljs4gq5UpZkv2X80=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.resources.wasm",
          "name": "Microsoft.CodeAnalysis.resources.vod4qvb9u3.wasm",
          "hash": "sha256-T0RHyFthP4pJMe8u8dfsYttBuZDpj4ed4VOLuGuCeBo=",
          "cache": "force-cache"
        }
      ],
      "ja": [
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.ey15evjwn3.wasm",
          "hash": "sha256-JnPt6ibamnA4+n68jqkvjYBYsP3qZl+/O5gV3OvKViI=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.resources.szsfk14jr2.wasm",
          "hash": "sha256-iHhEYkmMWDAinAXvFkAbxce1WaEU0i2y0gyrAxcqqYc=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.Scripting.resources.lblgdp8moo.wasm",
          "hash": "sha256-D//SFcylr5H7rfzVKJ6XEIPEuApBffm5RRtVMxvj0Io=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.resources.wasm",
          "name": "Microsoft.CodeAnalysis.resources.lhixzqvyrl.wasm",
          "hash": "sha256-Jwn8N7CLQfiJKmSnPFsGcshk95z2BsUOm8puX6Y5h+E=",
          "cache": "force-cache"
        }
      ],
      "ko": [
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.dga4f3tl39.wasm",
          "hash": "sha256-E4dVaFojMbigqneMsYLeExX2HUjx4aEY21bVZq2gTgU=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.resources.vux0mam71w.wasm",
          "hash": "sha256-mfZQegl3mnjaMSVS0Viqnnn1u8kS9FthwkZ/l5HRTdQ=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.Scripting.resources.wqt817e6oj.wasm",
          "hash": "sha256-aDpZOhFBRTPuo4m+fBsmqdldsQJzvsIwD3ZummWkuqs=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.resources.wasm",
          "name": "Microsoft.CodeAnalysis.resources.pocz1jlibi.wasm",
          "hash": "sha256-BzizxrFl+9WxakIb1YhSTPdbg5gLzt3hltS0FyEDWOI=",
          "cache": "force-cache"
        }
      ],
      "pl": [
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.el2dvuxcrq.wasm",
          "hash": "sha256-YhUiafgXsFEUAyDu/xMh752AQVHeWsp9XziXRdNDnic=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.resources.jd8zx0nos5.wasm",
          "hash": "sha256-J0aXYs7ezPhHf3hazrz18NHj9Ke/u20LwC+SzHV5soA=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.Scripting.resources.v4a3iyum02.wasm",
          "hash": "sha256-8S/xd2u8Jpm399cgSD6twHbXDHmm7iAtgQkn/sq8GdY=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.resources.wasm",
          "name": "Microsoft.CodeAnalysis.resources.78npgullks.wasm",
          "hash": "sha256-Keb4WJgDJG7390G4nKmm9ats3l6W9dUtxceLu0uGRO8=",
          "cache": "force-cache"
        }
      ],
      "pt-BR": [
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.fqg4ykos6a.wasm",
          "hash": "sha256-t991jWezRZcuSWeKWMKLEDWWW7RLmEPM1HbErGBp3no=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.resources.n09flat7cu.wasm",
          "hash": "sha256-Zwhci5dC3gVirtRkh3bpdZWiVYwWEogHb+Tcgz3R8Jo=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.Scripting.resources.l0xek5arif.wasm",
          "hash": "sha256-WzkatZPcpn/M8vst7BzTf/K0El0Ls60/8yY3Za3glFU=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.resources.wasm",
          "name": "Microsoft.CodeAnalysis.resources.xppfo1vvpj.wasm",
          "hash": "sha256-9ZuelbiWmC9EmKdx5dv/cMD2F0jL8MYDZHulDBTLyGU=",
          "cache": "force-cache"
        }
      ],
      "ru": [
        {
          "virtualPath": "DemoCenter.resources.wasm",
          "name": "DemoCenter.resources.phbc7226u8.wasm",
          "hash": "sha256-LRRzfXCKh0Y50Ckj1/Pf/SYcvqxgFalQ5pq5fRTRJ20=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Avalonia.Charts.resources.wasm",
          "name": "Eremex.Avalonia.Charts.resources.qkjxunlx4p.wasm",
          "hash": "sha256-pvTnCb9r6GS3vWxJ6BFDxvnY5NxQ0lhIkfQK0XOiO90=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Avalonia.Controls.ApplicationServices.resources.wasm",
          "name": "Eremex.Avalonia.Controls.ApplicationServices.resources.hy7dl2hrin.wasm",
          "hash": "sha256-d0lYZkAdnV2QBle09ovEq5DZI50ELG/6lofuEQpfqww=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Avalonia.Controls.resources.wasm",
          "name": "Eremex.Avalonia.Controls.resources.2cbr1yvr7c.wasm",
          "hash": "sha256-IosxKyJqwLVGAjPqP4619qrdBz/6hgBTWz5kSRJbwa0=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Drawing.Skia.resources.wasm",
          "name": "Eremex.Drawing.Skia.resources.o71i8g0hug.wasm",
          "hash": "sha256-pF40mvHNhRZjNS5MGLOT+iimqtV8Hlu+wJpRzZQhFRI=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.zkee0ki897.wasm",
          "hash": "sha256-rc6qr1aj57bFtWHiROwTo4fgQ72XUbFj6IlS4Sqs+9I=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.resources.vlfaomuerx.wasm",
          "hash": "sha256-g/1S1bSEHUdlJC84RW2VZALn8m/KRLuD2mIUqlPxhkM=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.Scripting.resources.ntmmqiutyx.wasm",
          "hash": "sha256-9eC+u2T7f/m1OIl9ZxayWWR89DIRZZUh0hQJkTguf7A=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.resources.wasm",
          "name": "Microsoft.CodeAnalysis.resources.52y6h4yshg.wasm",
          "hash": "sha256-7VKj0AwVeBdp+4mKhjt0rKvjZhvkaQ42ZSOX8tM9Ay8=",
          "cache": "force-cache"
        }
      ],
      "tr": [
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.dhjpxnqcij.wasm",
          "hash": "sha256-H7T6PSHQKpyJ41f6EcgW5CbRkyXb8HLwiItJQRxVku8=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.resources.g3x167iair.wasm",
          "hash": "sha256-zD+x10sv9K9kWe2gDmdB7UOqFAtbyQ73po/bhVIl1Gg=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.Scripting.resources.asg5lt8rbj.wasm",
          "hash": "sha256-xhLNv7c2piKOHh5MRa4j2SI8hPTylstOkpE3nRHrWVc=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.resources.wasm",
          "name": "Microsoft.CodeAnalysis.resources.0c9krrb2vf.wasm",
          "hash": "sha256-QON+pGpDL0Z0c94paTQ0ilrQQYi1XCtY6TskI8mfdCE=",
          "cache": "force-cache"
        }
      ],
      "zh-Hans": [
        {
          "virtualPath": "AvaloniaEdit.resources.wasm",
          "name": "AvaloniaEdit.resources.xs9sdbpi0z.wasm",
          "hash": "sha256-oWmLM8dsezI2HlxX8tcaXwt4V3mgeekes29eLTmgDjA=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "DemoCenter.resources.wasm",
          "name": "DemoCenter.resources.1lisxrq4zh.wasm",
          "hash": "sha256-6wVHWFRKAazgZ0tdJHN0k0W8gFzkmm0XZMMl6YeLIBo=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Avalonia.Charts.resources.wasm",
          "name": "Eremex.Avalonia.Charts.resources.siwsk383tq.wasm",
          "hash": "sha256-pFNNOP6c+7gAf7xKQrx9tTYD2hTm2OOSk1KnRjMNqRE=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Avalonia.Controls.ApplicationServices.resources.wasm",
          "name": "Eremex.Avalonia.Controls.ApplicationServices.resources.to530g6r68.wasm",
          "hash": "sha256-jZ9NXJMiEMRDY/uRrghzp+6w3UMAje2fQMaK5s6RHRg=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Avalonia.Controls.resources.wasm",
          "name": "Eremex.Avalonia.Controls.resources.azfrm2mnfm.wasm",
          "hash": "sha256-Wjj5fGKeLVf+9iE2KZ+RLlbQXE3sftpSkPyT+5rjzUQ=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Drawing.Skia.resources.wasm",
          "name": "Eremex.Drawing.Skia.resources.107v0dr4xi.wasm",
          "hash": "sha256-z8aWSonhAujmEzc2LTZkrbj98puEfBv1dHuoq9hFuu8=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.6hjksl81t4.wasm",
          "hash": "sha256-mm+vcEUHF8Qy5F2p3tKnFhlAvdpEcC9YwqDcAmsitHc=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.resources.8qy3sudi65.wasm",
          "hash": "sha256-cAlywYzC+FSN/GK1Mtl5lEE+qj9QO2lwGZ3gKbLEoK8=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.Scripting.resources.fn7b0kh8zi.wasm",
          "hash": "sha256-hSQ2g/1pmjWczzlfLJHJJNv461WeGFiqJObSoDMbGMI=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.resources.wasm",
          "name": "Microsoft.CodeAnalysis.resources.77hjznamuh.wasm",
          "hash": "sha256-07xl9XGknpkzhOphQLZ1POMHvg5Oi9KSC/fJmL2yC54=",
          "cache": "force-cache"
        }
      ],
      "zh-Hant": [
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.Scripting.resources.gu9c7nwm0c.wasm",
          "hash": "sha256-KKADG1XcTaPIC6yHrlrOfIR9dFsVjqt6v1Z7XHQDMvY=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.CSharp.resources.wasm",
          "name": "Microsoft.CodeAnalysis.CSharp.resources.khqca3r4at.wasm",
          "hash": "sha256-GPc21nJym7RqhqgGQ903tVKyyjxaE5zMGZQaKV+ki40=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.Scripting.resources.wasm",
          "name": "Microsoft.CodeAnalysis.Scripting.resources.gpu9vtflii.wasm",
          "hash": "sha256-tEY9z6CKh84IXcyp1UcYavKv7d4N7r3juBZFh4jNZPM=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Microsoft.CodeAnalysis.resources.wasm",
          "name": "Microsoft.CodeAnalysis.resources.2ms5iuyvlm.wasm",
          "hash": "sha256-GopXaFatWK5WXpaHHw7X+IKagVJbQAxkJiwuVHs4SME=",
          "cache": "force-cache"
        }
      ]
    }
  },
  "debugLevel": 0,
  "globalizationMode": "sharded",
  "runtimeConfig": {
    "runtimeOptions": {
      "configProperties": {
        "MVVMTOOLKIT_ENABLE_INOTIFYPROPERTYCHANGING_SUPPORT": true,
        "System.Diagnostics.Debugger.IsSupported": false,
        "System.Diagnostics.Metrics.Meter.IsSupported": false,
        "System.Diagnostics.Tracing.EventSource.IsSupported": false,
        "System.Globalization.Invariant": false,
        "System.TimeZoneInfo.Invariant": false,
        "System.Linq.Enumerable.IsSizeOptimized": true,
        "System.Net.Http.EnableActivityPropagation": false,
        "System.Net.Http.WasmEnableStreamingResponse": true,
        "System.Net.SocketsHttpHandler.Http3Support": false,
        "System.Reflection.Metadata.MetadataUpdater.IsSupported": false,
        "System.Resources.UseSystemResourceKeys": true,
        "System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization": false,
        "System.Text.Encoding.EnableUnsafeUTF7Encoding": false
      }
    }
  }
}/*json-end*/);export{gt as default,ft as dotnet,mt as exit};
