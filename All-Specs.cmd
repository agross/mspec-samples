@echo off
rmdir /q /s _reports
mkdir _reports

packages\Machine.Specifications.Runner.Console\tools\mspec-clr4.exe --html .\_reports "source\Banking\bin\Debug\Banking.dll" "source\Exceptions\bin\Debug\Exceptions.dll" "source\Behaviors\bin\Debug\Behaviors.dll" "source\Web\Specs\bin\Debug\Specs.dll"
