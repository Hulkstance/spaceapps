using System;
using System.Collections.Generic;

namespace SpaceApps.Models
{
    public class Link
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Prompt { get; set; }
    }

    public class Link2
    {
        public string Render { get; set; }
        public string Href { get; set; }
        public string Rel { get; set; }
    }

    public class Datum
    {
        public DateTime Date_created { get; set; }
        public string Nasa_id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Media_type { get; set; }
        public List<string> Keywords { get; set; }
        public string Center { get; set; }
        public string Description_508 { get; set; }
        public string Secondary_creator { get; set; }
        public string Photographer { get; set; }
        public string Location { get; set; }
        public List<string> Album { get; set; }
    }

    public class Item
    {
        public string Href { get; set; }
        public List<Link2> Links { get; set; }
        public List<Datum> Data { get; set; }
    }

    public class Metadata
    {
        public int Total_hits { get; set; }
    }

    public class Collection
    {
        public List<Link> Links { get; set; }
        public string Href { get; set; }
        public List<Item> Items { get; set; }
        public string Version { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class RootObject
    {
        public Collection Collection { get; set; }
    }
}
