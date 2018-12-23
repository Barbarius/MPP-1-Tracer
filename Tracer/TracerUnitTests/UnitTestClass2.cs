using TracerLib;
using System.Threading;

namespace TracerUnitTest
{
    class UnitTestClass2
    {
        private ITracer tracer;
        private UnitTestClass1 class1;

        public UnitTestClass2(ITracer tracerConstr)
        {
            tracer = tracerConstr;
            class1 = new UnitTestClass1(tracer);
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
