using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphicsEditor.Models
{
    [XmlInclude(typeof(LineElement))]
    [XmlInclude(typeof(BrokenLine))]
    [XmlInclude(typeof(PolygonElement))]
    [XmlInclude(typeof(RectangleElement))]
    [XmlInclude(typeof(EllipseElement))]
    [XmlInclude(typeof(CompFig))]
    public class Figures
    {
        public string Name { get; set; }
    }
}
