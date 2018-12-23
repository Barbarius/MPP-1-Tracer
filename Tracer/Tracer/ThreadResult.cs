using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Diagnostics;

namespace TracerLib
{
    [Serializable]
    [DataContract]
    public class ThreadResult
    {
        [DataMember]
        public int id;
        [DataMember]
        public long time;
        [XmlIgnore]
        protected internal Stopwatch timer;
        [XmlIgnore]
        protected internal Stack<MethodResult> stack;
        [DataMember]
        public List<MethodResult> methods;

        protected internal ThreadResult DeepCopy()
        {
            ThreadResult threadCopy = new ThreadResult
            {
                id = id,
                time = time,
                methods = new List<MethodResult>()
            };
            foreach (MethodResult innerMethod in methods)
            {
                threadCopy.methods.Add(innerMethod.DeepCopy());
            }
            return threadCopy;
        }
    }
}
