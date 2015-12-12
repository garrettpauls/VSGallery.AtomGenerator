using VSGallery.AtomGenerator.Vsix.Schemas;

namespace VSGallery.AtomGenerator.Vsix
{
    public sealed class VsixWithPackageManifest : VsixPackage
    {
        private readonly PackageManifest mManifest;

        public VsixWithPackageManifest(string file, PackageManifest manifest) : base(file)
        {
            mManifest = manifest;
        }

        public override string Description => mManifest.Metadata.Description;
        public override string DisplayName => mManifest.Metadata.DisplayName;
        public override string Id => mManifest.Metadata.Identity.Id;
        public override string Publisher => mManifest.Metadata.Identity.Publisher;
        public override string Version => mManifest.Metadata.Identity.Version;
        protected override string IconName => mManifest.Metadata.Icon;
        protected override string PreviewImageName => mManifest.Metadata.PreviewImage;
    }
}
