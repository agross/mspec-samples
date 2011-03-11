@echo off
rem Please compile and start the web application with Visual Studio first.
@mkdir _Report 2> NUL
packages\Machine.Specifications.0.4.7.0\tools\mspec-clr4.exe --html _Report\WebSpecs.html "WebSpecs\LoginApp.Specs\bin\Debug\LoginApp.Specs.dll"