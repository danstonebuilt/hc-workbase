@echo off
cls
for /f "eol=; tokens=* delims=," %%x in (C:\workstation\hc-workbase\general\__qck_var_pth.txt) do (
   set dnvar=%%x
   set dnvar=!%dnvar%!
   echo %dnvar%   
)
pause  