@echo off
	
	if exist *.exe (
	    move *.exe C:\Users\Daniel\Desktop\anytemp 
		rem echo Existe!
	) else (
		echo não existe!
	)
	
pause