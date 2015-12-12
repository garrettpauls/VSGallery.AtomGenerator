using Ionic.Zip;
using System.Threading.Tasks;
using VSGallery.AtomGenerator.Infrastructure;

namespace VSGallery.AtomGenerator.Vsix
{
    public sealed class VsixPackageFactory
    {
        public async Task<ErrorSuccess<VsixPackage, string>> LoadFromFile(string file)
        {
            var ef = new ErrorSuccessFactory<VsixPackage, string>();

            if (!ZipFile.IsZipFile(file))
            {
                return ef.Error($"{file} is not a zip archive.");
            }

            await Task.Delay(1);

            return ef.Success(new VsixPackage());
        }
    }
}
