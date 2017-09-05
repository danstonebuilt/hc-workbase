@echo on
cls
setlocal enabledelayedexpansion
for /f "eol=; tokens=* delims=," %%x in (C:\workstation\hc-workbase\general\qck_var_pth.txt) do (
   set tmppath=%%xi
   set tmppath=!tmppath!
)

echo %tmppath%
pause 

