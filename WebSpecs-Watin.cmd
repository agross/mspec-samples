@echo off
rem Please compile the web application with Visual Studio first.
@mkdir _Report 2> NUL

packages\Machine.Specifications.0.4.23.0\tools\mspec-clr4.exe --html _Report\WebSpecs-Watin.html "WebSpecs\LoginApp.Watin.Specs\bin\Debug\LoginApp.Watin.Specs.dll"