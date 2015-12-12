"C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe" VSGallery.AtomGenerator.sln /p:Configuration=Release
mkdir "%~dp0\Release"
xcopy /Y "%~dp0\VSGallery.AtomGenerator\bin\Release\VSGallery.AtomGenerator.exe" "%~dp0\Release"
pause