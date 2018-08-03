@echo off

::set global variables

setlocal EnableDelayedExpansion
set APPNAME=UpstoxNet
set LOGFILE=%USERPROFILE%\Documents\%APPNAME%_Install.log
set DLLPATH=%~dp0
set WORDIR=%~dp0
set BATFILE=%~0
set EXCELBIT=1
set OSBIT=1
set ARCHITECTURE=1
set INSTALLDIR=C:\Users\Public\Documents
set EXCELBUILD=11.0
set EXCELVERSION=2003
set NETFRAME=%WINDIR%\Microsoft.NET\Framework6464\v4.0*

cd /d %DLLPATH%

set MSG=#START#
echo %MSG% >>"%LOGFILE%"

set MSG=Batch file started at %DATE% %TIME%
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=Registering .Net DLL using regasm
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=APP NAME : %APPNAME%
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=LOG FILE : %LOGFILE%
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=BAT FILE : %BATFILE%
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=WORKING DIRECTORY : %WORDIR%
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=DLL PATH : %DLLPATH%
echo %MSG% && echo %MSG% >>"%LOGFILE%"

echo. && echo.
set MSG=*** User Consent to run this batch file ***
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=This batch file is created for easy registration of DLL and settings for the end user. Without this batch file, user has to register DLL and do settings manually that may take hours. Eventhough we tried hard to make this file, some settings may not work and may still need manual intervention due to OS specific issues.
echo %MSG% && echo %MSG% >>"%LOGFILE%"

echo. && echo.
set MSG=*** Actually what this batch file does? ***
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=Since this file can be viewed in Notepad, You can open it yourself and see what it does.
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=For others, here is what it does...
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=* Checks for Administrator rights
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=* Checks for OS and Excel bitness
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=* Registering the DLL file as per OS and Excel bit
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=* Enabling Macros in Excel
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=* Enabling ActiveX in Excel
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=* Enable External Data Connections in Excel
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=* Change throttle interval to 0 (else Excel RTD will not update in Real Time.Default delay is 2 sec)
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=* Browser Emulation for Excel
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=* Internet Explorer Options and Registry settings
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=* Adding Sites to Trusted Sites
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=* Optionally disable UAC (Disabling UAC will not prompt for Permission to run an application in Admin Mode)
echo %MSG% && echo %MSG% >>"%LOGFILE%"

echo. && echo.
echo Do you want to proceed [Type Y/N and Press Enter]? >>"%LOGFILE%"
set /P INPUT=Do you want to proceed [Type Y/N and Press Enter]?
echo %INPUT% && echo %INPUT% >>"%LOGFILE%"

if /I "%INPUT%" equ "N" (
set ERR=%APPNAME% DLL is not registered. Exiting Command Prompt.
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,48, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
) 

if NOT "%DLLPATH%"=="%DLLPATH:(=%" (
set ERR=Your folder contains special characters '^('.Please rename the folder without special character and try again.
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,48, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)

if NOT "%DLLPATH%"=="%DLLPATH:)=%" (
set ERR=Your folder contains special characters '^)'.Please rename the folder without special character and try again.
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,48, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)


::::::::ADMIN
echo. && echo.
set MSG=Checking for Administrative rights
echo %MSG% && echo %MSG% >>"%LOGFILE%"

net session >nul 2>&1
if NOT %errorLevel% == 0 (
set ERR=You do not have Administrative rights. I assume you are running this file without extracting it. You can try as follows. Extract the downloaded Zip file contents to a folder. Right click on the Register.bat file and select Run As Administrator
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,16, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
) 

set MSG=ADMIN RIGHTS : YES
echo %MSG% && echo %MSG% >>"%LOGFILE%"

echo. && echo.
set MSG=List of files in the current directory
echo %MSG% && echo %MSG% >>"%LOGFILE%"

for %%i in (%WORKDIR%*) do echo %%i && echo %%i >>"%LOGFILE%"

echo. && echo.
set MSG=Checking whether Excel is running or not
echo %MSG% && echo %MSG% >>"%LOGFILE%"

