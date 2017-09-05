echo off
cls
for /f "eol=; tokens=* delims=," %%x in (%DN-CONT%\general\serials.txt) do (
   echo %%x 
)
pause  
