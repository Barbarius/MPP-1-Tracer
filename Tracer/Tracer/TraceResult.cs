using System;
using System.Collections.Concurrent;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TracerLib
{
    [Serializable]
    [DataContract]
    public class TraceResult
    {
        [XmlIgnore]
        public ConcurrentDictionary<int, ThreadResult> threads;

        [DataMember]
        public List<ThreadResult> serializableThreads;

        protected internal TraceResult DeepCopy()
        {
            TraceResult traceResultCopy = new TraceResult
            {
                serializableThreads = new List<ThreadResult>()
            };

            foreach (ThreadResult threadResult in threads.Values)
            {
                traceResultCopy.serializableThreads.Add(threadResult.DeepCopy());
            }
            return traceResultCopy;
        }
    }
}