tasklist /FI "IMAGENAME eq EXCEL.exe" 2>NUL | find /I /N "EXCEL.exe">NUL
if "%ERRORLEVEL%"=="0" (
set ERR=Excel is running. Please close Excel and try again.
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,48, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)

set MSG=EXCEL RUNNING : NO
echo %MSG% && echo %MSG% >>"%LOGFILE%"


echo. && echo.
set MSG=Checking whether AmiBroker is running or not
echo %MSG% && echo %MSG% >>"%LOGFILE%"

tasklist /FI "IMAGENAME eq Broker.exe" 2>NUL | find /I /N "Broker.exe">NUL
if "%ERRORLEVEL%"=="0" (
set ERR=AmiBroker is running. Please close AmiBroker and try again.
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,48, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)

set MSG=AMIBROKER RUNNING : NO
echo %MSG% && echo %MSG% >>"%LOGFILE%"


::::::::OS
echo. && echo.
set MSG=Checking for OS Architecture
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=PROCESSOR_ARCHITECTURE : %PROCESSOR_ARCHITECTURE%
echo %MSG% && echo %MSG% >>"%LOGFILE%"

reg query "HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Environment" /v PROCESSOR_ARCHITECTURE  | find "AMD64" >nul 2>&1
if %errorlevel% equ 0 (
set OSBIT=64
set ARCHITECTURE=AMD64
) else (
	reg query "HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Environment" /v PROCESSOR_ARCHITECTURE  | find "x86" >nul 2>&1
	if !errorlevel! equ 0 (
	set OSBIT=32
	set ARCHITECTURE=X86
	) 
)

if %OSBIT% == 1 (
set ERR=Unable to find Operating System Architecture. Please try again. If problem persits contact Administrator.
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,16, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
) 

set MSG=CPU ARCHITECTURE : %ARCHITECTURE%
echo %MSG% && echo %MSG% >>"%LOGFILE%"
set MSG=OS BIT : %OSBIT%
echo %MSG% && echo %MSG% >>"%LOGFILE%"
echo. && echo.

if %OSBIT% == 64 (
goto CheckMSOffice64
) else (
goto CheckMSOffice32
)

::::::::MS OFFICE
:CheckMSOffice64
set MSG=Checking MS Excel version and bitness in AMD64
echo %MSG% && echo %MSG% >>"%LOGFILE%"

reg query HKLM\Software\Wow6432Node\Microsoft\Office\16.0\Excel >nul 2>&1
if %errorlevel% equ 0 (
set EXCELBIT=32
set EXCELBUILD=16.0
set EXCELVERSION=2016
goto OfficeEnd
) 

reg query HKLM\Software\Microsoft\Office\16.0\Excel >nul 2>&1
if %errorlevel% equ 0 (
set EXCELBIT=64
set EXCELBUILD=16.0
set EXCELVERSION=2016
goto OfficeEnd
) 

reg query HKLM\Software\Wow6432Node\Microsoft\Office\15.0\Excel >nul 2>&1 
if %errorlevel% equ 0 (
set EXCELBIT=32
set EXCELBUILD=15.0
set EXCELVERSION=2013
goto OfficeEnd
) 

reg query HKLM\Software\Microsoft\Office\15.0\Excel  >nul 2>&1
if %errorlevel% equ 0 (
set EXCELBIT=64
set EXCELBUILD=15.0
set EXCELVERSION=2013
goto OfficeEnd
) 

reg query HKLM\Software\Wow6432Node\Microsoft\Office\14.0\Excel >nul 2>&1
if %errorlevel% equ 0 (
set EXCELBIT=32
set EXCELBUILD=14.0
set EXCELVERSION=2010
goto OfficeEnd
) 

reg query HKLM\Software\Microsoft\Office\14.0\Excel >nul 2>&1
if %errorlevel% equ 0 (
set EXCELBIT=64
set EXCELBUILD=14.0
set EXCELVERSION=2010
goto OfficeEnd
) 

