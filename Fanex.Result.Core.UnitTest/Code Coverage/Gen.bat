..\..\NexcelDependencies\OpenCover\OpenCover.Console.exe -register:user -target:"C:\Program Files (x86)\NUnit 2.6.3\bin\nunit-console.exe" -targetargs:"/noshadow ..\bin\Debug\Fanex.Result.Core.UnitTest.dll" -filter:+[*]* -output:"Data\coverage.xml"

..\..\NexcelDependencies\ReportGenerator\bin\ReportGenerator.exe -reports:"Data\coverage.xml" -targetdir:"Report" 