using System;
using System.IO;

namespace TracerLib
{
    class FileTraceResultWriter : ITraceResultWriter
    {
        private String filename;

        public FileTraceResultWriter(String nameOfFile)
        {
            filename = (String)nameOfFile.Clone();
        }

        public void Write(MemoryStream ms)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                ms.WriteTo(fs);
            }
        }
    }
}
