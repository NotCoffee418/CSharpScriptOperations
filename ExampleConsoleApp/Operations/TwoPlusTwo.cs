using CSharpScriptOperations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleConsoleApp.Operations
{
    class TwoPlusTwo : IOperation
    {
        public string Description => 
            "Print the result of 2+2";

        public void Run()
        {
            int result = 2 + 2;
            Console.WriteLine($"2 + 2 = {result}");
        }
    }
}
