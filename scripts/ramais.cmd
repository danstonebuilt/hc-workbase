@echo off
cls
for /f "eol=; tokens=* delims=," %%x in (%DN-CONT%\general\ramais.txt) do (
   echo %%x 
)
pause  
