# CSharpScriptOperations

[![Nuget](https://img.shields.io/nuget/v/CSharpScriptOperations?style=for-the-badge "Nuget")](https://www.nuget.org/packages/CSharpScriptOperations)

## What is it?
CSharpScriptOperations is a library for .NET console applications to quickly set up a console application interface.  
Developers can use it to get quick access to specific portions of their codebase by creating an `IOperation` for it.
This library optionally supports dependency injection through Autofac.

## Quick Start
Create an operation in a new class implementing `IOperation` with an `OperationDescription` attribute.
```csharp
[OperationDescription("Print the result of 2+2")]
class TwoPlusTwo : IOperation
{
    public async Task RunAsync()
    {
        int result = 2 + 2;
        Console.WriteLine($"2 + 2 = {result}");
    }
}
```
Register your operations in `Program.cs`.
```csharp
OperationManager.AutoRegisterOperations();
await OperationManager.StartListeningAsync();
```

## What does it look like?
This is an example taken from the [DemoApp](https://github.com/NotCoffee418/CSharpScriptOperations/blob/main/DemoApp).
```
Available Operations:
0. Exit Application
1. Say 'Hello World!'
2. Print the result of 2+2
3. Print the current weather in London
4. Demo UserInput
5. Multiply with Autofac dependency injection
6. Legacy Description'

Select an operation ('help' for list of operations)
1

Running operation 1...
Hello World!
```

## Detailed Instructions
### 1. Install the nuget package.
Install the  [nuget package](https://www.nuget.org/packages/CSharpScriptOperations/) into a **Console Application**.  
Call `using CSharpScriptOperations` wherever you need it.

### 2. Create operations
Operations are class objects dedicated to a specific task or set of tasks. They implement this package's `IOperation` class.  
An operation will look something like this:
```csharp

[OperationDescription("Print the result of 2+2")]
class TwoPlusTwo : IOperation
{
    public async Task RunAsync()
    {
        int result = 2 + 2;
        Console.WriteLine($"2 + 2 = {result}");
    }
}
```
Whatever is in the `RunAsync()` method is called when the operation is requested.  
The description attribute is used in the console to show what the operation does.

### 3. Register your operations
Bulk register your operations in one swoop:

### Automatic Registration
```csharp
OperationManager.AutoRegisterOperations()
```

#### Manual registration
```csharp
OperationManager.RegisterOperationsBulk(
    new List<Type>() {
        typeof(TwoPlusTwo),
        typeof(LondonWeather),
    }
);
```

Or register operations one by one:
```csharp
OperationManager.RegisterOperation(typeof(HelloWorld));
```

### 4. Register dependencies (optional)
You can optionally use dependency injection with Autofac or Microsoft Dependency Injection.


Register any dependencies to `OperationManager.ContainerBuilder` before starting the listener.
```csharp
ContainerBuilder AutofacContainerBuilder = new ContainerBuilder();

// Include application dependencies
OperationManager.ContainerBuilder
    .RegisterType<ExampleDependency>()
    .As<IExampleDependency>();

// Include the services registered by the OperationManager
AutofacContainerBuilder.Populate(OperationManager.Services);

// Build the container
var serviceProvider = new AutofacServiceProvider(AutofacContainerBuilder.Build());
```

### 5. Start the listener
This will display our options and interpret user input to run the approperiate operation.
```csharp
await OperationManager.StartListeningAsync();

// or if you use dependency injection
await OperationManager.StartListeningAsync(serviceProvider);
```
Alternatively you can implement your own version of `StartListening()`.  
You can access the registered operations and it's classes through the
`OperationManager.RegisteredOperations` object.

### 6. Try it out
The output of the ExampleConsoleApp looks like this:
```
Available Operations:
0. Exit Application
1. Print the result of 2+2
2. Print the current weather in London
3. Say 'Hello World!'

Select an operation ('help' for list of operations)
```
And we can run the operations by entering the requested operation's number.
```
Select an operation ('help' for list of operations)
2

Running operation 2: Print the current weather in London...
The weather in London is currently: Light Rain
Operation complete (or running in the background)
```
The application will run until operation 0 is called.  
The `0. Exit Application` operation is already installed by default.

### Example

See the full implementation examples in the [DemoApp](https://github.com/NotCoffee418/CSharpScriptOperations/blob/main/DemoApp).