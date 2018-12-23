using System;

namespace TracerLib
{
    class Programm
    {
        static void Main(string[] args)
        {
            // инициализация
            ITracer tracer = new Tracer();
            ExampleClass2 class2 = new ExampleClass2(tracer);

            // подсчёт времени методов
            tracer.StartTrace();             

            class2.ThirdMethod();
            class2.FothMethod();

            tracer.StopTrace();

            // сериализация и вывод
            XMLSerializer newXMLSerializer = new XMLSerializer();
            JSONSerializer newJSONSerializer = new JSONSerializer();
            ITraceResultWriter consoleWriter = new ConsoleTraceResultWriter();
            ITraceResultWriter xmlFileWriter = new FileTraceResultWriter("XMLresult.txt");
            ITraceResultWriter jsonFileWriter = new FileTraceResultWriter("JSONresult.txt");

            // вывод на консоль
            consoleWriter.Write(newXMLSerializer.serialize(tracer.GetTraceResult()));
            consoleWriter.Write(newJSONSerializer.serialize(tracer.GetTraceResult()));

            // вывод в файл
            xmlFileWriter.Write(newXMLSerializer.serialize(tracer.GetTraceResult()));
            jsonFileWriter.Write(newJSONSerializer.serialize(tracer.GetTraceResult()));

            Console.ReadKey();
        }
    }
}
