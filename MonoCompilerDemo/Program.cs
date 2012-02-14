using System;
using System.IO;
using Mono.CSharp;

namespace MonoCompilerDemo
{
    public interface IFoo { string Bar(string s); }

    class Program
    {
        static void Main(string[] args)
        {
            var evaluator = new Evaluator(
                new CompilerSettings(),
                new Report(new ConsoleReportPrinter()));

            evaluator.ReferenceAssembly(typeof(IFoo).Assembly);

            evaluator.Compile(
                @"
    public class Foo : MonoCompilerDemo.IFoo
    {
        public string Bar(string s) { return s.ToUpper(); }
    }");

            for (; ; )
            {
                string line = Console.ReadLine();
                if (line == null) break;

                try
                {
                    Console.WriteLine(evaluator.Evaluate(line));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
