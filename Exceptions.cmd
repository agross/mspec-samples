@echo off
rmdir /q /s _reports
mkdir _reports

packages\Machine.Specifications.Runner.Console\tools\mspec-clr4.exe --html .\_reports\Exceptions.html "source\Exceptions\bin\Debug\Exceptions.dll"
