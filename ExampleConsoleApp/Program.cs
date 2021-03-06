using System;
using System.Collections.Generic;
using CSharpScriptOperations;
using ExampleConsoleApp.Operations;

namespace ExampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Register our operations in the order we want them displayed
            OperationManager.RegisterOperationsBulk(
                new List<IOperation>() {
                    new TwoPlusTwo(),
                    new LondonWeather(),
                    new DemoUserInput(),
                }
            );

            // Alternatively we can register operations one by one
            OperationManager.RegisterOperation(new HelloWorld());

            // Start the listener loop
            // This will display our options and interpret user input to run the approperiate operation
            OperationManager.StartListening();

            // Alternatively, you can implement your own approach 
            // using the OperationManager.RegisteredOperations object
        }
    }
}
