@ECHO OFF

REM The following directory is for .NET v4.0.30319
set DOTNETFX2=C:\Windows\Microsoft.NET\Framework\v4.0.30319
set PATH=%PATH%;%DOTNETFX2%
echo *****************************************************************
echo Un-Installing WindowsService...
echo ---------------------------------------------------
InstallUtil /u  bin\Debug\MSSSQL.DIARY.SERVICE.exe
echo ---------------------------------------------------
echo *****************************************************************
timeout 30
pause
echo *****************************************************************
echo Installing WindowsService...
echo ---------------------------------------------------
InstallUtil /i  bin\Debug\MSSSQL.DIARY.SERVICE.exe
echo ---------------------------------------------------
pause
echo *****************************************************************
echo Done.