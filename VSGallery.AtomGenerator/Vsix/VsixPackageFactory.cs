using System;
using System.IO.Compression;
using System.Linq;
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

        public ErrorSuccess<IVsixPackage, string> LoadFromFile(string file, Logger log)
        {
            var ef = new ErrorSuccessFactory<IVsixPackage, string>();

            try
            {
                return _LoadFromFile(file, log, ef);
            }
            catch (Exception ex)
            {
                log.Error($"An unexpected error occurred while trying to read {file} as a vsix.", ex);
                return ef.Error($"An unexpected error occurred while trying to read {file} as a vsix: {ex.Message}");
            }
        }

        private ErrorSuccess<IVsixPackage, string> _CreateFromZipManifest(string file, ZipArchiveEntry manifestEntry, ErrorSuccessFactory<IVsixPackage, string> ef)
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

        private ErrorSuccess<IVsixPackage, string> _LoadFromFile(string file, Logger log, ErrorSuccessFactory<IVsixPackage, string> ef)
        {
            ZipArchive zipFile;
            try
            {
                zipFile = ZipFile.OpenRead(file);
            }
            catch (Exception ex)
            {
                log.Error($"{file} is not a zip archive.", ex);
                return ef.Error($"{file} is not a zip archive: {ex.Message}");
            }

            using (zipFile)
            {
                var manifestEntry = zipFile.Entries.FirstOrDefault(entry => entry.FullName == Entry.Manifest);
                if (manifestEntry == null)
                {
                    return ef.Error($"{file} does not contain a {Entry.Manifest} entry.");
                }

                return _CreateFromZipManifest(file, manifestEntry, ef);
            }
        }

        private PackageManifest _TryGetPackageManifest(ZipArchiveEntry manifestEntry)
        {
            using (var reader = manifestEntry.Open())
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

        private Schemas.Vsix _TryGetVsixManifest(ZipArchiveEntry manifestEntry)
        {
            using (var reader = manifestEntry.Open())
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