reg query HKLM\Software\Wow6432Node\Microsoft\Office\12.0\Excel >nul 2>&1
if %errorlevel% equ 0 (
set EXCELBIT=32
set EXCELBUILD=12.0
set EXCELVERSION=2007
goto OfficeEnd
)

reg query HKLM\Software\Microsoft\Office\12.0\Excel >nul 2>&1
if %errorlevel% equ 0 (
set EXCELBIT=64
set EXCELBUILD=12.0
set EXCELVERSION=2007
goto OfficeEnd
)
 

:CheckMSOffice32
set MSG=Checking MS Excel version and bitness in X86
echo %MSG% && echo %MSG% >>"%LOGFILE%"

reg query HKLM\Software\Microsoft\Office\16.0\Excel >nul 2>&1
if %errorlevel% equ 0 (
set EXCELBIT=32
set EXCELBUILD=16.0
set EXCELVERSION=2016
goto OfficeEnd
) 

reg query HKLM\Software\Microsoft\Office\15.0\Excel >nul 2>&1
if %errorlevel% equ 0 (
set EXCELBIT=32
set EXCELBUILD=15.0
set EXCELVERSION=2013
goto OfficeEnd
) 

reg query HKLM\Software\Microsoft\Office\14.0\Excel >nul 2>&1
if %errorlevel% equ 0 (
set EXCELBIT=32
set EXCELBUILD=14.0
set EXCELVERSION=2010
goto OfficeEnd
) 

reg query HKLM\Software\Microsoft\Office\12.0\Excel >nul 2>&1
if %errorlevel% equ 0 (
set EXCELBIT=32
set EXCELBUILD=12.0
set EXCELVERSION=2007
goto OfficeEnd
) 

:OfficeEnd
if %EXCELBIT% == 1 (
set ERR=Unable to find MS Office bitness. Please try again. If problem persits contact Administrator.
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,16, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
) 

set MSG=EXCEL BIT : %EXCELBIT%
echo %MSG% && echo %MSG% >>"%LOGFILE%"
set MSG=EXCEL BUILD : %EXCELBUILD%
echo %MSG% && echo %MSG% >>"%LOGFILE%"
set MSG=EXCEL VERSION : %EXCELVERSION%
echo %MSG% && echo %MSG% >>"%LOGFILE%"
echo. && echo.


::::::::DLL REGISTER
set MSG=Setting .Net Framework as per Excel Bit
echo %MSG% && echo %MSG% >>"%LOGFILE%"

if %EXCELBIT% == 32 (
set NETFRAME=%WINDIR%\Microsoft.NET\Framework\v4.0*
) else (
set NETFRAME=%WINDIR%\Microsoft.NET\Framework64\v4.0*
)

set MSG=Setting System Directory as per Excel Bit and OS Bit
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=Checking .Net Framework Installation
echo %MSG% && echo %MSG% >>"%LOGFILE%"

if not exist %NETFRAME% (
set ERR=Unable to find .Net Framework installation. Please try again. If problem persits contact Administrator.
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,16, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)

set MSG=DOTNET FRAME : %NETFRAME%
echo %MSG% && echo %MSG% >>"%LOGFILE%"

if NOT EXIST "%DLLPATH%%APPNAME%.dll" (
set ERR=Unable to copy file %DLLPATH%%APPNAME%.dll : You are running the batch file without extracting or File may be missing or You do not have rights to copy
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,16, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)

if NOT EXIST  "%DLLPATH%Newtonsoft.Json.dll" (
set ERR=Unable to copy file %DLLPATH%Newtonsoft.Json.dll : You are running the batch file without extracting or File may be missing or You do not have rights to copy
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,16, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)

if NOT EXIST "%DLLPATH%NDde.dll" (
set ERR=Unable to copy file %DLLPATH%NDde.dll : You are running the batch file without extracting or File may be missing or You do not have rights to copy
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,16, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)

