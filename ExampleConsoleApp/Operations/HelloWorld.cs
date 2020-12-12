using CSharpScriptOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleConsoleApp.Operations
{
    class HelloWorld : IOperation
    {
        public string Description => "Say 'Hello World!'";

        public void Run()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
