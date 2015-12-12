using Ionic.Zip;
using System;
using System.IO;

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

        private Uri _SaveEntry(string destinationFolder, string entryName)
        {
            if (string.IsNullOrEmpty(entryName))
            {
                return null;
            }

            using (var zip = new ZipFile(File))
            {
                if (!zip.ContainsEntry(entryName))
                {
                    return null;
                }

                var icon = zip[entryName];
                var iconPath = Path.Combine(destinationFolder, Id, entryName);
                Directory.CreateDirectory(Path.GetDirectoryName(iconPath));
                using (var stream = new FileStream(iconPath, FileMode.Create))
                {
                    icon.Extract(stream);
                }

                return new Uri(iconPath);
            }
        }
    }
}
