using CSharpScriptOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Operations
{
    class HelloWorld : IOperation
    {
        public string Description => "Say 'Hello World!'";

        public async Task RunAsync()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
