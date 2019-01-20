@echo off
cd /d "C:\Program Files (x86)\STMicroelectronics\STM32 ST-LINK Utility\ST-LINK Utility"
set FWFile=%~1
echo "%FWFile%"
ping 127.0.0.1
goto end

echo Disabling Read protection:
ST-LINK_CLI.exe 每c UR 每OB RDP=0
if errorlevel 1 goto fail 
echo Erase Memmory
ST-LINK_CLI.exe 每c UR -me
if errorlevel 1 goto fail 

echo Programming start
ST-LINK_CLI.exe -c UR -v after_programming -p "%~dp0%FWFile%"
if errorlevel 1 goto fail 

echo ENabling Read protection:
ST-LINK_CLI.exe 每c UR 每OB RDP=1

echo SUCCESSFULLY TEST
goto end

:fail
echo errorlevel:%errorlevel%
ECHO FAILED TEST
exit /b %errorlevel%

:end
cd /d %~dp0
echo 1>1.txt