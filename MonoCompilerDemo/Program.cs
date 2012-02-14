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

            // Make it reference our own assembly so it can use IFoo
            evaluator.ReferenceAssembly(typeof(IFoo).Assembly);

            // Feed it some code
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

                object result;
                bool result_set;
                evaluator.Evaluate(line, out result, out result_set);
                if (result_set) Console.WriteLine(result);
            }
        }
    }
}
