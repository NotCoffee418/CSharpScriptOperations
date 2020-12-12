using CSharpScriptOperations.InteralOperations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CSharpScriptOperations
{
    public static class OperationManager
    {
        /// <summary>
        /// Static constructor registers InternalOperations
        /// </summary>
        static OperationManager()
        {
            RegisterOperation(new ExitApplication());
        }

        private static Dictionary<int, IOperation> _registeredOperations 
            = new Dictionary<int, IOperation>();

        /// <summary>
        /// Read-only access to RegisteredOperations.
        /// Dictionary: InputNumber, Operation
        /// </summary>
        public static ReadOnlyDictionary<int, IOperation> RegisteredOperations 
        { 
            get {  return new ReadOnlyDictionary<int, IOperation>(_registeredOperations); }
        }

        /// <summary>
        /// Registers a new operation to the application
        /// </summary>
        /// <param name="operation">Instance of an IOperation</param>
        /// <param name="overrideIndex">Manually overrides index. Leave 0 for auto-assign (recommended).</param>
        public static void RegisterOperation(IOperation operation, int overrideIndex = 0)
        {
            // Determine new index;
            int newIndex = _registeredOperations.Count;
            if (overrideIndex != 0)
                newIndex = overrideIndex;

            // Validate index
            if (_registeredOperations.ContainsKey(newIndex))
                throw new ArgumentException($"RegisterOperation: The provided overrideIndex {overrideIndex} is already assigned.");

            // Register the operation
            _registeredOperations.Add(newIndex, operation);
        }

        /// <summary>
        /// Function that registers a list of operations for less verbose.
        /// </summary>
        /// <param name="operations">List of IOperation instances</param>
        public static void RegisterOperationsBulk(List<IOperation> operations)
            => operations.ForEach(op => RegisterOperation(op));

        /// <summary>
        /// Starts the listener loop which displays the registered operations,
        /// requests user input and runs the requested operation.
        /// This will keep looping until the Exit Application operation is called.
        /// </summary>
        public static void StartListening()
        {
            // List all operations
            Console.Write(GetOperationsDisplay());

            // Request user
            while (true) // Breakout is calling Exit operation
            {
                Console.WriteLine(); // empty
                Console.WriteLine("Select an operation ('help' for list of operations)");
                string userInput = Console.ReadLine();

                // Handle "help"
                if (userInput.ToLower() == "help")
                {
                    Console.Write(GetOperationsDisplay());
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
                if (!_registeredOperations.ContainsKey(reqOperationId))
                {
                    Console.WriteLine("Invalid operation number. Try again.");
                    continue;
                }

                // Valid registered operation, report and run it run it.
                Console.WriteLine();
                Console.WriteLine($"Running operation {reqOperationId}: {_registeredOperations[reqOperationId].Description}...");
                _registeredOperations[reqOperationId].Run();
                Console.WriteLine("Operation complete (or running in the background)");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Gets a string listing registered operations by their description and activation key
        /// </summary>
        /// <returns></returns>
        public static string GetOperationsDisplay()
        {
            string result = "Available Operations: " + Environment.NewLine;
            foreach (var opKvp in _registeredOperations)
                result += $"{opKvp.Key}. {opKvp.Value.Description}{Environment.NewLine}";
            return result;
        }
    }
}
