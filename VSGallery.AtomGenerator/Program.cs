using System;
using System.IO;
using System.Linq;

using VSGallery.AtomGenerator.Infrastructure;
using VSGallery.AtomGenerator.Infrastructure.Linq;
using VSGallery.AtomGenerator.Infrastructure.Threading;
using VSGallery.AtomGenerator.Vsix;

namespace VSGallery.AtomGenerator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var rootDirectory = args.FirstOrDefault(Directory.Exists) ?? Environment.CurrentDirectory;
            var log = new Logger(Path.Combine(rootDirectory, "logs", $"log_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt"));

            try
            {
                _GenerateAtomFeed(rootDirectory, log);
            }
            catch (Exception ex)
            {
                log.Error("Application level exception", ex);
            }
            finally
            {
                log.Dispose();
            }
        }

        private static void _GenerateAtomFeed(string rootDirectory, Logger log)
        {
            var packageFactory = new VsixPackageFactory();
            var packages = Directory
                .EnumerateFiles(rootDirectory, "*.vsix", SearchOption.AllDirectories)
                .Select(packageFactory.LoadFromFile)
                .WhenAll().Result
                .Do(result => result.IfError(log.Error))
                .TakeSuccess()
                .ToArray();

            var feedFile = Path.Combine(rootDirectory, "atom.xml");
            _WriteAtomFeed(feedFile, packages);
        }

        private static void _WriteAtomFeed(string file, VsixPackage[] packages)
        {
            throw new NotImplementedException();
        }
    }
}
