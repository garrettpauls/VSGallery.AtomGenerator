using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;

namespace VSGallery.AtomGenerator.Vsix
{
    public abstract class VsixPackage : IVsixPackage
    {
        public VsixPackage(string file)
        {
            File = file;
        }

        public abstract string Description { get; }

        public abstract string DisplayName { get; }

        public string File { get; }

        public abstract string Id { get; }

        public abstract string Publisher { get; }

        public abstract string Version { get; }

        protected abstract string IconName { get; }

        protected abstract string PreviewImageName { get; }

        public Uri TrySaveIcon(string destinationFolder)
        {
            return _SaveEntry(destinationFolder, IconName);
        }

        public Uri TrySavePreviewImage(string destinationFolder)
        {
            return _SaveEntry(destinationFolder, PreviewImageName);
        }

        private static string _NormalizeZipEntryName(string value)
        {
            value = value.Replace('\\', '/');
            value = Regex.Replace(value, "[/]{2,}", "/");
            return value;
        }

        private Uri _SaveEntry(string destinationFolder, string entryName)
        {
            if (string.IsNullOrEmpty(entryName))
            {
                return null;
            }

            entryName = _NormalizeZipEntryName(entryName);

            using (var zip = ZipFile.OpenRead(File))
            {
                var entry = zip.GetEntry(entryName);
                if (entry == null)
                {
                    return null;
                }

                var iconPath = Path.Combine(destinationFolder, Id, entryName);
                Directory.CreateDirectory(Path.GetDirectoryName(iconPath));
                entry.ExtractToFile(new Uri(iconPath).LocalPath, true);

                return new Uri(iconPath);
            }
        }
    }
}
