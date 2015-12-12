using VSGallery.AtomGenerator.Vsix.Schemas;

namespace VSGallery.AtomGenerator.Vsix
{
    public sealed class VsixWithPackageManifest : IVsixPackage
    {
        private readonly PackageManifest mManifest;

        public VsixWithPackageManifest(string file, PackageManifest manifest)
        {
            File = file;
            mManifest = manifest;
        }

        public string Description => mManifest.Metadata.Description;
        public string DisplayName => mManifest.Metadata.DisplayName;
        public string File { get; }
        public string Id => mManifest.Metadata.Identity.Id;
        public string Publisher => mManifest.Metadata.Identity.Publisher;
        public string Version => mManifest.Metadata.Identity.Version;
    }
}
