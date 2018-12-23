using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using TracerLib;

namespace TracerUnitTest
{
    [TestClass]
    public class TracerUnitTest
    {
        private static UnitTestClass1 testClass1;
        private static UnitTestClass2 testClass2;
        private static ITracer tracer;

        [TestInitialize]
        public void Initialize()
        {
            tracer = new Tracer();
            testClass1 = new UnitTestClass1(tracer);
            testClass2 = new UnitTestClass2(tracer);
        }
        
        [TestMethod]
        public void InitTimeTest()
        {
            tracer.StartTrace();

            Thread.Sleep(5);

            tracer.StopTrace();

            Assert.IsTrue(5 <= tracer.GetTraceResult().serializableThreads[0].time);
        }

        [TestMethod]
        public void FirstMethodTimeTest()
        {
            tracer.StartTrace();

            testClass1.FirstMethod();

            tracer.StopTrace();

            Assert.IsTrue(20 <= tracer.GetTraceResult().serializableThreads[0].time);
        }

        [TestMethod]
        public void FirstMethodNameTest()
        {
            tracer.StartTrace();

            testClass1.FirstMethod();

            tracer.StopTrace();

            Assert.AreEqual("FirstMethod", tracer.GetTraceResult().serializableThreads[0].methods[0].methodName);
        }

        [TestMethod]
        public void FirstClassNameTest()
        {
            tracer.StartTrace();

            testClass1.FirstMethod();

            tracer.StopTrace();

            Assert.AreEqual("TracerUnitTest.UnitTestClass1", tracer.GetTraceResult().serializableThreads[0].methods[0].className);
        }

        [TestMethod]
        public void SecondMethodTimeTest()
        {
            tracer.StartTrace();

            testClass1.SecondMethod();

            tracer.StopTrace();

            Assert.IsTrue(35 <= tracer.GetTraceResult().serializableThreads[0].time);
        }

        [TestMethod]
        public void FirstAndSecondMethodNameTest()
        {
            tracer.StartTrace();

            testClass1.SecondMethod();

            tracer.StopTrace();

            Assert.AreEqual("SecondMethod", tracer.GetTraceResult().serializableThreads[0].methods[0].methodName);
            Assert.AreEqual("FirstMethod", tracer.GetTraceResult().serializableThreads[0].methods[0].methods[0].methodName);
        }

        [TestMethod]
        public void MultithreadTest()
        {
            tracer.StartTrace();

            Thread FirstThread = new Thread(ThirdMethod);
            FirstThread.Start();
            Thread SecondThread = new Thread(FothMethod);
            SecondThread.Start();

            FirstThread.Join();
            SecondThread.Join();

            tracer.StopTrace();

            Assert.AreEqual("ThirdMethod", tracer.GetTraceResult().serializableThreads[1].methods[0].methodName);
            Assert.AreEqual("FothMethod", tracer.GetTraceResult().serializableThreads[2].methods[0].methodName);
        }

        static void ThirdMethod()
        {
            tracer.StartTrace();

            testClass2.ThirdMethod();

            tracer.StopTrace();
        }

        static void FothMethod()
        {
            tracer.StartTrace();

            testClass2.FothMethod();

            tracer.StopTrace();
        }
    }
}
