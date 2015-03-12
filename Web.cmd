@mkdir _report 2> NUL
packages\Machine.Specifications.Runner.Console\tools\mspec-clr4.exe --html _report\Web.html "source\Web\Specs\bin\Debug\Specs.dll"