if NOT EXIST "%DLLPATH%WebSocket4Net.dll" (
set ERR=Unable to copy file %DLLPATH%WebSocket4Net.dll : You are running the batch file without extracting or File may be missing or You do not have rights to copy
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,16, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)

if NOT EXIST "%DLLPATH%License\UPSTOXNET.txt" (
set ERR=Unable to copy file %DLLPATH%License\UPSTOXNET.txt : You are running the batch file without extracting or File may be missing or You do not have rights to copy
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,16, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)

if NOT EXIST "%DLLPATH%License\NDDE.txt" (
set ERR=Unable to copy file %DLLPATH%License\NDDE.txt : You are running the batch file without extracting or File may be missing or You do not have rights to copy
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,16, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)

if NOT EXIST "%DLLPATH%License\WEBSOCKET4NET.txt" (
set ERR=Unable to copy file %DLLPATH%License\WEBSOCKET4NET.txt : You are running the batch file without extracting or File may be missing or You do not have rights to copy
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,16, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)

if NOT EXIST "%DLLPATH%License\NEWTONSOFT.JSON.txt" (
set ERR=Unable to copy file %DLLPATH%License\NEWTONSOFT.JSON.txt : You are running the batch file without extracting or File may be missing or You do not have rights to copy
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,16, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)

