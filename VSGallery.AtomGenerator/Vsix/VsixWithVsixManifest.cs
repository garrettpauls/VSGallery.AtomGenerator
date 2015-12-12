namespace VSGallery.AtomGenerator.Vsix
{
    public sealed class VsixWithVsixManifest : IVsixPackage
    {
        private readonly string mFile;
        private readonly Schemas.Vsix mManifest;

        public VsixWithVsixManifest(string file, Schemas.Vsix manifest)
        {
            mFile = file;
            mManifest = manifest;
        }

        public string Description => mManifest.Identifier.Description;

        public string DisplayName => mManifest.Identifier.Name;

        public string File => mFile;

        public string Id => mManifest.Identifier.Id;

        public string Publisher => mManifest.Identifier.Author;

        public string Version => mManifest.Identifier.Version;
    }
}
