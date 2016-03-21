@echo off

cd\

cd %temp%

ren "EditorLineEnds.ttr" "EditorLineEnds_"%RANDOM%".ttr"

if defined %programfiles(x86)% (
	start "%programfiles(x86)%\Borland\BDS\4.0\Bin\" bds.exe -pDelphi
)
else (
	start "%programfiles%\Borland\BDS\4.0\Bin\" bds.exe -pDelphi
)