if %OSBIT% == 32 (
CD /d %WINDIR%\Microsoft.NET\Framework\v4.0*
Regasm "C:\Windows\System32\%APPNAME%.dll" /tlb /codebase /u >> "%LOGFILE%" 2>&1

del /q /f /a C:\Windows\System32\UpstoxNet.dll >> "%LOGFILE%" 2>&1
del /q /f /a C:\Windows\System32\UpstoxNet.tlb >> "%LOGFILE%" 2>&1
del /q /f /a C:\Windows\System32\Newtonsoft.Json.dll >> "%LOGFILE%" 2>&1
del /q /f /a C:\Windows\System32\NDde.dll >> "%LOGFILE%" 2>&1
del /q /f /a C:\Windows\System32\WebSocket4Net.dll >> "%LOGFILE%" 2>&1

del /q /f /a "C:\Program Files\AmiBroker\UpstoxNet.dll"  >> "%LOGFILE%" 2>&1
del /q /f /a "C:\Program Files\AmiBroker\Newtonsoft.Json.dll"  >> "%LOGFILE%" 2>&1
del /q /f /a "C:\Program Files\AmiBroker\NDde.dll" >>  "%LOGFILE%" 2>&1
del /q /f /a "C:\Program Files\AmiBroker\WebSocket4Net.dll"  >> "%LOGFILE%" 2>&1

xcopy /h /y /r "%DLLPATH%%APPNAME%.dll" "C:\Windows\System32" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%Newtonsoft.Json.dll" "C:\Windows\System32" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%NDde.dll" "C:\Windows\System32" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%WebSocket4Net.dll" "C:\Windows\System32" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%License\UPSTOXNET.txt" "C:\Windows\System32" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%License\NDDE.txt" "C:\Windows\System32" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%License\WEBSOCKET4NET.txt" "C:\Windows\System32" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%License\NEWTONSOFT.JSON.txt" "C:\Windows\System32" >> "%LOGFILE%" 2>&1

CD /d %WINDIR%\Microsoft.NET\Framework\v4.0*
Regasm "C:\Windows\System32\%APPNAME%.dll" /tlb /codebase >> "%LOGFILE%" 2>&1

if NOT !errorlevel! equ 0 (
set ERR=Unable to Register DLL. Please try again. If problem persits contact Administrator.
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,16, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)

) else (

CD /d %WINDIR%\Microsoft.NET\Framework\v4.0*
Regasm "C:\Windows\SysWOW64\%APPNAME%.dll" /tlb /codebase /u >> "%LOGFILE%" 2>&1

del /q /f /a C:\Windows\SysWOW64\UpstoxNet.dll >> "%LOGFILE%" 2>&1
del /q /f /a C:\Windows\SysWOW64\UpstoxNet.tlb >> "%LOGFILE%" 2>&1
del /q /f /a C:\Windows\SysWOW64\Newtonsoft.Json.dll >> "%LOGFILE%" 2>&1
del /q /f /a C:\Windows\SysWOW64\NDde.dll >> "%LOGFILE%" 2>&1
del /q /f /a C:\Windows\SysWOW64\WebSocket4Net.dll >> "%LOGFILE%" 2>&1

del /q /f /a "C:\Program Files (x86)\AmiBroker\UpstoxNet.dll"  >> "%LOGFILE%" 2>&1
del /q /f /a "C:\Program Files (x86)\AmiBroker\Newtonsoft.Json.dll"  >> "%LOGFILE%" 2>&1
del /q /f /a "C:\Program Files (x86)\AmiBroker\NDde.dll"  >> "%LOGFILE%" 2>&1
del /q /f /a "C:\Program Files (x86)\AmiBroker\WebSocket4Net.dll"  >> "%LOGFILE%" 2>&1

xcopy /h /y /r "%DLLPATH%%APPNAME%.dll" "C:\Windows\SysWOW64" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%Newtonsoft.Json.dll" "C:\Windows\SysWOW64" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%NDde.dll" "C:\Windows\SysWOW64" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%WebSocket4Net.dll" "C:\Windows\SysWOW64" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%License\UPSTOXNET.txt" "C:\Windows\SysWOW64" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%License\NDDE.txt" "C:\Windows\SysWOW64" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%License\WEBSOCKET4NET.txt" "C:\Windows\SysWOW64" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%License\NEWTONSOFT.JSON.txt" "C:\Windows\SysWOW64" >> "%LOGFILE%" 2>&1

CD /d %WINDIR%\Microsoft.NET\Framework\v4.0*
Regasm "C:\Windows\SysWOW64\%APPNAME%.dll" /tlb /codebase >> "%LOGFILE%" 2>&1

if NOT !errorlevel! equ 0 (
set ERR=Unable to Register DLL. Please try again. If problem persits contact Administrator.
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,16, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)

CD /d %WINDIR%\Microsoft.NET\Framework64\v4.0*
Regasm "C:\Windows\System32\%APPNAME%.dll" /tlb /codebase /u >> "%LOGFILE%" 2>&1

del /q /f /a C:\Windows\System32\UpstoxNet.dll >> "%LOGFILE%" 2>&1
del /q /f /a C:\Windows\System32\UpstoxNet.tlb >> "%LOGFILE%" 2>&1
del /q /f /a C:\Windows\System32\Newtonsoft.Json.dll >> "%LOGFILE%" 2>&1
del /q /f /a C:\Windows\System32\NDde.dll >> "%LOGFILE%" 2>&1
del /q /f /a C:\Windows\System32\WebSocket4Net.dll >> "%LOGFILE%" 2>&1

del /q /f /a "C:\Program Files\AmiBroker\UpstoxNet.dll"  >> "%LOGFILE%" 2>&1
del /q /f /a "C:\Program Files\AmiBroker\Newtonsoft.Json.dll"  >> "%LOGFILE%" 2>&1
del /q /f /a "C:\Program Files\AmiBroker\NDde.dll"  >> "%LOGFILE%" 2>&1
del /q /f /a "C:\Program Files\AmiBroker\WebSocket4Net.dll"  >> "%LOGFILE%" 2>&1

xcopy /h /y /r "%DLLPATH%%APPNAME%.dll" "C:\Windows\System32" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%Newtonsoft.Json.dll" "C:\Windows\System32" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%NDde.dll" "C:\Windows\System32" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%WebSocket4Net.dll" "C:\Windows\System32" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%License\UPSTOXNET.txt" "C:\Windows\System32" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%License\NDDE.txt" "C:\Windows\System32" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%License\WEBSOCKET4NET.txt" "C:\Windows\System32" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%License\NEWTONSOFT.JSON.txt" "C:\Windows\System32" >> "%LOGFILE%" 2>&1

CD /d %WINDIR%\Microsoft.NET\Framework64\v4.0*
Regasm "C:\Windows\System32\%APPNAME%.dll" /tlb /codebase >> "%LOGFILE%" 2>&1

if NOT !errorlevel! equ 0 (
set ERR=Unable to Register DLL. Please try again. If problem persits contact Administrator.
echo !ERR! && echo !ERR! >>"%LOGFILE%"
echo x=msgbox^("!ERR!" ,16, "Error"^) > %TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs
exit
)

)

