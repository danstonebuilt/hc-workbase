@echo off

        
	set dirpath=C:\Users\Usuario\Desktop
        set dwlpath=C:\Users\Usuario\Downloads
	

	cd %dirpath%
	
	if exist *_lkp.* (
	    
	    move *_lkp.*  %dirpath%\scratch\wkb-lab 
		rem echo Existe!
	)

	if exist *_rsc.* (
	  move  *_rsc.*  %dirpath%\scratch
	)	 
      

	if exist *_app.* (
		move  *_app.* %dwlpath%
	)


	if exist *_tmp.* (
		move  *_tmp.* %dirpath%\old
	)
    
	
pause
