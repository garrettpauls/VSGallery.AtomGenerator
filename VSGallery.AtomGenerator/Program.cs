using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using VSGallery.AtomGenerator.Infrastructure;
using VSGallery.AtomGenerator.Infrastructure.Linq;
using VSGallery.AtomGenerator.Infrastructure.Threading;
using VSGallery.AtomGenerator.Vsix;
using VSGallery.AtomGenerator.Vsix.Schemas;

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
                .TakeSuccess()
                .ToArray();

            var feedFile = Path.Combine(rootDirectory, "atom.xml");
            _WriteAtomFeed(feedFile, packages);
        }

        private static void _WriteAtomFeed(string file, VsixPackage[] packages)
        {
            // See https://msdn.microsoft.com/en-us/library/hh266717.aspx
            // http://www.cambiaresearch.com/articles/71/easily-build-an-atom-or-rss-feed-with-csharp-and-the-syndication-namespace

            var feed = new SyndicationFeed();
            feed.Id = "TODO: keep consistent id";
            feed.Title = new TextSyndicationContent("TODO: consistent title");
            feed.Description = new TextSyndicationContent("TODO: consistent description");
            feed.Copyright = new TextSyndicationContent("TODO: consistent copyright");
            feed.LastUpdatedTime = DateTimeOffset.Now;
            feed.Generator = "TODO: consistent generator";

            var items = new List<SyndicationItem>();
            feed.Items = items;

            foreach (var pkg in packages)
            {
                var item = new SyndicationItem(
                    pkg.Manifest.Metadata.DisplayName,
                    pkg.Manifest.Metadata.Description,
                    new Uri(pkg.File),
                    pkg.Manifest.Metadata.Identity.Id,
                    new DateTimeOffset(File.GetLastWriteTimeUtc(pkg.File)));

                var ns = XNamespace.Get("http://schemas.microsoft.com/developer/vsx-syndication-schema/2010");
                var xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
                var content = new XElement(
                    ns + "Vsix",
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XElement(ns + "Id", pkg.Manifest.Metadata.Identity.Id),
                    new XElement(ns + "Version", pkg.Manifest.Metadata.Identity.Version),
                    new XElement(ns + "References"),
                    new XElement(
                        ns + "Rating",
                        new XAttribute(xsi + "nil", "true")),
                    new XElement(
                        ns + "RatingCount",
                        new XAttribute(xsi + "nil", "true")),
                    new XElement(
                        ns + "DownloadCount",
                        new XAttribute(xsi + "nil", "true")));

                using (var stringReader = new StringReader(content.ToString()))
                using (var reader = XmlReader.Create(stringReader))
                {
                    item.ElementExtensions.Add(reader);
                }

                items.Add(item);
            }

            using (var stream = new FileStream(file, FileMode.Create))
            using (var writer = XmlWriter.Create(stream, new XmlWriterSettings
            {
                IndentChars = "\t",
                Indent = true
            }))
            {
                var formatter = new Atom10FeedFormatter(feed);
                formatter.WriteTo(writer);
            }
        }
    }
}