set MSG=Successfully registered DLL files
echo %MSG% && echo %MSG% >>"%LOGFILE%"

::::::::::EXCEL
echo. && echo.
set MSG=Registry settings for Excel
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=* Enabling Macros in Excel via Registry
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Office\%EXCELBUILD%\Excel\Security" /v "VBAWarnings" /t REG_DWORD /d 0x00000001 /f >nul 2>&1

set MSG=* Enabling External Contents in Excel via Registry
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Office\%EXCELBUILD%\Excel\Security" /v "DataConnectionWarnings" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add "HKCU\Software\Microsoft\Office\%EXCELBUILD%\Excel\Security" /v "WorkbookLinkWarnings" /t REG_DWORD /d 0x00000000 /f >nul 2>&1

set MSG=* Enabling ActiveX in Excel via Registry
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Office\Common\Security" /v "UFIControls" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add "HKCU\Software\Microsoft\Office\Common\Security" /v "DisableAllActiveX" /t REG_DWORD /d 0x00000000 /f >nul 2>&1

set MSG=* Changing Throttle Interval in Excel via Registry
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Office\%EXCELBUILD%\Excel\Options" /v "RTDThrottleInterval" /t REG_DWORD /d 0x00000000 /f >nul 2>&1

set MSG=* Disabling AutoRecover
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Office\%EXCELBUILD%\Excel\Options" /v "AutoRecoverEnabled" /t REG_DWORD /d 0x00000000 /f >nul 2>&1

set MSG=* Changing AutoRecover time
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Office\%EXCELBUILD%\Excel\Options" /v "AutoRecoverTime" /t REG_DWORD /d 0x00000120 /f >nul 2>&1

set MSG=* Changing AutoRecover Delay
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Office\%EXCELBUILD%\Excel\Options" /v "AutoRecoverDelay" /t REG_DWORD /d 0x00000600 /f >nul 2>&1

set MSG=* Enabling Browser Emulation via Registry
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKLM\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION" /v "EXCEL.EXE" /t REG_DWORD /d 0x00007000 /f >nul 2>&1
reg add "HKCU\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION" /v "EXCEL.EXE" /t REG_DWORD /d 0x00007000 /f >nul 2>&1
reg add "HKLM\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION" /v "HelloUpstox.exe" /t REG_DWORD /d 0x00007000 /f >nul 2>&1
reg add "HKCU\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION" /v "HelloUpstox.exe" /t REG_DWORD /d 0x00007000 /f >nul 2>&1

set MSG=* Excel Web Query BypassNoCacheCheck Enable
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings" /v "BypassSSLNoCacheCheck" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings" /v "BypassHTTPNoCacheCheck" /t REG_DWORD /d 0x00000001 /f >nul 2>&1


:::::::::: IE
echo. && echo.
set MSG=Registry settings for Internet Explorer
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set MSG=* Launching unsafe Files
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3" /v "1806" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\2" /v "1806" /t REG_DWORD /d 0x00000000 /f >nul 2>&1

set MSG=* Allow Scriptlets
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3" /v "1209" /t REG_DWORD /d 0x00000000 /f >nul 2>&1

set MSG=* Allow scripting of Internet Explorer Web browser control
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3" /v "1206" /t REG_DWORD /d 0x00000000 /f >nul 2>&1

set MSG=* Display mixed content
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3" /v "1609" /t REG_DWORD /d 0x00000000 /f >nul 2>&1

set MSG=* Scripting of Java applets
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3" /v "1402" /t REG_DWORD /d 0x00000000 /f >nul 2>&1

