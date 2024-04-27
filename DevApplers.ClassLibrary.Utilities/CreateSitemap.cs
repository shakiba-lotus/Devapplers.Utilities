using System;
using System.Collections;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace DevApplers.ClassLibrary.Utilities
{
    public class Location
    {
        public enum ChangeFreq
        {
            Always,
            Hourly,
            Daily,
            Weekly,
            Monthly,
            Yearly,
            Never
        }

        [XmlElement("loc")]
        public string Url { get; set; }

        [XmlElement("lastmod")]
        public DateTime? LastModified { get; set; }
        public bool ShouldSerializeLastModified() { return LastModified.HasValue; }

        [XmlElement("changefreq")]
        public ChangeFreq? ChangeFrequency { get; set; }
        public bool ShouldSerializeChangeFrequency() { return ChangeFrequency.HasValue; }

        [XmlElement("priority")]
        public double? Priority { get; set; }
        public bool ShouldSerializePriority() { return Priority.HasValue; }
    }

    [XmlRoot("urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class Sitemap
    {
        private readonly ArrayList _map;

        public Sitemap()
        {
            _map = new ArrayList();
        }

        [XmlElement("url")]
        public Location[] Locations
        {
            get
            {
                var items = new Location[_map.Count];
                _map.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null)
                    return;
                var items = value;
                _map.Clear();
                foreach (var item in items)
                    _map.Add(item);
            }
        }

        public int Add(Location item)
        {
            return _map.Add(item);
        }
    }

    public class XmlResult : ActionResult
    {
        private readonly object _objectToSerialize;

        public XmlResult(object objectToSerialize)
        {
            _objectToSerialize = objectToSerialize;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (_objectToSerialize == null) return;
            context.HttpContext.Response.Clear();
            var xmlSerializer = new XmlSerializer(_objectToSerialize.GetType());
            context.HttpContext.Response.ContentType = "text/xml";
            xmlSerializer.Serialize(context.HttpContext.Response.Output, _objectToSerialize);
        }
    }
}