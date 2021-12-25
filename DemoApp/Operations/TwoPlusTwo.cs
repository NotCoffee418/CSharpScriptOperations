using CSharpScriptOperations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Operations
{
    class TwoPlusTwo : IOperation
    {
        public string Description => 
            "Print the result of 2+2";

        public async Task RunAsync()
        {
            int result = 2 + 2;
            Console.WriteLine($"2 + 2 = {result}");
        }
    }
}
