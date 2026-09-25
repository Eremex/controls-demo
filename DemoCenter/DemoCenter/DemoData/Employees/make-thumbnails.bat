@echo off
rem Rebuilds Thumbnails\ from the full size portraits in this folder.
rem
rem Why a separate set: the grids draw a portrait at 24 or 48 pixels, and scaling a 192 pixel
rem image down that far at draw time is what made the small pictures look rough. 96 divides
rem exactly into both sizes, and is drawn pixel for pixel at 200%% display scaling.
rem
rem Lanczos rather than the default: it keeps the thin contour lines of the artwork legible at
rem small sizes, where a box filter turns them into grey mush.
rem
rem Needs ImageMagick 7 on PATH. Run it after replacing or adding a portrait.

setlocal
where magick >nul 2>nul || (echo ImageMagick 7 ^(magick^) was not found on PATH. & exit /b 1)

if not exist "%~dp0Thumbnails" mkdir "%~dp0Thumbnails"

for %%f in ("%~dp0*.png") do (
    echo %%~nxf
    magick "%%f" -filter Lanczos -resize 96x96 -colors 64 -strip "%~dp0Thumbnails\%%~nxf"
)

echo Done.
endlocal
