@echo off
cd /d %~dp0

ping 127.0.0.1 -n 2 

if exist CameraFwUpdateLog*.txt del CameraFwUpdateLog*.txt
if not exist CameraFirmwareUpdate.exe (	
	set errorlevel=-1
	echo Error!! Do not exist CameraFirmwareUpdate.exe
	goto fail
)
call CameraFirmwareUpdate.exe
for /f "" %%i in ('dir /b CameraFwUpdateLog*.txt') do set Logfile=%%i

find /i "[NG]" %Logfile% >nul
if not errorlevel 1 (
	set errorlevel=1
	goto fail
)
find /i "[OK]" %Logfile% >nul
if not errorlevel 1 (
	echo SUCCESSFULLY TEST
	goto end
)
set errorlevel=1
goto fail

echo SUCCESSFULLY TEST
goto end

:fail
echo.
echo errorlevel:%errorlevel%
ECHO FAILED TEST
echo.
if exist type %Logfile% (
	echo %Logfile% is as following:
	echo. 
	type %Logfile%	
)
exit /b %errorlevel%

:end
cd /d %~dp0
echo.
if exist type %Logfile% (
	echo %Logfile% is as following:
	echo. 
	type %Logfile%	
)
