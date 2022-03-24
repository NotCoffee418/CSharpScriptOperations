using CSharpScriptOperations;
using DemoApp.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Operations
{
    class AnOperationWithDependency : IOperation
    {
        private IExampleDependency _exampleDependency;

        public AnOperationWithDependency(IExampleDependency exampleDependency)
        {
            _exampleDependency = exampleDependency;
        }

        public string Description => "Multiply with Autofac dependency dnjection";

        public async Task RunAsync()
        {
            Console.WriteLine("Multiplying 2 and 3 through IExampleDependency.");

            double result = _exampleDependency.Multiply(2, 3);
            Console.WriteLine($"Result: {result}");
        }
    }
}
