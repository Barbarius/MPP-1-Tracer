using TracerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TracerLib
{
    class ExampleClass2
    {
        private ITracer tracer;
        private ExampleClass1 class1;

        public ExampleClass2(ITracer tracerConstr)
        {
            tracer = tracerConstr;
            class1 = new ExampleClass1(tracer);
        }

        public void ThirdMethod()
        {
            tracer.StartTrace();

            Thread.Sleep(25);
            class1.FirstMethod();

            tracer.StopTrace();
        }

        public void FothMethod()
        {
            tracer.StartTrace();

            Thread.Sleep(30);
            class1.SecondMethod();

            tracer.StopTrace();
        }
    }
}
