# CSharpScriptOperations

[![Nuget](https://img.shields.io/nuget/v/CSharpScriptOperations?style=for-the-badge "Nuget")](https://www.nuget.org/packages/CSharpScriptOperations)

This is an approach to running blocks of code or long-running operations using C#.  

## How to use it
### 1. Install the nuget package.
Install the  [nuget package](https://www.nuget.org/packages/CSharpScriptOperations/) into a **Console Application**.  
Call `using CSharpScriptOperations` wherever you need it.

### 2. Create operations
Operations are class objects dedicated to a specific task or set of tasks. They implement this package's `IOperation` class.  
An operation will look something like this:
```csharp
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
```
Whatever is in the `RunAsync()` method is called when the operation is requested.  
The description is used in the console to show what the operration does.

### 3. Register your operations
Bulk register your operations in one swoop:
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

### 4. Start the listener
This will display our options and interpret user input to run the approperiate operation.
```csharp
await OperationManager.StartListeningAsync();
```
Alternatively you can implement your own version of `StartListening()`.  
You can access the registered operations and it's classes through the
`OperationManager.RegisteredOperations` object.

### 5. Try it out
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
The `0. Exit Application` is already installed by default.

### Example

See the implementation in the [DemoApp](https://github.com/NotCoffee418/CSharpScriptOperations/blob/main/DemoApp).