using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using VSGallery.AtomGenerator.Vsix;

namespace VSGallery.AtomGenerator
{
    public sealed class FeedBuilder
    {
        public void WriteFeed(string feedFile, IEnumerable<IVsixPackage> packages)
        {
            var rootDirectory = new Uri(Path.GetDirectoryName(feedFile) + "\\");
            var feed = _ConfigureFromExistingFeed(feedFile);
            feed.LastUpdatedTime = DateTimeOffset.Now;
            _AddPackages(rootDirectory, packages, feed);
            _WriteAtomFeed(feedFile, feed);
        }

        private static void _AddPackages(Uri root, IEnumerable<IVsixPackage> packages, SyndicationFeed feed)
        {
            // See https://msdn.microsoft.com/en-us/library/hh266717.aspx

            var items = new List<SyndicationItem>();
            feed.Items = items;

            foreach (var pkg in packages)
            {
                var item = new SyndicationItem(
                    pkg.DisplayName,
                    pkg.Description,
                    new Uri(pkg.File),
                    pkg.Id,
                    new DateTimeOffset(File.GetLastWriteTimeUtc(pkg.File)));
                item = new SyndicationItem();
                item.Id = pkg.Id;
                item.Title = new TextSyndicationContent(pkg.DisplayName);
                item.Summary = new TextSyndicationContent(pkg.Description);
                item.PublishDate = new DateTimeOffset(File.GetLastWriteTimeUtc(pkg.File));
                item.LastUpdatedTime = new DateTimeOffset(File.GetLastWriteTimeUtc(pkg.File));
                item.Authors.Add(new SyndicationPerson { Name = pkg.Publisher });
                item.Content = SyndicationContent.CreateUrlContent(root.MakeRelativeUri(new Uri(pkg.File)), "application/octet-stream");

                // TODO: add preview links

                var ns = XNamespace.Get("http://schemas.microsoft.com/developer/vsx-syndication-schema/2010");
                var xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
                var content = new XElement(
                    ns + "Vsix",
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XElement(ns + "Id", pkg.Id),
                    new XElement(ns + "Version", pkg.Version),
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
        }

        private static SyndicationFeed _ConfigureFromExistingFeed(string file)
        {
            try
            {
                using (var stream = File.OpenRead(file))
                using (var reader = XmlReader.Create(stream))
                {
                    return SyndicationFeed.Load(reader);
                }
            }
            catch
            {
                var feed = new SyndicationFeed();
                feed.Id = Guid.NewGuid().ToString("D").ToUpper();
                feed.Title = new TextSyndicationContent("TODO: add a title");
                feed.Generator = "VSGallery.AtomGenerator";
                return feed;
            }
        }

        private static void _WriteAtomFeed(string file, SyndicationFeed feed)
        {
            var sb = new StringBuilder();
            using (var stringStream = new StringWriter(sb))
            using (var writer = XmlWriter.Create(stringStream))
            {
                var formatter = new Atom10FeedFormatter(feed);
                formatter.WriteTo(writer);
            }

            File.WriteAllText(file, XElement.Parse(sb.ToString()).ToString());
        }
    }
}
