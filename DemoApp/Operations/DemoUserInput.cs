using CSharpScriptOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Operations
{
    class DemoUserInput : IOperation
    {
        public string Description => "Demo UserInput";

        public async Task RunAsync()
        {
            bool input = UserInput.PoseBoolQuestion("This is a sample question?");
            Console.WriteLine($"User input: {input}");
        }
    }
}
