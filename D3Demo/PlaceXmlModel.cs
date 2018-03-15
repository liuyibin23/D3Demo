using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace D3Demo
{
    [XmlRoot("PlaceItems")]
    public class PlaceXmlModel
    {
        [XmlAttribute("Point0")]
        public string Point0 { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlElement("Item")]
        public List<Item> Items { get; set; }

        public class Item
        {
            [XmlAttribute("Flag")]
            public string Flag { get; set; }

            [XmlAttribute("Index")]
            public string Index { get; set; }

            [XmlAttribute("cls")]
            public string Cls { get; set; }

            [XmlAttribute("PlaceFlag")]
            public string PlaceFlag { get; set; }

            [XmlAttribute("Name")]
            public string Name { get; set; }

            [XmlAttribute("HaveSensor")]
            public string HaveSensor { get; set; }

            [XmlAttribute("StartMode")]
            public string StartMode { get; set; }

            [XmlAttribute("Area")]
            public Area Area { get; set; }

            [XmlAttribute("SubAreas")]
            public SunArea SubAreas { get; set; }
        }

        public class SunArea
        {
            [XmlElement("Area")]
            public List<Area> Areas { get; set; }
        }

        public class Area
        {
            [XmlAttribute("Flag")]
            public string Flag { get; set; }

            [XmlAttribute("Note")]
            public string Note { get; set; }

            [XmlElement("Point")]
            public List<Point> Points { get; set; }
        }

        public class Point
        {
            public Point()
            { }

            public Point(double x,double y)
            {
                X = x.ToString();
                Y = y.ToString();
            }

            [XmlAttribute("X")]
            public string X { get; set; }

            [XmlAttribute("Y")]
            public string Y { get; set; }
        }
    }
}
