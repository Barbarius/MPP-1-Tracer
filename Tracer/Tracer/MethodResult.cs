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
    public class MethodResult
    {
        [DataMember]
        public string methodName;
        [DataMember]
        public string className;
        [DataMember]
        public long time;
        [XmlIgnore]
        protected internal Stopwatch timer;
        [DataMember]
        public List<MethodResult> methods;

        protected internal MethodResult DeepCopy()
        {
            MethodResult methodCopy = new MethodResult
            {
                methodName = (string)methodName.Clone(),
                className = (string)className.Clone(),
                time = time,
                methods = new List<MethodResult>()
            };
            foreach (MethodResult innerMethod in methods)
            {
                methodCopy.methods.Add(innerMethod.DeepCopy());
            }
            return methodCopy;
        }
    }
}
