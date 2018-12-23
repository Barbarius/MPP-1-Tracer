using System.IO;

namespace TracerLib
{
    interface ITraceResultWriter
    {
        void Write(MemoryStream ms);
    }
}
