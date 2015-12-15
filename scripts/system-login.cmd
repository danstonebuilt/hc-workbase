echo off
cls
for /f "eol=; tokens=* delims=," %%x in (%DN-CONT%\general\set-system-login.txt) do (
   echo %%x 
)
pause  
