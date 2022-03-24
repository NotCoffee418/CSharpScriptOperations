using Autofac; // Import Autofac if you want DI
using CSharpScriptOperations;
using DemoApp.Logic;
using DemoApp.Operations;

// Register our operations one by one
OperationManager.RegisterOperation(typeof(HelloWorld));

// Alternatively we can register operations in the order we want them displayed
OperationManager.RegisterOperationsBulk(
    new List<Type>() {
        typeof(TwoPlusTwo),
        typeof(LondonWeather),
        typeof(DemoUserInput),
        typeof(AnOperationWithDependency),
    }
);

// Register any custom dependencies through "OperationManager.ContainerBuilder" if needed
OperationManager.ContainerBuilder
    .RegisterType<ExampleDependency>()
    .As<IExampleDependency>();

// Start the listener loop
// This will display our options and interpret user input to run the approperiate operation
await OperationManager.StartListeningAsync();

// Alternatively, you can implement your own approach 
// using the OperationManager.RegisteredOperations object