set MSG=* Active scripting
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3" /v "1400" /t REG_DWORD /d 0x00000000 /f >nul 2>&1

set MSG=* File Download
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3" /v "1803" /t REG_DWORD /d 0x00000000 /f >nul 2>&1


::::::::::TRUDTED SITES
echo. && echo.
set MSG=Adding Websites to Trusted Sites of IE
echo %MSG% && echo %MSG% >>"%LOGFILE%"

set DOMAIN=upstox.com
set MSG=* Adding %DOMAIN% to Trusted Sites
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\EscDomains\%DOMAIN%" /f >nul 2>&1
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\EscDomains\%DOMAIN%" /v "https" /t REG_DWORD /d 0x00000002 /f >nul 2>&1
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\EscDomains\%DOMAIN%" /v "http" /t REG_DWORD /d 0x00000002 /f >nul 2>&1

set DOMAIN=howutrade.in
set MSG=* Adding %DOMAIN% to Trusted Sites
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\EscDomains\%DOMAIN%" /f >nul 2>&1
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\EscDomains\%DOMAIN%" /v "https" /t REG_DWORD /d 0x00000002 /f >nul 2>&1
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\EscDomains\%DOMAIN%" /v "http" /t REG_DWORD /d 0x00000002 /f >nul 2>&1

set DOMAIN=google.com
set MSG=* Adding %DOMAIN% to Trusted Sites
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\EscDomains\%DOMAIN%" /f >nul 2>&1
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\EscDomains\%DOMAIN%" /v "https" /t REG_DWORD /d 0x00000002 /f >nul 2>&1
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\EscDomains\%DOMAIN%" /v "http" /t REG_DWORD /d 0x00000002 /f >nul 2>&1

set DOMAIN=google.co.in
set MSG=* Adding %DOMAIN% to Trusted Sites
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\EscDomains\%DOMAIN%" /f >nul 2>&1
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\EscDomains\%DOMAIN%" /v "https" /t REG_DWORD /d 0x00000002 /f >nul 2>&1
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\EscDomains\%DOMAIN%" /v "http" /t REG_DWORD /d 0x00000002 /f >nul 2>&1

set MSG=* Un-Checking Require Server Authentication
echo %MSG% && echo %MSG% >>"%LOGFILE%"
reg add "HKCU\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\2" /v "Flags" /t REG_DWORD /d 0x00000043 /f >nul 2>&1


::::::::::UAC
echo. && echo.
set MSG=Enable or Disable UAC
echo %MSG% && echo %MSG% >>"%LOGFILE%"
echo Disable UAC now [Type Y/N and Press Enter]? >>"%LOGFILE%"

set /P INPUT=Disable UAC now [Type Y/N and Press Enter]?
echo %INPUT% && echo %INPUT% >>"%LOGFILE%"

if /I "%INPUT%" equ "Y" (
set ERR=Disabling UAC
echo !ERR! && echo !ERR! >>"%LOGFILE%"
reg add "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\policies\system" /v "EnableLUA" /t REG_DWORD /d 0x00000000 /f >nul 2>&1
) else (
set ERR=Enabling UAC
echo !ERR! && echo !ERR! >>"%LOGFILE%"
reg add "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\policies\system" /v "EnableLUA" /t REG_DWORD /d 0x00000001 /f >nul 2>&1
)

::::::::Hello World
set hour=%time:~0,2%
if "%hour:~0,1%" == " " set hour=0%hour:~1,1%
set min=%time:~3,2%
if "%min:~0,1%" == " " set min=0%min:~1,1%
set secs=%time:~6,2%
if "%secs:~0,1%" == " " set secs=0%secs:~1,1%
set year=%date:~-4%
set month=%date:~4,2%
if "%month:~0,1%" == " " set month=0%month:~1,1%
set day=%date:~0,2%
if "%day:~0,1%" == " " set day=0%day:~1,1%

