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
        public static int Main(string[] args)
        {
            if (args.Any(arg => arg == "/?" || arg == "/help" || arg == "--help"))
            {
                _ShowUsage();
                return (int) ReturnCodes.Success;
            }

            var rootDirectory = args.FirstOrDefault(Directory.Exists) ?? Environment.CurrentDirectory;
            var log = Logger.Create(Path.Combine(rootDirectory, "logs", $"log_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}.txt"));

            try
            {
                _GenerateAtomFeed(rootDirectory, log);
            }
            catch (Exception ex)
            {
                log.Error("Unhandled exception", ex);
                return (int) ReturnCodes.Error;
            }

            return (int) ReturnCodes.Success;
        }

        private static void _GenerateAtomFeed(string rootDirectory, Logger log)
        {
            log.Info("Loading vsix files");
            var packageFactory = new VsixPackageFactory();
            var packages = Directory
                .EnumerateFiles(rootDirectory, "*.vsix", SearchOption.AllDirectories)
                .Select(file => packageFactory.LoadFromFile(file, log))
                .Do(result => result.IfError(log.Error))
                .TakeSuccess()
                .ToArray();

            packages = _OnlyMostRecentVersions(packages, log).ToArray();

            var feedFile = Path.Combine(rootDirectory, "atom.xml");

            var feedBuilder = new FeedBuilder();
            feedBuilder.WriteFeed(feedFile, packages, log);

            log.Info("Feed generated successfully");
        }

        private static IEnumerable<IVsixPackage> _OnlyMostRecentVersions(IEnumerable<IVsixPackage> packages, Logger log)
        {
            var groups = packages.GroupBy(pkg => pkg.Id);
            var versionComparer = new VersionComparer();

            foreach (var grp in groups)
            {
                if (grp.Count() > 1)
                {
                    var package = grp.OrderByDescending(pkg => pkg.Version, versionComparer).First();
                    log.Info($"Multiple versions of {grp.Key} detected, using version {package.Version}");
                }
                else
                {
                    yield return grp.First();
                }
            }
        }

        private static void _ShowUsage()
        {
            Console.WriteLine(@"Generates an atom.xml file containing a feed of all *.vsix files in the same and subdirectories.

If any arguments are supplied, the first argument corresponding to a valid directory will be used as the directory for the feed and fsix files.
If no arguments correspond to a valid directory the current working directory will be used.
");
        }
    }
}
