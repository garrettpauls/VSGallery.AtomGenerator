using Ionic.Zip;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using VSGallery.AtomGenerator.Infrastructure;
using VSGallery.AtomGenerator.Vsix.Schemas;

namespace VSGallery.AtomGenerator.Vsix
{
    public sealed class VsixPackageFactory
    {
        private readonly XmlSerializer mPackageManifestSerializer = new XmlSerializer(typeof(PackageManifest));

        public ErrorSuccess<VsixPackage, string> LoadFromFile(string file)
        {
            var ef = new ErrorSuccessFactory<VsixPackage, string>();

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

                PackageManifest manifest;
                using (var reader = manifestEntry.OpenReader())
                using (var xmlReader = XmlReader.Create(reader, new XmlReaderSettings
                {
                    Schemas = SchemaSets.PackageManifest
                }))
                {
                    if (!mPackageManifestSerializer.CanDeserialize(xmlReader))
                    {
                        return ef.Error($"{file}\\{Entry.Manifest} is not a valid package manifest file.");
                    }

                    manifest = (PackageManifest)mPackageManifestSerializer.Deserialize(xmlReader);
                }

                return ef.Success(new VsixPackage(file, manifest));
            }
        }

        private static class Entry
        {
            public const string Manifest = "extension.vsixmanifest";
        }
    }
}
