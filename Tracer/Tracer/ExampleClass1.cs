using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracerLib;
using System.Threading;

namespace TracerLib
{
    class ExampleClass1
    {
        private ITracer tracer;

        public ExampleClass1(ITracer tracerConstr)
        {
            tracer = tracerConstr;
        }

        public void FirstMethod()
        {
            tracer.StartTrace();

            Thread.Sleep(20);

            tracer.StopTrace();
        }

        public void SecondMethod()
        {
            tracer.StartTrace();

            Thread.Sleep(15);
            this.FirstMethod();

            tracer.StopTrace();
        }
    }
}
