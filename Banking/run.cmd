rmdir /s /q _Report
mkdir _Report
..\lib\MSpec\mspec-clr4.exe --html _Report\run.html "Banking\bin\Debug\Banking.dll"