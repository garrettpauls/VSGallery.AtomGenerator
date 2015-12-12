using System;

namespace VSGallery.AtomGenerator.Vsix
{
    public interface IVsixPackage
    {
        string Description { get; }
        string DisplayName { get; }
        string File { get; }
        string Id { get; }
        string Publisher { get; }
        string Version { get; }
    }
}
