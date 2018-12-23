using System;
using System.IO;

namespace TracerLib
{
    class ConsoleTraceResultWriter : ITraceResultWriter
    {
        public void Write(MemoryStream ms)
        {
            Console.WriteLine(new StreamReader(ms).ReadToEnd());
        }
    }
}
