rem @ FILE     Nome de arquivo.
rem @ FNAME    Nome do arquivo sem extensão.
rem @ EXT      Extensão de nome de arquivo.
rem @ PATH     Caminho completo do arquivo.
rem @ RELPATH  Caminho relativo do arquivo.
rem @ ISDIR    Avaliado como TRUE se um tipo de arquivo é um diretório. Caso contrário, essa variável é avaliada como FALSE.
rem @ FSIZE    Tamanho do arquivo em bytes.
rem @ FDATE    Último carimbo de data de modificação no arquivo.
rem @ FTIME    Carimbo de hora modificado por último no arquivo

@echo off

set dirpath=C:\Users\Usuario\Desktop\old

forfiles /p %dirpath% /m *.pdf /c "cmd /c echo @file @fdate"

pause