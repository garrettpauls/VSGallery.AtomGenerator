using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using VSGallery.AtomGenerator.Vsix;

namespace VSGallery.AtomGenerator
{
    public sealed class FeedBuilder
    {
        public void WriteFeed(string feedFile, IEnumerable<IVsixPackage> packages, Logger log)
        {
            var rootDirectory = new Uri(Path.GetDirectoryName(feedFile) + "\\");

            var imageRoot = Path.Combine(rootDirectory.LocalPath, "VSIXImages");
            Directory.CreateDirectory(imageRoot);

            var feed = _ConfigureFromExistingFeed(feedFile, log);
            feed.LastUpdatedTime = DateTimeOffset.Now;

            _AddPackages(rootDirectory, imageRoot, packages, feed, log);
            _WriteAtomFeed(feedFile, feed, log);
        }

        private static void _AddPackages(Uri root, string imageRoot, IEnumerable<IVsixPackage> packages, SyndicationFeed feed, Logger log)
        {
            // See https://msdn.microsoft.com/en-us/library/hh266717.aspx

            var items = new List<SyndicationItem>();
            feed.Items = items;

            var orderedPackages = packages
                .OrderBy(pkg => pkg.DisplayName)
                .ThenBy(pkg => pkg.Id);

            foreach (var pkg in orderedPackages)
            {
                log.Info($"Adding package {pkg.DisplayName} ({pkg.Id}) to feed");

                var item = new SyndicationItem();
                item.Id = pkg.Id;
                item.Title = new TextSyndicationContent(pkg.DisplayName);
                item.Summary = new TextSyndicationContent(pkg.Description);
                item.PublishDate = new DateTimeOffset(File.GetLastWriteTimeUtc(pkg.File));
                item.LastUpdatedTime = new DateTimeOffset(File.GetLastWriteTimeUtc(pkg.File));
                item.Authors.Add(new SyndicationPerson { Name = pkg.Publisher });
                item.Content = SyndicationContent.CreateUrlContent(root.MakeRelativeUri(new Uri(pkg.File)), "application/octet-stream");

                _AddPreviewImages(root, imageRoot, item, pkg, log);

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

        private static void _AddPreviewImages(Uri root, string imageRoot, SyndicationItem item, IVsixPackage pkg, Logger log)
        {
            var icon = pkg.TrySaveIcon(imageRoot);
            var preview = pkg.TrySavePreviewImage(imageRoot);

            if (icon != null)
            {
                log.Info($"Extracted icon to {icon}");
                item.Links.Add(new SyndicationLink(root.MakeRelativeUri(icon), "icon", "", "", 0));
            }
            else
            {
                log.Info("No icon found");
            }

            if (preview != null)
            {
                log.Info($"Extracted preview image to {preview}");
                item.Links.Add(new SyndicationLink(root.MakeRelativeUri(preview), "previewimage", "", "", 0));
            }
            else
            {
                log.Info("No preview image found");
            }
        }

        private static SyndicationFeed _ConfigureFromExistingFeed(string file, Logger log)
        {
            try
            {
                using (var stream = File.OpenRead(file))
                using (var reader = XmlReader.Create(stream))
                {
                    var feed = SyndicationFeed.Load(reader);

                    log.Info($"Configured base feed information from {file}");

                    return feed;
                }
            }
            catch
            {
                var feed = new SyndicationFeed();
                feed.Id = Guid.NewGuid().ToString("D").ToUpper();
                feed.Title = new TextSyndicationContent("");
                feed.Generator = "VSGallery.AtomGenerator";

                log.Info($"Existing feed file {file} not found, generating a new feed id");

                return feed;
            }
        }

        private static void _WriteAtomFeed(string file, SyndicationFeed feed, Logger log)
        {
            var sb = new StringBuilder();
            using (var stringStream = new StringWriter(sb))
            using (var writer = XmlWriter.Create(stringStream))
            {
                var formatter = new Atom10FeedFormatter(feed);
                formatter.WriteTo(writer);
            }

            log.Info($"Writing updated feed to {file}");
            File.WriteAllText(file, XElement.Parse(sb.ToString()).ToString());
        }
    }
}
