using VSGallery.AtomGenerator.Vsix.Schemas;

namespace VSGallery.AtomGenerator.Vsix
{
    public sealed class VsixPackage
    {
        public VsixPackage(string file, PackageManifest manifest)
        {
            File = file;
            Manifest = manifest;
        }

        public string File { get; }

        public PackageManifest Manifest { get; }
    }
}
