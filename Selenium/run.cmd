@echo off
rem Please compile and start the web application with Visual Studio first.
rmdir /s /q _Report > NUL
mkdir _Report > NUL
..\lib\MSpec\mspec-clr4.exe --html _Report\run.html "LoginApp.Specs\bin\Debug\LoginApp.Specs.dll"