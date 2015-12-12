set xsd="C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6.1 Tools\xsd.exe"

%xsd% "%~dp0\PackageManifest\PackageManifestSchema.xsd" /classes /language:CS /namespace:VSGallery.AtomGenerator.Vsix.Schemas /out:%~dp0

%xsd% "%~dp0\Atom\Atom.xsd" /classes /language:CS /namespace:VSGallery.AtomGenerator.Vsix.Schemas /out:%~dp0
