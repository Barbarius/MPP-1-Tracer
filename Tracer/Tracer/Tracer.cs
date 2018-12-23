using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Collections.Concurrent;

namespace TracerLib
{
    public class Tracer : ITracer
    {
        private TraceResult traceResult;
        private object locker;

        private void InitializeThread(int currentThreadId)
        {
            traceResult.threads[currentThreadId] = new ThreadResult
            {
                id = currentThreadId,
                timer = new Stopwatch(),
                stack = new Stack<MethodResult>(),
                methods = new List<MethodResult>()
            };
        }

        private MethodResult InitializeMethod(int currentThreadId, string methodName, string className)
        {
            MethodResult tempMethod;

            if (traceResult.threads[currentThreadId].stack.Count > 0)
            {
                tempMethod = traceResult.threads[currentThreadId].stack.Peek();
                tempMethod.methods = new List<MethodResult>
                {
                    new MethodResult()
                };
                tempMethod = tempMethod.methods[0];
            }
            else
            {
                traceResult.threads[currentThreadId].methods.Add(new MethodResult());
                tempMethod = traceResult.threads[currentThreadId].methods[traceResult.threads[currentThreadId].methods.Count - 1];
            }
            tempMethod.methodName = methodName;
            tempMethod.className = className;
            tempMethod.timer = new Stopwatch();
            tempMethod.methods = new List<MethodResult>();

            return tempMethod;
        }

        public void StartTrace()
        {
            lock (locker)
            {
                int currentThreadId = Thread.CurrentThread.ManagedThreadId;

                // выбираем поток из существующих
                if (traceResult.threads.ContainsKey(currentThreadId))
                {
                    // определяем метод
                    StackFrame newFrame = new StackFrame(1);
                    string className = newFrame.GetMethod().DeclaringType.ToString();
                    string methodName = newFrame.GetMethod().Name;

                    // инициализация при первом запуске
                    if (traceResult.threads.Count > 0)
                    {
                        MethodResult tempMethod = InitializeMethod(currentThreadId, methodName, className);

                        // измерение времени выполения
                        tempMethod.timer.Start();

                        // добавляем в стек для сохранения иерархии
                        traceResult.threads[currentThreadId].stack.Push(tempMethod);
                    }
                }
                else
                {
                    // инициализация нового потока
                    InitializeThread(currentThreadId);

                    // измерение времени выполения
                    traceResult.threads[currentThreadId].timer.Start();
                }
            }
        }

        public void StopTrace()
        {
            lock (locker)
            {
                int currentThreadId = Thread.CurrentThread.ManagedThreadId;

                // выбираем метод
                if (traceResult.threads.ContainsKey(currentThreadId))
                {
                    if (traceResult.threads[currentThreadId].stack.Count > 0)
                    {
                        // удаляем из стека и останавливаем измерения
                        MethodResult tempMethod = traceResult.threads[currentThreadId].stack.Pop();
                        tempMethod.timer.Stop();
                        tempMethod.time = tempMethod.timer.ElapsedMilliseconds;
                    }
                    else if (traceResult.threads[currentThreadId].stack.Count == 0)
                    {
                        // удаляем из стека и останавливаем измерения
                        traceResult.threads[currentThreadId].timer.Stop();
                        traceResult.threads[currentThreadId].time = traceResult.threads[currentThreadId].timer.ElapsedMilliseconds;
                    }
                }
            }
        }

        public TraceResult GetTraceResult()
        {
            TraceResult newTraceResult;
            lock (locker)
            {
                newTraceResult = traceResult.DeepCopy();
            }
            return newTraceResult;
        }

        // конструктор
        public Tracer()
        {
            locker = new object();
            traceResult = new TraceResult
            {
                threads = new ConcurrentDictionary<int, ThreadResult>()
            };
            traceResult.serializableThreads = new List<ThreadResult>();
        }
    }
}
