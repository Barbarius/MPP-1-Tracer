using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace TracerLib
{
    public class XMLSerializer : ISerializer
    {
        private XmlSerializer newXmlFormatter = new XmlSerializer(typeof(TraceResult));

        public MemoryStream serialize(TraceResult traceResult)
        {
            MemoryStream ms = new MemoryStream();
            newXmlFormatter.Serialize(ms, traceResult);
            ms.Position = 0;

            return ms;
        }
    }
}
