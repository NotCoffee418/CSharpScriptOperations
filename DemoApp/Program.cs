using CSharpScriptOperations;
using DemoApp.Operations;

// Register our operations in the order we want them displayed
OperationManager.RegisterOperationsBulk(
    new List<Type>() {
        typeof(TwoPlusTwo),
        typeof(LondonWeather),
        typeof(DemoUserInput),
    }
);

// Alternatively we can register operations one by one
OperationManager.RegisterOperation(typeof(HelloWorld));

// Start the listener loop
// This will display our options and interpret user input to run the approperiate operation
await OperationManager.StartListeningAsync();

// Alternatively, you can implement your own approach 
// using the OperationManager.RegisteredOperations object