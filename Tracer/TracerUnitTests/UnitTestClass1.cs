using TracerLib;
using System.Threading;

namespace TracerUnitTest
{
    class UnitTestClass1
    {
        private ITracer tracer;

        public UnitTestClass1(ITracer tracerConstr)
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
