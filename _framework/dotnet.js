//! Licensed to the .NET Foundation under one or more agreements.
//! The .NET Foundation licenses this file to you under the MIT license.

var e=!1;const t=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,4,1,96,0,0,3,2,1,0,10,8,1,6,0,6,64,25,11,11])),o=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,15,1,13,0,65,1,253,15,65,2,253,15,253,128,2,11])),n=async()=>WebAssembly.validate(new Uint8Array([0,97,115,109,1,0,0,0,1,5,1,96,0,1,123,3,2,1,0,10,10,1,8,0,65,0,253,15,253,98,11])),r=Symbol.for("wasm promise_control");function i(e,t){let o=null;const n=new Promise((function(n,r){o={isDone:!1,promise:null,resolve:t=>{o.isDone||(o.isDone=!0,n(t),e&&e())},reject:e=>{o.isDone||(o.isDone=!0,r(e),t&&t())}}}));o.promise=n;const i=n;return i[r]=o,{promise:i,promise_control:o}}function s(e){return e[r]}function a(e){e&&function(e){return void 0!==e[r]}(e)||Be(!1,"Promise is not controllable")}const l="__mono_message__",c=["debug","log","trace","warn","info","error"],d="MONO_WASM: ";let u,f,m,g,p,h;function w(e){g=e}function b(e){if(Pe.diagnosticTracing){const t="function"==typeof e?e():e;console.debug(d+t)}}function y(e,...t){console.info(d+e,...t)}function v(e,...t){console.info(e,...t)}function E(e,...t){console.warn(d+e,...t)}function _(e,...t){if(t&&t.length>0&&t[0]&&"object"==typeof t[0]){if(t[0].silent)return;if(t[0].toString)return void console.error(d+e,t[0].toString())}console.error(d+e,...t)}function x(e,t,o){return function(...n){try{let r=n[0];if(void 0===r)r="undefined";else if(null===r)r="null";else if("function"==typeof r)r=r.toString();else if("string"!=typeof r)try{r=JSON.stringify(r)}catch(e){r=r.toString()}t(o?JSON.stringify({method:e,payload:r,arguments:n.slice(1)}):[e+r,...n.slice(1)])}catch(e){m.error(`proxyConsole failed: ${e}`)}}}function j(e,t,o){f=t,g=e,m={...t};const n=`${o}/console`.replace("https://","wss://").replace("http://","ws://");u=new WebSocket(n),u.addEventListener("error",A),u.addEventListener("close",S),function(){for(const e of c)f[e]=x(`console.${e}`,T,!0)}()}function R(e){let t=30;const o=()=>{u?0==u.bufferedAmount||0==t?(e&&v(e),function(){for(const e of c)f[e]=x(`console.${e}`,m.log,!1)}(),u.removeEventListener("error",A),u.removeEventListener("close",S),u.close(1e3,e),u=void 0):(t--,globalThis.setTimeout(o,100)):e&&m&&m.log(e)};o()}function T(e){u&&u.readyState===WebSocket.OPEN?u.send(e):m.log(e)}function A(e){m.error(`[${g}] proxy console websocket error: ${e}`,e)}function S(e){m.debug(`[${g}] proxy console websocket closed: ${e}`,e)}function D(){Pe.preferredIcuAsset=O(Pe.config);let e="invariant"==Pe.config.globalizationMode;if(!e)if(Pe.preferredIcuAsset)Pe.diagnosticTracing&&b("ICU data archive(s) available, disabling invariant mode");else{if("custom"===Pe.config.globalizationMode||"all"===Pe.config.globalizationMode||"sharded"===Pe.config.globalizationMode){const e="invariant globalization mode is inactive and no ICU data archives are available";throw _(`ERROR: ${e}`),new Error(e)}Pe.diagnosticTracing&&b("ICU data archive(s) not available, using invariant globalization mode"),e=!0,Pe.preferredIcuAsset=null}const t="DOTNET_SYSTEM_GLOBALIZATION_INVARIANT",o=Pe.config.environmentVariables;if(void 0===o[t]&&e&&(o[t]="1"),void 0===o.TZ)try{const e=Intl.DateTimeFormat().resolvedOptions().timeZone||null;e&&(o.TZ=e)}catch(e){y("failed to detect timezone, will fallback to UTC")}}function O(e){var t;if((null===(t=e.resources)||void 0===t?void 0:t.icu)&&"invariant"!=e.globalizationMode){const t=e.applicationCulture||(ke?globalThis.navigator&&globalThis.navigator.languages&&globalThis.navigator.languages[0]:Intl.DateTimeFormat().resolvedOptions().locale),o=e.resources.icu;let n=null;if("custom"===e.globalizationMode){if(o.length>=1)return o[0].name}else t&&"all"!==e.globalizationMode?"sharded"===e.globalizationMode&&(n=function(e){const t=e.split("-")[0];return"en"===t||["fr","fr-FR","it","it-IT","de","de-DE","es","es-ES"].includes(e)?"icudt_EFIGS.dat":["zh","ko","ja"].includes(t)?"icudt_CJK.dat":"icudt_no_CJK.dat"}(t)):n="icudt.dat";if(n)for(let e=0;e<o.length;e++){const t=o[e];if(t.virtualPath===n)return t.name}}return e.globalizationMode="invariant",null}(new Date).valueOf();const C=class{constructor(e){this.url=e}toString(){return this.url}};async function k(e,t){try{const o="function"==typeof globalThis.fetch;if(Se){const n=e.startsWith("file://");if(!n&&o)return globalThis.fetch(e,t||{credentials:"same-origin"});p||(h=Ne.require("url"),p=Ne.require("fs")),n&&(e=h.fileURLToPath(e));const r=await p.promises.readFile(e);return{ok:!0,headers:{length:0,get:()=>null},url:e,arrayBuffer:()=>r,json:()=>JSON.parse(r),text:()=>{throw new Error("NotImplementedException")}}}if(o)return globalThis.fetch(e,t||{credentials:"same-origin"});if("function"==typeof read)return{ok:!0,url:e,headers:{length:0,get:()=>null},arrayBuffer:()=>new Uint8Array(read(e,"binary")),json:()=>JSON.parse(read(e,"utf8")),text:()=>read(e,"utf8")}}catch(t){return{ok:!1,url:e,status:500,headers:{length:0,get:()=>null},statusText:"ERR28: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t},text:()=>{throw t}}}throw new Error("No fetch implementation available")}function I(e){return"string"!=typeof e&&Be(!1,"url must be a string"),!M(e)&&0!==e.indexOf("./")&&0!==e.indexOf("../")&&globalThis.URL&&globalThis.document&&globalThis.document.baseURI&&(e=new URL(e,globalThis.document.baseURI).toString()),e}const U=/^[a-zA-Z][a-zA-Z\d+\-.]*?:\/\//,P=/[a-zA-Z]:[\\/]/;function M(e){return Se||Ie?e.startsWith("/")||e.startsWith("\\")||-1!==e.indexOf("///")||P.test(e):U.test(e)}let L,N=0;const $=[],z=[],W=new Map,F={"js-module-threads":!0,"js-module-runtime":!0,"js-module-dotnet":!0,"js-module-native":!0,"js-module-diagnostics":!0},B={...F,"js-module-library-initializer":!0},V={...F,dotnetwasm:!0,heap:!0,manifest:!0},q={...B,manifest:!0},H={...B,dotnetwasm:!0},J={dotnetwasm:!0,symbols:!0},Z={...B,dotnetwasm:!0,symbols:!0},Q={symbols:!0};function G(e){return!("icu"==e.behavior&&e.name!=Pe.preferredIcuAsset)}function K(e,t,o){null!=t||(t=[]),Be(1==t.length,`Expect to have one ${o} asset in resources`);const n=t[0];return n.behavior=o,X(n),e.push(n),n}function X(e){V[e.behavior]&&W.set(e.behavior,e)}function Y(e){Be(V[e],`Unknown single asset behavior ${e}`);const t=W.get(e);if(t&&!t.resolvedUrl)if(t.resolvedUrl=Pe.locateFile(t.name),F[t.behavior]){const e=ge(t);e?("string"!=typeof e&&Be(!1,"loadBootResource response for 'dotnetjs' type should be a URL string"),t.resolvedUrl=e):t.resolvedUrl=ce(t.resolvedUrl,t.behavior)}else if("dotnetwasm"!==t.behavior)throw new Error(`Unknown single asset behavior ${e}`);return t}function ee(e){const t=Y(e);return Be(t,`Single asset for ${e} not found`),t}let te=!1;async function oe(){if(!te){te=!0,Pe.diagnosticTracing&&b("mono_download_assets");try{const e=[],t=[],o=(e,t)=>{!Z[e.behavior]&&G(e)&&Pe.expected_instantiated_assets_count++,!H[e.behavior]&&G(e)&&(Pe.expected_downloaded_assets_count++,t.push(se(e)))};for(const t of $)o(t,e);for(const e of z)o(e,t);Pe.allDownloadsQueued.promise_control.resolve(),Promise.all([...e,...t]).then((()=>{Pe.allDownloadsFinished.promise_control.resolve()})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e})),await Pe.runtimeModuleLoaded.promise;const n=async e=>{const t=await e;if(t.buffer){if(!Z[t.behavior]){t.buffer&&"object"==typeof t.buffer||Be(!1,"asset buffer must be array-like or buffer-like or promise of these"),"string"!=typeof t.resolvedUrl&&Be(!1,"resolvedUrl must be string");const e=t.resolvedUrl,o=await t.buffer,n=new Uint8Array(o);pe(t),await Ue.beforeOnRuntimeInitialized.promise,Ue.instantiate_asset(t,e,n)}}else J[t.behavior]?("symbols"===t.behavior&&(await Ue.instantiate_symbols_asset(t),pe(t)),J[t.behavior]&&++Pe.actual_downloaded_assets_count):(t.isOptional||Be(!1,"Expected asset to have the downloaded buffer"),!H[t.behavior]&&G(t)&&Pe.expected_downloaded_assets_count--,!Z[t.behavior]&&G(t)&&Pe.expected_instantiated_assets_count--)},r=[],i=[];for(const t of e)r.push(n(t));for(const e of t)i.push(n(e));Promise.all(r).then((()=>{Ce||Ue.coreAssetsInMemory.promise_control.resolve()})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e})),Promise.all(i).then((async()=>{Ce||(await Ue.coreAssetsInMemory.promise,Ue.allAssetsInMemory.promise_control.resolve())})).catch((e=>{throw Pe.err("Error in mono_download_assets: "+e),Xe(1,e),e}))}catch(e){throw Pe.err("Error in mono_download_assets: "+e),e}}}let ne=!1;function re(){if(ne)return;ne=!0;const e=Pe.config,t=[];if(e.assets)for(const t of e.assets)"object"!=typeof t&&Be(!1,`asset must be object, it was ${typeof t} : ${t}`),"string"!=typeof t.behavior&&Be(!1,"asset behavior must be known string"),"string"!=typeof t.name&&Be(!1,"asset name must be string"),t.resolvedUrl&&"string"!=typeof t.resolvedUrl&&Be(!1,"asset resolvedUrl could be string"),t.hash&&"string"!=typeof t.hash&&Be(!1,"asset resolvedUrl could be string"),t.pendingDownload&&"object"!=typeof t.pendingDownload&&Be(!1,"asset pendingDownload could be object"),t.isCore?$.push(t):z.push(t),X(t);else if(e.resources){const o=e.resources;o.wasmNative||Be(!1,"resources.wasmNative must be defined"),o.jsModuleNative||Be(!1,"resources.jsModuleNative must be defined"),o.jsModuleRuntime||Be(!1,"resources.jsModuleRuntime must be defined"),K(z,o.wasmNative,"dotnetwasm"),K(t,o.jsModuleNative,"js-module-native"),K(t,o.jsModuleRuntime,"js-module-runtime"),o.jsModuleDiagnostics&&K(t,o.jsModuleDiagnostics,"js-module-diagnostics");const n=(e,t,o)=>{const n=e;n.behavior=t,o?(n.isCore=!0,$.push(n)):z.push(n)};if(o.coreAssembly)for(let e=0;e<o.coreAssembly.length;e++)n(o.coreAssembly[e],"assembly",!0);if(o.assembly)for(let e=0;e<o.assembly.length;e++)n(o.assembly[e],"assembly",!o.coreAssembly);if(0!=e.debugLevel&&Pe.isDebuggingSupported()){if(o.corePdb)for(let e=0;e<o.corePdb.length;e++)n(o.corePdb[e],"pdb",!0);if(o.pdb)for(let e=0;e<o.pdb.length;e++)n(o.pdb[e],"pdb",!o.corePdb)}if(e.loadAllSatelliteResources&&o.satelliteResources)for(const e in o.satelliteResources)for(let t=0;t<o.satelliteResources[e].length;t++){const r=o.satelliteResources[e][t];r.culture=e,n(r,"resource",!o.coreAssembly)}if(o.coreVfs)for(let e=0;e<o.coreVfs.length;e++)n(o.coreVfs[e],"vfs",!0);if(o.vfs)for(let e=0;e<o.vfs.length;e++)n(o.vfs[e],"vfs",!o.coreVfs);const r=O(e);if(r&&o.icu)for(let e=0;e<o.icu.length;e++){const t=o.icu[e];t.name===r&&n(t,"icu",!1)}if(o.wasmSymbols)for(let e=0;e<o.wasmSymbols.length;e++)n(o.wasmSymbols[e],"symbols",!1)}if(e.appsettings)for(let t=0;t<e.appsettings.length;t++){const o=e.appsettings[t],n=he(o);"appsettings.json"!==n&&n!==`appsettings.${e.applicationEnvironment}.json`||z.push({name:o,behavior:"vfs",cache:"no-cache",useCredentials:!0})}e.assets=[...$,...z,...t]}async function ie(e){const t=await se(e);return await t.pendingDownloadInternal.response,t.buffer}async function se(e){try{return await ae(e)}catch(t){if(!Pe.enableDownloadRetry)throw t;if(Ie||Se)throw t;if(e.pendingDownload&&e.pendingDownloadInternal==e.pendingDownload)throw t;if(e.resolvedUrl&&-1!=e.resolvedUrl.indexOf("file://"))throw t;if(t&&404==t.status)throw t;e.pendingDownloadInternal=void 0,await Pe.allDownloadsQueued.promise;try{return Pe.diagnosticTracing&&b(`Retrying download '${e.name}'`),await ae(e)}catch(t){return e.pendingDownloadInternal=void 0,await new Promise((e=>globalThis.setTimeout(e,100))),Pe.diagnosticTracing&&b(`Retrying download (2) '${e.name}' after delay`),await ae(e)}}}async function ae(e){for(;L;)await L.promise;try{++N,N==Pe.maxParallelDownloads&&(Pe.diagnosticTracing&&b("Throttling further parallel downloads"),L=i());const t=await async function(e){if(e.pendingDownload&&(e.pendingDownloadInternal=e.pendingDownload),e.pendingDownloadInternal&&e.pendingDownloadInternal.response)return e.pendingDownloadInternal.response;if(e.buffer){const t=await e.buffer;return e.resolvedUrl||(e.resolvedUrl="undefined://"+e.name),e.pendingDownloadInternal={url:e.resolvedUrl,name:e.name,response:Promise.resolve({ok:!0,arrayBuffer:()=>t,json:()=>JSON.parse(new TextDecoder("utf-8").decode(t)),text:()=>{throw new Error("NotImplementedException")},headers:{get:()=>{}}})},e.pendingDownloadInternal.response}const t=e.loadRemote&&Pe.config.remoteSources?Pe.config.remoteSources:[""];let o;for(let n of t){n=n.trim(),"./"===n&&(n="");const t=le(e,n);e.name===t?Pe.diagnosticTracing&&b(`Attempting to download '${t}'`):Pe.diagnosticTracing&&b(`Attempting to download '${t}' for ${e.name}`);try{e.resolvedUrl=t;const n=fe(e);if(e.pendingDownloadInternal=n,o=await n.response,!o||!o.ok)continue;return o}catch(e){o||(o={ok:!1,url:t,status:0,statusText:""+e});continue}}const n=e.isOptional||e.name.match(/\.pdb$/)&&Pe.config.ignorePdbLoadErrors;if(o||Be(!1,`Response undefined ${e.name}`),!n){const t=new Error(`download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`);throw t.status=o.status,t}y(`optional download '${o.url}' for ${e.name} failed ${o.status} ${o.statusText}`)}(e);return t?(J[e.behavior]||(e.buffer=await t.arrayBuffer(),++Pe.actual_downloaded_assets_count),e):e}finally{if(--N,L&&N==Pe.maxParallelDownloads-1){Pe.diagnosticTracing&&b("Resuming more parallel downloads");const e=L;L=void 0,e.promise_control.resolve()}}}function le(e,t){let o;return null==t&&Be(!1,`sourcePrefix must be provided for ${e.name}`),e.resolvedUrl?o=e.resolvedUrl:(o=""===t?"assembly"===e.behavior||"pdb"===e.behavior?e.name:"resource"===e.behavior&&e.culture&&""!==e.culture?`${e.culture}/${e.name}`:e.name:t+e.name,o=ce(Pe.locateFile(o),e.behavior)),o&&"string"==typeof o||Be(!1,"attemptUrl need to be path or url string"),o}function ce(e,t){return Pe.modulesUniqueQuery&&q[t]&&(e+=Pe.modulesUniqueQuery),e}let de=0;const ue=new Set;function fe(e){try{e.resolvedUrl||Be(!1,"Request's resolvedUrl must be set");const t=function(e){let t=e.resolvedUrl;if(Pe.loadBootResource){const o=ge(e);if(o instanceof Promise)return o;"string"==typeof o&&(t=o)}const o={};return e.cache?o.cache=e.cache:Pe.config.disableNoCacheFetch||(o.cache="no-cache"),e.useCredentials?o.credentials="include":!Pe.config.disableIntegrityCheck&&e.hash&&(o.integrity=e.hash),Pe.fetch_like(t,o)}(e),o={name:e.name,url:e.resolvedUrl,response:t};return ue.add(e.name),o.response.then((()=>{"assembly"==e.behavior&&Pe.loadedAssemblies.push(e.name),de++,Pe.onDownloadResourceProgress&&Pe.onDownloadResourceProgress(de,ue.size)})),o}catch(t){const o={ok:!1,url:e.resolvedUrl,status:500,statusText:"ERR29: "+t,arrayBuffer:()=>{throw t},json:()=>{throw t}};return{name:e.name,url:e.resolvedUrl,response:Promise.resolve(o)}}}const me={resource:"assembly",assembly:"assembly",pdb:"pdb",icu:"globalization",vfs:"configuration",manifest:"manifest",dotnetwasm:"dotnetwasm","js-module-dotnet":"dotnetjs","js-module-native":"dotnetjs","js-module-runtime":"dotnetjs","js-module-threads":"dotnetjs"};function ge(e){var t;if(Pe.loadBootResource){const o=null!==(t=e.hash)&&void 0!==t?t:"",n=e.resolvedUrl,r=me[e.behavior];if(r){const t=Pe.loadBootResource(r,e.name,n,o,e.behavior);return"string"==typeof t?I(t):t}}}function pe(e){e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null}function he(e){let t=e.lastIndexOf("/");return t>=0&&t++,e.substring(t)}async function we(e){e&&await Promise.all((null!=e?e:[]).map((e=>async function(e){try{const t=e.name;if(!e.moduleExports){const o=ce(Pe.locateFile(t),"js-module-library-initializer");Pe.diagnosticTracing&&b(`Attempting to import '${o}' for ${e}`),e.moduleExports=await import(/*! webpackIgnore: true */o)}Pe.libraryInitializers.push({scriptName:t,exports:e.moduleExports})}catch(t){E(`Failed to import library initializer '${e}': ${t}`)}}(e))))}async function be(e,t){if(!Pe.libraryInitializers)return;const o=[];for(let n=0;n<Pe.libraryInitializers.length;n++){const r=Pe.libraryInitializers[n];r.exports[e]&&o.push(ye(r.scriptName,e,(()=>r.exports[e](...t))))}await Promise.all(o)}async function ye(e,t,o){try{await o()}catch(o){throw E(`Failed to invoke '${t}' on library initializer '${e}': ${o}`),Xe(1,o),o}}function ve(e,t){if(e===t)return e;const o={...t};return void 0!==o.assets&&o.assets!==e.assets&&(o.assets=[...e.assets||[],...o.assets||[]]),void 0!==o.resources&&(o.resources=_e(e.resources||{assembly:[],jsModuleNative:[],jsModuleRuntime:[],wasmNative:[]},o.resources)),void 0!==o.environmentVariables&&(o.environmentVariables={...e.environmentVariables||{},...o.environmentVariables||{}}),void 0!==o.runtimeOptions&&o.runtimeOptions!==e.runtimeOptions&&(o.runtimeOptions=[...e.runtimeOptions||[],...o.runtimeOptions||[]]),Object.assign(e,o)}function Ee(e,t){if(e===t)return e;const o={...t};return o.config&&(e.config||(e.config={}),o.config=ve(e.config,o.config)),Object.assign(e,o)}function _e(e,t){if(e===t)return e;const o={...t};return void 0!==o.coreAssembly&&(o.coreAssembly=[...e.coreAssembly||[],...o.coreAssembly||[]]),void 0!==o.assembly&&(o.assembly=[...e.assembly||[],...o.assembly||[]]),void 0!==o.lazyAssembly&&(o.lazyAssembly=[...e.lazyAssembly||[],...o.lazyAssembly||[]]),void 0!==o.corePdb&&(o.corePdb=[...e.corePdb||[],...o.corePdb||[]]),void 0!==o.pdb&&(o.pdb=[...e.pdb||[],...o.pdb||[]]),void 0!==o.jsModuleWorker&&(o.jsModuleWorker=[...e.jsModuleWorker||[],...o.jsModuleWorker||[]]),void 0!==o.jsModuleNative&&(o.jsModuleNative=[...e.jsModuleNative||[],...o.jsModuleNative||[]]),void 0!==o.jsModuleDiagnostics&&(o.jsModuleDiagnostics=[...e.jsModuleDiagnostics||[],...o.jsModuleDiagnostics||[]]),void 0!==o.jsModuleRuntime&&(o.jsModuleRuntime=[...e.jsModuleRuntime||[],...o.jsModuleRuntime||[]]),void 0!==o.wasmSymbols&&(o.wasmSymbols=[...e.wasmSymbols||[],...o.wasmSymbols||[]]),void 0!==o.wasmNative&&(o.wasmNative=[...e.wasmNative||[],...o.wasmNative||[]]),void 0!==o.icu&&(o.icu=[...e.icu||[],...o.icu||[]]),void 0!==o.satelliteResources&&(o.satelliteResources=function(e,t){if(e===t)return e;for(const o in t)e[o]=[...e[o]||[],...t[o]||[]];return e}(e.satelliteResources||{},o.satelliteResources||{})),void 0!==o.modulesAfterConfigLoaded&&(o.modulesAfterConfigLoaded=[...e.modulesAfterConfigLoaded||[],...o.modulesAfterConfigLoaded||[]]),void 0!==o.modulesAfterRuntimeReady&&(o.modulesAfterRuntimeReady=[...e.modulesAfterRuntimeReady||[],...o.modulesAfterRuntimeReady||[]]),void 0!==o.extensions&&(o.extensions={...e.extensions||{},...o.extensions||{}}),void 0!==o.vfs&&(o.vfs=[...e.vfs||[],...o.vfs||[]]),Object.assign(e,o)}function xe(){const e=Pe.config;if(e.environmentVariables=e.environmentVariables||{},e.runtimeOptions=e.runtimeOptions||[],e.resources=e.resources||{assembly:[],jsModuleNative:[],jsModuleWorker:[],jsModuleRuntime:[],wasmNative:[],vfs:[],satelliteResources:{}},e.assets){Pe.diagnosticTracing&&b("config.assets is deprecated, use config.resources instead");for(const t of e.assets){const o={};switch(t.behavior){case"assembly":o.assembly=[t];break;case"pdb":o.pdb=[t];break;case"resource":o.satelliteResources={},o.satelliteResources[t.culture]=[t];break;case"icu":o.icu=[t];break;case"symbols":o.wasmSymbols=[t];break;case"vfs":o.vfs=[t];break;case"dotnetwasm":o.wasmNative=[t];break;case"js-module-threads":o.jsModuleWorker=[t];break;case"js-module-runtime":o.jsModuleRuntime=[t];break;case"js-module-native":o.jsModuleNative=[t];break;case"js-module-diagnostics":o.jsModuleDiagnostics=[t];break;case"js-module-dotnet":break;default:throw new Error(`Unexpected behavior ${t.behavior} of asset ${t.name}`)}_e(e.resources,o)}}e.debugLevel,e.applicationEnvironment||(e.applicationEnvironment="Production"),e.applicationCulture&&(e.environmentVariables.LANG=`${e.applicationCulture}.UTF-8`),Ue.diagnosticTracing=Pe.diagnosticTracing=!!e.diagnosticTracing,Ue.waitForDebugger=e.waitForDebugger,Pe.maxParallelDownloads=e.maxParallelDownloads||Pe.maxParallelDownloads,Pe.enableDownloadRetry=void 0!==e.enableDownloadRetry?e.enableDownloadRetry:Pe.enableDownloadRetry}let je=!1;async function Re(e){var t;if(je)return void await Pe.afterConfigLoaded.promise;let o;try{if(e.configSrc||Pe.config&&0!==Object.keys(Pe.config).length&&(Pe.config.assets||Pe.config.resources)||(e.configSrc="dotnet.boot.js"),o=e.configSrc,je=!0,o&&(Pe.diagnosticTracing&&b("mono_wasm_load_config"),await async function(e){const t=e.configSrc,o=Pe.locateFile(t);let n=null;void 0!==Pe.loadBootResource&&(n=Pe.loadBootResource("manifest",t,o,"","manifest"));let r,i=null;if(n)if("string"==typeof n)n.includes(".json")?(i=await s(I(n)),r=await Ae(i)):r=(await import(I(n))).config;else{const e=await n;"function"==typeof e.json?(i=e,r=await Ae(i)):r=e.config}else o.includes(".json")?(i=await s(ce(o,"manifest")),r=await Ae(i)):r=(await import(ce(o,"manifest"))).config;function s(e){return Pe.fetch_like(e,{method:"GET",credentials:"include",cache:"no-cache"})}Pe.config.applicationEnvironment&&(r.applicationEnvironment=Pe.config.applicationEnvironment),ve(Pe.config,r)}(e)),xe(),await we(null===(t=Pe.config.resources)||void 0===t?void 0:t.modulesAfterConfigLoaded),await be("onRuntimeConfigLoaded",[Pe.config]),e.onConfigLoaded)try{await e.onConfigLoaded(Pe.config,Le),xe()}catch(e){throw _("onConfigLoaded() failed",e),e}xe(),Pe.afterConfigLoaded.promise_control.resolve(Pe.config)}catch(t){const n=`Failed to load config file ${o} ${t} ${null==t?void 0:t.stack}`;throw Pe.config=e.config=Object.assign(Pe.config,{message:n,error:t,isError:!0}),Xe(1,new Error(n)),t}}function Te(){return!!globalThis.navigator&&(Pe.isChromium||Pe.isFirefox)}async function Ae(e){const t=Pe.config,o=await e.json();t.applicationEnvironment||o.applicationEnvironment||(o.applicationEnvironment=e.headers.get("Blazor-Environment")||e.headers.get("DotNet-Environment")||void 0),o.environmentVariables||(o.environmentVariables={});const n=e.headers.get("DOTNET-MODIFIABLE-ASSEMBLIES");n&&(o.environmentVariables.DOTNET_MODIFIABLE_ASSEMBLIES=n);const r=e.headers.get("ASPNETCORE-BROWSER-TOOLS");return r&&(o.environmentVariables.__ASPNETCORE_BROWSER_TOOLS=r),o}"function"!=typeof importScripts||globalThis.onmessage||(globalThis.dotnetSidecar=!0);const Se="object"==typeof process&&"object"==typeof process.versions&&"string"==typeof process.versions.node,De="function"==typeof importScripts,Oe=De&&"undefined"!=typeof dotnetSidecar,Ce=De&&!Oe,ke="object"==typeof window||De&&!Se,Ie=!ke&&!Se;let Ue={},Pe={},Me={},Le={},Ne={},$e=!1;const ze={},We={config:ze},Fe={mono:{},binding:{},internal:Ne,module:We,loaderHelpers:Pe,runtimeHelpers:Ue,diagnosticHelpers:Me,api:Le};function Be(e,t){if(e)return;const o="Assert failed: "+("function"==typeof t?t():t),n=new Error(o);_(o,n),Ue.nativeAbort(n)}function Ve(){return void 0!==Pe.exitCode}function qe(){return Ue.runtimeReady&&!Ve()}function He(){Ve()&&Be(!1,`.NET runtime already exited with ${Pe.exitCode} ${Pe.exitReason}. You can use runtime.runMain() which doesn't exit the runtime.`),Ue.runtimeReady||Be(!1,".NET runtime didn't start yet. Please call dotnet.create() first.")}function Je(){ke&&(globalThis.addEventListener("unhandledrejection",et),globalThis.addEventListener("error",tt))}let Ze,Qe;function Ge(e){Qe&&Qe(e),Xe(e,Pe.exitReason)}function Ke(e){Ze&&Ze(e||Pe.exitReason),Xe(1,e||Pe.exitReason)}function Xe(t,o){var n,r;const i=o&&"object"==typeof o;t=i&&"number"==typeof o.status?o.status:void 0===t?-1:t;const s=i&&"string"==typeof o.message?o.message:""+o;(o=i?o:Ue.ExitStatus?function(e,t){const o=new Ue.ExitStatus(e);return o.message=t,o.toString=()=>t,o}(t,s):new Error("Exit with code "+t+" "+s)).status=t,o.message||(o.message=s);const a=""+(o.stack||(new Error).stack);try{Object.defineProperty(o,"stack",{get:()=>a})}catch(e){}const l=!!o.silent;if(o.silent=!0,Ve())Pe.diagnosticTracing&&b("mono_exit called after exit");else{try{We.onAbort==Ke&&(We.onAbort=Ze),We.onExit==Ge&&(We.onExit=Qe),ke&&(globalThis.removeEventListener("unhandledrejection",et),globalThis.removeEventListener("error",tt)),Ue.runtimeReady?(Ue.jiterpreter_dump_stats&&Ue.jiterpreter_dump_stats(!1),0===t&&(null===(n=Pe.config)||void 0===n?void 0:n.interopCleanupOnExit)&&Ue.forceDisposeProxies(!0,!0),e&&0!==t&&(null===(r=Pe.config)||void 0===r||r.dumpThreadsOnNonZeroExit)):(Pe.diagnosticTracing&&b(`abort_startup, reason: ${o}`),function(e){Pe.allDownloadsQueued.promise_control.reject(e),Pe.allDownloadsFinished.promise_control.reject(e),Pe.afterConfigLoaded.promise_control.reject(e),Pe.wasmCompilePromise.promise_control.reject(e),Pe.runtimeModuleLoaded.promise_control.reject(e),Ue.dotnetReady&&(Ue.dotnetReady.promise_control.reject(e),Ue.afterInstantiateWasm.promise_control.reject(e),Ue.beforePreInit.promise_control.reject(e),Ue.afterPreInit.promise_control.reject(e),Ue.afterPreRun.promise_control.reject(e),Ue.beforeOnRuntimeInitialized.promise_control.reject(e),Ue.afterOnRuntimeInitialized.promise_control.reject(e),Ue.afterPostRun.promise_control.reject(e))}(o))}catch(e){E("mono_exit A failed",e)}try{l||(function(e,t){if(0!==e&&t){const e=Ue.ExitStatus&&t instanceof Ue.ExitStatus?b:_;"string"==typeof t?e(t):(void 0===t.stack&&(t.stack=(new Error).stack+""),t.message?e(Ue.stringify_as_error_with_stack?Ue.stringify_as_error_with_stack(t.message+"\n"+t.stack):t.message+"\n"+t.stack):e(JSON.stringify(t)))}!Ce&&Pe.config&&(Pe.config.logExitCode?Pe.config.forwardConsoleLogsToWS?R("WASM EXIT "+e):v("WASM EXIT "+e):Pe.config.forwardConsoleLogsToWS&&R())}(t,o),function(e){if(ke&&!Ce&&Pe.config&&Pe.config.appendElementOnExit&&document){const t=document.createElement("label");t.id="tests_done",0!==e&&(t.style.background="red"),t.innerHTML=""+e,document.body.appendChild(t)}}(t))}catch(e){E("mono_exit B failed",e)}Pe.exitCode=t,Pe.exitReason||(Pe.exitReason=o),!Ce&&Ue.runtimeReady&&We.runtimeKeepalivePop()}if(Pe.config&&Pe.config.asyncFlushOnExit&&0===t)throw(async()=>{try{await async function(){try{const e=await import(/*! webpackIgnore: true */"process"),t=e=>new Promise(((t,o)=>{e.on("error",o),e.end("","utf8",t)})),o=t(e.stderr),n=t(e.stdout);let r;const i=new Promise((e=>{r=setTimeout((()=>e("timeout")),1e3)}));await Promise.race([Promise.all([n,o]),i]),clearTimeout(r)}catch(e){_(`flushing std* streams failed: ${e}`)}}()}finally{Ye(t,o)}})(),o;Ye(t,o)}function Ye(e,t){if(Ue.runtimeReady&&Ue.nativeExit)try{Ue.nativeExit(e)}catch(e){!Ue.ExitStatus||e instanceof Ue.ExitStatus||E("set_exit_code_and_quit_now failed: "+e.toString())}if(0!==e||!ke)throw Se&&Ne.process?Ne.process.exit(e):Ue.quit&&Ue.quit(e,t),t}function et(e){ot(e,e.reason,"rejection")}function tt(e){ot(e,e.error,"error")}function ot(e,t,o){e.preventDefault();try{t||(t=new Error("Unhandled "+o)),void 0===t.stack&&(t.stack=(new Error).stack),t.stack=t.stack+"",t.silent||(_("Unhandled error:",t),Xe(1,t))}catch(e){}}!function(e){if($e)throw new Error("Loader module already loaded");$e=!0,Ue=e.runtimeHelpers,Pe=e.loaderHelpers,Me=e.diagnosticHelpers,Le=e.api,Ne=e.internal,Object.assign(Le,{INTERNAL:Ne,invokeLibraryInitializers:be}),Object.assign(e.module,{config:ve(ze,{environmentVariables:{}})});const r={mono_wasm_bindings_is_ready:!1,config:e.module.config,diagnosticTracing:!1,nativeAbort:e=>{throw e||new Error("abort")},nativeExit:e=>{throw new Error("exit:"+e)}},l={gitHash:"8e0f8ed2312954727f8919d7d1c2f78b483b4ee7",config:e.module.config,diagnosticTracing:!1,maxParallelDownloads:16,enableDownloadRetry:!0,_loaded_files:[],loadedFiles:[],loadedAssemblies:[],libraryInitializers:[],workerNextNumber:1,actual_downloaded_assets_count:0,actual_instantiated_assets_count:0,expected_downloaded_assets_count:0,expected_instantiated_assets_count:0,afterConfigLoaded:i(),allDownloadsQueued:i(),allDownloadsFinished:i(),wasmCompilePromise:i(),runtimeModuleLoaded:i(),loadingWorkers:i(),is_exited:Ve,is_runtime_running:qe,assert_runtime_running:He,mono_exit:Xe,createPromiseController:i,getPromiseController:s,assertIsControllablePromise:a,mono_download_assets:oe,resolve_single_asset_path:ee,setup_proxy_console:j,set_thread_prefix:w,installUnhandledErrorHandler:Je,retrieve_asset_download:ie,invokeLibraryInitializers:be,isDebuggingSupported:Te,exceptions:t,simd:n,relaxedSimd:o};Object.assign(Ue,r),Object.assign(Pe,l)}(Fe);let nt,rt,it,st=!1,at=!1;async function lt(e){if(!at){if(at=!0,ke&&Pe.config.forwardConsoleLogsToWS&&void 0!==globalThis.WebSocket&&j("main",globalThis.console,globalThis.location.origin),We||Be(!1,"Null moduleConfig"),Pe.config||Be(!1,"Null moduleConfig.config"),"function"==typeof e){const t=e(Fe.api);if(t.ready)throw new Error("Module.ready couldn't be redefined.");Object.assign(We,t),Ee(We,t)}else{if("object"!=typeof e)throw new Error("Can't use moduleFactory callback of createDotnetRuntime function.");Ee(We,e)}await async function(e){if(Se){const e=await import(/*! webpackIgnore: true */"process"),t=14;if(e.versions.node.split(".")[0]<t)throw new Error(`NodeJS at '${e.execPath}' has too low version '${e.versions.node}', please use at least ${t}. See also https://aka.ms/dotnet-wasm-features`)}const t=/*! webpackIgnore: true */import.meta.url,o=t.indexOf("?");var n;if(o>0&&(Pe.modulesUniqueQuery=t.substring(o)),Pe.scriptUrl=t.replace(/\\/g,"/").replace(/[?#].*/,""),Pe.scriptDirectory=(n=Pe.scriptUrl).slice(0,n.lastIndexOf("/"))+"/",Pe.locateFile=e=>"URL"in globalThis&&globalThis.URL!==C?new URL(e,Pe.scriptDirectory).toString():M(e)?e:Pe.scriptDirectory+e,Pe.fetch_like=k,Pe.out=console.log,Pe.err=console.error,Pe.onDownloadResourceProgress=e.onDownloadResourceProgress,ke&&globalThis.navigator){const e=globalThis.navigator,t=e.userAgentData&&e.userAgentData.brands;t&&t.length>0?Pe.isChromium=t.some((e=>"Google Chrome"===e.brand||"Microsoft Edge"===e.brand||"Chromium"===e.brand)):e.userAgent&&(Pe.isChromium=e.userAgent.includes("Chrome"),Pe.isFirefox=e.userAgent.includes("Firefox"))}Ne.require=Se?await import(/*! webpackIgnore: true */"module").then((e=>e.createRequire(/*! webpackIgnore: true */import.meta.url))):Promise.resolve((()=>{throw new Error("require not supported")})),void 0===globalThis.URL&&(globalThis.URL=C)}(We)}}async function ct(e){return await lt(e),Ze=We.onAbort,Qe=We.onExit,We.onAbort=Ke,We.onExit=Ge,We.ENVIRONMENT_IS_PTHREAD?async function(){(function(){const e=new MessageChannel,t=e.port1,o=e.port2;t.addEventListener("message",(e=>{var n,r;n=JSON.parse(e.data.config),r=JSON.parse(e.data.monoThreadInfo),st?Pe.diagnosticTracing&&b("mono config already received"):(ve(Pe.config,n),Ue.monoThreadInfo=r,xe(),Pe.diagnosticTracing&&b("mono config received"),st=!0,Pe.afterConfigLoaded.promise_control.resolve(Pe.config),ke&&n.forwardConsoleLogsToWS&&void 0!==globalThis.WebSocket&&Pe.setup_proxy_console("worker-idle",console,globalThis.location.origin)),t.close(),o.close()}),{once:!0}),t.start(),self.postMessage({[l]:{monoCmd:"preload",port:o}},[o])})(),await Pe.afterConfigLoaded.promise,function(){const e=Pe.config;e.assets||Be(!1,"config.assets must be defined");for(const t of e.assets)X(t),Q[t.behavior]&&z.push(t)}(),setTimeout((async()=>{try{await oe()}catch(e){Xe(1,e)}}),0);const e=dt(),t=await Promise.all(e);return await ut(t),We}():async function(){var e;await Re(We),re();const t=dt();(async function(){try{const e=ee("dotnetwasm");await se(e),e&&e.pendingDownloadInternal&&e.pendingDownloadInternal.response||Be(!1,"Can't load dotnet.native.wasm");const t=await e.pendingDownloadInternal.response,o=t.headers&&t.headers.get?t.headers.get("Content-Type"):void 0;let n;if("function"==typeof WebAssembly.compileStreaming&&"application/wasm"===o)n=await WebAssembly.compileStreaming(t);else{ke&&"application/wasm"!==o&&E('WebAssembly resource does not have the expected content type "application/wasm", so falling back to slower ArrayBuffer instantiation.');const e=await t.arrayBuffer();Pe.diagnosticTracing&&b("instantiate_wasm_module buffered"),n=Ie?await Promise.resolve(new WebAssembly.Module(e)):await WebAssembly.compile(e)}e.pendingDownloadInternal=null,e.pendingDownload=null,e.buffer=null,e.moduleExports=null,Pe.wasmCompilePromise.promise_control.resolve(n)}catch(e){Pe.wasmCompilePromise.promise_control.reject(e)}})(),setTimeout((async()=>{try{D(),await oe()}catch(e){Xe(1,e)}}),0);const o=await Promise.all(t);return await ut(o),await Ue.dotnetReady.promise,await we(null===(e=Pe.config.resources)||void 0===e?void 0:e.modulesAfterRuntimeReady),await be("onRuntimeReady",[Fe.api]),Le}()}function dt(){const e=ee("js-module-runtime"),t=ee("js-module-native");if(nt&&rt)return[nt,rt,it];"object"==typeof e.moduleExports?nt=e.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${e.resolvedUrl}' for ${e.name}`),nt=import(/*! webpackIgnore: true */e.resolvedUrl)),"object"==typeof t.moduleExports?rt=t.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${t.resolvedUrl}' for ${t.name}`),rt=import(/*! webpackIgnore: true */t.resolvedUrl));const o=Y("js-module-diagnostics");return o&&("object"==typeof o.moduleExports?it=o.moduleExports:(Pe.diagnosticTracing&&b(`Attempting to import '${o.resolvedUrl}' for ${o.name}`),it=import(/*! webpackIgnore: true */o.resolvedUrl))),[nt,rt,it]}async function ut(e){const{initializeExports:t,initializeReplacements:o,configureRuntimeStartup:n,configureEmscriptenStartup:r,configureWorkerStartup:i,setRuntimeGlobals:s,passEmscriptenInternals:a}=e[0],{default:l}=e[1],c=e[2];s(Fe),t(Fe),c&&c.setRuntimeGlobals(Fe),await n(We),Pe.runtimeModuleLoaded.promise_control.resolve(),l((e=>(Object.assign(We,{ready:e.ready,__dotnet_runtime:{initializeReplacements:o,configureEmscriptenStartup:r,configureWorkerStartup:i,passEmscriptenInternals:a}}),We))).catch((e=>{if(e.message&&e.message.toLowerCase().includes("out of memory"))throw new Error(".NET runtime has failed to start, because too much memory was requested. Please decrease the memory by adjusting EmccMaximumHeapSize. See also https://aka.ms/dotnet-wasm-features");throw e}))}const ft=new class{withModuleConfig(e){try{return Ee(We,e),this}catch(e){throw Xe(1,e),e}}withOnConfigLoaded(e){try{return Ee(We,{onConfigLoaded:e}),this}catch(e){throw Xe(1,e),e}}withConsoleForwarding(){try{return ve(ze,{forwardConsoleLogsToWS:!0}),this}catch(e){throw Xe(1,e),e}}withExitOnUnhandledError(){try{return ve(ze,{exitOnUnhandledError:!0}),Je(),this}catch(e){throw Xe(1,e),e}}withAsyncFlushOnExit(){try{return ve(ze,{asyncFlushOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withExitCodeLogging(){try{return ve(ze,{logExitCode:!0}),this}catch(e){throw Xe(1,e),e}}withElementOnExit(){try{return ve(ze,{appendElementOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withInteropCleanupOnExit(){try{return ve(ze,{interopCleanupOnExit:!0}),this}catch(e){throw Xe(1,e),e}}withDumpThreadsOnNonZeroExit(){try{return ve(ze,{dumpThreadsOnNonZeroExit:!0}),this}catch(e){throw Xe(1,e),e}}withWaitingForDebugger(e){try{return ve(ze,{waitForDebugger:e}),this}catch(e){throw Xe(1,e),e}}withInterpreterPgo(e,t){try{return ve(ze,{interpreterPgo:e,interpreterPgoSaveDelay:t}),ze.runtimeOptions?ze.runtimeOptions.push("--interp-pgo-recording"):ze.runtimeOptions=["--interp-pgo-recording"],this}catch(e){throw Xe(1,e),e}}withConfig(e){try{return ve(ze,e),this}catch(e){throw Xe(1,e),e}}withConfigSrc(e){try{return e&&"string"==typeof e||Be(!1,"must be file path or URL"),Ee(We,{configSrc:e}),this}catch(e){throw Xe(1,e),e}}withVirtualWorkingDirectory(e){try{return e&&"string"==typeof e||Be(!1,"must be directory path"),ve(ze,{virtualWorkingDirectory:e}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariable(e,t){try{const o={};return o[e]=t,ve(ze,{environmentVariables:o}),this}catch(e){throw Xe(1,e),e}}withEnvironmentVariables(e){try{return e&&"object"==typeof e||Be(!1,"must be dictionary object"),ve(ze,{environmentVariables:e}),this}catch(e){throw Xe(1,e),e}}withDiagnosticTracing(e){try{return"boolean"!=typeof e&&Be(!1,"must be boolean"),ve(ze,{diagnosticTracing:e}),this}catch(e){throw Xe(1,e),e}}withDebugging(e){try{return null!=e&&"number"==typeof e||Be(!1,"must be number"),ve(ze,{debugLevel:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArguments(...e){try{return e&&Array.isArray(e)||Be(!1,"must be array of strings"),ve(ze,{applicationArguments:e}),this}catch(e){throw Xe(1,e),e}}withRuntimeOptions(e){try{return e&&Array.isArray(e)||Be(!1,"must be array of strings"),ze.runtimeOptions?ze.runtimeOptions.push(...e):ze.runtimeOptions=e,this}catch(e){throw Xe(1,e),e}}withMainAssembly(e){try{return ve(ze,{mainAssemblyName:e}),this}catch(e){throw Xe(1,e),e}}withApplicationArgumentsFromQuery(){try{if(!globalThis.window)throw new Error("Missing window to the query parameters from");if(void 0===globalThis.URLSearchParams)throw new Error("URLSearchParams is supported");const e=new URLSearchParams(globalThis.window.location.search).getAll("arg");return this.withApplicationArguments(...e)}catch(e){throw Xe(1,e),e}}withApplicationEnvironment(e){try{return ve(ze,{applicationEnvironment:e}),this}catch(e){throw Xe(1,e),e}}withApplicationCulture(e){try{return ve(ze,{applicationCulture:e}),this}catch(e){throw Xe(1,e),e}}withResourceLoader(e){try{return Pe.loadBootResource=e,this}catch(e){throw Xe(1,e),e}}async download(){try{await async function(){lt(We),await Re(We),re(),D(),oe(),await Pe.allDownloadsFinished.promise}()}catch(e){throw Xe(1,e),e}}async create(){try{return this.instance||(this.instance=await async function(){return await ct(We),Fe.api}()),this.instance}catch(e){throw Xe(1,e),e}}async run(){try{return We.config||Be(!1,"Null moduleConfig.config"),this.instance||await this.create(),this.instance.runMainAndExit()}catch(e){throw Xe(1,e),e}}},mt=Xe,gt=ct;Ie||"function"==typeof globalThis.URL||Be(!1,"This browser/engine doesn't support URL API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),"function"!=typeof globalThis.BigInt64Array&&Be(!1,"This browser/engine doesn't support BigInt64Array API. Please use a modern version. See also https://aka.ms/dotnet-wasm-features"),ft.withConfig(/*json-start*/{
  "mainAssemblyName": "DemoCenter.Web",
  "resources": {
    "hash": "sha256-1g/B2JQrFnMwsGlIs8vmSiTZGGTS4WqQiVj1MM82Rc8=",
    "jsModuleNative": [
      {
        "name": "dotnet.native.q7pem6aru4.js"
      }
    ],
    "jsModuleRuntime": [
      {
        "name": "dotnet.runtime.v2rwe8u38b.js"
      }
    ],
    "wasmNative": [
      {
        "name": "dotnet.native.hco5yndqm3.wasm",
        "hash": "sha256-gXiYw4M+nKILLWGSHs1zSu0zHqAKHI+kglXlvBDAuwM=",
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
        "name": "System.Private.CoreLib.qs84b5aymh.wasm",
        "hash": "sha256-drv4bwxoBtL+JFymXEe+TzwiUAa5KP7tAdQrevc3IPA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.InteropServices.JavaScript.wasm",
        "name": "System.Runtime.InteropServices.JavaScript.ihwonp6bwv.wasm",
        "hash": "sha256-dlWMiJwTZGA+hvoF+J6+3amYgM04HOxLRISvgwXGYsc=",
        "cache": "force-cache"
      }
    ],
    "assembly": [
      {
        "virtualPath": "Avalonia.wasm",
        "name": "Avalonia.3mrwkgoqza.wasm",
        "hash": "sha256-Ndj23ywZXCaE58ZejpGYS16jOrXOnYPLmb0tLb6dNns=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Base.wasm",
        "name": "Avalonia.Base.bn3f63c5kv.wasm",
        "hash": "sha256-yY2vZ3MqZA2fZc2YVvfyAx5xBKrI5reZjNTYz1Z/ygU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Browser.wasm",
        "name": "Avalonia.Browser.b2pamz975d.wasm",
        "hash": "sha256-ndAbCokKEVq3vl2xK/3MsKse6TnIflem3VFTm1BnR9o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Controls.wasm",
        "name": "Avalonia.Controls.uz1sabgdwz.wasm",
        "hash": "sha256-Go9fzTGVpNZrHxjlIAafpQryOTV5fV55/KQggNJQ8us=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.DesignerSupport.wasm",
        "name": "Avalonia.DesignerSupport.vrxj9uygk5.wasm",
        "hash": "sha256-uzIQcTUVjXw34Mcepksp2SdVGUyBoa2QuczQrRQn/DM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Dialogs.wasm",
        "name": "Avalonia.Dialogs.z84nexhpbx.wasm",
        "hash": "sha256-Q7fpWuJYdwwQr/oI1U20guBXwyseB/TRFVRFFqU+OIg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Fonts.Inter.wasm",
        "name": "Avalonia.Fonts.Inter.5zxnsrnfs7.wasm",
        "hash": "sha256-wSO2cqc9RME5QbA/vlrQHmHYJzCFPFhvg+5VDMNiNs4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.HarfBuzz.wasm",
        "name": "Avalonia.HarfBuzz.cqnd4qiy04.wasm",
        "hash": "sha256-ZIUIqg8iT5pXJEwMoT2igM/4Snh55T+JqmYFyYo7YAw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Markup.wasm",
        "name": "Avalonia.Markup.2yqiouurwc.wasm",
        "hash": "sha256-FrcnPyOArPK9hLu4N7UUycN+gBCWL7zTCk8vybvVSE0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Markup.Xaml.wasm",
        "name": "Avalonia.Markup.Xaml.j9enk151tv.wasm",
        "hash": "sha256-CZDByY5JeVeSYT+x531oWwamRo+kWKOX7aiIyD9Dz5s=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Metal.wasm",
        "name": "Avalonia.Metal.e9q7oh3cng.wasm",
        "hash": "sha256-TlMvIDbSk9LdBjeui7S6/1xhbZRiSXAQyuBNQktp5xw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.MicroCom.wasm",
        "name": "Avalonia.MicroCom.acqhk5jryw.wasm",
        "hash": "sha256-mlfT2r/zZ/sN3pDQefL5DyZSFk1eRLE2zZzvHUjtmVw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.OpenGL.wasm",
        "name": "Avalonia.OpenGL.dinyei44dr.wasm",
        "hash": "sha256-e75CL+daDCmIiG+zAArrzITtGQxYAwdZPVE5oPeznKg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Remote.Protocol.wasm",
        "name": "Avalonia.Remote.Protocol.4c127gid5e.wasm",
        "hash": "sha256-RHq0crq9T0wT1Oh/Ol6T8nlt8OuuyaOZkWvDwFNQWTw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Skia.wasm",
        "name": "Avalonia.Skia.1n3yb1aihv.wasm",
        "hash": "sha256-7fXQqyscV50u77Tx/LNt2CRSSS62GilMcCvqYVMbC5c=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Themes.Fluent.wasm",
        "name": "Avalonia.Themes.Fluent.e626h5ok37.wasm",
        "hash": "sha256-hrXCDBPCzZw46btQ95bx+ALNTpKAG728cNHs+bUb7yA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Avalonia.Vulkan.wasm",
        "name": "Avalonia.Vulkan.oeqyj0dfzt.wasm",
        "hash": "sha256-0MKP6hEfsHjbDtOp6kHjb3uCuC6vjJgwfZAZdglAZHE=",
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
        "virtualPath": "DemoCenter.Web.wasm",
        "name": "DemoCenter.Web.qldnkaw62w.wasm",
        "hash": "sha256-niAV+y/sIhBuX6HSofBcAE9LS6HamvPemPAQCM077Ak=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "DemoCenter.wasm",
        "name": "DemoCenter.ytefh7ispk.wasm",
        "hash": "sha256-KrrfkITx70CyOp5ljCv0RkS9cJAVQ2hkodpdI0y/g+Q=",
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
        "name": "Eremex.Avalonia.Charts.95xp9w08bi.wasm",
        "hash": "sha256-DZ0SeEm/N/8t1N1fOFpHa5Yx/dolVy+6LP1YW6qNans=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Avalonia.Controls.wasm",
        "name": "Eremex.Avalonia.Controls.5hk4468bbh.wasm",
        "hash": "sha256-VyPiHMJ5lXeW27YOgHSw6Jrc3S/prRYpx/A7cWFXtrw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Avalonia.Controls.ApplicationServices.wasm",
        "name": "Eremex.Avalonia.Controls.ApplicationServices.aky0e636sg.wasm",
        "hash": "sha256-Bvu5K5crCM64Gw7QUEAm0kA0Ahs9esPh16YBOMeohCM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Avalonia.Icons.wasm",
        "name": "Eremex.Avalonia.Icons.lgrlkkybky.wasm",
        "hash": "sha256-uz1iD1MQuC/JMzM9bmBvL8JaIMHFbJYAEnCQXKX+Wwc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Avalonia.Themes.DeltaDesign.wasm",
        "name": "Eremex.Avalonia.Themes.DeltaDesign.onmesuh1gs.wasm",
        "hash": "sha256-NI5FvRHbT3hMWe0SSwEduTp2LZsVPFXZ10F2dQ4UAFc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Common.Contracts.wasm",
        "name": "Eremex.Common.Contracts.5i97o0y4v5.wasm",
        "hash": "sha256-3WN4OjGhGeV7Rdd8pZC6Eu1MJVhlsSmGEn6vbjgQuQI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.DocumentProcessing.wasm",
        "name": "Eremex.DocumentProcessing.1f81sagseg.wasm",
        "hash": "sha256-3eTmFAWnVUlnA3sjwm83bzzFaixxJetqddHnpJI4GpQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Drawing.Skia.wasm",
        "name": "Eremex.Drawing.Skia.5f91jyghe7.wasm",
        "hash": "sha256-Dx9hUJ3fSrituZ/U8Y0PNGVR4gVpMhbCl2Ny7r1//wE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Drawing.wasm",
        "name": "Eremex.Drawing.qd9qljb8hp.wasm",
        "hash": "sha256-/sLDXB14mBMJl2zLWu60Etbm/k5pa2bDlwFeuovz16A=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Eremex.Pdf.wasm",
        "name": "Eremex.Pdf.wz30apnza2.wasm",
        "hash": "sha256-PJI57Awfi/480vaaoO6rIaNbWp412xB06hnS5JudZK0=",
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
        "name": "MicroCom.Runtime.xit9ilgz3o.wasm",
        "hash": "sha256-R3yF2bl/ikaFWpIsIwvydXJqhLYYJIcGRW8MDcYk1vA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.CSharp.wasm",
        "name": "Microsoft.CSharp.1zdxgb4i6c.wasm",
        "hash": "sha256-/aD5aoAPdJ0+VWK29BCwyPKikrETw/5L9RX5w11VqL4=",
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
        "name": "Microsoft.VisualBasic.8p7acm0ol9.wasm",
        "hash": "sha256-hBbsdszuvNjfobDQ8ndRZiARjWwJoqEQZiKQaqhg1oI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.VisualBasic.Core.wasm",
        "name": "Microsoft.VisualBasic.Core.oa0ivpxjss.wasm",
        "hash": "sha256-ANtiyRGv1V3UXu5e+7N0yRNdc46M6D/lFyFYA0QoaXM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Win32.Primitives.wasm",
        "name": "Microsoft.Win32.Primitives.73ed8hphgj.wasm",
        "hash": "sha256-k0QyJMoSp2EFQwOuJlXvI19gCFM/4gyb18KTQ9l/V0Y=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "Microsoft.Win32.Registry.wasm",
        "name": "Microsoft.Win32.Registry.22ncz2iyf9.wasm",
        "hash": "sha256-uurlWJMm3o9KIatGfBd00MmACPJ35iRHId8WkB9gmgQ=",
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
        "name": "System.AppContext.2dp0f0ejq7.wasm",
        "hash": "sha256-Zo9YIYie+8Y/nusOB6zG4ljnLLzzVZDtlqEGwPq9Lfo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Buffers.wasm",
        "name": "System.Buffers.8vidsi8mmx.wasm",
        "hash": "sha256-xMTiecIP3/EAwVylZUJjz/RX/hge27pODh2rN1emUpc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.wasm",
        "name": "System.Collections.74zlx30b7i.wasm",
        "hash": "sha256-+e3Wd1/41MmEKwqvMUkpngET9VOtATyhbheMuGQqAeM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Concurrent.wasm",
        "name": "System.Collections.Concurrent.tabgwox5m0.wasm",
        "hash": "sha256-y5ICvatEtqvHAUmm2IV6PBcwcYVO3XvCSrXdNwAjQd0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Immutable.wasm",
        "name": "System.Collections.Immutable.to6qdoyt18.wasm",
        "hash": "sha256-o26ieG8vFyPaktRgfNmtC2uSnn2+aAm49kAHL7NjZvw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.NonGeneric.wasm",
        "name": "System.Collections.NonGeneric.p70gjjw8w1.wasm",
        "hash": "sha256-63s7PJ0GOWeO9u2u4R+uiU44l/9a45C8k6h29cZrxzA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Collections.Specialized.wasm",
        "name": "System.Collections.Specialized.vnnt3ky8uo.wasm",
        "hash": "sha256-C/DGmbxbR99ivF9ZRwil/81uIMiVS2vg5p1cZ6Kd8ZI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.Annotations.wasm",
        "name": "System.ComponentModel.Annotations.zhcplxg4oy.wasm",
        "hash": "sha256-h2QFXWtsPH0ZWOqFCw6BG6zGGT4sO5gU3r7FXVPLYfY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.DataAnnotations.wasm",
        "name": "System.ComponentModel.DataAnnotations.788hkt1ewv.wasm",
        "hash": "sha256-5/sDVM3ltwFfRRf1MLc5UJTxoUGAN3oXKrkGXoOXKGM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.EventBasedAsync.wasm",
        "name": "System.ComponentModel.EventBasedAsync.lbc2n1ye5z.wasm",
        "hash": "sha256-H5pdyMo0uNKuVVvdhHCo2Z0AdA8/azj7Ty6yR6IFwZA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.Primitives.wasm",
        "name": "System.ComponentModel.Primitives.pex3ixe3ao.wasm",
        "hash": "sha256-Hx5pnLVJ4waO2eI7dBX8k4LdlO2y+w+HIpGvXu78QH8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.TypeConverter.wasm",
        "name": "System.ComponentModel.TypeConverter.hhn7an7dkt.wasm",
        "hash": "sha256-pSgFReG3UiACBN7EziMhiM6t2TbkyJv7P0MDiil4CSw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ComponentModel.wasm",
        "name": "System.ComponentModel.aawix4lw4j.wasm",
        "hash": "sha256-8hRq9TVKCCZk5twD3yT/b1rZXWILl2n+71ynBRRv090=",
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
        "name": "System.Configuration.tmaxdexzhp.wasm",
        "hash": "sha256-61at8UuJijrguXr3iRmVGhSIIdyQzHZ2bmEqEKHMQLQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Console.wasm",
        "name": "System.Console.yqhk4diokz.wasm",
        "hash": "sha256-JnFLlR9N2crxYuNdGY9e9jTqD1Q0SXil7s2V6uofYVo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Core.wasm",
        "name": "System.Core.23snkn3dp4.wasm",
        "hash": "sha256-7q311GdhNkFFhdtQLnLFUnn4FR+dNJvIh5GtwD50QBk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Data.Common.wasm",
        "name": "System.Data.Common.dwkc8wjaae.wasm",
        "hash": "sha256-M9PmSZ/XfKLsJ9Z/sh3ODs+6q3f/BbEBxjKqrH8we44=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Data.DataSetExtensions.wasm",
        "name": "System.Data.DataSetExtensions.97w5uv4ky2.wasm",
        "hash": "sha256-u9OtqSNlYMLpcRbWPtc0gu9J0926+qix52F/bpobACU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Data.wasm",
        "name": "System.Data.zfmtg4q89z.wasm",
        "hash": "sha256-31a4EGNTfUgvhTeHS7S3Mpd9+qOq29BVgSpqajZbJKo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Contracts.wasm",
        "name": "System.Diagnostics.Contracts.sc8wbz44vv.wasm",
        "hash": "sha256-tHbaxd5HSmqDLJs02iI7sdX5mWgsHkLy2k4fccy46QA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Debug.wasm",
        "name": "System.Diagnostics.Debug.ssebekbvl8.wasm",
        "hash": "sha256-VNLXTTQmtLCNKVsiWamELFiG4zPJ3yzu8G42C4wZrMQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.DiagnosticSource.wasm",
        "name": "System.Diagnostics.DiagnosticSource.avg0tantl3.wasm",
        "hash": "sha256-Zqo6Yj52dyROQRbI6elj++SH8YPvjsqtKmOtt3zGEgE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.FileVersionInfo.wasm",
        "name": "System.Diagnostics.FileVersionInfo.5rdbnjxo9x.wasm",
        "hash": "sha256-Yc1DWVLxn9os5gAjx6+DzlAM2hRezUlTVaX6I4HI9oI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Process.wasm",
        "name": "System.Diagnostics.Process.71tldatimy.wasm",
        "hash": "sha256-xZQl6zew1Mf617j5+nP64zsxbhsT8yUPt5qALh7T+ok=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.StackTrace.wasm",
        "name": "System.Diagnostics.StackTrace.eel6t5sjam.wasm",
        "hash": "sha256-1qJXNT6WsTcpN1lGDirUQpdkED1wwC0VFIlo9Ib2EhM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.TextWriterTraceListener.wasm",
        "name": "System.Diagnostics.TextWriterTraceListener.2lpqdrbyvj.wasm",
        "hash": "sha256-+lJ7oiA++eLN+/4f9UXr5wB3YdNGgFwvyC86yeWQWUw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Tools.wasm",
        "name": "System.Diagnostics.Tools.8r0w546mgb.wasm",
        "hash": "sha256-1GAxK9apArVuGjNIawzInskxa9PtmhwGp7OC0ftzaSI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.TraceSource.wasm",
        "name": "System.Diagnostics.TraceSource.evg5srbq7x.wasm",
        "hash": "sha256-qvyD+MYwDIdBV75VczNSAo/gXCmFsKj1Di3mdQYAA08=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Diagnostics.Tracing.wasm",
        "name": "System.Diagnostics.Tracing.c4nj3jgy8w.wasm",
        "hash": "sha256-zIpiAGOWqCMIIpPma0mMEiIGMngxYg3ZyS6IZDSITgI=",
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
        "name": "System.Drawing.Primitives.dsyadcp7bc.wasm",
        "hash": "sha256-YwREpJzMyty2yDFvVd95m8v1AZdFjXt6zl4iZ6qIrlI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Drawing.wasm",
        "name": "System.Drawing.r8ctzqfz3s.wasm",
        "hash": "sha256-xcFtav2p7V6nwhqDW+HRrCgkKHblS0jB8Eu7BVofRLE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Dynamic.Runtime.wasm",
        "name": "System.Dynamic.Runtime.2jg3ea504o.wasm",
        "hash": "sha256-kgO9JijE5Y+nsKBdosN7lh9Eva58t/x6kw+gobco3+g=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Formats.Asn1.wasm",
        "name": "System.Formats.Asn1.tj27qbtyne.wasm",
        "hash": "sha256-V0EjnZ0rFgT7wVDuY9zYI+cX3mbKPRFcj9HYK0j0h+o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Formats.Tar.wasm",
        "name": "System.Formats.Tar.jrl9x1kdrb.wasm",
        "hash": "sha256-kURjHhVansLXSpaOYOxC47B+vjC/627FHWa5iVD4fqQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Globalization.wasm",
        "name": "System.Globalization.8ol83agd9k.wasm",
        "hash": "sha256-SFVfAZup5GziScH1bye7oV9MouRFWD2CH+987eaP6M0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Globalization.Calendars.wasm",
        "name": "System.Globalization.Calendars.432uu2gifj.wasm",
        "hash": "sha256-8PO3Yx4RSUuWgiaNtbvba2JyFvcEgE9f/Y2LkphxNu8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Globalization.Extensions.wasm",
        "name": "System.Globalization.Extensions.os94lb91vi.wasm",
        "hash": "sha256-aOSw7PQHq0LyOdMAzEnt+rOC3u7FvK1pQNHh6uq48Lk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.wasm",
        "name": "System.IO.Compression.4j0xorrkai.wasm",
        "hash": "sha256-8JSDGy3FyM886NSTQp/J3wtegbnB09j9EX8mU7ogD2o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.Brotli.wasm",
        "name": "System.IO.Compression.Brotli.j05vktvklp.wasm",
        "hash": "sha256-3bwbUD5VA0DRaypOhUHZYBEjpDjx1zHDFxhVcFoRMBg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.FileSystem.wasm",
        "name": "System.IO.Compression.FileSystem.7zrgwijmmi.wasm",
        "hash": "sha256-XbaKxqupgD2Qn+Br7Ng0qWwLVQFEQT5CHklG6o+OXG0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Compression.ZipFile.wasm",
        "name": "System.IO.Compression.ZipFile.emark595wb.wasm",
        "hash": "sha256-hgZEZCNbsUN0bVCFRXyqiGeq8JARv6S34fgynQ0476c=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.FileSystem.AccessControl.wasm",
        "name": "System.IO.FileSystem.AccessControl.rewhgl76z1.wasm",
        "hash": "sha256-rc0zsxh36QDco7ks/eBafJilmLGei68KGFqwAJC3Fag=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.FileSystem.DriveInfo.wasm",
        "name": "System.IO.FileSystem.DriveInfo.dmvxxorjba.wasm",
        "hash": "sha256-Va/dcC4JaRV8TFx/qxlYrCqWfMxPSbz6pRP387em4Oo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.FileSystem.Primitives.wasm",
        "name": "System.IO.FileSystem.Primitives.5t9eqnsoiz.wasm",
        "hash": "sha256-1wD9/aMvUd7TdK9CZlxqYq+vOhUrrDsVDhC/J17IizM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.FileSystem.Watcher.wasm",
        "name": "System.IO.FileSystem.Watcher.2pss0nqamx.wasm",
        "hash": "sha256-uqNG4cjRwPqXEGUKkwf3qL+5fP+7hbLTII6r6hP+czo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.FileSystem.wasm",
        "name": "System.IO.FileSystem.cty81b1k5j.wasm",
        "hash": "sha256-wOANApo8vqGR+jdzfCDshct1HCbV1RptnyHfii1RdmE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.IsolatedStorage.wasm",
        "name": "System.IO.IsolatedStorage.c9c0rheycm.wasm",
        "hash": "sha256-EH2U3UhFhL0b2QMPJUmZMm+irz8MsUlpbzOJVahsSFc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.MemoryMappedFiles.wasm",
        "name": "System.IO.MemoryMappedFiles.sv95d7u6qw.wasm",
        "hash": "sha256-SMoee8W2YYxivEz2VrSNij5QeVGF7qKw8Yng9D8ALnI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Pipelines.wasm",
        "name": "System.IO.Pipelines.65ffzjinb2.wasm",
        "hash": "sha256-agngjqQXQ3B6CCmtyVPfexoqr9luROMcBevSMYZqeUY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Pipes.AccessControl.wasm",
        "name": "System.IO.Pipes.AccessControl.kbihw8uqqq.wasm",
        "hash": "sha256-AOlh7RlkYU2fvPlmXaUUbDwLNXVovqesjBvBgpSjLao=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.Pipes.wasm",
        "name": "System.IO.Pipes.cihgde06uy.wasm",
        "hash": "sha256-pGHpYJXMARMqBC0dIJPFUE5SUB0HMbyv1bo12yKrDw4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.UnmanagedMemoryStream.wasm",
        "name": "System.IO.UnmanagedMemoryStream.88ect3m4l0.wasm",
        "hash": "sha256-+KPmXbjEi6iNQqhbf4uYM3MQQg61fcFt4frNieSxnGg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.IO.wasm",
        "name": "System.IO.a9sbv9nbp4.wasm",
        "hash": "sha256-ztaSsMeNblw3WYm7xL+F7YpB1uK4PIwxPmnbmnSwpGU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.AsyncEnumerable.wasm",
        "name": "System.Linq.AsyncEnumerable.pyln97q6bg.wasm",
        "hash": "sha256-8RN68HBBeU5ck8cq7jZOcJ0CjVfs0kxJjn0paYtl+Qk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Expressions.wasm",
        "name": "System.Linq.Expressions.womfyvgnun.wasm",
        "hash": "sha256-oB5P74hL5b8Mk0WvX3yFp+PGJK1HuA9H1DaBHxAzj1k=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Parallel.wasm",
        "name": "System.Linq.Parallel.zr7hr3g4io.wasm",
        "hash": "sha256-Ydcjjj00tLyIBqmPBykQ3efbl6RUubOHe7KceuLnyMc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.Queryable.wasm",
        "name": "System.Linq.Queryable.ns0hmyg188.wasm",
        "hash": "sha256-R/zg8bYP5RZNHiZm9W87Sez9t7MvTCZSFgHGuWEAEEA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Linq.wasm",
        "name": "System.Linq.ppcgmf9c7o.wasm",
        "hash": "sha256-XaZItiE9ZTMZfqStlgfLPn3cBQFQRzrg7iKuFTM6NVg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Memory.wasm",
        "name": "System.Memory.vb0yeapaz9.wasm",
        "hash": "sha256-1YN4GXaecc26XUxU21L8BW/9BfwuIqd4AGBd1lFu9+4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Http.Json.wasm",
        "name": "System.Net.Http.Json.alujhet4mx.wasm",
        "hash": "sha256-npr+tzzLaVkH5+yTA6QWfzuAhIP4b9JJDVvyKmQ3lDY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Http.wasm",
        "name": "System.Net.Http.xmvh7bridh.wasm",
        "hash": "sha256-l6IS585fWXj/FykeMDCsP20udAP3Weo1zSSwfuwttw8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.HttpListener.wasm",
        "name": "System.Net.HttpListener.i7i9ejbld8.wasm",
        "hash": "sha256-bq6l6UJJw3Eakf+E952DXq/uoJIws947yLT3oxi5Cd8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Mail.wasm",
        "name": "System.Net.Mail.v3rf5dawse.wasm",
        "hash": "sha256-BbBXVntO13KI704IAEAVH5KRZP3VUz1RoA9OJh7kDcY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.NameResolution.wasm",
        "name": "System.Net.NameResolution.mx92xvj4oh.wasm",
        "hash": "sha256-ymuMKkgxDMc/pGqJK+ptslSt6kY6pFMnpsPzMb4Ukq4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.NetworkInformation.wasm",
        "name": "System.Net.NetworkInformation.7k7slt7v8s.wasm",
        "hash": "sha256-ufFRDK/1ZYur1amURrtNMqYRe0PqhBB0XjCVfgQ11hU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Ping.wasm",
        "name": "System.Net.Ping.mirbj3r46e.wasm",
        "hash": "sha256-8hBzVH5/hjSL4gC2ZcrBCyLy9C+KoI+NzZRG8623SRs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Primitives.wasm",
        "name": "System.Net.Primitives.345a7xrlba.wasm",
        "hash": "sha256-nUxBYNvIw3nPUihCnA8buHCcRtclq4lw8Sg9hoDWVMU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Quic.wasm",
        "name": "System.Net.Quic.cazmxplxyh.wasm",
        "hash": "sha256-JLFQYeZx9QlLzWuwCmjnbUv36Nfa8KS6q0kV55yDo+8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Requests.wasm",
        "name": "System.Net.Requests.npv5tzyfx8.wasm",
        "hash": "sha256-NUcZ/ff5Jr7NavJ6JEGlh19xPjy0W7yGVMjs4xG6K5s=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Security.wasm",
        "name": "System.Net.Security.lmkjqh58aa.wasm",
        "hash": "sha256-04Kj/kQOg6X1cnzOmJk65wykXibJjn7Xk3XARK+2hX0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.ServerSentEvents.wasm",
        "name": "System.Net.ServerSentEvents.8f8940kwv2.wasm",
        "hash": "sha256-HKkmlGCigrOc1zIbYzSnpr/5AJY0V6LJ7IMCngc8K+s=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.ServicePoint.wasm",
        "name": "System.Net.ServicePoint.8yy2rp8t01.wasm",
        "hash": "sha256-cNz53r8/oekbKCq83D+TmT/XCaYMN83X7VQCzywAM7w=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.Sockets.wasm",
        "name": "System.Net.Sockets.tiddtar9xc.wasm",
        "hash": "sha256-S+XskPMPk1nnT+dJuF3ty+zsKy4uGzop8tLDZo1pS90=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.WebClient.wasm",
        "name": "System.Net.WebClient.spdi7a3o7b.wasm",
        "hash": "sha256-EOvuJxZLoxi7m8DNUAi071I1yglO0atgv2WJK+4tFWQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.WebHeaderCollection.wasm",
        "name": "System.Net.WebHeaderCollection.qzik1b5uyx.wasm",
        "hash": "sha256-2uVF5uIDYZftqlGxBQn9uYxcOwHucZfm1UQLKseQk+E=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.WebProxy.wasm",
        "name": "System.Net.WebProxy.g7xor81jet.wasm",
        "hash": "sha256-HKl6L357SQ0Fdz2XJMqhRInUo43EdtTDoYlLHQEjmG0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.WebSockets.Client.wasm",
        "name": "System.Net.WebSockets.Client.rvgdbgb40f.wasm",
        "hash": "sha256-t3G4vyEjv2I0YCz7Ap1dQlpamQiupUe7BCs7ieu1vOA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.WebSockets.wasm",
        "name": "System.Net.WebSockets.perfrb39i1.wasm",
        "hash": "sha256-f1DIPIx+SQ3k5461TV7cIC+gy9k2WmpCnqJgmVyLdFI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Net.wasm",
        "name": "System.Net.vg2l3oaa6h.wasm",
        "hash": "sha256-QZ0FZpQMUFqgkwWqty1gabA1pFMZkXpgPhpclMmCxA4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Numerics.Vectors.wasm",
        "name": "System.Numerics.Vectors.5owqmorqwv.wasm",
        "hash": "sha256-5Y2LWfXlRGpt4iYc5MC8BK0JQ+zPMQErg3yN2HDhpn4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Numerics.wasm",
        "name": "System.Numerics.k1ch8jsbnr.wasm",
        "hash": "sha256-yPT2Zsh3bQa3luoXUdhi91Sh/9uDVl+k9fR7PttoRG8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ObjectModel.wasm",
        "name": "System.ObjectModel.m28eg6rhv7.wasm",
        "hash": "sha256-ogco2Rm75SCdpnY7HaOh3kDtLRoXov9OG4GEh9Rl2f0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.DataContractSerialization.wasm",
        "name": "System.Private.DataContractSerialization.j63mailp75.wasm",
        "hash": "sha256-mxt7gVrgcoJiyWQ5jI7FR0JI47W9uxw+r+J2poMlhkc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Uri.wasm",
        "name": "System.Private.Uri.kf2guqg0jf.wasm",
        "hash": "sha256-UPbw+DDoOkIaoCPl5TITTmvs6QBX3VeZ7lsjmJ/YJLY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Xml.wasm",
        "name": "System.Private.Xml.3kom3rm40g.wasm",
        "hash": "sha256-LUvfhi6uKYykdy6aDHYlXjpPSe91RcTZELn+xa0aiS4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Private.Xml.Linq.wasm",
        "name": "System.Private.Xml.Linq.ltl4x0v9fz.wasm",
        "hash": "sha256-h8fgSN7afuGw2Ddl522xD2eDKbC9CsPYUJSw6RsxtBY=",
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
        "name": "System.Reflection.DispatchProxy.hipc1t5afe.wasm",
        "hash": "sha256-1+4HtsjCurDORFog9wjQ0OTdIQH3uoBqeaJr2lr3E/g=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Emit.ILGeneration.wasm",
        "name": "System.Reflection.Emit.ILGeneration.wwath7io1s.wasm",
        "hash": "sha256-gClpps71G2EKzWrMJAO5bBHpyrRe6uiAlJFPZTyM7qA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Emit.Lightweight.wasm",
        "name": "System.Reflection.Emit.Lightweight.7aaw1pg58s.wasm",
        "hash": "sha256-D+FaVHL34XxooSqLli+7AymtKyUjNuJjxi/rCRVgn+c=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Emit.wasm",
        "name": "System.Reflection.Emit.k0pqccqwaz.wasm",
        "hash": "sha256-JN37F70S+W0WgBDSkPX2Cp4NAHltTi/9BO/xOXcaFu8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Extensions.wasm",
        "name": "System.Reflection.Extensions.722avyn84q.wasm",
        "hash": "sha256-79/jHhuFx7g7TxvkEi2L6XCThjpJwD4ivHzngX//0xI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Metadata.wasm",
        "name": "System.Reflection.Metadata.id4kv16n5e.wasm",
        "hash": "sha256-2mnN4HE+CsaTpk7QOlRkWxYMdOjpakfEntLhBdO2Z9I=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.Primitives.wasm",
        "name": "System.Reflection.Primitives.f9mnznjcse.wasm",
        "hash": "sha256-jUBcIdyk2tbNeIdaoqq3VXv6cqkx+VvLKVxwy2qp2Jc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.TypeExtensions.wasm",
        "name": "System.Reflection.TypeExtensions.5suhx0vvch.wasm",
        "hash": "sha256-69hoLVj1y1qukOIypQAtsvDdLIk6pVqlPJ4NWggBJc8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Reflection.wasm",
        "name": "System.Reflection.whj3ck3k4k.wasm",
        "hash": "sha256-jO5GcguGvW+fxCShIe5UGy6ZanvdqSRBkByseW453Lo=",
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
        "name": "System.Resources.Reader.ga6bbtgeda.wasm",
        "hash": "sha256-mPOTPydK9UN7rC3IOqEzH+3ZDSF8nzplu4bKzO+T9jk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Resources.ResourceManager.wasm",
        "name": "System.Resources.ResourceManager.phcxz1g8t7.wasm",
        "hash": "sha256-g5swGb99D8/hB5Kxn6G7StKW2YacAA1493LeOMwsXnE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Resources.Writer.wasm",
        "name": "System.Resources.Writer.h7pzlmlqa2.wasm",
        "hash": "sha256-o3jRaOw9MVrvXErrA5IGOOFgKeKisI2oFHAd+MUr7rs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.CompilerServices.Unsafe.wasm",
        "name": "System.Runtime.CompilerServices.Unsafe.v0xu6afmuv.wasm",
        "hash": "sha256-r6Af4luoE2hZtpcbQTfGFXKkqnomABHPtYaUDj3LeXQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.CompilerServices.VisualC.wasm",
        "name": "System.Runtime.CompilerServices.VisualC.c0vk7qfuq7.wasm",
        "hash": "sha256-BKc1dy78OnT7+fOb/CXRCTIHdmf706x5QvhIe+RTGBs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Extensions.wasm",
        "name": "System.Runtime.Extensions.7gffmsy02e.wasm",
        "hash": "sha256-9+EyL1/iNUhLgBdm/geEcRnZeLLijXR9VbeBZMdXEvs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Handles.wasm",
        "name": "System.Runtime.Handles.b3j4lhbvsp.wasm",
        "hash": "sha256-WT1++1u4nOuomaywciPZaf//nO6Kl1xXjoZkptRdQYc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.InteropServices.wasm",
        "name": "System.Runtime.InteropServices.1wbzjt3ynj.wasm",
        "hash": "sha256-D6K20pJw7BCU3YyQGP/DsyUfYn+CGcVvoWE2vSRLuQA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.InteropServices.RuntimeInformation.wasm",
        "name": "System.Runtime.InteropServices.RuntimeInformation.k6or61xhbq.wasm",
        "hash": "sha256-1JJBlP3gLUqVnk++R2xzQqyWEdGaietWYMhYPUJrr5Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Intrinsics.wasm",
        "name": "System.Runtime.Intrinsics.1yjlvvm3iq.wasm",
        "hash": "sha256-xywM8Z2ciDP/6wo152mfZr4ItQr4Yu65rAq0wyfkQjM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Loader.wasm",
        "name": "System.Runtime.Loader.p0qgw5hq7k.wasm",
        "hash": "sha256-uctu16wiebRrcOTsoaJEwFf+DH1j8bvi9XDkTJFGhXE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Numerics.wasm",
        "name": "System.Runtime.Numerics.s1noleq393.wasm",
        "hash": "sha256-sBfo4vXj80VyISeQRn8AHzTgFW1HanbVl66K1Y6VK18=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Formatters.wasm",
        "name": "System.Runtime.Serialization.Formatters.gd2a081w1j.wasm",
        "hash": "sha256-hFowjhhgXM4OOrb7yjw9IF6gHkzTIcaAI3rBvsACHkw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Json.wasm",
        "name": "System.Runtime.Serialization.Json.yi5ad2zufu.wasm",
        "hash": "sha256-hqBkUxtUElKbBifCwrTjG4adyoJCiOnPXngqLMSDNZw=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Primitives.wasm",
        "name": "System.Runtime.Serialization.Primitives.p1sm98x571.wasm",
        "hash": "sha256-fVC/s4Ykt3dMgosW9IIRKB30/iT2kLDqEkZPT7nVUDc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.Xml.wasm",
        "name": "System.Runtime.Serialization.Xml.zmzxax7gye.wasm",
        "hash": "sha256-Ve8QXbbhNCDC2b43uWGqXNEIz9eDQJGikQLKv4zFrMA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.Serialization.wasm",
        "name": "System.Runtime.Serialization.ctyzgyrihi.wasm",
        "hash": "sha256-AKM9pgxv/8U07LpITuXuVt77+EuR7ZaStMJ92EbH7kA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Runtime.wasm",
        "name": "System.Runtime.e0y1myuey7.wasm",
        "hash": "sha256-bmDo21I8p14ZnCXdOCjv5l6R76hdA4dNvYPnqR2MmJ4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.AccessControl.wasm",
        "name": "System.Security.AccessControl.pkgtz5qfy7.wasm",
        "hash": "sha256-KZAGGg7+N9wkDirlFr7jfR0du370I9OV7th5hT653lc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Claims.wasm",
        "name": "System.Security.Claims.rnvmznsmir.wasm",
        "hash": "sha256-x0gT5/HgCHESQly8wWA2it5PBULHdedHxHdiOwzTSzs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.wasm",
        "name": "System.Security.Cryptography.8o6pmvyg53.wasm",
        "hash": "sha256-+D8bMDbdvqmYo6wMJP43GgSjeoqNUk7NI0oNg3ta2Dk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.Algorithms.wasm",
        "name": "System.Security.Cryptography.Algorithms.rgzswqy8le.wasm",
        "hash": "sha256-dXWz1JqgZBHZHnaARsrzmSPtaJ1SNZU6ZDWOBDiRulU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.Cng.wasm",
        "name": "System.Security.Cryptography.Cng.4nz59bj525.wasm",
        "hash": "sha256-sGi7XypXEJBcJ6OHfe0F2HlGUTLd63fELgadDXEXoxk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.Csp.wasm",
        "name": "System.Security.Cryptography.Csp.jut03xkfhr.wasm",
        "hash": "sha256-G57sEcL8eZpvERKZkPekb+NcblzLHuBR2gYWbmhMS+c=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.Encoding.wasm",
        "name": "System.Security.Cryptography.Encoding.x6tsax21dj.wasm",
        "hash": "sha256-N6/2c9lWQkKgsHcOt2kOpB2R1A+K2AobtBD3/d+JfD0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.OpenSsl.wasm",
        "name": "System.Security.Cryptography.OpenSsl.8cwnuqry2l.wasm",
        "hash": "sha256-iEboWVTuH6eJrmFQEe0pjm4H+H/ag87aFAbzkXIzp00=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Cryptography.Primitives.wasm",
        "name": "System.Security.Cryptography.Primitives.wz8g0roqk9.wasm",
        "hash": "sha256-dCKR/WoscH+7IIgI4mr5MU1iM9qK2vkYs7X1QIQ37KQ=",
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
        "name": "System.Security.Cryptography.X509Certificates.cm5h86j3m5.wasm",
        "hash": "sha256-tAasxmYDwC9a+Z8VKcCiqHMhVui2j9zi9aWOATYUBv0=",
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
        "name": "System.Security.Principal.Windows.etrnujb4wy.wasm",
        "hash": "sha256-blkNqVYE8hnR0xvxKn7p5zJP9dYTGOSMT2Bdwd/V5Rs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.Principal.wasm",
        "name": "System.Security.Principal.xu056u1ch5.wasm",
        "hash": "sha256-Z17U2NcXRkivjCFP3aburtHr6Xn9bczn9v4r6jTh1KY=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.SecureString.wasm",
        "name": "System.Security.SecureString.0k442d6jgp.wasm",
        "hash": "sha256-8FmSuWLe0/uScELWAgeo3o+4UifvmMSTpvFIB2dVfNU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Security.wasm",
        "name": "System.Security.u84awx1pgv.wasm",
        "hash": "sha256-AjiGnqRsHrGioyUdTxy2ua5jPkw9ztWYolE6Wh/d1BE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ServiceModel.Web.wasm",
        "name": "System.ServiceModel.Web.f93tj7eoea.wasm",
        "hash": "sha256-PfuKSy3+KlT4bRXA5zzeV3Cz8J+NELIKqPxsRxvI3u0=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ServiceProcess.wasm",
        "name": "System.ServiceProcess.3r2kdm8pcz.wasm",
        "hash": "sha256-709a0ToqNMU81HSxDTNrIWJIOG1PH964JhfcXugxsYc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encoding.wasm",
        "name": "System.Text.Encoding.5vojv66xcx.wasm",
        "hash": "sha256-XwS3/mr0TGne1JhURMmHzcKPsF5LoRw2md+8h1/xCek=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encoding.CodePages.wasm",
        "name": "System.Text.Encoding.CodePages.nnghcjkvki.wasm",
        "hash": "sha256-bdkSQFxoXwXtfyKI3a9hFc6T9WmgpMzyXYkPMW5TwlQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encoding.Extensions.wasm",
        "name": "System.Text.Encoding.Extensions.48qnmwjov9.wasm",
        "hash": "sha256-fJVPuMcviG3F9JzoBQTIf/xrxf4X2r12OB1oGaKk4ak=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Encodings.Web.wasm",
        "name": "System.Text.Encodings.Web.zl3zqgk0vk.wasm",
        "hash": "sha256-B9fZ9iQQRklbzmogxHfgUdLzYY8gPiVb8hdpSFhvZy4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.Json.wasm",
        "name": "System.Text.Json.vo1qjih7nn.wasm",
        "hash": "sha256-D9YvwmXyarIdQisxZ72t5M005DmIT2XMuAQULBOUSRs=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Text.RegularExpressions.wasm",
        "name": "System.Text.RegularExpressions.u8qsrbg2k7.wasm",
        "hash": "sha256-3vp6A+TAbglqHXSq5Y8F7klLq3M4V7EzAA2Cd4Ocz58=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.AccessControl.wasm",
        "name": "System.Threading.AccessControl.skdizhbtk3.wasm",
        "hash": "sha256-POw1tOXUyg5RVjL8NI55j9TXGxkB2/GUlgInSYPFMO8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Channels.wasm",
        "name": "System.Threading.Channels.ewt62bdcpm.wasm",
        "hash": "sha256-IgVDDafn4JDXozxPIsjPMLm5R5nJfai95e7I4QnDa8Q=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Overlapped.wasm",
        "name": "System.Threading.Overlapped.mx25iycsmc.wasm",
        "hash": "sha256-GngEqlZKx8Qa7t1fKDuJEeiZzSwubsjhZKE8rblNz08=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.Dataflow.wasm",
        "name": "System.Threading.Tasks.Dataflow.1ik1l1b41h.wasm",
        "hash": "sha256-CafZa3tvF6NQBwNCpx7/9npHgftS7FLkrkbVrUfgdO4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.Extensions.wasm",
        "name": "System.Threading.Tasks.Extensions.sfgggatu9l.wasm",
        "hash": "sha256-yIBymsiGe52tgpIGkEBuJneYB0CXAjSR25l+DiWZazk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.Parallel.wasm",
        "name": "System.Threading.Tasks.Parallel.tw2k3kmfbm.wasm",
        "hash": "sha256-Q+EbtZ7nyFP2TUyvSWKd3DgrFwIVswK5wJDQ+MsHTHo=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Tasks.wasm",
        "name": "System.Threading.Tasks.jyaf0nnrau.wasm",
        "hash": "sha256-xS4DUvxMrU3nEwdfdtHObZiyE/NreJgZ4CQGFxyzewg=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Thread.wasm",
        "name": "System.Threading.Thread.bmd799jukj.wasm",
        "hash": "sha256-s10VoOocFbx9PFLNQL8/d7uOfAKGZs2d8y7i9FwInHI=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.ThreadPool.wasm",
        "name": "System.Threading.ThreadPool.mx60t041r4.wasm",
        "hash": "sha256-5h8DvNJJUBTObBvh8rVLvAAat3dEoxJYeAPagDutZo4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.Timer.wasm",
        "name": "System.Threading.Timer.h77bpk4usf.wasm",
        "hash": "sha256-PUKtww3hFnJXuSqY0f1E/mCQGH/upjfg3E+8FDUKauM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Threading.wasm",
        "name": "System.Threading.m84ea5kjix.wasm",
        "hash": "sha256-CnUO0/0YBUrVsVplDd27lrcSZ4hU9cGoJTxjUtZ0ph8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Transactions.Local.wasm",
        "name": "System.Transactions.Local.vjy1w7nhuq.wasm",
        "hash": "sha256-1U5d3PtmFrKLipHdM+eLItqix6JPm6NZHHGHkbSHUk4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Transactions.wasm",
        "name": "System.Transactions.lm95w86y0x.wasm",
        "hash": "sha256-/d+/JaNupGp5c8uTos/s6EbPaGG7wwZhzZCxC+7GFa4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.ValueTuple.wasm",
        "name": "System.ValueTuple.37yuez73dj.wasm",
        "hash": "sha256-n3KO71CVRvMuAq4YbQccEXZL37wyd9LYtKBBXRTf3vE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Web.wasm",
        "name": "System.Web.6pm7jhgcbu.wasm",
        "hash": "sha256-Vrr/U1dzKmvS0GxU6dgx5LfpeVHXI9d2XV45SlhLhRc=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Web.HttpUtility.wasm",
        "name": "System.Web.HttpUtility.uo8hwcfq94.wasm",
        "hash": "sha256-PoadJ9NvaWQs9lfeIBQKJsLeEcctmF8YXt1or/o8NO4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Windows.Extensions.wasm",
        "name": "System.Windows.Extensions.quwz1fxlaw.wasm",
        "hash": "sha256-Eh4ZyIvqo34A19igrKJBmrt1iW+vrM/Cjy/zUQHTPe8=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Windows.wasm",
        "name": "System.Windows.f7q1laww5j.wasm",
        "hash": "sha256-FSiP4eP2qSzsWE0glMg+POSNdsPCVSmqUrynsLNqWdU=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.wasm",
        "name": "System.Xml.420qqu8b94.wasm",
        "hash": "sha256-NKv1jPqEC8zBtqIRVJa233VYsw6sfJDTx2bdZHpkTZ4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.Linq.wasm",
        "name": "System.Xml.Linq.oywnbte638.wasm",
        "hash": "sha256-oOV6VC1H5wxp8Amp2NxmQF5yjzZw4DI6vnbNycDRDcM=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.ReaderWriter.wasm",
        "name": "System.Xml.ReaderWriter.io8dfhiylc.wasm",
        "hash": "sha256-zp8IT5s8HY33gvp/VWNAGlOp4oUOsOSFgXOLHSxf5JQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.Serialization.wasm",
        "name": "System.Xml.Serialization.23tmlr7zbu.wasm",
        "hash": "sha256-QhKnJ0/SStHigpS+Pji/bmty+fzZtDMJ1HmsRsvmF/U=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.XDocument.wasm",
        "name": "System.Xml.XDocument.52qckfyt8d.wasm",
        "hash": "sha256-E2Gd5wVfNjCvi0JpOODmTm+FyUE0f0zDswKd2MusNF4=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.XPath.XDocument.wasm",
        "name": "System.Xml.XPath.XDocument.odpp5i46n6.wasm",
        "hash": "sha256-REbtDFR2VGrsgGzGw/8eIIL4n4cl9xoGdR8lAF/ILPk=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.XPath.wasm",
        "name": "System.Xml.XPath.kq6o8cuziq.wasm",
        "hash": "sha256-HJDOan11IUBzlzrjdelYiVvckcMokde4lmDTEh9EAaQ=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.XmlDocument.wasm",
        "name": "System.Xml.XmlDocument.4t4sexnyaw.wasm",
        "hash": "sha256-WOlilfEatXFFoIQysrPiHWTjPgKgkmUENMgj/8nsEKE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.Xml.XmlSerializer.wasm",
        "name": "System.Xml.XmlSerializer.za5mns16iy.wasm",
        "hash": "sha256-W7uFjr/TMK4csyBQLgbzysGPtQfUCj/Oa/4FlFnWGnE=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "System.wasm",
        "name": "System.ucdpr4j4v7.wasm",
        "hash": "sha256-IjVdePfZ9iviTS5RAy/K0xWOkpXC/NbwsJH/76/mcp4=",
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
        "name": "WindowsBase.6306p4b7y4.wasm",
        "hash": "sha256-8v7qB30Hns8d3u/8m80jAp75yP8uPJPbYWLQUnCwFQA=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "mscorlib.wasm",
        "name": "mscorlib.k1plxbe0uu.wasm",
        "hash": "sha256-+OAE5927p5jgfqu7dDkWwu6GYHfqLi5oy5nMw72V97o=",
        "cache": "force-cache"
      },
      {
        "virtualPath": "netstandard.wasm",
        "name": "netstandard.hsgbwlytoc.wasm",
        "hash": "sha256-wR2B1JmxGTcTjQ5VHl06b/nxi2XJy48E04ExnhXZ3GY=",
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
          "name": "DemoCenter.resources.rdt98x3aov.wasm",
          "hash": "sha256-/9hyt6umeQlnv3NXDS2PJGAcR/uUcVS6tTLn73binu0=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Avalonia.Charts.resources.wasm",
          "name": "Eremex.Avalonia.Charts.resources.i6dfyyw3c2.wasm",
          "hash": "sha256-hiXTmmb7b2SG6AK0JDlSx9sHb0iPJeFqlgpgIOdUPg0=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Avalonia.Controls.ApplicationServices.resources.wasm",
          "name": "Eremex.Avalonia.Controls.ApplicationServices.resources.k2ncb5gd4t.wasm",
          "hash": "sha256-zF4KQFaCmoUM0N+y8V/88mCIRDqu8f79RWRP/v6ablg=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Avalonia.Controls.resources.wasm",
          "name": "Eremex.Avalonia.Controls.resources.vstmqwrime.wasm",
          "hash": "sha256-X0rU6DH6E8EI4QgelHFXGRHkiTseplRT8mpKVP0dCUQ=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Drawing.Skia.resources.wasm",
          "name": "Eremex.Drawing.Skia.resources.kppwlvlq6u.wasm",
          "hash": "sha256-KCvY7FIsCDxrVojJzS5yeQyMscLAahAWkEJqndXNGZc=",
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
          "name": "DemoCenter.resources.wp07mazqzj.wasm",
          "hash": "sha256-nBI8cPrzJND+wgRt0uGCs92xuFJgxgQlkJYRNe1S1WA=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Avalonia.Charts.resources.wasm",
          "name": "Eremex.Avalonia.Charts.resources.91uboegpdk.wasm",
          "hash": "sha256-M0M5eZs8J/2kxsJsvzMcZ6lpE6eOFlFmJjBKTxur1/w=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Avalonia.Controls.ApplicationServices.resources.wasm",
          "name": "Eremex.Avalonia.Controls.ApplicationServices.resources.rr0aej6mra.wasm",
          "hash": "sha256-mQMBDtNBdH6Y32rMDBlEV9w2w6cExF0JExZJaBnpu3U=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Avalonia.Controls.resources.wasm",
          "name": "Eremex.Avalonia.Controls.resources.nra9h5pfru.wasm",
          "hash": "sha256-PSzfysC/BlKTYVyWfUwmcGE6da75kUM9GSJH8OZaCTg=",
          "cache": "force-cache"
        },
        {
          "virtualPath": "Eremex.Drawing.Skia.resources.wasm",
          "name": "Eremex.Drawing.Skia.resources.mbrj69x80e.wasm",
          "hash": "sha256-EqsSLwkY2gcjp6Na07KNeBZEEE1UH/Ls4/XgnWoeTOU=",
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
