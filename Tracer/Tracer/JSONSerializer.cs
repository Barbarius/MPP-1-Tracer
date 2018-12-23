using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Json;

namespace TracerLib
{
    public class JSONSerializer : ISerializer
    {
        private DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(TraceResult));

        public MemoryStream serialize(TraceResult traceResult)
        {
            MemoryStream ms = new MemoryStream();
            using (XmlDictionaryWriter jsonWriter = JsonReaderWriterFactory.CreateJsonWriter(ms, Encoding.UTF8, ownsStream: false, indent: true))
            {
                jsonSerializer.WriteObject(jsonWriter, traceResult);
            }
            ms.Position = 0;

            return ms;
        }
    }
}
