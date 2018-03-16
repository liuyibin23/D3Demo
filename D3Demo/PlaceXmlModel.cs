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

            [XmlAttribute("StartArea")]
            public string StartArea { get; set; }

            [XmlAttribute("StartMode")]
            public string StartMode { get; set; }

            [XmlElement("Area")]
            //[XmlIgnore]
            public Area Area { get; set; }

            [XmlElement("SubAreas")]
            //[XmlIgnore]
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
            //[XmlIgnore]
            public string Flag { get; set; }

            [XmlAttribute("Note")]
            //[XmlIgnore]
            public string Note { get; set; }

            [XmlElement("Point")]
            //[XmlIgnore]
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
