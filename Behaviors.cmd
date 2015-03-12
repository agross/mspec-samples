@echo off
rmdir /q /s _reports
mkdir _reports

packages\Machine.Specifications.Runner.Console\tools\mspec-clr4.exe --html .\_reports\Behaviors.html "source\Behaviors\bin\Debug\Behaviors.dll"
