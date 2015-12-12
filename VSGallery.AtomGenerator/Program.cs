using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using VSGallery.AtomGenerator.Infrastructure;
using VSGallery.AtomGenerator.Infrastructure.Linq;
using VSGallery.AtomGenerator.Vsix;

namespace VSGallery.AtomGenerator
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var rootDirectory = args.FirstOrDefault(Directory.Exists) ?? Environment.CurrentDirectory;
            var log = Logger.Create(Path.Combine(rootDirectory, "logs", $"log_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt"));

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
                .Do(result => result.IfError(log.Error))
                .TakeSuccess();

            packages = _OnlyMostRecentVersions(packages);

            var feedFile = Path.Combine(rootDirectory, "atom.xml");

            var feedBuilder = new FeedBuilder();
            feedBuilder.WriteFeed(feedFile, packages);
        }

        private static IEnumerable<IVsixPackage> _OnlyMostRecentVersions(IEnumerable<IVsixPackage> packages)
        {
            return packages
                .GroupBy(pkg => pkg.Id)
                .Select(grp => grp.OrderByDescending(pkg => pkg.Version, new VersionComparer())
                                  .First());
        }
    }
}
