@echo off
cls
for /f "eol=; tokens=* delims=," %%x in (%DN-CONT%\general\accounts.txt) do (
   echo %%x 
)
pause  
