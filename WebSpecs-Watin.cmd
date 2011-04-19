@echo off
rem Please compile and start the web application with Visual Studio first.
@mkdir _Report 2> NUL
packages\Machine.Specifications.0.4.10.0\tools\mspec-clr4.exe --html _Report\WebSpecs-Watin.html "WebSpecs\LoginApp.Watin.Specs\bin\Debug\LoginApp.Watin.Specs.dll"