using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Serialization;

namespace ProgInz_MajasDarbs2
{
    public class ArrayOfMouseAction
    {
        [XmlElement("MouseAction")]
        public List<MouseAction> actionList = new List<MouseAction>();
    }

    public class MouseAction
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Click { get; set; }
        public UInt64 Time { get; set; }
        public bool Mouse { get; set; }
        public string Modifier { get; set; }
    }
}