SET DATEFORMATTED=%year%%month%%day%_%hour%%min%%secs%
SET APPDIR=C:\Program Files\Hello Upstox\
SET LICDIR=C:\Program Files\Hello Upstox\License\
SET XLDIR=C:\Program Files\Hello Upstox\Excel\
SET SHORTCUTPATH=%USERPROFILE%\Desktop\Hello Upstox.lnk
SET HOTKEY=ALT+U

if not exist "%APPDIR%" mkdir "%APPDIR%" >> %LOGFILE% 2>&1 
if not exist "%LICDIR%" mkdir "%LICDIR%" >> %LOGFILE% 2>&1 
if not exist "%XLDIR%" mkdir "%XLDIR%" >> %LOGFILE% 2>&1 

xcopy /h /y /r "%DLLPATH%%APPNAME%.dll" "%APPDIR%" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%Newtonsoft.Json.dll" "%APPDIR%" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%NDde.dll" "%APPDIR%" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%WebSocket4Net.dll" "%APPDIR%" >> "%LOGFILE%" 2>&1

xcopy /h /y /r "%DLLPATH%AppIcon.ico" "%APPDIR%" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%Samples\HelloUpstox\HelloUpstox\bin\Debug\HelloUpstox.exe" "%APPDIR%" >> "%LOGFILE%" 2>&1

if exist "%XLDIR%\UpstoxXL.xlsm" REN "%XLDIR%\UpstoxXL.xlsm" "UpstoxXL_%DATEFORMATTED%.xlsm" >> %LOGFILE% 2>&1 
xcopy /h /y /r "%DLLPATH%Samples\BatchHist.txt" "%APPDIR%" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%Samples\BatchOrder.txt" "%APPDIR%" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%Samples\TestAFL_UpstoxNet.afl" "%APPDIR%" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%Samples\MA_Crossover_Sample_UpstoxNet.afl" "%APPDIR%" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%Samples\SuperTrend_Minified_UpstoxNet.afl" "%APPDIR%" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%Samples\UpstoxXL.xlsm" "%XLDIR%" >> "%LOGFILE%" 2>&1

xcopy /h /y /r "%DLLPATH%License\UPSTOXNET.txt" "%LICDIR%" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%License\NDDE.txt" "%LICDIR%" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%License\WEBSOCKET4NET.txt" "%LICDIR%" >> "%LOGFILE%" 2>&1
xcopy /h /y /r "%DLLPATH%License\NEWTONSOFT.JSON.txt" "%LICDIR%" >> "%LOGFILE%" 2>&1

set SCRIPT="%TEMP%\%RANDOM%-%RANDOM%-%RANDOM%-%RANDOM%.vbs"

echo Set oWS = WScript.CreateObject("WScript.Shell") >> %SCRIPT%
echo sLinkFile = "%SHORTCUTPATH%" >> %SCRIPT%
echo Set oLink = oWS.CreateShortcut(sLinkFile) >> %SCRIPT%
echo oLink.TargetPath = "%APPDIR%\HelloUpstox.exe" >> %SCRIPT%
echo oLink.WorkingDirectory = "%APPDIR%" >> %SCRIPT%
echo oLink.HotKey = "%HOTKEY%" >> %SCRIPT%
echo oLink.IconLocation = "%APPDIR%\AppIcon.ico" >> %SCRIPT%
echo oLink.Save >> %SCRIPT%

cscript /nologo %SCRIPT% >> %LOGFILE% 2>&1  
del %SCRIPT% >> %LOGFILE% 2>&1  


::::::::END
echo. && echo.
set MSG=Successfully registered UpstoxNet.dll. Please go through the License Terms carefully.
echo %MSG% && echo %MSG% >>"%LOGFILE%"
echo x=msgbox^("Successfully registered UpstoxNet.dll." ^& vbCrLf ^& "Please go through the License Terms carefully.",64, "Success"^) >%TEMP%\msgbox.vbs && start /w %TEMP%\msgbox.vbs

start notepad "%DLLPATH%License\UPSTOXNET.txt"
