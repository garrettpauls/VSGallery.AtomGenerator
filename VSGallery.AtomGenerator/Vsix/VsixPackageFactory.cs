using Ionic.Zip;
using System.Xml;
using System.Xml.Serialization;
using VSGallery.AtomGenerator.Infrastructure;
using VSGallery.AtomGenerator.Vsix.Schemas;

namespace VSGallery.AtomGenerator.Vsix
{
    public sealed class VsixPackageFactory
    {
        private readonly XmlSerializer mPackageManifestSerializer = new XmlSerializer(typeof(PackageManifest));
        private readonly XmlSerializer mVsixManifestSerializer = new XmlSerializer(typeof(Schemas.Vsix));

        public ErrorSuccess<IVsixPackage, string> LoadFromFile(string file)
        {
            var ef = new ErrorSuccessFactory<IVsixPackage, string>();

            if (!ZipFile.IsZipFile(file))
            {
                return ef.Error($"{file} is not a zip archive.");
            }

            using (var zipFile = ZipFile.Read(file))
            {
                if (!zipFile.ContainsEntry(Entry.Manifest))
                {
                    return ef.Error($"{file} does not contain a {Entry.Manifest} entry.");
                }

                var manifestEntry = zipFile[Entry.Manifest];
                if (manifestEntry.IsDirectory)
                {
                    return ef.Error($"{file}\\{Entry.Manifest} is not a file.");
                }

                return _CreateFromZipManifest(file, manifestEntry, ef);
            }
        }

        private ErrorSuccess<IVsixPackage, string> _CreateFromZipManifest(string file, ZipEntry manifestEntry, ErrorSuccessFactory<IVsixPackage, string> ef)
        {
            var packageManifest = _TryGetPackageManifest(manifestEntry);
            var vsixManifest = _TryGetVsixManifest(manifestEntry);

            if (packageManifest != null)
            {
                return ef.Success(new VsixWithPackageManifest(file, packageManifest));
            }

            if (vsixManifest != null)
            {
                return ef.Success(new VsixWithVsixManifest(file, vsixManifest));
            }

            return ef.Error($"{file}\\{Entry.Manifest} is not a valid package manifest file.");
        }

        private PackageManifest _TryGetPackageManifest(ZipEntry manifestEntry)
        {
            using (var reader = manifestEntry.OpenReader())
            using (var xmlReader = XmlReader.Create(reader, new XmlReaderSettings
            {
                Schemas = SchemaSets.PackageManifest
            }))
            {
                if (!mPackageManifestSerializer.CanDeserialize(xmlReader))
                {
                    return null;
                }

                return (PackageManifest)mPackageManifestSerializer.Deserialize(xmlReader);
            }
        }

        private Schemas.Vsix _TryGetVsixManifest(ZipEntry manifestEntry)
        {
            using (var reader = manifestEntry.OpenReader())
            using (var xmlReader = XmlReader.Create(reader, new XmlReaderSettings
            {
                Schemas = SchemaSets.VsixManifest
            }))
            {
                if (!mVsixManifestSerializer.CanDeserialize(xmlReader))
                {
                    return null;
                }

                return (Schemas.Vsix)mVsixManifestSerializer.Deserialize(xmlReader);
            }
        }

        private static class Entry
        {
            public const string Manifest = "extension.vsixmanifest";
        }
    }
}
