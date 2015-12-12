namespace VSGallery.AtomGenerator.Vsix
{
    public sealed class VsixWithVsixManifest : VsixPackage
    {
        private readonly Schemas.Vsix mManifest;

        public VsixWithVsixManifest(string file, Schemas.Vsix manifest) : base(file)
        {
            mManifest = manifest;
        }

        public override string Description => mManifest.Identifier.Description;
        public override string DisplayName => mManifest.Identifier.Name;
        public override string Id => mManifest.Identifier.Id;
        public override string Publisher => mManifest.Identifier.Author;
        public override string Version => mManifest.Identifier.Version;
        protected override string IconName => mManifest.Identifier.Icon;
        protected override string PreviewImageName => mManifest.Identifier.PreviewImage;
    }
}
