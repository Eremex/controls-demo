@echo off
rem Resamples the car artwork to the size the demo ships.
rem
rem Source images are rendered at 1024x576; the demo draws them at 256x144, so 512x288 is an exact
rem 2:1 downscale in the pages and an exact 1:1 at 200%% display scaling.
rem
rem Lanczos rather than the default: it keeps the thin contour lines of the artwork legible.
rem A 64 colour palette holds the flat fills of the drawings and keeps a file near 25 KB.
rem
rem Pass the folder holding the full size renders as the first argument; output overwrites the
rem PNGs in this folder. Needs ImageMagick 7 on PATH.

setlocal
where magick >nul 2>nul || (echo ImageMagick 7 ^(magick^) was not found on PATH. & exit /b 1)

if "%~1"=="" (echo Usage: convert.bat ^<folder-with-full-size-renders^> & exit /b 1)
if not exist "%~1" (echo %~1 does not exist. & exit /b 1)

for %%f in ("%~1\*.png") do (
    echo %%~nxf
    magick "%%f" -filter Lanczos -resize 512x288 -colors 64 -strip "%~dp0%%~nxf"
)

echo Done.
endlocal
