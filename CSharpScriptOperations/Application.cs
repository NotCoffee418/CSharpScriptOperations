using Autofac;
using System;
using System.Threading.Tasks;

namespace CSharpScriptOperations;

internal class Application
{
    internal async Task RunAsync()
    {
        // List all operations
        Console.Write(OperationManager.GetOperationsDisplay());

        // Request user
        while (true) // Breakout is calling Exit operation
        {
            Console.WriteLine(); // empty
            Console.WriteLine("Select an operation ('help' for list of operations)");
            string userInput = Console.ReadLine();

            // Handle "help"
            if (userInput.ToLower() == "help")
            {
                Console.Write(OperationManager.GetOperationsDisplay());
                continue;
            }

            // Parse input, report and repeat on invalid
            int reqOperationId = -1;
            if (!int.TryParse(userInput, out reqOperationId))
            {
                Console.WriteLine("Input must be the numeric identifier of a registered operation. Try again.");
                continue;
            }

            // Handle invalid operation id
            if (!OperationManager.OperationIdExists(reqOperationId))
            {
                Console.WriteLine("Invalid operation number. Try again.");
                continue;
            }

            // Create instance
            Type operationType = OperationManager.GetOperationTypeById(reqOperationId);
            IOperation operationInstance = (IOperation)OperationManager.Container.Resolve(operationType);

            // Valid registered operation, report and run it run it.
            Console.WriteLine();
            Console.WriteLine($"Running operation {reqOperationId}...");
            await operationInstance.RunAsync();
            Console.WriteLine();
        }
    }
}
